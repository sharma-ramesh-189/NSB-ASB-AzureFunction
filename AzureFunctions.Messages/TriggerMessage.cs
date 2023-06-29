using NServiceBus;

public class TriggerMessage : IMessage
{
    public string Msg1 = "First message from trigger handler.";
    public string Msg2 = "Second message from trigger handler.";
}