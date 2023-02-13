using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class FakePartNumberRepository : IPartNumberRepository
    {
        private readonly IMapper _mapper;

        public FakePartNumberRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartNumber>> GetAsync()
        {
            var path = @$"{AppDomain.CurrentDomain.BaseDirectory}Resources/FakeTraxDB.json";
            var file = await File.ReadAllTextAsync(path);
            var entities = JsonConvert.DeserializeObject<List<PartNumberEntity>>(file);
            var result = _mapper.Map<List<PartNumber>>(entities);

            return result.Take(25);
        }

        public async Task<IEnumerable<PartNumber>> GetAsync(DateTime startDate)
        {
            var path = @$"{AppDomain.CurrentDomain.BaseDirectory}Resources/FakeTraxDB.json";
            var file = await File.ReadAllTextAsync(path);
            var entities = JsonConvert.DeserializeObject<List<PartNumberEntity>>(file);
            // Para nivel de teste mudar (x => x.MODIFIED_DATE <= startDate) para Menor...
            entities = entities.Where(x => x.MODIFIED_DATE <= startDate).ToList();
            var result = _mapper.Map<List<PartNumber>>(entities);

            return result.Take (25);
        }
    }
}