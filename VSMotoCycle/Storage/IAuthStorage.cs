using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSMotoCycle.Storage
{
    public interface IAuthStorage
    {
        void SetUser(GetUserDto user);
        GetUserDto GetUser();
        void removeUser();
    }
}
