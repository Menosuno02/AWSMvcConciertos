using AWSMvcConciertos.Models;
using System.Net.Http.Headers;

namespace AWSMvcConciertos.Services
{
    public class ServiceApiConciertos
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiConciertos(KeysModel keys)
        {
            this.UrlApi = keys.ApiConciertos;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.GetAsync(this.UrlApi + request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                return default(T);
            }
        }

        public async Task<List<Evento>> GetEventosAsync()
        {
            string request = "api/Conciertos";
            List<Evento> data = await CallApiAsync<List<Evento>>(request);
            return data;
        }

        public async Task<List<Evento>> GetEventosCategoriaAsync(int idCategoria)
        {
            string request = "api/Conciertos/EventosCategoria/" + idCategoria;
            List<Evento> data = await CallApiAsync<List<Evento>>(request);
            return data;
        }

        public async Task<List<CategoriaEvento>> GetCategoriasEventosAsync()
        {
            string request = "api/Conciertos/CategoriasEventos";
            List<CategoriaEvento> data = await CallApiAsync<List<CategoriaEvento>>(request);
            return data;
        }
    }
}
