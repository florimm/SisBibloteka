using System.Windows;
using System.Windows.Interactivity;
using Biblioteka.DataAccess;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.DomainModel.DomainDefaultImplementation;
using Biblioteka.DomainModel.Interfaces;
using Biblioteka.Presentation.Messages;
using GalaSoft.MvvmLight.Messaging;
using StructureMap;

namespace Biblioteka.Presentation.Utils
{
    public static class Bootstrapper
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
                {
                    x.Scan(scan =>
                        {
                            scan.TheCallingAssembly();
                            scan.WithDefaultConventions();
                        });
                    x.For<IEFContext>().Singleton().Use<EFContext>().Ctor<string>("connectionString").Is("Name=con");
                    x.FillAllPropertiesOfType<IEFContext>();
                    x.For<ILogger>().Use<OutputLogger>();
                    x.FillAllPropertiesOfType<ILogger>();

                    x.For<IUserRepository>().Use<UserRepository>();
                    x.For<IBookRepository>().Use<BookRepository>();
                    x.For<IAuthorRepository>().Use<AuthorRepository>();
                    x.For<IBorrowerRepository>().Use<BorrowerRepository>();
                    x.For<IBookCopysRepository>().Use<BookCopysRepository>();
                    x.For<IStudentRepository>().Use<StudentRepository>();
                    x.For<ICurrent>().Singleton().Use<Current>();
                    x.For<IBusy>().Singleton().Use<BusyStatus>();
                    x.FillAllPropertiesOfType<IBusy>();

                });
            return ObjectFactory.Container;
        }

        public static IContainer InitializeForBlend()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
                x.For<IEFContext>().HybridHttpOrThreadLocalScoped().Use<EFContext>().Ctor<string>("connectionString")
                    .Is("Name=con");
                x.FillAllPropertiesOfType<IEFContext>();
                x.For<ILogger>().Use<OutputLogger>();
                x.FillAllPropertiesOfType<ILogger>();

                x.For<IUserRepository>().Use<UserRepository>();
                x.For<IRepository<Book>>().Use<Repository<Book>>();
                x.For<IRepository<Author>>().Use<Repository<Author>>();
            });
            return ObjectFactory.Container;
        }
    }

    public interface IDialogService
    {
        bool? ShowDialog();
    }

    public class GenericDialogMessageAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            var message = parameter as DialogMessage;
            if (message != null)
            {
                var view = ObjectFactory.GetNamedInstance<IDialogService>(message.Target.ToString());
                MessageBoxResult result = view.ShowDialog().GetValueOrDefault(false) ? MessageBoxResult.OK : MessageBoxResult.Cancel;
                message.Callback(result);
            }
        }
    }

    public class DialogMessageAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            var message = parameter as WM;
            if (message != null)
            {
                //Kataklizma view = new Kataklizma();
                //var sh = view.ShowDialog().GetValueOrDefault(false) ? MessageBoxResult.OK : MessageBoxResult.Cancel;
                //message.Callback(sh);
                //MessageBoxResult result = MessageBox.Show(message.Content, message.Caption, message.Button);
                //message.Callback(result);
            }
        }

    }

    public class DialogMessageTrigger : TriggerBase<DependencyObject>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            Messenger.Default.Register<DialogMessage>(base.AssociatedObject, p => this.InvokeActions(p));
            Messenger.Default.Register<WM>(base.AssociatedObject, p=> this.InvokeActions(p));
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            //Messenger.Default.Unregister<DialogMessage>(base.AssociatedObject);
            Messenger.Default.Unregister<WM>(base.AssociatedObject);
        }
    }
}