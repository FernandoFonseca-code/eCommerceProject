using eCommerceProject.Models;

namespace Models
{
    public class CraftCatalogViewModel
    {
        public CraftCatalogViewModel(IEnumerable<Craft> crafts, int lastPage, int currPage)
        {
            Crafts = crafts;
            LastPage = lastPage;
            CurrentPage = currPage;
        }
        public IEnumerable<Craft> Crafts { get; set; }
        public int LastPage { get; set; }

        public int CurrentPage { get; set; }
    }
}
