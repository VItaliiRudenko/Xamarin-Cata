using System;
using Microsoft.AspNetCore.Mvc;
using Epam.Xmp.Vts.Server.Common;
using Newtonsoft.Json;

namespace Epam.Xmp.Vts.Server.Controllers
{
    public class UserController : Controller
    {
        private const string Login = "ARK";
        private const string Pass = "123";

        [HttpPost]
        public ResponseMessage SignIn()
        {
            SignIn obj;
            using (var s = new System.IO.StreamReader(Request.Body))
            {
                var str = s.ReadToEnd();
                Console.WriteLine(str);

                if (string.IsNullOrWhiteSpace(str))
                {
                    return new ResponseMessage
                    {
                        Result = "User not found",
                        ResultCode = -1
                    };
                }

                obj = JsonConvert.DeserializeObject<SignIn>(str);
            }

            if (obj.Login?.ToUpperInvariant() == Login && obj.Password == Pass)
            {
                Response.Headers.Add("token", Guid.NewGuid().ToString());

                return new ResponseMessage
                {
                    Result = "OK",
                    ResultCode = 0
                };               
            }

            return new ResponseMessage
            {
                Result = "User not found",
                ResultCode = -1
            };            
        }
    }
}