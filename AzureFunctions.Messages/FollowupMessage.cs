using NServiceBus;

public class FollowupMessage : IMessage
{
    public string Msg1 = "First message from follow-up handler.";
    public string Msg2 = "Second message from follow-up handler.";
}