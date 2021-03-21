using ManageMyHyper.Server.Data;
using ManageMyHyper.Shared;
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
    public class WorkTaskController : ControllerBase
    {
        private readonly DataContext _context;

        public WorkTaskController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkTasks()
        {
            //TODO
            //var workTasks = await _context.WorkTasks.ToListAsync();
            var workTasks = new List<WorkTask>();
            return Ok(workTasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkTask()
        {
            //TODO
            //var workTasks = await _context.WorkTasks.ToListAsync();
            var workTask = new WorkTask();
            return Ok(workTask);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWorkTask()
        {
            //TODO
            //var workTasks = await _context.WorkTasks.ToListAsync();
            var workTask = new WorkTask();
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
