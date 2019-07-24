namespace SampleDomain.Commands
{
    public class SendEmailGenericCommandResponse
    {
        public SendEmailGenericCommandResponse(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public bool Succeeded { get; private set; }
    }
}