using HotelListing.Models;
using System;
using System.Threading.Tasks;

namespace HotelListing.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get; }
        Task Save();
    }
}
