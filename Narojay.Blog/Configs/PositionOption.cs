using System;
using Newtonsoft.Json;

namespace Narojay.Blog.Configs
{
    public class Test
    {
        public string X { get; set; }
        public string Y { get; set; }
    }

    public class PositionOption
    {
        public void Configure<TMyOption>(Action<TMyOption> action) where TMyOption : new()
        {
            var option = new TMyOption();
            action(option);
            Console.WriteLine(JsonConvert.SerializeObject(option));
        }
    }
}