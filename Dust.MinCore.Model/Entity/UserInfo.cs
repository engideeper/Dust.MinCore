using FreeSql.DataAnnotations;
using Newtonsoft.Json;

namespace Dust.MinCore.Models.Entity
{
    [JsonObject(MemberSerialization.OptIn), Table(DisableSyncStructure = true)]
    public partial class UserInfo
    {
        [JsonProperty, Column(StringLength = 50, IsPrimary = true, IsNullable = false)]
        public string OpenId { get; set; }

        [JsonProperty, Column(StringLength = -1, IsNullable = false)]
        public string AvatarUrl { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string City { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string Country { get; set; }

        [JsonProperty, Column(DbType = "int")]
        public int? Gender { get; set; }

        [JsonProperty, Column(StringLength = 20)]
        public string Language { get; set; }

        [JsonProperty, Column(StringLength = 50, IsNullable = false)]
        public string NickName { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string Province { get; set; }

        [JsonProperty, Column(DbType = "int")]
        public int? Step { get; set; } = 0;

        [JsonProperty, Column(DbType = "int")]
        public int? StepIntegral { get; set; } = 0;

        [JsonProperty, Column(StringLength = 20)]
        public string WorkName { get; set; }

        [JsonProperty, Column(StringLength = 20)]
        public string WorkNo { get; set; }
    }
}
