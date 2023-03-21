using Business.Entities;

namespace Business.Interface
{
    public interface IEmailService
    {
        /*bool SendEmail(MailRequest mailRequest);
        void SendEmail(MailRequestMetadata request);*/
        bool SendEmail(MailRequest mailRequest);
        /*bool SendEmail(MailRequestMetadata mailRequest);
        bool SendEmail(MailRequestMetadata mailRequest, bool isTemplateBody = false);*/
    }
}
