﻿namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using Helpers;
    
    public class ApiService
    {

        public async Task<Responses> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Responses
                {
                    Correcto = false,
                    Texto  = Languages.ActivarConexion,
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Responses
                {
                    Correcto = false,
                    Texto = Languages.SinConexion,
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

        public async Task<Responses> Post<T>(string urlBase, string prefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await cliente.PostAsync (url,content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Responses
                    {
                        Correcto = false,
                        Texto = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Responses
                {
                    Correcto = true,
                    Resultado = obj,
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
