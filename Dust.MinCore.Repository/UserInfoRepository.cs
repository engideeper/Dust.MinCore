using Dust.MinCore.Common.Attribute;
using Dust.MinCore.Models.Entity;
using FreeSql;
using System.Threading.Tasks;

namespace Dust.MinCore.Repository
{
    public interface IUserInfoRepository : IBaseRepository<UserInfo, string>
    {
        /// <summary>
        /// 用户排名
        /// </summary>
        /// <returns> </returns>
        Task<dynamic> GetUserInfoRank(string openId);
    }

    [Scoped(iName: "IUserInfoRepository")]
    public class UserInfoRepository : BaseRepository<UserInfo, string>, IUserInfoRepository
    {
        private readonly IFreeSql _fsql;

        public UserInfoRepository(IFreeSql fsql) : base(fsql, null, null)
        {
            _fsql = fsql;
        }

        public Task<dynamic> GetUserInfoRank(string openId)
        {
            dynamic result = _fsql.Select<UserInfo>()
                           .Where(t => t.OpenId == openId)
                           .ToOneAsync(t => new
                           {
                               UserInfo = t,
                               Rank = _fsql.Select<UserInfo>()
                                           .Where(r =>
                                           r.Step + r.StepIntegral >
                                           t.Step + t.StepIntegral).Count() + 1
                           });
            return result;
        }
    }
}
