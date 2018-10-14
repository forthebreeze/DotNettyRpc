namespace DotNettyRpc.Library
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using DotNetty.Codecs.Protobuf;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Sockets;
    using Google.Protobuf;

    public class RpcServer
    {
        private int _port;
        public RpcServer(int port)
        {
            _port = port;
        }

        public async Task start()
        {
            IEventLoopGroup bossGroup;
            IEventLoopGroup workGroup;
            bossGroup = new MultithreadEventLoopGroup(1);
            workGroup = new MultithreadEventLoopGroup();

            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(bossGroup, workGroup);
                bootstrap.Channel<TcpServerSocketChannel>();

                bootstrap
                    .Option(ChannelOption.SoBacklog, 80)
                    .Option(ChannelOption.SoReuseaddr, true)
                    .Option(ChannelOption.TcpNodelay, true)
                    .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(new ProtobufVarint32FrameDecoder())
                                .AddLast(new ProtobufDecoder(Request.Parser))
                                .AddLast(new ProtobufVarint32LengthFieldPrepender())
                                .AddLast(new ProtobufEncoder())
                                .AddLast(new RequestHandler());
                    }));

                IChannel bootstrapChannel = await bootstrap.BindAsync(IPAddress.Any, _port);
                Console.ReadLine();
                await bootstrapChannel.CloseAsync();
            }
            finally
            {
                await workGroup.ShutdownGracefullyAsync();
                await bossGroup.ShutdownGracefullyAsync();
            }
        }
    }
}
