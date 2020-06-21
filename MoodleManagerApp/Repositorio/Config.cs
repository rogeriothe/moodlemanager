using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleManagerApp.Repositorio
{
    public class Config
    {
        public static string getUrl()
        {
            return "http://app.unifsa.com.br:8080/webservice/rest/server.php?";
        }

        public static string getToken()
        {
            return "wstoken=e920af60445086404a0417af0b6fc9ae";
        }
    }

}
