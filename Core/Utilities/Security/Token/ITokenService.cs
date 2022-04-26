using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Token
{
    public interface ITokenService
    {
        AccessToken CreateToken(int userId, string userName);
    }
}
