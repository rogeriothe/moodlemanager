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
    public class GroupsApi
    {


        public static List<Groups> get_groups()
        {

            Cursor.Current = Cursors.WaitCursor;

            moodleEntities db = new moodleEntities();
            
            var client = new RestClient(ConfigRepo.getUrl() + "?wstoken=" + ConfigRepo.getToken() + "&wsfunction=core_course_get_courses&moodlewsrestformat=json");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);

            List<Courses> lista_cursos = JsonConvert.DeserializeObject<List<Courses>>(response.Content);

            List<Groups> lista = new List<Groups>();

            foreach (var item in lista_cursos)
            {

                var name = item.fullname.Split(new string[] { " - " }, StringSplitOptions.None)[0].Trim();
                var category_id = item.categoryid;

                var qr = (from p in lista
                         where p.category_id == category_id && p.name == name
                         select p).ToList();

                if (qr.Count() == 0)
                {
                    Groups group = new Groups();
                    group.name = name;
                    group.category_id = category_id;
                    lista.Add(group);
                }                
            }

            return lista;
        }

        
    }
}
