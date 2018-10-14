namespace DotNettyRpc.Library
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Sockets;

    public class RpcClient
    {
        private Bootstrap _bootstrap;
        private IEventLoopGroup _group;
        private IChannel _ch;
        private int _port;
        private string _host;
        public RpcClient(int port, string host)
        {
            _port = port;
            _host = host;
        }

        public void SendMessage(Request request)
        {
            _ch.WriteAndFlushAsync(request);
        }

        public async Task Close()
        {
            await _ch.CloseAsync();
        }

        private int Callback(Response response)
        {
            Console.WriteLine(response.OutputNumber);
            return 0;
        }

        public async Task start()
        {
            _bootstrap = new Bootstrap();
            _group = new MultithreadEventLoopGroup();
            _bootstrap.Group(_group).Option(ChannelOption.TcpNodelay, true);
            _bootstrap.Channel<TcpSocketChannel>().Option(ChannelOption.ConnectTimeout, new TimeSpan(0, 0, 10));
            _bootstrap.Channel<TcpSocketChannel>();
            _bootstrap.Handler(new ActionChannelInitializer<IChannel>(channel =>
            {
                IChannelPipeline pipeline = channel.Pipeline;
                pipeline.AddLast(new RequestEncoder())
                .AddLast(new ResponseDecoder())
                .AddLast(new ResponseHandler(Callback));
            }));

            _ch = await _bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(_host), _port));
        }
    }
}
