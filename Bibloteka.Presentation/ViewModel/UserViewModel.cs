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
    public class UserViewModel : ObservableViewModel
    {
        private readonly Func<IUserRepository> _userRepo;
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                RaisePropertyChanged(()=> Users);
            }
        }


        private RelayCommand cancel;
        private RelayCommand delete;
        private RelayCommand ok;
        private User selected;

        public UserViewModel(Func<IUserRepository> userRepo, IBusy status)
        {
            _userRepo = userRepo;
            Status = status;
            Status.Busy = true;
            Messenger.Default.Register<string>(this, "menuaction", d => MessageBox.Show("Ariti"));
            Task.Factory.StartNew(() =>
                {
                    Users = userRepo().GetAll().ToObservable();
                    Status.Busy = false;
                    
                });
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
                            IUserRepository br = _userRepo();
                            br.Delete(selected);
                            if (br.Commit())
                            {
                                Users.Remove(selected);
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
                            IUserRepository br = _userRepo();
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

    public class TestViewMode : ObservableGenViewModel<User, IUserRepository>
    {
        public TestViewMode(IUserRepository repository)
        {
            _repo = repository;
        }
    }
}