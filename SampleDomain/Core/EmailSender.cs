namespace SampleDomain.Core
{
    public class EmailSender
    {
        private string lastSentMessage;

        public void SendEmail(string message)
        {
            this.lastSentMessage = message;
        }

        public string GetLastSentMessage()
        {
            return this.lastSentMessage;
        }
    }
}