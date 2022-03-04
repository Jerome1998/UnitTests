using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.AntiTests;

namespace UnitTests.Tests.AntiTests
{
    //THIS TEST SHOWCASES HOW NOT TO DO A UNIT TEST
    [TestFixture]
    public class UserNameControllerTest
    {
        //PLEASE ENTER TOM
        [Test]
        public void UserNameController_ShouldReturnInputValue()
        {
            var userNameController = new UserNameController();

            var result = userNameController.Invoke();

            Assert.That(result, Is.EqualTo("TOM"));
        }
    }
}
