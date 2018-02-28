using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ConsumoWebServiceBLL
    {
        public List<ArquivoTempBLL> ArquivosXlsSelecionados(List<int> ListaSelecionada)
        {
            List<ArquivoTempBLL> LObjArquivoTempBLL = new List<ArquivoTempBLL>();
            ArquivoTempBLL ObjArquivoTempBLL = new ArquivoTempBLL();


            //string URI = "http://server.candinho.com.br/reinfapi/api/projeto/getbyte";
            string URI = "http://localhost:52263/api/projeto/getbyte";

            try
            {

                var payload = ListaSelecionada;
                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    using (var response = client.PostAsync(URI, content).Result)
                    {
                        var resp = response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            var resposta = response.Content.ReadAsStringAsync().Result;
                            LObjArquivoTempBLL = JsonConvert.DeserializeObject<List<ArquivoTempBLL>>(resposta);                            
                            
                        }
                        else
                        {                            
                            
                        }
                    }
                }
            }
            catch (Exception again)
            {
                throw again;
            }

            return LObjArquivoTempBLL;

        }
    }
}
