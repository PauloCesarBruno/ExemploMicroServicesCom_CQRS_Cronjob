using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class FakePartNumberQuantityRepository : IPartNumberQuantityRepository
    {
        private readonly IMapper _mapper;

        public FakePartNumberQuantityRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartNumberQuantity>> GetAsync()
        {
            var path = @$"{AppDomain.CurrentDomain.BaseDirectory}Resources/FakeInventoryTraxDB.json";
            var file = await File.ReadAllTextAsync(path);
            var entities = JsonConvert.DeserializeObject<List<PartNumberQuantityEntity>>(file);
            var result = _mapper.Map<List<PartNumberQuantity>>(entities);

            return result.Take(12);
        }

        public async Task<IEnumerable<PartNumberQuantity>> GetAsync(DateTime startDate)
        {
            var path = @$"{AppDomain.CurrentDomain.BaseDirectory}Resources/FakeInventoryTraxDB.json";
            var file = await File.ReadAllTextAsync(path);
            var entities = JsonConvert.DeserializeObject<List<PartNumberQuantityEntity>>(file);
            // Para nivel de teste mudar (x => x.MODIFIED_DATE <= startDate) para Menor...
            entities = entities.Where(x => x.MODIFIED_DATE <= startDate).ToList();
            var result = _mapper.Map<List<PartNumberQuantity>>(entities);

            return result.Take(12);
        }
    }
}
