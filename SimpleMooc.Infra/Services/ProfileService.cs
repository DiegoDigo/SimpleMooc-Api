using System;
using System.Threading.Tasks;
using AutoMapper;
using SimpleMooc.Domain.Context.Users.Command.Output;
using SimpleMooc.Domain.Context.Users.Repositories;
using SimpleMooc.Domain.Context.Users.Services;
using SimpleMooc.Shared.Entities;
using Profile = SimpleMooc.Domain.Context.Users.Entities.Profile;


namespace SimpleMooc.Infra.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ProfileService(IProfileRepository profileRepository, IUserRepository userRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> GetByUserId(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user is null)
            {
                return new BaseResponse(false, "Usuário não encontrado.", null);
            }

            var profile = await _profileRepository.GetByUserId(user.Id);
            var response = _mapper.Map<Profile, ProfileResponse>(profile);
            
            return profile is null
                ? new BaseResponse(false, "Perfil não encontrado.", null)
                : new BaseResponse(true, "Perfil.", response);
        }
    }
}