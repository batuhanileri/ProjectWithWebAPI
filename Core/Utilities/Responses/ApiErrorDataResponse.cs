using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Responses
{
    public class ApiErrorDataResponse<T>:ApiDataResponse<T>
    {
        public ApiErrorDataResponse(T data):base(success:false)
        {
            Data = data;
        }
        public ApiErrorDataResponse(T data,string message):base(success:false,message:message)
        {
            Data = data;

        }
    }
}
