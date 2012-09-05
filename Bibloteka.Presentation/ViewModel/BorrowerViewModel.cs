using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.DomainModel.Interfaces;
using Biblioteka.Presentation.Utils;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Utilities;

namespace Biblioteka.Presentation.ViewModel
{
    public class BorrowerViewModel : ObservableViewModel
    {
        private readonly Func<IBookCopysRepository> bookCopyRepository;
        private readonly Func<IBorrowerRepository> borrowerRepository;
        private readonly ICurrent current;
        private readonly Func<IStudentRepository> studentRepository;
        private ObservableCollection<Borrower> _borrowers;

        private RelayCommand cancel;
        private RelayCommand delete;
        private RelayCommand ok;
        private Borrower selected;

        public BorrowerViewModel(
            Func<IBorrowerRepository> _borrowerRepository,
            Func<IStudentRepository> _studentRepository,
            Func<IBookCopysRepository> _bookCopyRepository,
            ICurrent _current, IBusy status)
        {
            Status = status;
            Status.Busy = true;
            borrowerRepository = _borrowerRepository;
            studentRepository = _studentRepository;
            bookCopyRepository = _bookCopyRepository;
            current = _current;


            Task.Factory.StartNew(() =>
                {
                    Borrowers =
                        borrowerRepository().GetAll(
                            c => !c.Return.HasValue && c.BookCopy.LibraryID == current.CurrentUser.LibraryID).
                            ToObservable();
                    Status.Busy = false;
                });
        }

        public ObservableCollection<Borrower> Borrowers
        {
            get { return _borrowers; }
            set
            {
                _borrowers = value;
                RaisePropertyChanged(() => Borrowers);
            }
        }

        public Borrower Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                RaisePropertyChanged(() => Selected);
            }
        }

        public RelayCommand Delete
        {
            get
            {
                if (delete == null)
                {
                    delete = new RelayCommand(() =>
                        {
                            Messenger.Default.Send("hide", "DialogBook");
                            Status.Busy = true;
                            Task.Factory.StartNew(() =>
                                {
                                    IBorrowerRepository br = borrowerRepository();
                                    br.Delete(selected);
                                    if (br.Commit())
                                    {
                                        Borrowers.Remove(selected);
                                        selected = null;
                                    }
                                    Status.Busy = false;
                                });
                        });
                }
                return delete;
            }
        }

        public RelayCommand OK
        {
            get
            {
                if (ok == null)
                {
                    ok = new RelayCommand(() =>
                        {
                            Status.Busy = true;
                            Task.Factory.StartNew(() =>
                                {
                                    IBorrowerRepository br = borrowerRepository();
                                    br.Save(selected);
                                    br.Commit();
                                    Messenger.Default.Send("hide", "DialogBook");
                                    Status.Busy = false;
                                });
                        });
                }
                return ok;
            }
        }

        public RelayCommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(() =>
                        {
                            Status.Busy = true;
                            selected = null;
                            Messenger.Default.Send("hide", "DialogBook");
                            Status.Busy = false;
                        });
                }
                return cancel;
            }
        }
    }
}