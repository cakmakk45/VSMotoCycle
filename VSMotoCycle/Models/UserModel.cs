using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSMotoCycle.Models
{
    public class UserModel
    {
        public GetUserDto UserDto { get; set; } = new GetUserDto();
        public List<GetUserDto> UserList { get; set; } = new List<GetUserDto>();
    }
}
