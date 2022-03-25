using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal: EfEntityRepositoryBase<User, ProjectContext>, IUserDal
    {
        public List<GetUserDto> GetAllUserDto()
        {
            using (var context = new ProjectContext())
            {
                var result = from user in context.Users
                             join userOperationClaim in context.UserOperationClaims on user.Id equals userOperationClaim.UserId
                             join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id

                             select new GetUserDto
                             {
                                 Id = user.Id,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 BloodGroup = user.BloodGroup,
                                 BirthDate = user.BirthDate,
                                 Address = user.Address,
                                 Status = user.Status,
                                 PasswordHash = user.PasswordHash,
                                 PasswordSalt = user.PasswordSalt,
                                 OperationClaimId = operationClaim.Id,
                                 OperationClaimName = operationClaim.ClaimName,
                             };
                return result.ToList();
            }
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new ProjectContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, ClaimName = operationClaim.ClaimName };
                return result.ToList();

            }
        }

        public GetUserDto GetLoginUserDto(UserForLoginDto userForLoginDto)
        {
            using (var context = new ProjectContext())
            {
                var result = from user in context.Users
                             join userOperationClaim in context.UserOperationClaims on user.Id equals userOperationClaim.Id
                             join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id
                             where user.Email == userForLoginDto.Email
                             select new GetUserDto
                             {
                                 Id = user.Id,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 BloodGroup = user.BloodGroup,
                                 BirthDate = user.BirthDate,
                                 Address = user.Address,
                                 Status = user.Status,
                                 PasswordHash = user.PasswordHash,
                                 PasswordSalt = user.PasswordSalt,
                                 OperationClaimId = operationClaim.Id,
                                 OperationClaimName = operationClaim.ClaimName,
                             };
                return result.FirstOrDefault();
            }
        }

        public GetUserDto GetUserDtoByUserId(int UserId)
        {
            using (var context = new ProjectContext())
            {
                var result = from user in context.Users
                             join userOperationClaim in context.UserOperationClaims on user.Id equals userOperationClaim.UserId
                             join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id
                             where user.Id == UserId
                             select new GetUserDto
                             {
                                 Id = user.Id,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 BloodGroup = user.BloodGroup,
                                 BirthDate = user.BirthDate,
                                 Address = user.Address,
                                 Status = user.Status,
                                 PasswordHash = user.PasswordHash,
                                 PasswordSalt = user.PasswordSalt,
                                 OperationClaimId = operationClaim.Id,
                                 OperationClaimName = operationClaim.ClaimName,
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
