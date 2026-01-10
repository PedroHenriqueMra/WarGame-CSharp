public interface ITransportSender
{
    public Task SendAsync(byte[] bytes);
}
