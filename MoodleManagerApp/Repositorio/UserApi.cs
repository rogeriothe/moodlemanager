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
        
        public static IList<User> get_users()
        {
            moodleEntities db = new moodleEntities();

            var system_users = new List<string> { "guest", "admin", "moodlemanager" };

            var client = new RestClient(Config.getUrl() + Config.getToken()  + "&wsfunction=core_user_get_users&moodlewsrestformat=json&criteria[0][key]=auth&criteria[0][value]=manual");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            JObject usuarioMoodle = JObject.Parse(response.Content);
            IList<JToken> results = usuarioMoodle["users"].Children().ToList();
            IList<User> userResults = new List<User>();
            foreach (JToken result in results)
            {
                User userResult = result.ToObject<User>();                
                if (!system_users.Contains(userResult.username)) {

                    userResults.Add(userResult);
                }
            }

            return userResults;
        }

        public static int create_user(User user)
        {

            var client = new RestClient(Config.getUrl() + Config.getToken() + "&wsfunction=core_user_create_users&moodlewsrestformat=json"+
                "&users[0][username]=" + user.username +
                "&users[0][auth]=manual" +
                "&users[0][password]=" + user.password +
                "&users[0][firstname]=" + user.firstname +
                "&users[0][lastname]=" + user.lastname +
                "&users[0][email]=" + user.email);
                       

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            string source = response.Content;
            dynamic data = JObject.Parse(source.Replace("[","").Replace("]", ""));            

            return data.id;

        }

        internal static void update_user(User user)
        {
            var client = new RestClient(Config.getUrl() + Config.getToken() + "&wsfunction=core_user_update_users&moodlewsrestformat=json" +
                "&users[0][id]=" + user.moodle_id +
                "&users[0][username]=" + user.username +
                "&users[0][auth]=manual" +
                "&users[0][password]=" + user.password +
                "&users[0][firstname]=" + user.firstname +
                "&users[0][lastname]=" + user.lastname +
                "&users[0][email]=" + user.email);

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            client.Execute(request);
            //IRestResponse response = client.Execute(request);
            //string source = response.Content;            

        }

        internal static void delete_user(User user)
        {
            var client = new RestClient(Config.getUrl() + Config.getToken() + "&wsfunction=core_user_delete_users&moodlewsrestformat=json" +
               "&userids[0]=" + user.moodle_id );
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            client.Execute(request);

        }
    }
    
}
