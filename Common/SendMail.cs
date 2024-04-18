using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MimeKit;
using Microsoft.AspNetCore.Http;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Net;
using Amazon.S3;
using Amazon;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using System.Text;
using System.Web.Mvc;
using System;

namespace Common
{
    public class SendMail
    {
        public async void SendInvMail(byte[] fileBytes, string EmailId, string InvoiceName,string contactno)
        {
            string cleanedInvoiceName = InvoiceName.Replace(" ", "");

            byte[] fileB = fileBytes;
            await sGetTemporaryUrl(fileBytes, cleanedInvoiceName, contactno);
            using (var clonedMemoryStream = new MemoryStream(fileB.ToArray()))
            {
                string htmlContent = $@" <div style = 'font - family: Arial, sans - serif; background - color: #f9f9f9; margin: 0; padding: 0;'>
                <div style = 'max - width: 600px; margin: 0 auto; padding: 20px; background - color: #ffffff; border-radius: 5px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);'>
                    <div style = 'text - align: center; margin - bottom: 20px; ' >
                        <a style = 'color: #00466a; text-decoration: none; font-size: 1.4em; font-weight: 600;'>Invoice Created On {InvoiceName}</a>
                    </div>
                    <div style = 'font - size: 1.1em; margin - bottom: 20px; ' >
                        <p> Hi,</ p >
                        <p> Please find the attached file for your invoice details.</p>
                        <p> Regards,<br> Prinsoft </p>
                    </div>
                    </div>
                </div> ";
                // Create a new message and set the HTML content
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Initial Infinity PrintSoft", "info@initialInfinity.com")); // set your email
                message.To.Add(new MailboxAddress("Hello User", EmailId)); // recipient email
                message.Subject = "Invoice created";
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = htmlContent;
                bodyBuilder.Attachments.Add(cleanedInvoiceName, clonedMemoryStream);
                message.Body = bodyBuilder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    client.Connect("smtpout.secureserver.net", 465, true); // SMTP server and port
                    client.Authenticate("info@initialInfinity.com", "Feb@2023$"); // Your email address and password
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            //return "Email send!";
        }
        public string SendInvsms(string contactno, string uploadedFileUrl)
        {
            //Uri uri = new Uri($"data:image/png;base64,{imageBase64}");
            const string accountSid = "AC12e80c0d405f15762ed78da1ad72573a";
            const string authToken = "51ea724235b89d57c694d606555dbfce";
            TwilioClient.Init(accountSid, authToken);
            //     var message = MessageResource.Create(
            //    from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
            //    body: "Hello, there! New Invoice is created!",
            //    to: new Twilio.Types.PhoneNumber("whatsapp:+918828712177")
            //);

            try
            {
                //    var mediaUrl = new[] {
                //    new Uri(uploadedFileUrl)
                //}.ToList();
                //    var message = MessageResource.Create(mediaUrl: mediaUrl, from: new Twilio.Types.PhoneNumber("WhatsApp:+14155238886"), body: "New Invoice is created!", to: new Twilio.Types.PhoneNumber($"WhatsApp:+{contactno}"));
                var message = MessageResource.Create(
                from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),//+12055840648
// to: new Twilio.Types.PhoneNumber(($"WhatsApp:+{contactno}")),
                to: new Twilio.Types.PhoneNumber(($"WhatsApp:+918828712177")),
                body: "Hello, there! New Invoice is created!",
                messagingServiceSid: "MG375a26b60581816362d59a21768280f3", // Replace with your messaging service SID
                mediaUrl: new[] { new Uri(uploadedFileUrl) }.ToList());

                Console.WriteLine($"Message SID: {message.Sid}");//MG375a26b60581816362d59a21768280f3
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Delete the temporary file when done
                //File.Delete(uploadedFileUrl);
            }

            return "SMS sent!";
        }
        private string GetTemporaryUrl(byte[] fileBytes, string fileName)
        {

            string server = "ftp://initialinfinity.com";
            string userName = "azim-i-f";
            string password = "S#uz7909d";
            string remoteFilePath = $"/wwwroot/img/WAFile/{fileName}";
            UploadFileToFtp(server, userName, password, remoteFilePath, fileBytes);


            // Return the URL of the uploaded file
            string fileUrl = $"https://initialinfinity.com/img/WAFile/{fileName}";
            SendInvsms("918828712177", fileUrl);
            return fileUrl;
            // return publicUrl;
        }
        static void UploadFileToFtp(string server, string userName, string password, string remoteFilePath, byte[] fileBytes)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create($"{server}{remoteFilePath}");
                ftpRequest.Credentials = new NetworkCredential(userName, password);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.UsePassive = true;

                using (Stream ftpStream = ftpRequest.GetRequestStream())
                {
                    ftpStream.Write(fileBytes, 0, fileBytes.Length);
                }

                Console.WriteLine("File uploaded successfully.");
            }
            catch (WebException ex)
            {
                FtpWebResponse res = (FtpWebResponse)ex.Response;
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Status: {res.StatusDescription}");
            }
        }

        private async Task sGetTemporaryUrl(byte[] fileBytes, string fileName,string contactno)
        {
            //string accessKey = "MansiMW";
            //string secretKey = "nO5-#55w";s3://infinitybills/invoice/Mansi_Resume.pdf
            string accessKey = "AKIATMGJHIIUKI34ULGQ";
            string secretKey = "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";
            string bucketName = "infinitybills";
            string keyName = $"invoice/{fileName}";
            string uploadedFileUrl = await UploadFileToS3(accessKey, secretKey, bucketName, fileBytes, keyName);


            // Return the URL of the uploaded file
            string fileUrl = $"https://infinitybills.s3.amazonaws.com/invoice/{fileName}";
            SendInvsms($"91{contactno}", fileUrl);
            //return fileUrl;s3://infinitybills/invoice/arn:aws:s3:us-east-1:232350237224:accesspoint/infinitybill
            // return publicUrl;s3://infinitybills/invoice/
        }
        static async Task<string> UploadFileToS3(string accessKey, string secretKey, string bucketName, byte[] fileBytes, string keyName)
        {
            try
            {
                var region = RegionEndpoint.USEast1; // Change the region to the appropriate one for your S3 bucket

                var credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);

                using (var client = new AmazonS3Client(credentials, region))
                {
                    using (var memoryStream = new MemoryStream(fileBytes))
                    {
                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            BucketName = bucketName,
                            Key = keyName,
                            InputStream = memoryStream,
                            ContentType = "application/octet-stream" // Set the content type based on your file type
                        };

                        var fileTransferUtility = new TransferUtility(client);
                        await fileTransferUtility.UploadAsync(uploadRequest);
                        //string url = client.GetPreSignedURL(uploadRequest);

                        return "";
                    }

                }
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error uploading file to S3: {ex.Message}");
                return "";
            }
        }
        public DateTime? findtime(string utcOffsetString)
        {
            try
            {
                // Attempt to parse the input timeZone as a TimeSpan
                string cleanedOffset = utcOffsetString.Replace("UTC", "").Trim();

                // Extract sign, hours, and minutes
                char sign = cleanedOffset[0];
                int hours = int.Parse(cleanedOffset.Substring(1, 2));
                int minutes = int.Parse(cleanedOffset.Substring(4, 2));

                // Calculate total minutes
                int totalMinutes = (sign == '+') ? (hours * 60 + minutes) : -(hours * 60 + minutes);

                // Create a TimeSpan with the total minutes
                TimeSpan utcOffset = TimeSpan.FromMinutes(totalMinutes);

                // Get the current time in UTC
                DateTime utcNow = DateTime.UtcNow;

                // Calculate the local time with the specified UTC offset
                DateTime localTime = utcNow + utcOffset;
                // Return the local time
                return localTime;

            }
            catch (Exception)
            {
                // Handle any exception that might occur during the process
                // Log the exception or take appropriate action based on your requirements
                return null;
            }
        }
    }
}
