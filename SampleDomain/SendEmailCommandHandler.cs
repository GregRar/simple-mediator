using SampleDomain.Commands;
using SampleDomain.Core;
using SampleDomain.Mediator.HandlerTypes;

namespace SampleDomain
{
    public class SendEmailCommandHandler :
      IGenericCommandHandler<SendEmailGenericCommand>,
      IGenericCommandWithResponseHandler<SendEmailGenericCommandWithResponse, SendEmailGenericCommandResponse>
    {
        private readonly EmailSender emailSender;

        public SendEmailCommandHandler(EmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public void Handle(SendEmailGenericCommand command)
        {
            this.emailSender.SendEmail(command.Body);
        }

        public SendEmailGenericCommandResponse Handle(SendEmailGenericCommandWithResponse command)
        {
            this.emailSender.SendEmail(command.Body);
            return new SendEmailGenericCommandResponse(true);
        }
    }
}