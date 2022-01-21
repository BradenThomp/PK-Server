using Application.Common.Repository;
using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class SpeakerRepository : BaseCRUDRepository<Speaker>, ISpeakerRepository
    {

        public SpeakerRepository(IConfiguration configuration) : base(configuration) { }

        public override async Task<int> AddAsync(Speaker entity)
        {
            throw new System.NotImplementedException();
        }

        public override Task DeleteAsync(Speaker entity)
        {
            throw new System.NotImplementedException();
        }

        public override Task<IEnumerable<Speaker>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public override Task<Speaker> GetAsync<Tid>(Tid id)
        {
            throw new System.NotImplementedException();
        }

        public override Task UpdateAsync(Speaker entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
