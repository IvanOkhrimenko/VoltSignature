using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using VoltSignature.DbCore.Entity;
using VoltSignature.DbCore.Interface;
using VoltSignature.DbCore.Repository;
using VoltSignature.Interface;
using VoltSignature.Model.Account;
using VoltSignature.Model.User;

namespace VoltSignature.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IStorage _storage;
        private readonly IFileStorage _fileStore;
        private readonly IRepository<User> _userRepository;

        public UserService(IMapper mapper, IFileStorage fileStore, IStorage storage)
        {
            _mapper = mapper;
            _storage = storage;
            _fileStore = fileStore;
            _userRepository = _storage.GetRepository<User>();
        }

        public string GenerateRegistrationToken(RegistrationParameters parameters)
        {
            var claims = new List<Claim>
            {
                new Claim(RegistrationParameters.CompanyClaim, parameters.CompanyId.ToString()),
                new Claim(RegistrationParameters.RoleClaim, parameters.Role),
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

        public async Task<UserModel> GetUser(string Id)
        {
            var user = await _userRepository.Get(x => x.Id == Id);
            if (user == null)
                throw new Exception("Not found user with id " + Id);
            var result = _mapper.Map<User, UserModel>(user);
            return result;
        }

        public async Task<UserModel> LoginUser(string login, string pwd)
        {
            var user = await _userRepository.Get(x => x.Email == login && x.Password == pwd);
            if (user == null)
                return null;
            var result = _mapper.Map<User, UserModel>(user);
            return result;
        }

        public async Task<UserModel> RegisterUser(RegisterModel model, RegistrationParameters parameters)
        {
            var user = _mapper.Map<RegisterModel, User>(model);
            user.Role = parameters.Role;
            user.Position = parameters.Position;
            user.CompanyId = parameters.CompanyId;
            user.RegisterDate = DateTime.UtcNow;
            if (model.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await model.Image.CopyToAsync(ms);
                    user.ImageId = await _fileStore.Save(ms, model.Image.FileName);
                }
            }
            await _userRepository.Create(user);
            return _mapper.Map<User, UserModel>(user);
        }
    }
}