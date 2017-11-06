# Zipkin.net Instrumentations

[Zipkin](http://zipkin.io) is a distributed logging framework, that allows to analyze the behavior of complex applications, specially those based in microservices, with access to different databases. This project provides different Zipkin instrumentations for common use .NET frameworks based on the [Zipkin.net library](https://github.com/d-collab/zipkin.net). All the instrumentations allow automatic logging to Zipkin of different parts of your application activity, according to your framework of choice:

* Owin: automatic logging of every incoming HTTP request to an Owin web interface.
* Ado: automatic logging of every database query.
* RestSharp: automatic logging of every outgoing HTTP request via the RestSharp library.


## Logging Owin HTTP Requests Into Zipkin

In order to automatically log all the HTTP requests received by your Owin-based web application you have to reference the Zipkin4Owin Nuget package, [![Zipkin4Owin](https://img.shields.io/nuget/v/Zipkin4Owin?style=flat)](https://www.nuget.org/packages/Zipkin4Owin), in your project and setup the  Zipkin4Owin Owin middleware during the Owin start-up via the `UseZipkinTracer` extension method:
```cs
using Zipkin4Owin;

...

public void Configuration(IAppBuilder app)
{
    if (zipkinEnabled)
    {
        app.UseZipkinTracer(new Zipkin.ZipkinBootstrapper("my-service")
            .ZipkinAt("localhost") // where's the zipkin server?
            .WithSampleRate(1.0)); // 0.1 means trace 10% of calls
    }

    app.Use...
}
```

As you can notice in the code, the Zipkin4Owin Owin middleware accepts a `ZipkinBootstrapper`, which carries the configuration for the Zipkin logger to start. Zipkin4Owin starts this bootstrapper for you at start up, so you don't need to do anything else in order to start logging messages.

### B3 Headers Propagation

In order to co-relate requests between different services in the processing of a single requests to your system, Zipkin has defined a series of standard HTTP headers. Zipkin4Owin automatically detects trace information in those HTTP request headers so that:

* If the appropriate headers are provided, the logged HTTP request will be logged as a child of the specified span, part of the same trace.
* If no headers are provided, the logged HTTP request will be a new span and trace.

The following are the expected standard Zipkin B3 HTTP headers:

* `X-B3-TraceId`
* `X-B3-SpanId`
* `X-B3-ParentSpanId`

If you are not familiar with the concept of a trace, span and parent span, I recommend you to have a look at http://zipkin.io.

## Logging Database Queries Into Zipkin

In order to automatically log all your database queries into Zipkin you have to reference the Zipkin4Ado Nuget package, [![Zipkin4Ado](https://img.shields.io/nuget/v/Zipkin4Ado?style=flat)](https://www.nuget.org/packages/Zipkin4Ado), in your project and wrap your database connection (`DbConnection`) into a `ZipkinDbConnection`.

```cs
DbConnection connection = new SqlConnection(_connectionString);
if (zipkinEnabled)
	connection = new Zipkin4Ado.ZipkinDbConnection(connection);
```

Having the database connection wrapped into a `ZipkinDbConnection` it is possible to create and execute database commands, that are automatically logged into Zipkin:
```cs
var dbCommand = connection.CreateCommand();
dbCommand.CommandText = "SELECT 1";
dbCommand.Execute();
```

Be careful to create the database commands via the wrapped `ZipkinDbConnection` because otherwise they will not be logged into Zipkin:
```cs
var dbCommand = new SqlCommand("SELECT 1")
dbCommand.Connection = connection;
dbCommand.ExecuteReader(); // THIS WILL NOT BE LOGGED
```

In order to start up the Zipkin logging you need to start and configure the Zipkin bootstrapper somewhere at the start of your application, as documented in the [Zipkin.net library](https://github.com/d-collab/zipkin.net):
```cs
new Zipkin.ZipkinBootstrapper("my-service")
			.ZipkinAt("localhost") // where's the zipkin server?
			.WithSampleRate(0.1)   // 0.1 means trace 10% of calls
			.Start();
``` 

If your application is an Owin web application and you are using the `Zipkin4Owin` library, you don't need to manually start the `ZipkinBootstrapper`, because `Zipkin4Owin` already does it for you.

The idea of Zipkin4Ado is based on the amazing project [Glimpse](http://getglimpse.com/), which also has a database connection wrapper for automatically logging database activity.

## Logging RestSharp HTTP Requests Into Zipkin

The Zipkin instrumentation for [RestSharp](http://restsharp.org/) serves 2 purposes:

* Automatically logging every request made via RestSharp into Zipkin. Currently logging is only available in synchronous operations: `Execute`, `ExecuteAsGet`, etc.
* Propagating the Zipkin traces information to the callee via standard Zipkin B3 HTTP headers.

In order to start logging and propagating Zipkin trace information in your RestSharp calls you have to reference the Zipkin4RestSharp Nuget package, [![Zipkin4RestSharp](https://img.shields.io/nuget/v/Zipkin4RestSharp?style=flat)](https://www.nuget.org/packages/Zipkin4RestSharp), in your project and wrap you `IRestClient` into a `ZipkinRestClient` instance:
```cs
using Zipkin4RestSharp;

...
    var restClient = new RestClient("http://example.com");
    if (zipkinEnabled)
    {
        restClient = new ZipkinRestClient(restClient);
    }

    var request = new RestRequest("resource/id", Method.GET);
    IRestResponse response = client.Execute(request);

```

As in the case of `Zipkin4Owin`, `Zipkin4RestSharp` uses the standard Zipkin B3 HTTP headers for propagating the Zipkin traces information to the callee, in order to co-relate logs from different services taking part in the processing of the same request. This way, every HTTP request performed by via `ZipkinRestClient` will be injected the `X-B3-TraceId`, `X-B3-SpanId` and `X-B3-ParentSpanId` headers containing information of the current Zipkin trace.