using System;
using System.Linq.Expressions;
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
            //Unary
            //MessageResponse response = await messageClient.SendMessageAsync(new MessageRequest
            //{
            //    Message = "Merhaba",
            //    Name = "Onur"
            //});

            //Server Streaming
            //var response = messageClient.SendMessage(new MessageRequest
            //    {
            //    Message = "Merhaba",
            //    Name = "Gençay"
            //    });
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            //while (await response.ResponseStream.MoveNext(cancellationTokenSource.Token))
            //{
            //    System.Console.WriteLine(response.ResponseStream.Current.Message);
            //}

            //Client Streaming
           // var request = messageClient.SendMessage();
           // for (int i = 0; i < 10; i++)
           // {
           //     await Task.Delay(1000);
           //     await request.RequestStream.WriteAsync(new MessageRequest
           //     {
           //         Message = "Merhaba",
           //         Name = "Onur"
           //     });
           // }

           // // Stream datanın bittiğini ifade eder.
           //await request.RequestStream.CompleteAsync();

           // Console.WriteLine((await request.ResponseAsync).Message);


            //Bi-Directional Streaming

            var request = messageClient.SendMessage();
            var task1 = Task.Run(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(1000);
                    await request.RequestStream.WriteAsync(new MessageRequest
                    {
                        Message = "Merhaba",
                        Name = "Onur"
                    });
                }

            
            });

            while (await request.ResponseStream.MoveNext(cancellationTokenSource.Token))
            {
                System.Console.WriteLine(request.ResponseStream.Current.Message);
            }

            await task1;
            await request.RequestStream.CompleteAsync();

            //HelloReply result = await greetClient.SayHelloAsync(new HelloRequest
            //{
            //    Name = "Onur'dan selamlar"
            //});
            //System.Console.WriteLine(response.Message);
        }
    }
}