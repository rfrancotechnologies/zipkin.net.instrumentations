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
		private const string traceIdB3Header = "X-B3-TraceId";
		private const string spanIdB3Header = "X-B3-SpanId";
		private const string parentSpanIdB3Header = "X-B3-ParentSpanId";

        public ZipkinOwinMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
			using (GetServerTraceFromRequestOrStartClientTrace(context))
            {
                await Next.Invoke(context);
            }
        }

		private ITrace GetServerTraceFromRequestOrStartClientTrace(IOwinContext context) 
		{
			ITrace trace;
			string traceName = context.Request.Method + " " + context.Request.Uri;

			if (context.Request.Headers.ContainsKey(traceIdB3Header))
			{
				IDictionary<string, string> zipkinContext = new Dictionary<string, string>();
				string[] headerValues;
				if (context.Request.Headers.TryGetValue(traceIdB3Header, out headerValues))
					zipkinContext.Add("_zipkin_traceid", headerValues.First());
				if (context.Request.Headers.TryGetValue(spanIdB3Header, out headerValues))
					zipkinContext.Add("_zipkin_spanid", headerValues.First());

				trace = new StartServerTrace(traceName, zipkinContext);
			}
			else
			{
				trace = new StartClientTrace(traceName);
			}

			trace.TimeAnnotateWith(PredefinedTag.ServerRecv);
			trace.AnnotateWith(PredefinedTag.HttpMethod, context.Request.Method);
			trace.AnnotateWith(PredefinedTag.HttpHost, context.Request.Host.ToString());
			trace.AnnotateWith(PredefinedTag.HttpPath, context.Request.Path.ToString());

			return trace;
		}
    }
}
