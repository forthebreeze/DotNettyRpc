namespace DotNettyRpc.Library
{
    using DotNetty.Buffers;
    using DotNetty.Codecs;
    using DotNetty.Transport.Channels;

    public class RequestEncoder : MessageToByteEncoder<Request>
    {
        protected override void Encode(IChannelHandlerContext context, Request message, IByteBuffer output)
        {
            output.WriteInt(message.InputNumber);
        }
    }
}
