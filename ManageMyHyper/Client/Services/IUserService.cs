using ManageMyHyper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyHyper.Client.Services
{
    public interface IUserService
    {
        IList<UserDTO> Users { get; set; }
        Task GetUsers();
    }
}
