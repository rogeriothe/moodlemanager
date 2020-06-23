using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace MoodleManagerApp.Repositorio
{
    public class Funcoes
    {

        public static bool get_site_info()
        {



            try
            {
                var client = new RestClient(ConfigRepo.getUrl() + "?wstoken=" + ConfigRepo.getToken() +
                "&wsfunction=core_webservice_get_site_info&moodlewsrestformat=json");

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                IRestResponse response = client.Execute(request);
                string source = response.Content;

                return source != null;
            }
            catch
            {

                return false;
            }
        }
        public static bool Pergunta(string frase, string titulo)
        {
            DialogResult result = MessageBox.Show(frase, titulo, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
