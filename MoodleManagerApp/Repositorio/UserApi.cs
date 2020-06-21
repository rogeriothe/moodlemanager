using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                userResults.Add(userResult);                
            }

            return userResults;
        }
    }
}
