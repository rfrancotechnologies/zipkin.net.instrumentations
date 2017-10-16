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

        public IAuthenticator Authenticator
        {
            get
            {
                return _innerClient.Authenticator;
            }

            set
            {
                _innerClient.Authenticator = value;
            }
        }

        public Uri BaseUrl
        {
            get
            {
                return _innerClient.BaseUrl;
            }

            set
            {
                _innerClient.BaseUrl = value;
            }
        }

        public RequestCachePolicy CachePolicy
        {
            get
            {
                return _innerClient.CachePolicy;
            }

            set
            {
                _innerClient.CachePolicy = value;
            }
        }

        public X509CertificateCollection ClientCertificates
        {
            get
            {
                return _innerClient.ClientCertificates;
            }

            set
            {
                _innerClient.ClientCertificates = value;
            }
        }

        public CookieContainer CookieContainer
        {
            get
            {
                return _innerClient.CookieContainer;
            }

            set
            {
                _innerClient.CookieContainer = value;
            }
        }

        public IList<Parameter> DefaultParameters
        {
            get
            {
                return _innerClient.DefaultParameters;
            }
        }

        public Encoding Encoding
        {
            get
            {
                return _innerClient.Encoding;
            }

            set
            {
                _innerClient.Encoding = value;
            }
        }

        public bool FollowRedirects
        {
            get
            {
                return _innerClient.FollowRedirects;
            }

            set
            {
                _innerClient.FollowRedirects = value;
            }
        }

        public int? MaxRedirects
        {
            get
            {
                return _innerClient.MaxRedirects;
            }

            set
            {
                _innerClient.MaxRedirects = value;
            }
        }

        public bool PreAuthenticate
        {
            get
            {
                return _innerClient.PreAuthenticate;
            }

            set
            {
                _innerClient.PreAuthenticate = value;
            }
        }

        public IWebProxy Proxy
        {
            get
            {
                return _innerClient.Proxy;
            }

            set
            {
                _innerClient.Proxy = value;
            }
        }

        public int ReadWriteTimeout
        {
            get
            {
                return _innerClient.ReadWriteTimeout;
            }

            set
            {
                _innerClient.ReadWriteTimeout = value;
            }
        }

        public int Timeout
        {
            get
            {
                return _innerClient.Timeout;
            }

            set
            {
                _innerClient.Timeout = value;
            }
        }

        public string UserAgent
        {
            get
            {
                return _innerClient.UserAgent;
            }

            set
            {
                _innerClient.UserAgent = value;
            }
        }

        public bool UseSynchronizationContext
        {
            get
            {
                return _innerClient.UseSynchronizationContext;
            }

            set
            {
                _innerClient.UseSynchronizationContext = value;
            }
        }

        public void AddHandler(string contentType, IDeserializer deserializer)
        {
            _innerClient.AddHandler(contentType, deserializer);
        }

        public Uri BuildUri(IRestRequest request)
        {
            return _innerClient.BuildUri(request);
        }

        public void ClearHandlers()
        {
            _innerClient.ClearHandlers();
        }

        public byte[] DownloadData(IRestRequest request)
        {
            return _innerClient.DownloadData(request);
        }

        public virtual IRestResponse Execute(IRestRequest request)
        {
            return _innerClient.Execute(request);
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
