using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserOperationClaimDal _userOperationClaimDal;


        public UserManager(IUserDal userDal, IUserOperationClaimDal userOperationClaimDal)
        {
            _userDal = userDal;
            _userOperationClaimDal = userOperationClaimDal;
        }

        public IResult Add(UserAddDto user)
        {
            var userResult = _userDal.Get(x => x.Email == user.Email);
            if(userResult  != null) 
            {
                return new ErrorResult("Mevcut Kullanıcı");
            
            }
            HashingHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var userAdd = new User
            {
                FirstName = user.FirsName,
                LastName = user.LastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                BloodGroup = user.BloodGroup,
                Address = user.Address,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            

            };
            _userDal.Add(userAdd);
            var userGet = _userDal.Get(x => x.Email == user.Email);
            if (userGet != null)
            {
                var operationClaim = new UserOperationClaim
                {
                    OperationClaimId = user.OperationClaimId,
                    UserId = userGet.Id
                };
                _userOperationClaimDal.Add(operationClaim);
            }
            return new SuccessResult("Kayıt Eklendi");
        }

        public IResult Delete(int UserId)
        {
            if (UserId > 0)
            {
                var getUser = _userDal.Get(x => x.Id == UserId);
                var getUserOperation = _userOperationClaimDal.Get(x => x.UserId == UserId);
                if (getUser != null)
                {
                    _userOperationClaimDal.Delete(getUserOperation);
                    _userDal.Delete(getUser);
                    return new SuccessResult("Kullanıcı silindi");

                }
                return new ErrorResult("Kullanıcı bulunamadı");
            }
            return new ErrorResult("Hatalı parametre");
        }

       

        public IDataResult<GetUserDto> GetUserDtoByUserId(int UserId)
        {
            var user = _userDal.GetUserDtoByUserId(UserId);
            return new SuccessDataResult<GetUserDto>(user);
        }

        public IDataResult<List<GetUserDto>> GetUserDtoList()
        {
            var result = _userDal.GetAllUserDto();
            return new SuccessDataResult<List<GetUserDto>>(result);

        }

        public IResult Update(UserAddDto user)
        {
            var users = _userDal.Get(x => x.Id == user.UserId);
            if (users == null)
            {
                return new ErrorResult("Kullanıcı Bulunamadı");
            }
            users.FirstName = user.FirsName;
            users.LastName = user.LastName;
            users.BirthDate = user.BirthDate;
            users.BloodGroup = user.BloodGroup;
            users.Address = user.Address;
            var userOperationClaim = _userOperationClaimDal.Get(x => x.UserId == user.UserId);
            userOperationClaim.OperationClaimId = user.OperationClaimId;
            _userOperationClaimDal.Update(userOperationClaim);
            _userDal.Update(users);
            return new SuccessResult("Kullanıcı bilgileri güncellendi");
        }
    }
}
