
using NUnit.Framework;
using UserManagement;

namespace Testing.UserTesting
{
    /// <summary>
    /// Tests the GameCenter methods
    /// </summary>
    [TestFixture]
    public class UserTest
    {
   

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Set_pathToImageTest()
        {
            string path = "c:/image.img";
            User user = new User("name", "password", 22);
            user.Set_pathToImage(path);
            Assert.IsTrue(path.Equals(user.Get_pathToImage()));

            string wrongPath = "c:/Nop";
            User user2 = new User("name2", "password", 25);
            user2.Set_pathToImage(path);
            Assert.IsFalse(wrongPath.Equals(user2.Get_pathToImage()));


        }


        [Test]
        public void Set_emailTest()
        {
            string email = "boaz@gmail.com";
            User user = new User("name", "password", 22);
            user.Set_email(email);
            Assert.IsTrue(email.Equals(user.Get_email()));

            string wrongEmail = "boaz@NOP.nop";
            User user2 = new User("name", "password", 222);
            user2.Set_email(email);
            Assert.IsFalse(wrongEmail.Equals(user2.Get_email()));
        }



        [Test]
        public void CheckPasswordTest()
        {
            //arange
            User user = new User("name", "password", 22);
            Assert.IsTrue(user.CheckPassword("password"));
            Assert.IsFalse(user.CheckPassword("NotThepassword"));

        }

        [Test]
        public void ChangePasswordTest()
        {

            User user = new User("name", "oldPassword", 22);
            Assert.IsTrue(user.ChangePassword("oldPassword", "newPassword"));


            User user2 = new User("name2", "oldPassword", 222);
            Assert.IsFalse(user2.ChangePassword("wrong password", "newPassword"));
        }
    }
}