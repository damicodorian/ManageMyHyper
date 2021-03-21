using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMyHyper.Shared
{
    public class WorkTask
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Veuillez renseigner le nom de la tâche.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDone { get; set; } = false;
        public DateTime? DateHasBeenDone { get; set; }
        public WorkTaskPriority WorkTaskPriority { get; set; }
        [Required(ErrorMessage = "Veuillez sélectionner la priorité de la tâche.")]
        public int WorkTaskPriorityId { get; set; }
        public int CreatorUserId { get; set; }
        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser { get; set; }
        public int? AssignedUserId { get; set; }
        [ForeignKey("AssignedUserId")]
        public virtual User AssignedUser { get; set; }
    }
}
