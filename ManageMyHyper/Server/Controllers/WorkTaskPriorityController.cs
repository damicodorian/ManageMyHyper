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
    public class WorkTaskPriorityController : ControllerBase
    {
        private readonly DataContext _context;

        public WorkTaskPriorityController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkTaskPriorities()
        {
            var workTaskPriorities = await _context.WorkTaskPriorities.ToListAsync();
            return Ok(workTaskPriorities);
        }
    }
}
