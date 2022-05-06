using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TestProject.Client.Services.Interfaces;
using TestProject.Shared.DataObjectTransfer;

namespace TestProject.Client.Services.Implementations
{
    public class ActorAPI : IActorAPI
    {
        private readonly string _endpoint = "api/actor";
        private readonly HttpClient _httpClient;

        public ActorAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AddActor(ActorDto ActorDto)
        {
            var result = await _httpClient.PostAsJsonAsync(_endpoint, ActorDto);

            if (!result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();

            return null;
        }

        public async Task<ActorDto> GetByID(int ID)
        {
            return await _httpClient.GetFromJsonAsync<ActorDto>($"{_endpoint}/{ID}");
        }

        public async Task<List<ActorDto>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<ActorDto>>($"{_endpoint}/all");
        }

        public async Task<string> EditActor(ActorDto ActorDto)
        {
            var result = await _httpClient.PutAsJsonAsync(_endpoint, ActorDto);

            if (!result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();

            return null;
        }
    }
}
