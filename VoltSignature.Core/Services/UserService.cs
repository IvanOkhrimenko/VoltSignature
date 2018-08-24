using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VoltSignature.Interface;
using VoltSignature.Model.Account;
using VoltSignature.Model.User;
using VoltSignature.MongoDb.Context;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.Repository.Interface;

namespace VoltSignature.Core.Services
{
    public class UserService : IUserService
    { 
        private readonly IMapper _mapper;
        private readonly IStorage _storage;
        private readonly IStoreImage _imageStore;
        private readonly IRepository<User> _userRepository;

        public UserService(IMapper mapper, IStoreImage imageStore,IStorage storage)
        {
            _mapper = mapper;
            _storage = storage;
            _imageStore = imageStore;
            _userRepository = _storage.Get<User>();
        }

        public string GenerateRegistrationToken(RegistrationParameters parameters)
        {
            var claims = new List<Claim>
            {
                new Claim(RegistrationParameters.CompanyClaim, parameters.CompanyId.ToString()),
                new Claim(RegistrationParameters.RoleClaim, parameters.RoleId.ToString()),
                new Claim(RegistrationParameters.PositionClaim, parameters.Position),
            };
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: RegisterTokenOptions.ISSUER,
                    audience: RegisterTokenOptions.AUDIENCE,
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(RegisterTokenOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(RegisterTokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        public async Task<UserModel> GetUser(int Id)
        {
            var user = await _userRepository.Get(x => x.Id == Id, inc => inc.Include(x => x.UserRole), true);
            if (user == null)
                throw new Exception("Not found user with id " + Id);
            var result = _mapper.Map<User, UserModel>(user);
            return result;
        }

         

        public async Task<User> RegisterUser(RegisterModel model, RegistrationParameters parameters)
        {
            
            var user = _mapper.Map<RegisterModel, User>(model);
            user.RoleId = parameters.RoleId;    
            user.Position = parameters.Position;
            user.CompanyId = parameters.CompanyId; 
            user.RegisterDate = DateTime.UtcNow;
            if (model.Image != null)
            {
                using(MemoryStream ms = new MemoryStream())
                {
                    await model.Image.CopyToAsync(ms);
                    user.ImageId = await _imageStore.SaveImage(ms, model.Image.FileName);
                } 
            }
            await _userRepository.Create(user);
            return user;
        }
    }
}
