namespace DotNettyRpc.Library
{
    using DotNetty.Transport.Channels;

    public class RequestHandler : SimpleChannelInboundHandler<Request>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, Request msg)
        {
            Response response = new Response()
            {
                OutputNumber = msg.InputNumber * 3
            };

            ctx.WriteAndFlushAsync(response);
        }
    }
}
