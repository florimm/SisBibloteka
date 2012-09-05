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
    public class StudentViewModel : ObservableViewModel
    {
        private readonly Func<IStudentRepository> _studentRepo;
        private ObservableCollection<Student> _students;

        private RelayCommand cancel;
        private RelayCommand delete;
        private RelayCommand ok;
        private Student selected;

        public StudentViewModel(Func<IStudentRepository> studentRepo, IBusy status)
        {
            _studentRepo = studentRepo;
            Status = status;
            Status.Busy = true;
            Task.Factory.StartNew(() =>
                {
                    Students = studentRepo().GetAll().ToObservable();
                    Status.Busy = false;
                });
        }

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                RaisePropertyChanged(() => Students);
            }
        }

        public Student Selected
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
                                    IStudentRepository br = _studentRepo();
                                    br.Delete(selected);
                                    if (br.Commit())
                                    {
                                        Students.Remove(selected);
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
                                    IStudentRepository br = _studentRepo();
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