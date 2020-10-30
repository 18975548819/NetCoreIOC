using DataModel.Authorize;
using IBusinessManage.Base;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessManage
{
    public interface IJwtFactory: IDependencyManager
    {
        Task<string> GenerateEncodedToken(string userNo, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(LoginUser user);
    }
}
