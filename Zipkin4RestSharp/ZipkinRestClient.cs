using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using System.Net;
using System.Net.Cache;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Zipkin4RestSharp
{
    public class ZipkinRestClient: IRestClient
    {
        private IRestClient _innerClient;

        public ZipkinRestClient(IRestClient innerClient): base()
        {
            _innerClient = innerClient;
        }

        public ZipkinRestClient(Uri baseUrl) : base(baseUrl)
        {
        }

        public ZipkinRestClient(string baseUrl) : base(baseUrl)
        {
        }

        public IAuthenticator Authenticator
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Uri BaseUrl
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public RequestCachePolicy CachePolicy
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public X509CertificateCollection ClientCertificates
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public CookieContainer CookieContainer
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<Parameter> DefaultParameters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Encoding Encoding
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool FollowRedirects
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int? MaxRedirects
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool PreAuthenticate
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IWebProxy Proxy
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int ReadWriteTimeout
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Timeout
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string UserAgent
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool UseSynchronizationContext
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void AddHandler(string contentType, IDeserializer deserializer)
        {
            throw new NotImplementedException();
        }

        public Uri BuildUri(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public void ClearHandlers()
        {
            throw new NotImplementedException();
        }

        public byte[] DownloadData(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public override IRestResponse Execute(IRestRequest request)
        {
            return base.Execute(request);
        }

        public IRestResponse<T> Execute<T>(IRestRequest request) where T : new()
        {
            throw new NotImplementedException();
        }

        public IRestResponse ExecuteAsGet(IRestRequest request, string httpMethod)
        {

        }

        public IRestResponse<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod) where T : new()
        {
            throw new NotImplementedException();
        }

        public IRestResponse ExecuteAsPost(IRestRequest request, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public IRestResponse<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod) where T : new()
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsync(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsync<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback)
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsyncGet(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsyncGet<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsyncPost(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsyncPost<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public void RemoveHandler(string contentType)
        {
            throw new NotImplementedException();
        }
    }
}
