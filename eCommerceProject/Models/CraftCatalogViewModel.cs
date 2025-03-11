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
        public IEnumerable<Craft> Crafts { get; private set; }
        public int LastPage { get; private set; }

        public int CurrentPage { get; private set; }
    }
}
