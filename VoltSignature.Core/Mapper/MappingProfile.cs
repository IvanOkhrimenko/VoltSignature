using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VoltSignature.Model.Account;
using VoltSignature.Model.Company;
using VoltSignature.Model.User;
using VoltSignature.PostgreSQL.Entity;

namespace VoltSignature.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ForMember(x => x.Role, opt => opt.MapFrom(x => x.UserRole== null ? null : x.UserRole.Name));
            CreateMap<Company, CompanyModel>();
            CreateMap<RegisterModel, User>();
        }
    }
}
