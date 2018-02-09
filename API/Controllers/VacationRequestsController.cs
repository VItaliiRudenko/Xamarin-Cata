using System;
using Microsoft.AspNetCore.Mvc;
using Epam.Xmp.Vts.Server.Common;
using Newtonsoft.Json;
using Epam.Xmp.Vts.Server.Services;

namespace Epam.Xmp.Vts.Server.Controllers
{
    public class VacationRequestsController : Controller
    {
        private readonly IRepository _repository;

        public VacationRequestsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ActionName("Main")]
        public ResponseMessage Get(string id)
        {
            if (id == null)
            {
                var requests = _repository.GetAll();
                return new ResponseListResultMessage
                {
                    Result = "OK",
                    ResultCode = 0,
                    ListResult = requests
                };
            }

            Guid guid;
            if (!Guid.TryParse(id, out guid))
            {
                return new ResponseMessage
                {
                    Result = "Not found",
                    ResultCode = -1
                };
            }

            var req = _repository.Get(guid);
            if (req != null)
            {
                return new ResponseItemResultMessage
                {
                    Result = "OK",
                    ResultCode = 0,
                    ItemResult = req
                };
            }

            return null;
        }

        [HttpPost]
        [ActionName("Main")]
        public ResponseMessage Upsert()
        {
            VacationRequest req = null;
            using (var s = new System.IO.StreamReader(Request.Body))
            {
                var str = s.ReadToEnd();
                Console.WriteLine(str);

                if (string.IsNullOrWhiteSpace(str))
                {
                    return new ResponseItemResultMessage
                    {
                        Result = "Cannot create/update the item since it's empty",
                        ResultCode = -1,
                        ItemResult = null
                    };
                }

                req = JsonConvert.DeserializeObject<VacationRequest>(str);

                if (req.Start == DateTime.MinValue || req.End == DateTime.MinValue || req.CreatedBy == null)
                {
                    return new ResponseItemResultMessage
                    {
                        Result = "To create or update a Vacation Request, fill its required fields: Start, End, CreatedBy",
                        ResultCode = -1,
                        ItemResult = null
                    };                   
                }
            }

            req = _repository.Upsert(req);

            return new ResponseItemResultMessage
            {
                Result = "OK",
                ResultCode = 0,
                ItemResult = req
            };
        }

        [HttpDelete]
        [ActionName("Main")]
        public ResponseMessage Delete(string id)
        {
            Guid guid;
            if (!Guid.TryParse(id, out guid))
            {
                return new ResponseMessage
                {
                    Result = "Not found",
                    ResultCode = -1
                };
            }

            var deleted = _repository.Delete(guid);
            if (deleted)
            {
                return new ResponseMessage
                {
                    Result = "OK",
                    ResultCode = 0
                };               
            }
            
            return new ResponseMessage
            {
                Result = "Not found",
                ResultCode = -1
            };            
        }
    }
}