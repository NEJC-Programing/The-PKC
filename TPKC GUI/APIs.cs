using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;

namespace TPKC
{
    class Resources : TPKC_GUI.Properties.Resources { }

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
            /// <summary>
            /// makes new credential and service varubals 
            /// </summary>
            public void reauth()
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(Local.GetStream(Resources.gapi)).Secrets,
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
            /// <summary>
            /// List all Messages of the user's mailbox matching the query.
            /// </summary>
            /// <param name="userId">User's email address. The special value "me"
            /// can be used to indicate the authenticated user.</param>
            /// <param name="query">String used to filter Messages returned.</param>
            public List<Message> ListMessages(string userId, string query)
            {
                List<Message> result = new List<Message>();
                UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List(userId);
                request.Q = query;

                do
                {
                    try
                    {
                        ListMessagesResponse response = request.Execute();
                        result.AddRange(response.Messages);
                        request.PageToken = response.NextPageToken;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occurred: " + e.Message);
                    }
                } while (!String.IsNullOrEmpty(request.PageToken));

                return result;
            }
        }

        public class Text
        {
            public static string Correct(string word)
            {
                string xml = new System.Net.WebClient().DownloadString("https://www.google.com/complete/search?output=toolbar&q=" + word);
                XDocument Suggestions = XDocument.Parse(xml);
                XAttribute attr = Suggestions.Root.Element("CompleteSuggestion").Element("suggestion").Attribute("data");
                return attr.Value;
            }

            public static List<string> Suggest(string word)
            {
                try
                {
                    string url = Uri.EscapeUriString("https://www.google.com/complete/search?output=toolbar&q=" + word);
                
                    string xml = new System.Net.WebClient().DownloadString(url);
                    System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                    document.LoadXml(xml);

                    string json = JsonConvert.SerializeXmlNode(document);

                    TextJSON.RootObject sugestions = JsonConvert.DeserializeObject<TextJSON.RootObject>(json);

                    List<string> data = new List<string>();
                    foreach (var item in sugestions.toplevel.CompleteSuggestion)
                        data.Add(item.suggestion.__invalid_name__data);

                    return data;
                }
                catch { }
                return null;
            }
        }
    }
}
