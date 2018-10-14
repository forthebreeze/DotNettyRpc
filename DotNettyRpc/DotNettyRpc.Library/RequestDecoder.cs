namespace DotNettyRpc.Library
{
    using System.Collections.Generic;
    using DotNetty.Buffers;
    using DotNetty.Codecs;
    using DotNetty.Transport.Channels;

    public class RequestDecoder : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (input.ReadableBytes < 4)
            {
                return;
            }

            int number = input.ReadInt();
            Request request = new Request() { InputNumber = number};
            output.Add(request);
        }
    }
}
