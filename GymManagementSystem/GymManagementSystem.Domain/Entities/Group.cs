using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        [Required]
        public string? GroupName { get; set; }
        public string? Description { get; set; }

        // navigation properties
        public virtual int? CoachId { get; set; }
        public virtual Coach? Coach { get; set; }

        public virtual List<Activity>? Activities { get; set; }
        public virtual List<Subscription>? Subscriptions { get; set; }
    }
}
