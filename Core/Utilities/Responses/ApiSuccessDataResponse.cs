using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Responses
{
    public class ApiSuccessDataResponse<T>:ApiDataResponse<T>
    {
        public ApiSuccessDataResponse(T data) : base(success: true)
        {
            Data = data;
        }
        public ApiSuccessDataResponse(T data, string message) : base(success: true, message: message)
        {
            Data = data;
        }
    }
}
