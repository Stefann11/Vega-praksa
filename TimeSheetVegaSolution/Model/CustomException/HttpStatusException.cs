using System;
using System.Net;

namespace Model.CustomException
{
    public class HttpStatusException : Exception
    {
        public HttpStatusException(HttpStatusCode status, string message) : base(message)
        {
            Status = status;
        }
        public HttpStatusCode Status { get; private set; }    
    }
}
