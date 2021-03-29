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
        //BASE URL UTEIS 
        private readonly string urlMeuRepositorio = "https://api.github.com/users/matheus55391/repos";
        private readonly string urlPesquisaRepositorio = "https://api.github.com/search/repositories?q=";
        private readonly string urlPesquisaRepositorioById = "https://api.github.com/repositories/";
        private readonly string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.78 Safari/537.36";
        private readonly WebClient client;

        public ApiGit()
        {
            client = new WebClient();
            client.Headers.Add("user-agent", userAgent);

        }

        public List<Repositorio> GetMeusRepositorios()
        { 
            
            List<Repositorio> repositorios = new List<Repositorio>();

            var resposta = client.DownloadString(urlMeuRepositorio);

            List<Repositorio> result = JsonConvert.DeserializeObject<List<Repositorio>>(resposta);

            foreach (var repositorio in result)
            {

                Repositorio obj = new Repositorio
                {
                    Id = repositorio.Id,
                    Name = repositorio.Name,
                    Owner = repositorio.Owner

                };

                repositorios.Add(obj);
            }
            return repositorios;
        }

        public List<Repositorio> Pesquisar(string repoNome)
        {
            //PESQUISA POR REPOSITORIOS APARTIR DE UM NOME 
            //RETORNA A LISTA DESSA PESQUISA
            List<Repositorio> repositorios = new List<Repositorio>();
            string urlPesquisa = this.urlPesquisaRepositorio + repoNome;

            var resposta = client.DownloadString(urlPesquisa);

            Root result = JsonConvert.DeserializeObject<Root>(resposta);

            var items = result.items;

            foreach (var item in items)
            {

                Repositorio obj = new Repositorio
                {
                    Id = item.id,
                    Name = item.name,
                    Owner = item.owner
                };
                repositorios.Add(obj);

            }
            return repositorios;
        }
        private List<string> GetContribuidores(string url)
        {
            
            List<String> contribuidores = new List<string>();
            WebClient client2 = new WebClient();
            client2.Headers.Add("user-agent", userAgent);

            url = string.Format(url);
            var resposta = client2.DownloadString(url);
            var repoJSON = JArray.Parse(resposta);

            foreach (var repo in repoJSON)
            {
                JObject repoJObject = (JObject)repo;
                contribuidores.Add(repoJObject.GetValue("login").ToString());
            }
            return contribuidores;
        }

        public Repositorio GetRepositorioID(String id)
        {
            //consome repositorio pelo id
            Repositorio rep = new Repositorio();
            WebClient client2 = new WebClient();
            client2.Headers.Add("user-agent", userAgent);
            var resposta = client2.DownloadString(urlPesquisaRepositorioById + id);
            Item result = JsonConvert.DeserializeObject<Item>(resposta);

            rep.Id = result.id;
            rep.Name = result.name;
            rep.Description = result.description;
            rep.Updated_at = result.updated_at.ToString();
            rep.Language = result.language;
            rep.Owner = result.owner;
            rep.Contributors = GetContribuidores(result.contributors_url);

            return rep;
        }



    }
}
