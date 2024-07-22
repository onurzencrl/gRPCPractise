using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using grpcMessageClient;

namespace grpcClient
{
    class Program
    {
   static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5214");
            //var greetClient = new Greeter.GreeterClient(channel);
            var messageClient = new Message.MessageClient(channel);
            MessageResponse response = await messageClient.SendMessageAsync(new MessageRequest
            {
                Message = "Merhaba",
                Name = "Onur"
            });
            //HelloReply result = await greetClient.SayHelloAsync(new HelloRequest
            //{
            //    Name = "Onur'dan selamlar"
            //});
            System.Console.WriteLine(response.Message);
        }
    }
}