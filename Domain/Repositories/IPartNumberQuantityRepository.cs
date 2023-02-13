using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPartNumberQuantityRepository
    {
        Task<IEnumerable<PartNumberQuantity>> GetAsync();
        Task<IEnumerable<PartNumberQuantity>> GetAsync(DateTime startDate);
    }
}
