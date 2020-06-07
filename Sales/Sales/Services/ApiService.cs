namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common.Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;

    public class ApiService
    {

        public async Task<Responses> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Responses
                {
                    Correcto = false,
                    Texto  = "Activa tu conexion a internet",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Responses
                {
                    Correcto = false,
                    Texto = "No dispone de conexión a internet",
                };
            }

            return new Responses
            {
                Correcto = true,
            };
        }



        public object Jsonconvert { get; private set; }

        public async Task<Responses> GetList<T>(string urlBase, string prefix, string controller)
        {
            try
            {
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await cliente.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Responses
                    {
                        Correcto = false,
                        Texto = answer,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Responses
                {
                    Correcto = true,
                    Resultado = list,
                };
            }
            catch (Exception ex)
            {
                return new Responses
                {
                    Correcto = false,
                    Texto = ex.Message,
                };
            }
        }
    }
}
