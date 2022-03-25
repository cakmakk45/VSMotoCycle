using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class GetUserDto : User
    {
        public int OperationClaimId { get; set; }
        public string OperationClaimName { get; set; }
    }
}
