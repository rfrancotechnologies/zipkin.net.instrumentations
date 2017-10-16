using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zipkin;

namespace Zipkin4Owin
{
    public class ZipkinOwinMiddleware : OwinMiddleware
    {
        public ZipkinOwinMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            using (var serverTrace = new StartClientTrace(context.Request.Method + " " + context.Request.Uri))
            {
                await Next.Invoke(context);
            }
        }
    }
}
