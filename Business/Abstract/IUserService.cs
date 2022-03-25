using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<GetUserDto>> GetUserDtoList();
        IResult Delete(int UserId);
        IResult Add(UserAddDto user);
        IDataResult<GetUserDto> GetUserDtoByUserId(int UserId);
        IResult Update(UserAddDto user);

    }
}
