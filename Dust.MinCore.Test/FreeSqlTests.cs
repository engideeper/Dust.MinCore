using Dust.MinCore.Models.Entity;
using FreeSql;
using NUnit.Framework;

namespace Dust.MinCore.Test
{
    public class FreeSqlTests
    {
        public IFreeSql Fsql { get; set; }

        [SetUp]
        public void Setup()
        {
            Fsql = new FreeSqlBuilder()
                   .UseConnectionString(DataType.MySql, "Database=pedometer_app;Data Source=localhost;User Id=root;Password=dust@123!;CharSet=utf8;port=3306")
                   .UseAutoSyncStructure(true)
                   .UseLazyLoading(true)
                   .Build();
        }

        /// <summary>
        /// ≈≈√˚Rank 1 2 3
        /// </summary>
        [TestCase("olQhY0hCkJjB15AcjKCohDP4RA5B")]
        [TestCase("olQhY0hCkJjB15AcjKCohDP4RA5A")]
        [TestCase("olQhY0hCkJjB15AcjKCohDP4RA5C")]
        [Test]
        public void GetUserRank(string openId)
        {
            var obj = Fsql.Select<UserInfo>()
                          .Where(t => t.OpenId == openId)
                          .ToOne(t => new
                          {
                              UserInfo = t,
                              Rank = Fsql.Select<UserInfo>()
                                         .Where(r => r.Step + r.StepIntegral > t.Step + t.StepIntegral).Count() + 1
                          });
            if (obj.Rank == 1 && openId == "olQhY0hCkJjB15AcjKCohDP4RA5B")
            {
                Assert.Pass();
            }
            else if (obj.Rank == 2 && openId == "olQhY0hCkJjB15AcjKCohDP4RA5A")
            {
                Assert.Pass();
            }
            else if (obj.Rank == 3 && openId == "olQhY0hCkJjB15AcjKCohDP4RA5C")
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
    }
}
