using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using MediatR;
using SimpleMooc.Domain.Context.Users.Command.Input;
using SimpleMooc.Domain.Context.Users.Command.Output;
using SimpleMooc.Domain.Context.Users.Entities;
using SimpleMooc.Domain.Context.Users.Repositories;
using SimpleMooc.Domain.Context.Users.Services;
using SimpleMooc.Shared.Entities;
using SimpleMooc.Shared.Repositories;

namespace SimpleMooc.Domain.Context.Users.Handler
{
    public class UserHandler : IRequestHandler<RegisterUserCommand, BaseResponse>,
        IRequestHandler<LoginCommand, BaseResponse>,
        IRequestHandler<RefreshTokenCommand, BaseResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserHandler(IUserRepository userRepository, ITokenService tokenService,
            IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var findByEmail = await _userRepository.GetByEmail(command.Email);
            if (findByEmail is not null)
            {
                return new BaseResponse(false, "Email is Register.", null);
            }

            var newUser = new User(command.Email);
            newUser.ChangePassword(command.Password);

            var refresh = _tokenService.GenerateRefreshToken();
            var refreshToken = new RefreshToken(refresh, newUser);

            await _userRepository.Save(newUser);
            await _refreshTokenRepository.Save(refreshToken);

            var token = _tokenService.GenerateToken(newUser);

            await _unitOfWork.Commit();
            var response = new TokenResponse(token, refreshToken.Token);
            return new BaseResponse(true, "user Register.", response);
        }

        public async Task<BaseResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(command.Email);
            if (user is null)
            {
                return new BaseResponse(false, "Email not register.", null);
            }

            if (!user.VerifyPassword(command.Password, user.Password))
            {
                return new BaseResponse(false, "User or password invalid.", null);
            }

            var refreshToken = await _refreshTokenRepository.GetByUserId(user.Id);

            if (refreshToken.IsExpired())
            {
                var refresh = _tokenService.GenerateRefreshToken();
                refreshToken.ChangeRefreshToken(refresh);
                _refreshTokenRepository.Update(refreshToken);
                await _unitOfWork.Commit();
            }

            var token = _tokenService.GenerateToken(user);

            var response = new TokenResponse(token, refreshToken.Token);
            return new BaseResponse(true, "user Register.", response);
        }

        public async Task<BaseResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetByToken(command.Refresh);
            if (refreshToken is null || refreshToken.IsExpired())
            {
                return new BaseResponse(false, "token invalid", null);
            }

            var refresh = _tokenService.GenerateRefreshToken();
            refreshToken.ChangeRefreshToken(refresh);
            _refreshTokenRepository.Update(refreshToken);
            await _unitOfWork.Commit();
            var token = _tokenService.GenerateToken(refreshToken.User);
            var response = new TokenResponse(token, refreshToken.Token);
            return new BaseResponse(true, "refresh token.", response);
        }
    }
}