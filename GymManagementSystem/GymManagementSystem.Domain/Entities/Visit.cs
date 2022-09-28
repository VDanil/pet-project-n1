using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Visit
    {
        [Key]
        public int VisitId { get; set;}
        [Required]
        public DateTime VisitDateTime { get; set; } 
        // TODO: another time add field for adding
        // (т.е. отдельно поле дата посещения, отдельно поле дата оформления)

        // navigation properties
        public virtual int? SubscriptionId { get; set;}
        public virtual Subscription? Subscription { get; set;}
    }
}
