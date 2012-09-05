using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.Presentation.Utils;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Utilities;

namespace Biblioteka.Presentation.ViewModel
{
    public class AuthorViewModel : ObservableViewModel
    {
        private readonly Func<IAuthorRepository> _authorRepo;
        private ObservableCollection<Author> _authors;

        private Author _selected;
        private RelayCommand cancel;
        private RelayCommand delete;
        private RelayCommand ok;

        public AuthorViewModel(Func<IAuthorRepository> authorRepo, IBusy status)
        {
            _authorRepo = authorRepo;
            Status = status;
            Status.Busy = true;
            Messenger.Default.Register<string>(this, "menuaction", d => MessageBox.Show("Ariti"));

            Task.Factory.StartNew(() =>
                {
                    Authors = authorRepo().GetAll().ToObservable();
                    Status.Busy = false;
                });
        }

        public ObservableCollection<Author> Authors
        {
            get { return _authors; }
            set
            {
                _authors = value;
                RaisePropertyChanged(() => Authors);
            }
        }

        public Author Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
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
                                    IAuthorRepository br = _authorRepo();
                                    br.Delete(Selected);
                                    if (br.Commit())
                                    {
                                        Authors.Remove(Selected);
                                        Selected = null;
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
                                    IAuthorRepository br = _authorRepo();
                                    br.Save(Selected);
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
                            Selected = null;
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