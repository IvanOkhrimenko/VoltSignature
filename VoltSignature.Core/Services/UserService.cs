using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VoltSignature.Interface;
using VoltSignature.Model.User;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.Repository.Interface;

namespace VoltSignature.Core.Services
{
    public class UserService : IUserService
    { 
        private readonly IMapper _mapper;
        private readonly IStorage _storage;
        private readonly IRepository<User> _userRepository;

        public UserService(IMapper mapper, IStorage storage)
        {
            _mapper = mapper;
            _storage = storage;
            _userRepository = _storage.Get<User>();
        }

        public async Task<UserModel> GetUser(int Id)
        {
            var user = await _userRepository.Get(x => x.Id == Id, inc => inc.Include(x => x.UserRole), true);
            if (user == null)
                throw new Exception("Not found user with id " + Id);
            var result = _mapper.Map<User, UserModel>(user);
            return result;
        }
    }
}
