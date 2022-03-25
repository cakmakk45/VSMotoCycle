using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserAddDto : IDto
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string BloodGroup { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
    }
}
