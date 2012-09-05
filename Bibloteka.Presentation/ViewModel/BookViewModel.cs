using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.Presentation.Utils;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Utilities;

namespace Biblioteka.Presentation.ViewModel
{
    public class BookViewModel : ObservableViewModel
    {
        private readonly Func<IBookRepository> bookRepo;
        private ObservableCollection<Book> _books;

        private Book _selected;
        private RelayCommand cancel;
        private RelayCommand delete;
        private RelayCommand ok;

        public BookViewModel(Func<IBookRepository> _bookRepo, IBusy status)
        {
            bookRepo = _bookRepo;
            Status = status;
            Status.Busy = true;
            Messenger.Default.Register<string>(this, "menuaction", d =>
                {
                    if (d == "New book")
                        selected = new Book();
                    Messenger.Default.Send(d, "DialogBook");
                });
            Task.Factory.StartNew(() =>
                {
                    Books = bookRepo().GetAll().ToObservable();
                    Status.Busy = false;
                });
        }

        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set
            {
                _books = value;
                RaisePropertyChanged(() => Books);
            }
        }

        public Book selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChanged(() => selected);
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
                                    IBookRepository br = bookRepo();
                                    br.Delete(selected);
                                    if (br.Commit())
                                    {
                                        Books.Remove(selected);
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
                                    IBookRepository br = bookRepo();
                                    br.Save(selected);
                                    br.Commit();
                                    
                                });
                            Messenger.Default.Send("hide", "DialogBook");
                            Status.Busy = false;
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


        public override void Cleanup()
        {
            Messenger.Default.Unregister<string>(this, "menuaction");
            base.Cleanup();
        }
    }
}