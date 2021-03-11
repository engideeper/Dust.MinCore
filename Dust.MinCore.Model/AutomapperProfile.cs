using AutoMapper;
using Dust.MinCore.Models.Dtos.Input;
using Dust.MinCore.Models.Dtos.Output;
using Dust.MinCore.Models.Entity;

namespace Dust.MinCore.Models
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            #region Entity UserInfo

            CreateMap<LoginInput, UserInfo>().ForMember(d => d.NickName, o => o.MapFrom(i => i.LoginName))
                                            .ForMember(d => d.OpenId, o => o.MapFrom(i => i.Id));
            CreateMap<UserInfo, LoginOutput>().ForMember(d => d.LoginName, o => o.MapFrom(i => i.NickName));

            #endregion Entity UserInfo
        }
    }
}
