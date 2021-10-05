using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog
{
    public interface ITestService
    {
        void Show();
    }

    public class TestService : ITestService
    {
     
        public void Show()
        {
            Console.WriteLine($"This is a {this.GetType().Name} Instance...");
        }
    }
}
