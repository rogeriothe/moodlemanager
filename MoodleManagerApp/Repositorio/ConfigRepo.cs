using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleManagerApp.Repositorio
{
    public class ConfigRepo
    {
        public static string getUrl()
        {
            string baseurl;
            using (moodleEntities db = new moodleEntities())
            {
                Config conf = db.Config.First();
                baseurl = conf.BaseUrl;
            }
            
            return baseurl;
        }

        public static string getToken()
        {
            string token;
            using (moodleEntities db = new moodleEntities())
            {
                Config conf = db.Config.First();
                token = conf.Token;
            }
                        
            return token;
        }
    }

}
