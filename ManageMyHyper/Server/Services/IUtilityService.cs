using ManageMyHyper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyHyper.Server.Services
{
    public interface IUtilityService
    {
        Task<User> GetUser();
    }
}
