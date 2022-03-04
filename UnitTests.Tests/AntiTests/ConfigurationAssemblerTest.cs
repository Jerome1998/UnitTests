using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.AntiTests;

namespace UnitTests.Tests.AntiTests
{
    [TestFixture]
    public class ConfigurationAssemblerTest
    {
        [Test]
        public void Assemble_ShouldAssembleConfigurationFromText()
        {
            var configurationAssembler = new ConfigurationAssembler();
            var text = File.ReadAllText(@"C:\\configuration.txt");
            var expectedConfiguration = new Configuration();

            var configuration = configurationAssembler.Assemble(text);

            Assert.That(configuration.Equals(expectedConfiguration));
        }
    }
}
