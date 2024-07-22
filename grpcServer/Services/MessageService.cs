using Grpc.Core;
using grpcMessageServer;
using grpcMessageServer;

namespace grpcServer.Services;

public class MessageService  : Message.MessageBase
{
    public override async Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
    {
        return await Task.FromResult(new MessageResponse
        {
            Message = $"Merhaba {request.Name}"
        });
    }

}