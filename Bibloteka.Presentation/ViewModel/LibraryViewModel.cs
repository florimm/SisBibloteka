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
    public class LibraryViewModel : ObservableViewModel
    {
        private readonly Func<ILibraryRepository> _libraryRepo;
        private ObservableCollection<Library> _libraries;

        private RelayCommand cancel;
        private RelayCommand delete;
        private RelayCommand ok;
        private Library selected;

        public LibraryViewModel(Func<ILibraryRepository> libraryRepo, IBusy status)
        {
            _libraryRepo = libraryRepo;
            Status = status;
            Status.Busy = true;
            Task.Factory.StartNew(() =>
                {
                    Libraries = libraryRepo().GetAll().ToObservable();
                    Status.Busy = false;
                });
        }

        public ObservableCollection<Library> Libraries
        {
            get { return _libraries; }
            set
            {
                _libraries = value;
                RaisePropertyChanged(() => Libraries);
            }
        }

        public Library Selected
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
                                    ILibraryRepository br = _libraryRepo();
                                    br.Delete(selected);
                                    if (br.Commit())
                                    {
                                        Libraries.Remove(selected);
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
                                    ILibraryRepository br = _libraryRepo();
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

        public override void Cleanup()
        {
            Messenger.Default.Unregister<string>(this, "menuaction");
            base.Cleanup();
        }
    }
}