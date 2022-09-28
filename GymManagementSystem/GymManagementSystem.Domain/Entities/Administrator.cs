using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Administrator
    {
        [Key]
        public int AdministratorId { get; set; }
        [Required]
        public string? AdminName { get; set; }
        [Required]
        public string? AdminSurname { get; set; }
    }
}
