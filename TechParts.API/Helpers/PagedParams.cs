

namespace TechParts.API.Helpers
{
    public class PagedParams
    {
        public int ItemPerPage { get; set; } = 2;

        public int NbOfPages { get; set; }  
        
        public int CurrentPage { get; set; } = 1;

        public int NbOfItems { get; set; }

        public void Calculate(int nbOfItems)
        {
            this.NbOfItems = nbOfItems;

            this.NbOfPages = this.NbOfItems / this.ItemPerPage;
        }
    }
}