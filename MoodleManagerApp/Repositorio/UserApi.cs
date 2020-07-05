using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace MoodleManagerApp.Repositorio
{
    public class UserApi
    {
        
        public static IList<Users> get_users()
        {

            Cursor.Current = Cursors.WaitCursor;

            moodleEntities db = new moodleEntities();

            var system_users = new List<string> { "guest", "admin", "moodlemanager" };

            var client = new RestClient(ConfigRepo.getUrl() + "?wstoken=" + ConfigRepo.getToken()  + "&wsfunction=core_user_get_users&moodlewsrestformat=json&criteria[0][key]=auth&criteria[0][value]=manual");
            client.Timeout = -1;
                        
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            JObject usuarioMoodle = JObject.Parse(response.Content);
            IList<JToken> results = usuarioMoodle["users"].Children().ToList();
            IList<Users> userResults = new List<Users>();
            foreach (JToken result in results)
            {
                Users userResult = result.ToObject<Users>();                
                if (!system_users.Contains(userResult.username)) {

                    userResults.Add(userResult);
                }
            }
            
            Cursor.Current = Cursors.Default;

            return userResults;
        }

        public static int create_user(Users user)
        {            
            Cursor.Current = Cursors.WaitCursor;

            IRestResponse response = Funcoes.Execute("&wsfunction=core_user_create_users&moodlewsrestformat=json" +
                "&users[0][username]=" + user.username +
                "&users[0][auth]=manual" +
                "&users[0][password]=" + user.password +
                "&users[0][firstname]=" + user.firstname +
                "&users[0][lastname]=" + user.lastname +
                "&users[0][email]=" + user.email, Method.POST);            
            
            string source = response.Content;
            dynamic data = JObject.Parse(source.Replace("[","").Replace("]", ""));

            Cursor.Current = Cursors.Default;
            return data.id == null ? 0 : data.id;

        }

        internal static void update_user(Users user, bool pergunta)
        {
            Cursor.Current = Cursors.WaitCursor;

            string paramChangePassword = "";
            if (pergunta)
            {
                if (Funcoes.Pergunta("Deseja atualizar a senha do usuário?", "Trocar senha?"))
                {
                    paramChangePassword = "&users[0][password]=" + user.password;
                }
            }

            int suspended = user.suspended == true ? 1 : 0;
            var client = new RestClient(ConfigRepo.getUrl() + "?wstoken=" + ConfigRepo.getToken() +
                "&wsfunction=core_user_update_users&moodlewsrestformat=json" +
                "&users[0][id]=" + user.id +
                "&users[0][username]=" + user.username +
                "&users[0][auth]=manual" +
                paramChangePassword +
                "&users[0][firstname]=" + user.firstname +
                "&users[0][lastname]=" + user.lastname +
                "&users[0][email]=" + user.email +
                "&users[0][suspended]=" + suspended );

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            client.Execute(request);
            //IRestResponse response = client.Execute(request);
            //string source = response.Content;          
            Cursor.Current = Cursors.Default;

        }

        internal static void delete_user(Users user)
        {
            Cursor.Current = Cursors.WaitCursor;

            var client = new RestClient(ConfigRepo.getUrl() + "?wstoken=" + ConfigRepo.getToken() +
               "&wsfunction=core_user_delete_users&moodlewsrestformat=json" +
               "&userids[0]=" + user.id );
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            client.Execute(request);

            Cursor.Current = Cursors.Default;

        }
    }
    
}
