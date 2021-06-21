using CommandLineTool;
using CommandLineTool.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleApp
{
    [App("my application")]
    class Program
    {
        static void Main(string[] args)
        {
            Cli cli = new()
            {
                Introduction = "my intro",
                PromptText = "CLI"
            };
            //optional: set list of keys to exit from the command loop
            cli.SetCancellationKeys(new() { "exit" });
            cli.Start();
        }
        /// <summary>
        /// Private static method Example
        /// </summary>
        /// <param name="v">simple string</param>
        [Command("method1", "My method1")]
        private static void Method1(
            [ParamArgument()] string v) => Console.WriteLine($"type: {v.GetType()}, {v}");

        /// <summary>
        /// public method sample
        /// </summary>
        /// <param name="v"></param>
        [Command("method2", "My method2")]
        public void Method2(
            [ParamArgument()] string v)
            => Console.WriteLine($"type: {v.GetType()}, {v}");

        /// <summary>
        /// List of string as Arguments
        /// Ex: lst val1 val2 -> List<string>{val1,val2}
        /// </summary>
        /// <param name="lst"></param>
        [Command("lst", "My method2")]
        public void MethodList(
            [ParamArgument()] List<string> lst)
            => lst.ForEach(v=> Console.WriteLine($"type: {v.GetType()}, {v}"));

        /// <summary>
        /// get custom object list 
        /// Ex: files <file1_path> <file2_path> -> List<FileInfo>{file1_path,file2_path}
        /// </summary>
        /// <param name="lst"></param>
        [Command("files", "My method2")]
        public void MethodFiles(
            [ParamArgument()] List<FileInfo> lst)
            => lst.ForEach(v => Console.WriteLine($"type: {v.GetType()}, {v.FullName}"));

        /// <summary>
        /// option sample 
        /// Ex: files <file1_path> <file2_path> -> List<FileInfo>{file1_path,file2_path}
        /// </summary>
        /// <param name="lst"></param>
        [Command("options", "My method2")]
        public void MethodOptions(
            [ParamArgument()] string v,[ParamOption("-a")] string op1)
        {
            Console.WriteLine($"type: {v.GetType()}, {v}");
            Console.WriteLine($"type: {op1.GetType()}, {op1}");
        }
    }
}
