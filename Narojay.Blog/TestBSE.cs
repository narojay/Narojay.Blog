using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog
{
  
    public interface ITestBSE
    {
        void Show();
    }

    public class TestBSE : ITestBSE
    {

        public void Show()
        {
            Console.WriteLine($"This is a {this.GetType().Name} Instance...");
        }
    }
}
