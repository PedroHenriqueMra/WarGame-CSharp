using System.Text;
using System.Text.Json;

public class SendOutput
{
    public Task SendAsync<TPayload>(ITransportSender transport, OutputEnvelope<TPayload> envelope)
    where TPayload : IPayload
    {
        string json = JsonSerializer.Serialize(envelope);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

        return transport.SendAsync(jsonBytes);
    }
}
