using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using System.Linq;
using System.Linq.Expressions;

using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserDal _userDal;
        private IUserOperationClaimDal _userOperationClaimDal;


        public AuthManager(IUserDal userDal, IUserOperationClaimDal userOperationClaimDal)
        {
            _userDal = userDal;
            _userOperationClaimDal = userOperationClaimDal;

        }

        public IDataResult<GetUserDto> Login(UserForLoginDto userForLoginDto)
        {
            var user = _userDal.GetLoginUserDto(userForLoginDto);
            if (user == null)
            {
                return new ErrorDataResult<GetUserDto>("Eposta veya sifre hatalı");
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorDataResult<GetUserDto>("Eposta veya sifre hatalı");
            }
            return new SuccessDataResult<GetUserDto>(user);
        }

        public IResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userResult = _userDal.Get(x => x.Email == userForRegisterDto.Email);
            if (userResult != null)
            {
                return new ErrorResult("Mevcut Kullanıcı");
            }
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                FirstName = userForRegisterDto.FirsName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                BirthDate = userForRegisterDto.BirthDate,
                BloodGroup = userForRegisterDto.BloodGroup,
                Address = userForRegisterDto.Address,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userDal.Add(user);
            var userGet = _userDal.Get(x => x.Email == user.Email);
            if (userGet != null)
            {
                var operationClaim = new UserOperationClaim
                {
                    OperationClaimId = 2,
                    UserId = userGet.Id
                };
                _userOperationClaimDal.Add(operationClaim);
            }
            return new SuccessResult("Kayıt olundu");
        }
    }
}
