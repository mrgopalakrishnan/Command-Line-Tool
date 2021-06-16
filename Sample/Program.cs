using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Cli cli = new()
            {
                Introduction = "my intro",
                PromptText = "CLI"
            };
            cli.RegisterCommand("M1", method1);
            cli.SetCancellationKeys(new() { "exit" });
            cli.Start();
        }
        static void method1(params string[] args) => args.ToList().ForEach(v => Console.WriteLine($"type: {v.GetType()}, {v}"));
    }
}
