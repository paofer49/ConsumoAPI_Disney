using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConsumoAPI_Disney.Model;
using Newtonsoft.Json;

namespace ConsumoAPI_Disney.Controller
{
    internal class ControllerPersonajes
    {
        static readonly HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7103/")
        };

        public async Task<List<Personajes>> MostrarPersonajes() 
        { 
            HttpResponseMessage response = await client.GetAsync("api/Personajes/");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Personajes>>(jsonString);
        }

        public async Task<Personajes> GetCharacterById(int id)
        {
            try
            {
                var response = await client.GetAsync($"api/Personajes/{id}");
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Personaje no encontrado.");

                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Personajes>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener personaje por ID: {ex.Message}");
            }
        }
    }
}
