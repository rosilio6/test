
using AcceptanceTests.Data;
using NUnit.Framework;

namespace AcceptanceTests.Tests
{
    [TestFixture]
    class TestMaintainingLeauge : TestCases
    {
        private League _basic = new League(LeagueType.Beginner);

        [Test]
        public void TestNewUserLeaugeDefaultLeague()
        {
            User user66 = new User("User66", "Arrow32", "user66@gmail.com");
            int user = TestRegister("User66", "Arrow32", user66.Email);
            Assert.AreEqual(user, 0);

            User user5FromSystem = TestfindUserByUserName("User66");
            LeagueType tmp = user5FromSystem.LeagueRank;
            Assert.AreEqual(LeagueType.Beginner , tmp);

        }

        [Test]
        public void TestUndefindLeague()
        {
            User User677 = new User("User677", "Arrow32", "user66@gmail.com");
            User user5FromSystem = TestfindUserByUserName("User677");
            LeagueType tmp = user5FromSystem.LeagueRank;
            Assert.AreEqual(LeagueType.Undefined, tmp);
        }
/*
        [Test]
        public void TestProffesionalLeague()
        {
            User User677 = new User("User677", "Arrow32", "user66@gmail.com");
            int user = TestRegister("User677", "Arrow32", User677.Email);
            Assert.AreEqual(user, 0);
            User user5FromSystem = TestfindUserByUserName("User677");         
            LeagueType tmp = user5FromSystem.LeagueRank;
            Assert.AreEqual(LeagueType.Professional, tmp);
        }
        */
    }
}
