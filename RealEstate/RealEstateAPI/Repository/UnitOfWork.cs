using RealEstateAPI.Data;
using RealEstateAPI.IRepository;
using System;
using System.Threading.Tasks;

namespace RealEstateAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //define DatabaseContext as _context. then IGenericRepository of class type Brand, Category and ....
        private readonly DatabaseContext _context;
        private IGenericRepository<PropertyType> _propertyTypes;
        private IGenericRepository<Property> _properties;
        private IGenericRepository<Comment> _comments;
        private IGenericRepository<FavouriteProperty> _favouriteProperties;
        private IGenericRepository<Journal> _journals;
        private IGenericRepository<Payment> _payments;
        private IGenericRepository<UserType> _userTypes;
        private IGenericRepository<User> _users;
        private IGenericRepository<Image> _images;
        private IGenericRepository<RentType> _rentTypes;
        private IGenericRepository<Country> _countries;
        private IGenericRepository<City> _cities;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        //set Brands to =. if _brands are null then return object of GenericRepository of type Brand
        //basically telling what it should return when someone calls UnitOfWork.Brands
        //return new GenericRepository<Brand>(_context) providing context which represents database
        public IGenericRepository<PropertyType> PropertyTypes => _propertyTypes ??= new GenericRepository<PropertyType>(_context);
        public IGenericRepository<Property> Properties => _properties ??= new GenericRepository<Property>(_context);
        public IGenericRepository<Comment> Comments => _comments ??= new GenericRepository<Comment>(_context);
        public IGenericRepository<FavouriteProperty> FavouriteProperties => _favouriteProperties ??= new GenericRepository<FavouriteProperty>(_context);
        public IGenericRepository<Journal> Journals => _journals ??= new GenericRepository<Journal>(_context);
        public IGenericRepository<Payment> Payments => _payments ??= new GenericRepository<Payment>(_context);
        public IGenericRepository<UserType> UserTypes => _userTypes ??= new GenericRepository<UserType>(_context);
        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);
        public IGenericRepository<Image> Images => _images ??= new GenericRepository<Image>(_context);
        public IGenericRepository<RentType> RentTypes => _rentTypes ??= new GenericRepository<RentType>(_context);
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_context);
        public IGenericRepository<City> Cities => _cities ??= new GenericRepository<City>(_context);

        public void Dispose()
        {
            //dispose is like garbage collector. when operations are done please Free up memory
            //kill any memory that connection to database was using, all recourses it was using
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Implementing save operation of IUnitOfWork
        /// </summary>
        /// <returns></returns>
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
