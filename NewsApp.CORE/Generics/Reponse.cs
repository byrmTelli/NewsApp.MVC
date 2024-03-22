using NewsApp.CORE.ViewModels.CustomViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NewsApp.CORE.Generics
{
    public class Response<T> where T:class 
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public ErrorViewModel Errors { get; set; }
        [JsonIgnore]
        public bool IsSuccesfull { get; set; }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> {Data = default,StatusCode = statusCode,IsSuccesfull =true};
        }

        public static Response<T> Success(T Data,int statusCode)
        {
            return new Response<T> {Data = Data,StatusCode = statusCode, IsSuccesfull =true};
        }

        public static Response<T> Fail(ErrorViewModel error,int statusCode)
        {
            return new Response<T> { Errors = error, StatusCode = statusCode, IsSuccesfull = false };
        }

        public static Response<T> Fail(string errorMessage,int statusCode,bool isShow)
        {
            var errorViewModel =  new ErrorViewModel(errorMessage,isShow);
            return new Response<T> {Errors = errorViewModel,StatusCode = statusCode,IsSuccesfull=false };
        }
    }
}
