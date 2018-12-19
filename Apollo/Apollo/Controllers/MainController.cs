using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.Net;
using System.IO;

namespace Apollo.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //fonction qui genere un string detaillant les genres filtres, pris en compte dans l'url de la recherche
        public string generateTextGenres(string text)
        {
            return text.Replace(",", "&filter[genres][eq]=");
        }

        //fonction qui genere l'url complet de la recherche dans l'api et qui la retourne sous forme de string
        public string search(string searchText, string genres)
        {
            string requestText = "https://api-endpoint.igdb.com/games/?fields=*&limit=20";

            if (searchText != "")
            {
                requestText += ("&search=" + searchText);
            }

            string textGenres = generateTextGenres(genres);
            requestText += textGenres;

            return reponseRequette(requestText);
        }
        
        //fonction qui fait la requete pour recuperer les genres
        public string genres()
        {
            string url = "https://api-endpoint.igdb.com/genres/?fields=*";
            return reponseRequette(url);
        }

        //fonction qui effectue la requete dans l'api et retourne la reponse sous forme de string
        private string reponseRequette(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("user-key:77e1f5c0a7a13d94655aafe425815472");
            Console.WriteLine(request.Headers);
            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                responseReader.Close();
                return response;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                return null;
            }
        }
    }
}