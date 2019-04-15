using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace WebApplication3.Filters
{
    public class ApiAuthorizeFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var header = actionContext.Request.Headers.Authorization;
            var parameters = Encoding.Default.GetString(Convert.FromBase64String(header.Parameter));

            var username = parameters.Split(':')[0];
            var password = parameters.Split(':')[1];

            if (username.ToLower() == "andrebaltieri" && 
                password.ToLower() == "andrebaltieri")
            {
                base.OnAuthorization(actionContext);
            }
            else
            {
                return;
            }
        }
    }
}