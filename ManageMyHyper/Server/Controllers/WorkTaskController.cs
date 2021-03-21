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
                    .OrderByDescending(w => w.CreationDate)
                    .ToListAsync();
            }
            else
            {
                workTasks = await _context.WorkTasks
                    .Where(w => w.IsDone == false && w.AssignedUserId == null)
                    .Include(w => w.WorkTaskPriority)
                    .Include(w => w.CreatorUser)
                    .Include(w => w.AssignedUser)
                    .OrderByDescending(w => w.CreationDate)
                    .ToListAsync();
            }
            return Ok(workTasks);
        }

        [HttpGet("getmyworktasks")]
        public async Task<IActionResult> GetMyWorkTasks()
        {
            var workTasks = new List<WorkTask>();
            var user = await _utilityService.GetUser();
            if (user.UserRole.Name == "Manager")
            {
                workTasks = await _context.WorkTasks
                    .Where(w => w.CreatorUserId == user.Id)
                    .Include(w => w.WorkTaskPriority)
                    .Include(w => w.AssignedUser)
                    .OrderByDescending(w => w.CreationDate)
                    .ToListAsync();
            }
            else
            {
                workTasks = await _context.WorkTasks
                    .Where(w => w.AssignedUserId == user.Id)
                    .Include(w => w.WorkTaskPriority)
                    .Include(w => w.CreatorUser)
                    .Include(w => w.AssignedUser)
                    .OrderBy(w => w.IsDone)
                    .OrderByDescending(w => w.CreationDate)
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
            var workTask = await _context.WorkTasks.FirstOrDefaultAsync(w => w.Id == workTaskId);

            var response = new ServiceResponse<string>();

            if (workTask != null && workTask.AssignedUserId == null)
            {
                workTask.AssignedUserId = user.Id;
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "La tâche vous a été attribuée avec succès.";
            }
            else
            {
                response.Success = false;
                response.Message = "Impossible de réserver cette tâche.";
            }

            return Ok(response);
        }

        [HttpPut("validworktask")]
        public async Task<IActionResult> ValidWorkTask([FromBody] int workTaskId)
        {
            var workTask = await _context.WorkTasks.FirstOrDefaultAsync(w => w.Id == workTaskId);

            var response = new ServiceResponse<string>();

            if (workTask != null)
            {
                workTask.IsDone = true;
                workTask.DateHasBeenDone = DateTime.Now;
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "La tâche a bien été validée.";
            }
            else
            {
                response.Success = false;
                response.Message = "Impossible de retrouver la tâche à valider.";
            }

            return Ok(response);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteWorkTask(int workTaskId)
        {
            var workTask = await _context.WorkTasks.FirstOrDefaultAsync(w => w.Id == workTaskId);
            var response = new ServiceResponse<string>();

            if (workTask != null)
            {
                _context.WorkTasks.Remove(workTask);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "La tâche a bien été supprimée.";
            }
            else
            {
                response.Success = false;
                response.Message = "Impossible de retrouver la tâche à supprimer.";
            }
            
            return Ok(response);
        }
    }
}
