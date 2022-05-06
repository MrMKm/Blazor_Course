using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Shared.DataObjectTransfer;

namespace TestProject.Client.Services.Interfaces
{
    public interface IGenderAPI
    {
        Task<string> AddGender(GenderDto genderDto);
        Task<string> EditGender(GenderDto genderDto);
        Task<List<GenderDto>> GetAll();
        Task<GenderDto> GetByID(int ID);
    }
}
