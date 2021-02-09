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


        public static List<Turma> get_turmas()
        {

            moodleEntities db = new moodleEntities();
            IRestResponse response = Funcoes.Execute("&wsfunction=core_course_get_courses&moodlewsrestformat=json", Method.POST);
            List<Courses> lista_cursos = JsonConvert.DeserializeObject<List<Courses>>(response.Content);
            List<Turma> lista = new List<Turma>();
            foreach (var item in lista_cursos)
            {
                var name = item.fullname.Split(new string[] { " - " }, StringSplitOptions.None)[0].Trim();
                var category_id = item.categoryid;
                var qr = (from p in lista
                         where p.category_id == category_id && p.name == name
                         select p).ToList();
                if (qr.Count() == 0)
                {
                    Turma turma = new Turma();
                    turma.name = name;
                    turma.category_id = category_id;
                    lista.Add(turma);
                }                
            }

            return lista;
        }

        
    }
}
