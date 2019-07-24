namespace SampleDomain.Commands
{
    public class SendEmailGenericCommandWithResponse : ICommandResponse<SendEmailGenericCommandResponse>
    {
        public SendEmailGenericCommandWithResponse(string body)
        {
            Body = body;
        }

        public string Body { get; private set; }
    }
}