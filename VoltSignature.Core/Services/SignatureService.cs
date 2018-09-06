﻿using VoltSignature.Interface;
using VoltSignature.Model.Signature;
using VoltSignature.Model.User;
using VoltSignature.DbCore.Entity;
using VoltSignature.DbCore.Repository;
using VoltSignature.Files.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;

namespace VoltSignature.Core.Services
{
    public class SignatureService : ISignatureService
    {
        private readonly IStorage _storage;
        private readonly IRepository<Signature> _signatureRepository;
        private readonly IFileRepository _filesRepository;
        private readonly IMapper _mapper;
        

        public SignatureService(IStorage storage, IMapper mapper)
        {
            _storage = storage;
            _signatureRepository = _storage.GetRepository<Signature>();
            _filesRepository = _storage.GetFileRepository(Files.Model.FileTypeEnum.SignatureFile);
            _mapper = mapper;
        }

        public async Task CreateSignatureRequest(SignatureRequest model, string userId)
        {
            List<UserSignature> list = new List<UserSignature>();
            model.Recipients.ForEach(x =>
            {
                list.Add(new UserSignature()
                {
                    UserId = x
                });
            });
            Signature signature = new Signature()
            {
                AuthorId = userId,
                CreateDate = DateTime.UtcNow,
                UserSignatures = list
            };
            using(MemoryStream ms = new MemoryStream())
            {
                await model.File.CopyToAsync(ms);
                signature.FileId = await _filesRepository.Save(ms, model.File.FileName);
            }
            await _signatureRepository.Create(signature); 
        }


        public async Task<List<SignatureModel>> GetForSignature(CurrentUser user)
        {
            IRepository<User> userRepository = _storage.GetRepository<User>();
            List<Signature> signatures = await _signatureRepository.GetList(x => x.UserSignatures.Exists(z => z.UserId == user.Id && z.SignatureHash == null));             
            var authors = await userRepository.GetList(x => signatures.Exists(z => z.AuthorId == x.Id));
            List<SignatureModel> result = new List<SignatureModel>();
            foreach(Signature s in signatures)
            {
                SignatureModel signViewModel = _mapper.Map<Signature, SignatureModel>(s);
                User current = authors.FirstOrDefault(x => x.Id == signViewModel.AuthroId);
                signViewModel.AuthorEmail = current.Email;
                signViewModel.AuthorFullName = current.FullName;
                signViewModel.ImageId = current.ImageId;
                signViewModel.FileName = await _filesRepository.GetName(signViewModel.FileId);
                result.Add(signViewModel);
            }
            return result;
        }
    }
}