using System;
using Nancy;
using Zipkin;
using System.Linq;
using System.Collections.Generic;

namespace Zipkin4Nancy
{
    public class ZipkinNancyTracer
    {
		private const string traceIdB3Header = "X-B3-TraceId";
		private const string spanIdB3Header = "X-B3-SpanId";
		private const string parentSpanIdB3Header = "X-B3-ParentSpanId";
		private const string zipkinTraceNancyContextItem = "ZipkinTraceNancyContext";

        public Response ZipkinTraceBeforeRequest(NancyContext context)
        {
			var trace = GetServerTraceFromRequestOrStartClientTrace(context);
			context.Items.Add(zipkinTraceNancyContextItem, trace);
			return null;
        }

        public void ZipkinTraceAfterRequest(NancyContext context)
        {
			var trace = context.Items[zipkinTraceNancyContextItem] as ITrace;
			if (context.Response?.StatusCode != null && trace.Span != null)
				trace.Span.BinaryAnnotations.Add(new BinaryAnnotation("http.status_code", context.Response.StatusCode.ToString()));
			if (trace != null)
				trace.Dispose();
        }

        public void ZipkinTraceOnError(NancyContext context, Exception exception)
        {
			var trace = context.Items[zipkinTraceNancyContextItem] as ITrace;
			trace.AnnotateWith(PredefinedTag.Error, exception.Message);

			if (trace != null)
				trace.Dispose();
        }

		private ITrace GetServerTraceFromRequestOrStartClientTrace(NancyContext context) 
		{
			ITrace trace;
			string traceName = context.Request.Method + " " + context.Request.Url;

			if (context.Request.Headers[traceIdB3Header].Any())
			{
				IDictionary<string, string> zipkinContext = new Dictionary<string, string>();
				IEnumerable<string> headerValues = context.Request.Headers[traceIdB3Header];
				if (headerValues.Any())
					zipkinContext.Add("_zipkin_traceid", headerValues.First());
				
				headerValues = context.Request.Headers[spanIdB3Header];
				if (headerValues.Any())
					zipkinContext.Add("_zipkin_spanid", headerValues.First());

				trace = new StartServerTrace(traceName, zipkinContext);
			}
			else
			{
				trace = new StartClientTrace(traceName);
			}

			trace.TimeAnnotateWith(PredefinedTag.ServerRecv);
			trace.AnnotateWith(PredefinedTag.HttpMethod, context.Request.Method);
			trace.AnnotateWith(PredefinedTag.HttpHost, context.Request.UserHostAddress);
			trace.AnnotateWith(PredefinedTag.HttpPath, context.Request.Path.ToString());

			return trace;
		}
    }
}
