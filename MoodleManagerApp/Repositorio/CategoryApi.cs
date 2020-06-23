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

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);

            List<Categories> categories = JsonConvert.DeserializeObject<List<Categories>>(response.Content);

            return categories;
        }

        public static int create_category(Categories category)
        {
            Cursor.Current = Cursors.WaitCursor;
            var client = new RestClient(ConfigRepo.getUrl() + "?wstoken=" + ConfigRepo.getToken() +
                "&wsfunction=core_course_create_categories&moodlewsrestformat=json" +
                "&categories[0][name]=" + category.name +
                "&categories[0][parent]=0");


            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            string source = response.Content;
            dynamic data = JObject.Parse(source.Replace("[", "").Replace("]", ""));

            Cursor.Current = Cursors.Default;
            return data.id;

        }

        internal static void update_category(Categories category)
        {
            Cursor.Current = Cursors.WaitCursor;
            var client = new RestClient(ConfigRepo.getUrl() + "?wstoken=" + ConfigRepo.getToken() +
                "&wsfunction=core_course_update_categories&moodlewsrestformat=json" +
                "&categories[0][id]=" + category.id +
                "&categories[0][name]=" + category.name);


            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
                        
            Cursor.Current = Cursors.Default;
            

        }

        internal static void delete_category(Categories category)
        {
            Cursor.Current = Cursors.WaitCursor;

            var client = new RestClient(ConfigRepo.getUrl() + "?wstoken=" + ConfigRepo.getToken() +
               "&wsfunction=core_course_delete_categories&moodlewsrestformat=json" +
               "&categories[0][id]=" + category.id +
               "&categories[0][recursive]=0" +
               "&categories[0][newparent]=1");

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            client.Execute(request);

            Cursor.Current = Cursors.Default;

        }
    }
}
