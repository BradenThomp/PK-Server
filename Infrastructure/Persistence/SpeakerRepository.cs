using Application.Common.Repository;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class SpeakerRepository : GenericCRUDRepository<Speaker>, ISpeakerRepository
    {
        protected override string TableName => "speaker";

        public SpeakerRepository(IConfiguration configuration) : base(configuration) { }
    }
}
