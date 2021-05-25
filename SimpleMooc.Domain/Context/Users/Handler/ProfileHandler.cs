using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimpleMooc.Domain.Context.Users.Repositories;
using SimpleMooc.Domain.Context.Users.Command.Input;
using SimpleMooc.Domain.Context.Users.Command.Output;
using SimpleMooc.Shared.Entities;
using SimpleMooc.Shared.Repositories;
using SimpleMooc.Shared.Services;

namespace SimpleMooc.Domain.Context.Users.Handler
{
    public class ProfileHandler : IRequestHandler<ProfileCommand, BaseResponse>,
        IRequestHandler<ProfileUpdateCommand, BaseResponse>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUploadService _uploadService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileHandler(IProfileRepository profileRepository, IUserRepository userRepository,
            IUploadService uploadService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _uploadService = uploadService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<BaseResponse> Handle(ProfileCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(command.UserId);
            if (user is null)
            {
                return new BaseResponse(false, "User not found.", null);
            }

            var profile = new Entities.Profile(command.FirstName, command.LastName, user);

            var image = command.Image;

            if (image is {Length: > 0})
            {
                profile.ChangeUrlImage(await _uploadService.UploadImageProfile(user.Id, image.OpenReadStream()));
            }

            await _profileRepository.Save(profile);
            await _unitOfWork.Commit();
            var profileResponse = _mapper.Map<Entities.Profile, ProfileResponse>(profile);
            return new BaseResponse(true, "Profile save success.", profileResponse);
        }

        public async Task<BaseResponse> Handle(ProfileUpdateCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(command.UserId);
            if (user is null)
            {
                return new BaseResponse(false, "User not found.", null);
            }

            var byEmail = await _userRepository.GetByEmail(command.Email);
            if (byEmail is not null && !user.Email.Equals(command.Email))
            {
                return new BaseResponse(false, "email already exists.", null);
            }

            user.ChangeEmail(command.Email);

            _userRepository.Update(user);

            var profile = await _profileRepository.GetById(command.Id);
            if (profile is null)
            {
                return new BaseResponse(false, "Profile not found", null);
            }

            profile.ChangeFirstName(command.FirstName);
            profile.ChangeLastName(command.LastName);
            profile.ChangeFullName();

            var image = command.Image;

            if (image is {Length: > 0})
            {
                profile.ChangeUrlImage(await _uploadService.UploadImageProfile(user.Id, image.OpenReadStream()));
            }

            _profileRepository.Update(profile);
            await _unitOfWork.Commit();
            var profileResponse = _mapper.Map<Entities.Profile, ProfileResponse>(profile);
            return new BaseResponse(true, "Profile save success.", profileResponse);
        }
    }
}