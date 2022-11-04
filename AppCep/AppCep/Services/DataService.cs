using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using AppConsultaCEP.Model;

namespace AppConsultaCEP.Services
{
    internal class DataService
    {
        public static async Task<CEP> getInfoFromCEP(string cep)
        {
            string urlApi = "https://cep.metoda.com.br/endereco/by-cep?cep=";

            dynamic resultado = await getDataFromService(urlApi + cep).ConfigureAwait(false);

            if (resultado.slip != null)
            {
                CEP info_cep = new CEP();

                info_cep.cep = (string)resultado.slip.CEP;
                info_cep.id_logradouro = (string)resultado.slip.id_logradouro;
                info_cep.tipo = (string)resultado.slip.tipo;
                info_cep.descricao = (string)resultado.slip.descricao;
                info_cep.id_cidade = (string)resultado.slip.id_cidade;
                info_cep.UF = (string)resultado.slip.UF;
                info_cep.complemento = (string)resultado.slip.complemento;
                info_cep.descricao_cidade = (string)resultado.slip.descricao_cidade;
                info_cep.codigo_cidade_ibge = (string)resultado.slip.codigo_cidade_ibge;
                info_cep.descricao_bairro = (string)resultado.slip.descricao_bairro;

                return info_cep;
            }
            else
            {
                return null;
            }
        }

        public static async Task<dynamic> getDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }
            return data;
        }
    }
}