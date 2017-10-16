using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zipkin;

namespace Zipkin4Owin
{
    public static class OwinExtensions
    {
        public static IAppBuilder UseZipkinTracer(this IAppBuilder appBuilder, ZipkinBootstrapper bootstrapper)
        {
            bootstrapper.Start();
            appBuilder.Use<ZipkinOwinMiddleware>();
            return appBuilder;
        }
    }
}
