using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.DomainModel.Interfaces;
using Biblioteka.Presentation.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Biblioteka.Presentation.ViewModel
{
    public abstract class ObservableViewModel : ViewModelBase
    {

        private ILogger _logger;

        public ILogger Logger
        {
            get { return _logger; }
            set
            {
                _logger = value;
                RaisePropertyChanged(() => Logger);
            }
        }

        private IBusy _status;

        public IBusy Status
        {
            get { return _status; }
            set
            {
                if (_status == value)
                    return;
                var temp = _status;
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }
    }

    public abstract class ObservableGenViewModel<T, R> : ViewModelBase where T : class
        where R : IRepository<T>
    {
        private ObservableCollection<T> _data;
        public ObservableCollection<T> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged(() => Data);
            }
        }
        private RelayCommand cancel;
        private RelayCommand delete;
        private RelayCommand ok;
        private T selected;

        protected R _repo;

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
                            _repo.Delete(selected);
                            if (_repo.Commit())
                            {
                                Data.Remove(selected);
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
                            _repo.Save(selected);
                            _repo.Commit();
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

        private ILogger _logger;

        public ILogger Logger
        {
            get { return _logger; }
            set
            {
                _logger = value;
                RaisePropertyChanged(() => Logger);
            }
        }

        private IBusy _status;

        public IBusy Status
        {
            get { return _status; }
            set
            {
                if (_status == value)
                    return;
                var temp = _status;
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }
    }
}