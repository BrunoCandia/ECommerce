namespace Catalog.Core.Specifications
{
    public class CatalogSpecParams
    {
        private const int MaxPageSize = 70;
        private int _pageSize = 10;

        public int PageIndex { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? BrandId { get; set; }

        public string? TypeId { get; set; }

        public string? Sort { get; set; }

        public string? Search { get; set; }

        #region Filtering
        private List<string> _brands = new List<string>();

        public List<string> Brands
        {
            get => _brands; // query string => /api/products?brands=Angular,React&types=Boots,Gloves
            set
            {
                _brands = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))
                               .ToList();
            }
        }

        private List<string> _types = new List<string>();

        public List<string> Types
        {
            get => _types; // query string => /api/products?brands=Angular,React&types=Boots,Gloves
            set
            {
                _types = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))
                               .ToList();
            }
        }
        #endregion
    }
}
