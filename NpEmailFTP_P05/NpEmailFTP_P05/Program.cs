using HtmlAgilityPack;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace NpEmailFTP_P05
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // parse HTML
            //var query = "Hello world";
            //var url = $"https://www.google.com/search?q={query}";
            //using var client = new HttpClient();
            //var html = await client.GetStringAsync(url);
            //html = WebUtility.HtmlDecode(html);
            ////Console.WriteLine(html);
            //var doc = new HtmlDocument();
            //doc.LoadHtml(html);
            //var resultNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'g')]");
            //foreach ( var node in resultNodes )
            //{
            //    //Console.WriteLine(node.InnerText);
            //    var titleNode = node.SelectSingleNode(".//h3");
            //    var uriNode = node.SelectSingleNode(".//a[@href]");
            //    if(titleNode != null&&uriNode!=null ) 
            //    {
            //        var title = titleNode.InnerText;
            //        var uri = uriNode.Attributes["href"].Value;
            //        Console.ForegroundColor= ConsoleColor.Green;
            //        Console.WriteLine(title);
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine(uri);
            //    }
            //}


            // smtp

            //var fromAddress = "itstepmailfortest@gmail.com";
            //var toAddress = "itstepmailfortest@gmail.com";
            //var subject = "Test Email";
            //var body = "Some text in the test email";

            //var message = new MailMessage(fromAddress,toAddress, subject, body);
            //message.Attachments.Add(new Attachment("attachmentTest.txt"));
            //message.Attachments.Add(new Attachment("1.jpg"));
            //var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            //smtpClient.EnableSsl= true;
            //smtpClient.Credentials = new NetworkCredential("itstepmailfortest@gmail.com", "zcuvixejnadycxtd");
            //try
            //{
            //    smtpClient.Send(message);
            //    Console.WriteLine("Email was sent successfully");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            // imap
            //    try
            //    {

            //        using var imapClient = new ImapClient();
            //        await imapClient.ConnectAsync("imap.gmail.com", 993, true);
            //        await imapClient.AuthenticateAsync("itstepmailfortest@gmail.com", "zcuvixejnadycxtd");
            //        await imapClient.Inbox.OpenAsync(FolderAccess.ReadOnly);

            //        var uids = await imapClient.Inbox.SearchAsync(SearchOptions.All, SearchQuery.All);
            //        foreach (var uid in uids.UniqueIds)
            //        {
            //            var message = await imapClient.Inbox.GetMessageAsync(uid);
            //            Console.WriteLine($"Message UID: {uid}");
            //            Console.WriteLine($"Subject: {message.Subject}");
            //            Console.WriteLine($"From: {message.From}");
            //            Console.WriteLine($"To: {message.To}");
            //            Console.WriteLine($"Date: {message.Date.ToLocalTime()}");
            //            Console.WriteLine($"Body: {message.TextBody}");
            //            Console.WriteLine($"Attachments:");

            //            foreach (MimeEntity attachment in message.Attachments)
            //            {
            //                var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
            //                Console.WriteLine($"\t\t{fileName}");
            //                using var stream = File.Create($"..\\{fileName}");
            //                var part = attachment as MimePart;
            //                part?.Content?.DecodeTo(stream);
            //            }
            //        }
            //        await imapClient.DisconnectAsync(true);
            //    }
            //    catch (Exception ex)
            //    {

            //        Console.WriteLine(ex.Message);
            //    }
            //    Console.WriteLine();

            // FTP

            //var request = (FtpWebRequest)WebRequest.Create("ftp://test.rebex.net/readme.txt");
            //request.Method = WebRequestMethods.Ftp.DownloadFile;
            //request.Credentials = new NetworkCredential("demo", "password");
            //using var response = (FtpWebResponse)await request.GetResponseAsync();
            //using var stream = response.GetResponseStream();
            //using var fs = new FileStream("readme.txt", FileMode.Create);
            //var bytes = new byte[96];
            //int size = 0;
            //while((size=await stream.ReadAsync(bytes,0,bytes.Length))>0)
            //{
            //    await fs.WriteAsync(bytes,0,size);
            //}

            //Console.WriteLine("Done");

            await ListFilesAndFoldersAsync("ftp://test.rebex.net/pub/example/", "demo", "password");
            Console.ReadKey();

        }
        static async Task ListFilesAndFoldersAsync(string ftpServerUri, string ftpUsername, string ftpPassword)
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpServerUri);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

            using var response = (FtpWebResponse)await request.GetResponseAsync();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            string? line = await reader.ReadLineAsync();
            while(line != null)
            {
                Console.WriteLine(line);
                line = await reader.ReadLineAsync();
            }

        }
    }
}