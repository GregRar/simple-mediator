namespace SampleDomain.Commands
{
    public class SendEmailGenericCommand
    {
        public SendEmailGenericCommand(string body)
        {
            Body = body;
        }

        public string Body { get; private set; }
    }
}