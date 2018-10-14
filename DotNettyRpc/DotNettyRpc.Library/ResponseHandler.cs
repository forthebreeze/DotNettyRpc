namespace DotNettyRpc.Library
{
    using System;
    using DotNetty.Transport.Channels;

    public class ResponseHandler : SimpleChannelInboundHandler<Response>
    {
        Func<Response, int> _callback;
        public ResponseHandler(Func<Response, int> callback)
        {
            _callback = callback;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, Response msg)
        {
            _callback(msg);
        }
    }
}
