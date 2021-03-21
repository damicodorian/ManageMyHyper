using ManageMyHyper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyHyper.Client.Services
{
    public interface IUserRoleService
    {
        IList<UserRole> UserRoles { get; set; }

        Task LoadUserRolesAsync();
    }
}
