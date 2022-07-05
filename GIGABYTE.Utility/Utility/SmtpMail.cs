using GIGABYTE.Utility.Dto;
using System.Net.Mail;
using System.Text;

namespace GIGABYTE.Utility
{
    public interface ISmtpMail
    {
        SmtpMailOutputDto SendMail (SmtpMailInputDto input);
    }

    public class SmtpMail: ISmtpMail
    {

        public SmtpMailOutputDto SendMail(SmtpMailInputDto input)
        {
            SmtpMailOutputDto result = new SmtpMailOutputDto();
            try
            {
                var sbMail = new StringBuilder();
                sbMail.Append(input.BodyContent);
                var objMail = new MailMessage
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    Body = sbMail.ToString()
                };
                objMail.Subject = input.Subject;
                objMail.From = new MailAddress("web@gigabyte.com");
                foreach(var toMail in input.ToMails)
                {
                    objMail.To.Add(toMail);
                }

                foreach (var ccToMail in input.CCToMails ?? new List<string>())
                {
                    objMail.CC.Add(ccToMail);
                }

                foreach (var BccToMail in input.BCCToMails ?? new List<string>())
                {
                    objMail.Bcc.Add(BccToMail);
                }
                var smtpMail = new SmtpClient("10.1.1.200", 25);
                smtpMail.Send(objMail);
                objMail.Dispose();
                result.Success = true;
            }
            catch(Exception ex)
            {
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}