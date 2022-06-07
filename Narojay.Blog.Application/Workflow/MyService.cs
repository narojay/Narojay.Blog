using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narojay.Blog.Application.Workflow
{
    public class MyService : IMyService
    {
        public void DoTheThings()
        {
            Console.WriteLine("Doing stuff...");
        }
    }
}
