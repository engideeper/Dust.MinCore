namespace Dust.MinCore.Common.WeChat
{
    public class Code2Session : Error
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 会话密钥
        /// </summary>
        public string session_key { get; set; }

        /// <summary>
        /// 用户在开放平台的唯一标识符，在满足 UnionID 下发条件的情况下会返回，详见 UnionID 机制说明。
        /// </summary>
        public string unionid { get; set; }
    }
}
