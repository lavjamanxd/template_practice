using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Nancy;
using Nancy.Owin;
using Nancy.Responses;
using Nancy.TinyIoc;
using Nancy.ViewEngines;
using Suave;

namespace WebTemplateEngine
{
    public class Program
    {
        public static void Main()
        {
            var opts = new NancyOptions
            {
                Bootstrapper = new Bootstrap()
            };
            var app = Suave.Owin.OwinAppModule.OfMidFunc("/", NancyMiddleware.UseNancy(opts));
            Web.startWebServer(Web.defaultConfig, app);
        }

        private class Bootstrap : DefaultNancyBootstrapper
        {
            protected override void ConfigureApplicationContainer(TinyIoCContainer container)
            {
                base.ConfigureApplicationContainer(container);
                container.Register<IViewEngine, SuperDuperViewEngine>().AsSingleton();
            }

            protected override IEnumerable<Type> ViewEngines
            {
                get { return new[] { typeof(SuperDuperViewEngine) }; }
            }
        }

        private class SuperDuperViewEngine : IViewEngine
        {
            private SuperDuperRenderer _renderer;

            public IEnumerable<string> Extensions => new string[] { "html" };

            public void Initialize(ViewEngineStartupContext viewEngineStartupContext)
            {
                _renderer = new SuperDuperRenderer();
            }

            public Nancy.Response RenderView(ViewLocationResult viewLocationResult, dynamic model, IRenderContext renderContext)
            {
                var renderedPage = _renderer.Process(viewLocationResult.Contents.Invoke().ReadToEnd());

                return new HtmlResponse(HttpStatusCode.OK, (Stream outputStream) =>
                                        new MemoryStream(Encoding.UTF8.GetBytes(renderedPage)).CopyTo(outputStream));
            }
        }
    }
}