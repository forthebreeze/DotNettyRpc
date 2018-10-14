namespace DotNettyRpc.Library
{
    using System.Collections.Generic;
    using DotNetty.Buffers;
    using DotNetty.Codecs;
    using DotNetty.Transport.Channels;

    public class ResponseDecoder : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (input.ReadableBytes < 4)
            {
                return;
            }

            int number = input.ReadInt();
            Response request = new Response() { OutputNumber = number };
            output.Add(request);
        }
    }
}
