using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Epam.Xmp.Vts.Server.Common;

namespace Epam.Xmp.Vts.Server.Services
{
    public interface IRepository
    {
        VacationRequest[] GetAll();
        VacationRequest Get(Guid id);
        VacationRequest Upsert(VacationRequest request);
        bool Delete(Guid id);
    }

    public class Repository : IRepository
    {
        private readonly IDictionary<Guid, VacationRequest> _data;

        public Repository()
        {
            _data = new ConcurrentDictionary<Guid, VacationRequest>();
            FillFakeData();
        }

        public VacationRequest[] GetAll()
        {
            return _data.Values.ToArray();
        }

        public VacationRequest Get(Guid id)
        {
            VacationRequest request = null;
            _data.TryGetValue(id, out request);
            return request;
        }

        public VacationRequest Upsert(VacationRequest request)
        {
            if (request != null)
            {
                if (request.Id == Guid.Empty)
                {
                    request.Id = Guid.NewGuid();
                }

                if (request.Created == DateTime.MinValue)
                {
                    request.Created = DateTime.UtcNow;
                }

                _data[request.Id] = request;

                return request;
            }

            return null;
        }

        public bool Delete(Guid id)
        {
            return _data.Remove(id);
        }

        private void FillFakeData()
        {
            var req1 = new VacationRequest
            {
                Id = new Guid("35055cea-59a5-43df-9d84-9f2bc8401008"),
                Start = new DateTime(2017, 3, 20),
                End = new DateTime(2017, 3, 30),
                VacationType = VacationType.Regular,
                VacationStatus = VacationStatus.Approved,
                CreatedBy = "Someone",
                Created = DateTime.UtcNow
            };

            var req2 = new VacationRequest
            {
                Id = new Guid("16954f5a-87cf-4030-8897-c8abe2e6d516"),
                Start = new DateTime(2016, 12, 26),
                End = new DateTime(2016, 12, 30),
                VacationType = VacationType.Regular,
                VacationStatus = VacationStatus.Closed,
                CreatedBy = "Someone",
                Created = DateTime.UtcNow                
            };

            var req3 = new VacationRequest
            {
                Id = new Guid("075bbbd8-4a50-4af0-beb7-96e89fc4f885"),
                Start = new DateTime(2016, 11, 2),
                End = new DateTime(2016, 11, 4),
                VacationType = VacationType.Sick,
                VacationStatus = VacationStatus.Closed,
                CreatedBy = "Someone",
                Created = DateTime.UtcNow                
            };

            var req4 = new VacationRequest
            {
                Id = new Guid("66d7d908-cb58-4fff-9a71-1067e148bdf5"),
                Start = new DateTime(2016, 7, 11),
                End = new DateTime(2016, 7, 13),
                VacationType = VacationType.Exceptional,
                VacationStatus = VacationStatus.Closed,
                CreatedBy = "Someone",
                Created = DateTime.UtcNow                
            };

            var req5 = new VacationRequest
            {
                Id = new Guid("b3d47866-6ccb-46bc-8006-2b6ee7b7f168"),
                Start = new DateTime(2016, 2, 6),
                End = new DateTime(2016, 2, 7),
                VacationType = VacationType.Overtime,
                VacationStatus = VacationStatus.Closed,
                CreatedBy = "Someone",
                Created = DateTime.UtcNow                
            };

            _data.Add(req1.Id, req1);
            _data.Add(req2.Id, req2);
            _data.Add(req3.Id, req3);
            _data.Add(req4.Id, req4);
            _data.Add(req5.Id, req5);
        }
    }
}