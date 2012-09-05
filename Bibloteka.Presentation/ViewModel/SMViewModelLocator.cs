using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.ServiceLocatorAdapter;

namespace Biblioteka.Presentation.ViewModel
{
    public class SMViewModelLocator
    {
        static SMViewModelLocator()
        {
            var ioc = CreateServiceLocator();
            ServiceLocator.SetLocatorProvider(() => ioc);
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public UserViewModel User
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserViewModel>();
            }
        }

        public BookViewModel Book
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BookViewModel>();
            }
        }

        public AuthorViewModel Author
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AuthorViewModel>();
            }
        }

        public LibraryViewModel Library
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LibraryViewModel>();
            }
        }

        public BorrowerViewModel Borrower
        {
            get { return ServiceLocator.Current.GetInstance<BorrowerViewModel>(); }
        }

        public static void Cleanup()
        {
            ObjectFactory.ResetDefaults();
        }

        private static IServiceLocator CreateServiceLocator()
        {
            return new StructureMapServiceLocator(ObjectFactory.Container);
        }
    }
}