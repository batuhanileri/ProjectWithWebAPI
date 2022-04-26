using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Responses
{
    public class ApiSuccessResponse:ApiResponse
    {
        public ApiSuccessResponse():base(success:true)
        {

        }
        public ApiSuccessResponse(string message):base(success:true,message:message)
        {

        }
    }
}
