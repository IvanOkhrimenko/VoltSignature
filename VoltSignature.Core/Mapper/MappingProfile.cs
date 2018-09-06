using AutoMapper;
using VoltSignature.DbCore.Entity;
using VoltSignature.Model.Account;
using VoltSignature.Model.Company;
using VoltSignature.Model.Signature;
using VoltSignature.Model.User;

namespace VoltSignature.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<Company, CompanyModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<Signature, SignatureModel>();
        }
    }
}