using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace TPKC
{
    namespace APIs
    {
        public class Local
        {
            public static Stream GetStream(string text)
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(text);
                return new MemoryStream(byteArray);
            }
        }
        public class Console
        {
            public static void WriteLine(string x)
            {
                System.Windows.Forms.MessageBox.Show(x);
            }
        }
        public class Gmail
        {
            static string[] Scopes = { GmailService.Scope.GmailReadonly, GmailService.Scope.GmailSend };
            static string ApplicationName = "NEJC PKC";//Gmail API .NET Quickstart
            UserCredential credential;
            GmailService service;

            public Gmail()
            {
                reauth();
            }

            public void reauth()
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(Local.GetStream(TPKC_GUI.Properties.Resources.gapi)).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None).Result;
                service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            }
            /// <summary>
            /// Retrieve a Message by ID.
            /// </summary>
            /// <param name="userId">User's email address. The special value "me"
            /// can be used to indicate the authenticated user.</param>
            /// <param name="messageId">ID of Message to retrieve.</param>
            public Message GetMessage(string userId, string messageId)
            {
                try
                {
                    return service.Users.Messages.Get(userId, messageId).Execute();
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                }
                return null;
            }
            /// <summary>
            /// Send an email from the user's mailbox to its recipient.
            /// </summary>
            /// <param name="userId">User's email address. The special value "me"
            /// can be used to indicate the authenticated user.</param>
            /// <param name="email">Email to be sent.</param>
            public Message SendMessage(string userId, Message email)
            {
                try
                {
                    return service.Users.Messages.Send(email, userId).Execute();
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                }
                return null;
            }
        }
    }
}
