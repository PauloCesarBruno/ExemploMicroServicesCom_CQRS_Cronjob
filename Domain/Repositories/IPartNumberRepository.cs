using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPartNumberRepository
    {
        Task<IEnumerable<PartNumber>> GetAsync();
        Task<IEnumerable<PartNumber>> GetAsync(DateTime startDate);
    }
}
