namespace DotNettyRpc.Library
{
    using DotNetty.Buffers;
    using DotNetty.Codecs;
    using DotNetty.Transport.Channels;

    public class ResponseEncoder : MessageToByteEncoder<Response>
    {
        protected override void Encode(IChannelHandlerContext context, Response message, IByteBuffer output)
        {
            output.WriteInt(message.OutputNumber);
        }
    }
}
