using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace VSMotoCycle.Storage
{
    public class AuthStorage : IAuthStorage
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthStorage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public GetUserDto GetUser()
        {
            var user = _httpContextAccessor.HttpContext.Session.GetString("user");
            if (user == null)
            {
                return null;
            }
            var userObject = JsonSerializer.Deserialize<GetUserDto>(user);
            return userObject;
        }

        public void removeUser()
        {
            _httpContextAccessor.HttpContext.Session.Remove("user");
        }

        public void SetUser(GetUserDto user)
        {
            var userString = JsonSerializer.Serialize(user);
            _httpContextAccessor.HttpContext.Session.SetString("user", userString);
        }
    }
}
