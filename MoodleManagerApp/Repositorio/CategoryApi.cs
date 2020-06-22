using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoodleManagerApp.Repositorio
{
    public class CategoryApi
    {


        public static IList<Categories> get_categories()
        {

            Cursor.Current = Cursors.WaitCursor;

            moodleEntities db = new moodleEntities();

            var system_categories = new List<string> { "Miscellaneous" };

            var client = new RestClient(ConfigRepo.getUrl() + "?wstoken=" + ConfigRepo.getToken() + "&wsfunction=core_course_get_categories&moodlewsrestformat=json");

            Clipboard.SetText(client.BaseUrl.ToString());

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);                      
            
            List<Categories> categories = JsonConvert.DeserializeObject<List<Categories>>(response.Content);
            
            return categories;
        }

        public static int create_user(Users user)
        {
            Cursor.Current = Cursors.WaitCursor;
            var client = new RestClient(ConfigRepo.getUrl() + "?wstoken=" + ConfigRepo.getToken() +
                "&wsfunction=core_user_create_users&moodlewsrestformat=json" +
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
            dynamic data = JObject.Parse(source.Replace("[", "").Replace("]", ""));

            Cursor.Current = Cursors.Default;
            return data.id;

        }

        internal static void update_user(Users user)
        {
            Cursor.Current = Cursors.WaitCursor;

            string paramChangePassword = "";

            if (Funcoes.Pergunta("Deseja atualizar a senha do usuário?", "Trocar senha?"))
            {
                paramChangePassword = "&users[0][password]=" + user.password;
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
                "&users[0][suspended]=" + suspended);

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
               "&userids[0]=" + user.id);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            client.Execute(request);

            Cursor.Current = Cursors.Default;

        }
    }
}
