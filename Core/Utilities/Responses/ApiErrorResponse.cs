using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Responses
{
    public class ApiErrorResponse:ApiResponse
    {
        public ApiErrorResponse():base(success:false)
        {

        }
        public ApiErrorResponse(string message):base(success:false,message:message)
        {

        }
    }
}
