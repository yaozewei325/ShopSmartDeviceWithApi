using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using ShopSmartDevice.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace ShopSmartDevice.Data
{
    public class DataProviderService
    {
        private static string BaseAddress = "https://10.0.2.2:7029";
        private HttpClient httpClient;
        private JsonSerializerOptions serializerOptions;

        public DataProviderService()
        {
#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            this.httpClient = new HttpClient(insecureHandler);
#else
            this.httpClient = new HttpClient();
#endif
            this.httpClient.BaseAddress = new Uri(DataProviderService.BaseAddress);
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

           

        }

        // This method must be in a class in a platform project, even if
        // the HttpClient object is constructed in a shared project.
        public HttpClientHandler GetInsecureHandler()
        {


            HttpClientHandler handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public async Task<List<SmartDevice>> GetAllDevicesAsync()
        {
            List<SmartDevice> Items = new List<SmartDevice>();

            try
            {
                //construire l'uri de la ressource
                Uri uri = new Uri($"{BaseAddress}/api/smartdevices");
                //Lancer la requete http a l'api web
                HttpResponseMessage response = await this.httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = System.Text.Json.JsonSerializer.Deserialize<List<SmartDevice>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<SmartDevice> GetDeviceDetail(SmartDevice device)
        {
            try
            {
                Uri uri = new Uri($"{BaseAddress}/api/smartdevices");

                HttpResponseMessage response = await httpClient.GetAsync($"{uri}/{device.Id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    device = JsonConvert.DeserializeObject<SmartDevice>(content);
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return device;
        }

        public async Task<SmartDevice> GetDeviceById(int id)
        {
            var device = new SmartDevice();
            try
            {
                Uri uri = new Uri($"{BaseAddress}/api/smartdevices");

                HttpResponseMessage response = await httpClient.GetAsync($"{uri}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    device = JsonConvert.DeserializeObject<SmartDevice>(content);
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return device;
        }

        public async Task<SmartDevice> AddDeviceAsync(SmartDevice device)
        {
            Uri uri = new Uri($"{BaseAddress}/api/smartdevices");

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(device), Encoding.UTF8, "application/json");


                HttpResponseMessage response = await httpClient.PostAsync(uri, content);


                if (response.IsSuccessStatusCode)
                {
                    string repcontent = await response.Content.ReadAsStringAsync();
                    SmartDevice item = System.Text.Json.JsonSerializer.Deserialize<SmartDevice>(repcontent, serializerOptions);
                    return item;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return null;
        }

        public async Task UpdateDeviceAsync(int id, string modele, string fabricant, string type, string platform, double prix, string imageUrl)
        {
            var newDevice = new SmartDevice()
            {
                Id = id,
                Modele = modele,
                Fabriquant = fabricant,
                Type = type,
                Plateforme = platform,
                Prix = prix,
                ImageUrl = imageUrl
            };
            //construire l'uri de la ressource
            Uri uri = new Uri($"{BaseAddress}/api/smartdevices/{id}");

            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize<SmartDevice>(newDevice, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                //Lancer la requete http a l'api web
                HttpResponseMessage response = await this.httpClient.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\t Produit modifié avec succès.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<bool> DeleteDeviceAsync(SmartDevice item)
        {
            //construire l'uri de la ressource
            Uri uri = new Uri($"{BaseAddress}/api/smartdevices/{item.Id}");

            try
            {
                HttpResponseMessage response = await this.httpClient.DeleteAsync(uri);

                return response.IsSuccessStatusCode;


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;
        }


        //*********************CRUD pours les Factures*************************
        public async Task<List<Facture>> GetAllFactureAsync()
        {
            List<Facture> Items = new List<Facture>();

            try
            {
                //construire l'uri de la ressource
                Uri uri = new Uri($"{BaseAddress}/api/factures");
                //Lancer la requete http a l'api web
                HttpResponseMessage response = await this.httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = System.Text.Json.JsonSerializer.Deserialize<List<Facture>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }


        public async Task<Facture> GetFactureById(int id)
        {
            var facture = new Facture();
            try
            {
                Uri uri = new Uri($"{BaseAddress}/api/factures");

                HttpResponseMessage response = await httpClient.GetAsync($"{uri}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    facture = JsonConvert.DeserializeObject<Facture>(content);
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return facture;
        }

        public async Task<Facture> AddFactureAsync(Facture facture)
        {
            Uri uri = new Uri($"{BaseAddress}/api/factures");

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(facture), Encoding.UTF8, "application/json");


                HttpResponseMessage response = await httpClient.PostAsync(uri, content);


                if (response.IsSuccessStatusCode)
                {
                    string repcontent = await response.Content.ReadAsStringAsync();
                    Facture item = System.Text.Json.JsonSerializer.Deserialize<Facture>(repcontent, serializerOptions);
                    return item;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return null;
        }


        public async Task<bool> DeleteFactureAsync(Facture item)
        {
            //construire l'uri de la ressource
            Uri uri = new Uri($"{BaseAddress}/api/factures/{item.Id}");

            try
            {
                HttpResponseMessage response = await this.httpClient.DeleteAsync(uri);

                return response.IsSuccessStatusCode;


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;
        }
    }
}
