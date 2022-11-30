namespace RealEstateAPI.ModelsDTO
{
    /// <summary>
    /// parameter names users allowed to pass in.
    /// localhost:444/api/countries/?pagesize=10&pagenumber=5
    /// we have constant int maxPageSize to 50, that means how many items MAXIMUM can be on one page
    /// then page number that we can set. but by default its 1. THEN _pageSize 
    /// _pageSize of 10 will be by default. public version of PageSize is to get and set pageSize
    /// user can request _pageSize more than 10 but smaller than 50. if bigger set it tp fifty
    /// </summary>
    public class RequestParams
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; }
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
