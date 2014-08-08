using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;

namespace BookMyBook
{
    public class Error
    {
        public static async void Send(Exception e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Address = "srujanjha.jha@gmail.com"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "Feedback for BookMyBook :Error Occurred";
            mail.Body = e.ToString()+Environment.NewLine+"Write your feedback here...";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }
    }
}
