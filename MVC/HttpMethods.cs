using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public static class HttpMethods
    {
        public const string Delete = "DELETE";
        public const string Get = "GET";
        public const string Head = "HEAD";
        public const string Options = "OPTIONS";
        public const string Post = "POST";
        public const string Put = "PUT";
        public const string Trace = "TRACE";
        public static string fromHttpMethodsEnum(HttpMethodsEnum e)
        {
            switch (e)
            {
                case HttpMethodsEnum.Delete: return Delete;
                case HttpMethodsEnum.Get: return Get;
                case HttpMethodsEnum.Head: return Head;
                case HttpMethodsEnum.Options: return Options;
                case HttpMethodsEnum.Post: return Post;
                case HttpMethodsEnum.Put: return Put;
                case HttpMethodsEnum.Trace: return Trace;
                default: throw new NotSupportedException();
            }
        }

        public static HttpMethodsEnum FromString(string sm)
        {
            switch (sm)
            {
                case Delete: return HttpMethodsEnum.Delete;
                case Get: return HttpMethodsEnum.Get;
                case Head: return HttpMethodsEnum.Head;
                case Options: return HttpMethodsEnum.Options;
                case Post: return HttpMethodsEnum.Post;
                case Put: return HttpMethodsEnum.Put;
                case Trace: return HttpMethodsEnum.Trace;
                default: throw new NotSupportedException();
            }
        }
    }

    public enum HttpMethodsEnum
    {
        Delete,
        Get,
        Head,
        Options,
        Post,
        Put,
        Trace
    }
}
