namespace MailMirror.Net.Api
{
    using System.Web.Http;

    using MailMirror.Net.Api.Data;

    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;

    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            RegisterNinject(app);
            RegisterWebApi(app);
            RegisterStaticHosting(app);
        }

        private IKernel RegisterNinject(IAppBuilder app)
        {
            var kernel = new StandardKernel();

            kernel.Bind<IMessagesDb>().To<MessagesDb>().InSingletonScope();

            app.UseNinjectMiddleware(() => kernel);
            return kernel;
        }

        private void RegisterWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            app.UseNinjectWebApi(config);
        }

        private void RegisterStaticHosting(IAppBuilder app)
        {
            var physicalFileSystem = new PhysicalFileSystem("./wwwroot");
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem,
                EnableDirectoryBrowsing = true
            };
            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.DefaultFilesOptions.DefaultFileNames = new[] {"index.html"};

            app.UseFileServer(options);
        }
    }
}