using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Shared.DataObjectTransfer;

namespace TestProject.Client.Services.Interfaces
{
    public interface IActorAPI
    {
        Task<string> AddActor(ActorDto ActorDto);
        Task<string> EditActor(ActorDto ActorDto);
        Task<List<ActorDto>> GetAll();
        Task<ActorDto> GetByID(int ID);
    }
}
