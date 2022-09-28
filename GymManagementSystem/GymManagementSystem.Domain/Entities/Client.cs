using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        [Required]
        public string? ClientName { get; set; }
        [Required]
        public string? ClientSurname { get; set; }

        // navigation properties
        public virtual int? SubscriptionId { get; set; }
        public virtual Subscription? Subscription { get; set; }
    }
}
