using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Xml;

namespace BLL
{
    public class Email
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Assunto { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentFileName { get; set; }

        public Email()
        {

        }
        public Email(string _from, string _to, string _assunto, string _subject, string _body, string _attachmentFileName)
        {
            From = _from;
            To = _to;
            Assunto = _assunto;
            Subject = _subject;
            Body = _body;
            AttachmentFileName = _attachmentFileName;
        }
        public void Enviar()
        {
            MailMessage email = new MailMessage();
            email.Priority = MailPriority.High;
            email.IsBodyHtml = true;

            MailAddress add = new MailAddress(From);
            email.From = add;
            email.To.Add(To);
            email.Subject = Subject;
            email.Body = Body;
            if (!string.IsNullOrEmpty(AttachmentFileName))
                email.Attachments.Add(new Attachment(AttachmentFileName));

            try
            {

                SmtpClient smtp = new SmtpClient("smtp.mic.ind.br");
                smtp.Credentials = new NetworkCredential("postmaster@mic.ind.br", "Ve@0213");
                smtp.Port = 587;
                smtp.Send(email);
                //Inicia a leitura do XML com as configuracoes SMTP.
                //XmlDocument xmlDoc = getConfig();

                //XmlNodeList element = xmlDoc.GetElementsByTagName("SMTP");
                //XmlNode user = element[0].SelectSingleNode("User");
                //XmlNode pass = element[0].SelectSingleNode("Pass");
                //XmlNode server = element[0].SelectSingleNode("Server");

                //SmtpClient smtp = new SmtpClient(server.InnerText);
                //smtp.Credentials = new NetworkCredential(user.InnerText, pass.InnerText);


                //smtp.Send(email);

            }
            catch (Exception)
            {
                //Caso as configuracoes nao estejam completas no XML, 
                //Carrega uma conta padrao.
                SmtpClient smtp = new SmtpClient("smtp.mic.ind.br");
                smtp.Credentials = new NetworkCredential("postmaster@mic.ind.br", "Ve@0213");


                smtp.Send(email);
            }


        }

    }
}
