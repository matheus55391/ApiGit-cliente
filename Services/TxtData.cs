using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Avonale_ApiGit.Services
{
    public class TxtData
    {
        public static string getFilePath()
        {
            string fileName = "data.txt";
            return Path.Combine(getFolderPath(), fileName);
        }
        public static string getFolderPath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(folderPath, "ApiGitConsumer");
        }

        public static bool Init()
        {
            //Garante que tudo esteja configurado
            //cria uma pasta com um arquivo data.txt no diretorio AppData\Roaming\
            Directory.CreateDirectory(getFolderPath());
            string filePath = getFilePath();
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                TextWriter arquivo = File.AppendText(filePath);
                arquivo.WriteLine("\n352186047\n"); // um repositorio de base
                arquivo.Close();
                return true;
            }
            return false;
        }


        public static string[] LerData()
        {
            //retornar uma lista com todos os id
            string filePath = getFilePath();
            string[] fileConteudo = { };

            if (File.Exists(filePath))
            {
                fileConteudo = File.ReadAllLines(filePath);
            }
            return fileConteudo;
        }

        public static bool Remover(string conteudo)
        {
            //remove um valor do arquivo txt


            // verifica se o valor existe para poder realizar a atualização da lista
            bool teste = false;

            //Ler o arquivo linha por linha para armazenar em uma nova lista
            List<string> lista_id = new List<string>();
            using (StreamReader sr = new StreamReader(getFilePath()))
            {
                while (!sr.EndOfStream)
                {
                    lista_id.Add(sr.ReadLine());
                }
            }

            for (int linha = 0; linha < lista_id.Count; linha++)
            {
                if (lista_id[linha] == conteudo)
                {
                    lista_id.RemoveAt(linha); //Remove a linha
                    teste = true;
                }

            }
            if (teste)
            {
                //Reescrever o arquivo linha a linha:
                using (StreamWriter sw = new StreamWriter(getFilePath(), false))
                {
                    bool first = true;
                    foreach (string str in lista_id)
                    {
                        if (str != "")
                        {
                            if (first)
                                first = false;
                            else
                                sw.WriteLine();
                            sw.Write("\n" + str);
                        }

                    }
                }
            }


            return false;
        }

        public static bool Adicionar(string conteudo)
        {
            //Adiciona um novo favorito

            try
            {
                string filePath = getFilePath();

                string[] fileConteudo = File.ReadAllLines(filePath);
                using (StreamWriter file = (File.Exists(filePath)) ? File.AppendText(filePath) : File.CreateText(filePath))
                {
                    if (!fileConteudo.Contains((string)conteudo))
                    {
                        file.WriteLine("\n" + conteudo + "\n");
                    }

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
