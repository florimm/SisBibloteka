using Biblioteka.DomainModel;

namespace Biblioteka.WebPresentation.ViewModels
{
    public class ManagedBookViewModel
    {
        public Library Library { get; set; }

        public Book Book { get; set; }

        public int NroOfCopy { get; set; }
    }
}