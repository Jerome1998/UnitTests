using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.AntiTests
{
    public class UserNameController
    {
        public string Invoke()
        {
            Console.WriteLine("Enter Username:");
            return Console.ReadLine();
        }
    }
}
