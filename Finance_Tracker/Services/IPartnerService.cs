using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance_Tracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Tracker.Services
{
    public interface IPartnerService
    {
        Task<Partner> GetPartnerById(Guid id);
        Task<Partner[]> GetPartner();
        void CreatePartner([FromBody] Partner partner);
        Task<bool> DeletePartnerById(Guid id);
    }
}
