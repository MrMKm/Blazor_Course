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
    public class GenderAPI : IGenderAPI
    {
        private readonly string _endpoint = "api/gender";
        private readonly HttpClient _httpClient;

        public GenderAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AddGender(GenderDto genderDto)
        {
            var result = await _httpClient.PostAsJsonAsync(_endpoint, genderDto);

            if (!result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();

            return null;
        }

        public async Task<GenderDto> GetByID(int ID)
        {
            return await _httpClient.GetFromJsonAsync<GenderDto>($"{_endpoint}/{ID}");
        }

        public async Task<List<GenderDto>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<GenderDto>>($"{_endpoint}/all");
        }

        public async Task<string> EditGender(GenderDto genderDto)
        {
            var result = await _httpClient.PutAsJsonAsync(_endpoint, genderDto);

            if (!result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();

            return null;
        }
    }
}
