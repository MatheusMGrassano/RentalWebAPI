using System.Net;

namespace Rental.DAL
{
    public class Response<T>
    {
        public T? Data { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public string Message { get; private set; }

        public static Response<T> Success(T data)
        {
            return new Response<T> 
            { 
                Data = data,
                StatusCode = HttpStatusCode.OK,
                Message = "Success"
            };
        }

        public static Response<T> Error(string errorMessage)
        {
            return new Response<T>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = errorMessage
            };
        }

        public static Response<T> NotFound()
        {
            return new Response<T>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Couldn't find the request object in the database."
            };
        }
    }
}
