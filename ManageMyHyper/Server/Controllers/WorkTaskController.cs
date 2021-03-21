using ManageMyHyper.Server.Data;
using ManageMyHyper.Server.Services;
using ManageMyHyper.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyHyper.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkTaskController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public WorkTaskController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkTasks()
        {
            var workTasks = new List<WorkTask>();
            var user = await _utilityService.GetUser();
            if(user.UserRole.Name == "Manager")
            {
                workTasks = await _context.WorkTasks
                    .Where(w => w.IsDone == false)
                    .Include(w => w.WorkTaskPriority)
                    .Include(w => w.CreatorUser)
                    .Include(w => w.AssignedUser)
                    .ToListAsync();
            }
            else
            {
                workTasks = await _context.WorkTasks
                    .Where(w => w.IsDone == false && w.AssignedUserId == null)
                    .Include(w => w.WorkTaskPriority)
                    .Include(w => w.CreatorUser)
                    .Include(w => w.AssignedUser)
                    .ToListAsync();
            }
            return Ok(workTasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkTask(WorkTask request)
        {
            var user = await _utilityService.GetUser();

            request.CreatorUserId = user.Id;
            request.CreationDate = DateTime.Now;

            await _context.WorkTasks.AddAsync(request);
            await _context.SaveChangesAsync();

            var response = new ServiceResponse<string>
            {
                Success = true,
                Message = "La tâche a bien été créée."
            };
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWorkTask([FromBody] int workTaskId)
        {
            var user = await _utilityService.GetUser();
            var workTask = await _context.WorkTasks.FindAsync(workTaskId);

            var response = new ServiceResponse<string>();
            if (workTask.AssignedUserId == null)
            {
                workTask.AssignedUserId = user.Id;
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "La tâche vous a été attribuée avec succès.";
            }
            else
            {
                response.Success = false;
                response.Message = "Cette tâche est déjà réservée.";
            }

            return Ok(workTask);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWorkTask()
        {
            //TODO
            //var workTasks = await _context.WorkTasks.ToListAsync();
            return Ok();
        }
    }
}
