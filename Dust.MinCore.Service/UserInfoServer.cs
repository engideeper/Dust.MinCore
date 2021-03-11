using AutoMapper;
using Dust.MinCore.Common.Attribute;
using Dust.MinCore.Models.Dtos.Input;
using Dust.MinCore.Models.Dtos.Output;
using Dust.MinCore.Models.Entity;
using Dust.MinCore.Repository;
using System.Threading.Tasks;

namespace Dust.MinCore.Server
{
    public interface IUserInfoServer
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInput"> </param>
        /// <returns> </returns>
        Task<LoginOutput> Login(LoginInput loginInput);
    }

    [Scoped(iName: "IUserInfoServer")]
    public class UserInfoServer : IUserInfoServer
    {
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IMapper _mapper;

        public UserInfoServer(IUserInfoRepository userInfoRepository, IMapper mapper)
        {
            _userInfoRepository = userInfoRepository;
            _mapper = mapper;
        }

        public async Task<LoginOutput> Login(LoginInput loginInput)
        {
            UserInfo userInfo = _mapper.Map<UserInfo>(loginInput);
            return _mapper.Map<LoginOutput>(await _userInfoRepository.Where(t => t.OpenId == userInfo.OpenId).ToOneAsync());
        }
    }
}
