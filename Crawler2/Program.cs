using System;
using System.Windows.Forms;
using Crawler2.BLL.Contracts;
using Crawler2.BLL.Services;
using Unity;
using Unity.Injection;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Registration;
using Crawler2.Interceptors;
using  Unity.Extension;

namespace Crawler2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = CofigureContainer();
            Application.Run(new CrawlerForm(container));
        }

        static UnityContainer CofigureContainer()
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<IHttpClientWrapper, HttpClientWrapper>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptionBehavior>()
            );
            container.RegisterType<IValidator, Validator>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptionBehavior>()
            );
            container.RegisterType<IPageParser, PageParser>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptionBehavior>()
            );

            container.RegisterType<ICrawler>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptionBehavior>(),
                new InjectionFactory(c => new Crawler(
                    c.Resolve<IHttpClientWrapper>(),
                    c.Resolve<IValidator>(),
                    c.Resolve<IPageParser>()
            )));

            return container;
        }
    }
}
