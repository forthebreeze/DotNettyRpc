namespace DotNettyRpc
{
    using DotNettyRpc.Library;

    class Program
    {
        static void Main(string[] args)
        {
            RpcServer rpcServer = new RpcServer(8080);
            rpcServer.start().Wait();
        }
    }
}
