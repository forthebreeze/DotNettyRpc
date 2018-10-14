namespace DotNettyRpc.Client
{
    using DotNettyRpc.Library;

    class Program
    {
        static void Main(string[] args)
        {
            RpcClient rpcClient = new RpcClient(8080, "127.0.0.1");
            rpcClient.start().Wait();
            while (true)
            {
                var ret = System.Console.ReadLine();
                int number = 0;
                if (int.TryParse(ret, out number))
                {
                    Request request = new Request()
                    {
                        InputNumber = number
                    };

                    rpcClient.SendMessage(request);
                }
                else
                {
                    rpcClient.Close().Wait();
                    break;
                }
            }
        }
    }
}
