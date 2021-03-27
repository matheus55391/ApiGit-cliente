using Avonale_ApiGit.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Avonale_ApiGit.Services
{
    public class ApiGit
    {
        private readonly string urlMeuRepositorio = "https://api.github.com/users/matheus55391/repos";
        private readonly string urlPesquisaRepositorio = "https://api.github.com/search/repositories?q=";
        private readonly string urlParametro_PorPagina = "?per_page =20";
        private string urlParametro_NumPagina = "&page =";
        private readonly WebClient client;

        public ApiGit()
        {
            client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.78 Safari/537.36");

        }

        public List<Repositorio> GetMeusRepositorios()
        {
            List<Repositorio> repositorios = new List<Repositorio>();
            var resposta = client.DownloadString(urlMeuRepositorio);
            var repoJSON = JArray.Parse(resposta);

            foreach (var repoJToken in repoJSON)
            {
                JObject repo = (JObject)repoJToken;
                Repositorio repoObject = new Repositorio
                {
                    Name = repo.GetValue("name").ToString()
                };

                repositorios.Add(repoObject);
            }
            return repositorios;
        }

        public List<Repositorio> Pesquisar(string repoNome)
        {
            List<Repositorio> repositorios = new List<Repositorio>();
            string urlPesquisa = this.urlPesquisaRepositorio + repoNome;          

            var resposta = client.DownloadString(urlPesquisa);

            Root result = JsonConvert.DeserializeObject<Root>(resposta);

            var items = result.items;
            
            foreach (var item in items)
            {

                Repositorio repoObject = new Repositorio
                {
                    Name = item.name,
                    Description = item.description,
                    Owner = item.owner.login,
                    Language = item.language,
                    Updated_at = item.updated_at.ToString(),
                    Contributors = GetContribuidores(item.contributors_url)
                };
                repositorios.Add(repoObject); 
                
            }
            return repositorios;
        }
        private List<string> GetContribuidores(string url)
        {
            List<String> contribuidores = new List<string>();
            WebClient client2 = new WebClient();
            client2.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.78 Safari/537.36");

            url = string.Format(url);
            var resposta = client2.DownloadString(url);
            var repoJSON = JArray.Parse(resposta);
           
            foreach (var repoJToken in repoJSON)
            {
                JObject repoJObject = (JObject)repoJToken;
                contribuidores.Add(repoJObject.GetValue("login").ToString());
            }
            return contribuidores;
        }
    }
}
