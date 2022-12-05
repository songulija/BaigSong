using RealEstateAPI.Data;
using System;
using System.Threading.Tasks;

namespace RealEstateAPI.IRepository
{
    /// <summary>
    /// it will inherit from IDissposable
    /// IUnitOfWork will act as register for every variation of GenericRepository
    /// relative to class T. IDisposable provides mechanism for releasing unmanaged resources
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        //now we have eight tables. some people instead of Countries
        //SO we could just Call IUnitOfWork.Brands, Categories ... operationName that we want to call
        IGenericRepository<UserType> UserTypes { get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<PropertyType> PropertyTypes { get; }
        IGenericRepository<Property> Properties { get; }
        IGenericRepository<Comment> Comments { get; }
        IGenericRepository<FavouriteProperty> FavouriteProperties { get; }
        IGenericRepository<Journal> Journals { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Image> Images { get; }
        IGenericRepository<RentType> RentTypes { get; }
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<City> Cities { get; }
        //then we have one more operation which is Save(). to call save when adding, updating
        //but this is outside repository becouse if there are multiple changes made at time it can be cought in one operation
        Task Save();
    }
}
