using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.CommandLine;
using System.Diagnostics;
using System.Reflection;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using CommandLineTool.Attributes;
using CommandLineTool.Exceptions;

namespace CommandLineTool
{
    public class Cli
    {
        /// <summary>
        /// text to be used in CLI prompt.
        /// Example: CLI will be displayed as 'CLI >'
        /// </summary>
        public string PromptText { get; init; } = "CLI";
        /// <summary>
        /// A text which will be displayed during the launch of the console as a welcome note.
        /// </summary>
        public string Introduction { get; init; }
        /// <summary>
        /// Title of the console
        /// </summary>
        public string Title { get; init; }

        private static bool isCreated;
        private Queue<string> CommandQueue;
        private List<string> CancellationKeys;
        private CancellationTokenSource cts;
        private bool isRunning;
        private RootCommand root;
        private readonly Type baseType;

        /// <summary>
        /// Initialize and Create command list from the calling class.
        /// </summary>
        public Cli()
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var clasName = methodInfo.ReflectedType.FullName;
            baseType = GetType(clasName);
            init();
        }
        /// <summary>
        /// Initialize and create command list from the given class reference.
        /// </summary>
        /// <param name="consoleAppClass">The class reference to be used to build command list</param>
        public Cli(Type consoleAppClass)
        {
            baseType = consoleAppClass;
            init();
        }
        private void init()
        {
            if (isCreated)
                throw new InstanceAlreadyExistException("CLi instance is already in exist.");

            BuildCommadList(baseType);
            cts = new();
            CancellationKeys = new();
            CommandQueue = new();
#pragma warning disable S3010 // Static fields should not be updated in constructors
            isCreated = true;
#pragma warning restore S3010 // Static fields should not be updated in constructors
        }
        /// <summary>
        /// set 
        /// </summary>
        ~Cli() => isCreated = false;
        private static Type GetType(string clsName) => GetAssembly().GetType(clsName);
        private static Assembly GetAssembly() =>
            Assembly.GetEntryAssembly() ??
            Assembly.GetExecutingAssembly();
        private void BuildCommadList(Type clsType)
        {
            var claAttr = clsType.GetCustomAttribute<AppAttribute>();
            if (claAttr is null)
                throw new MissingAppAttributeException($"Given type {clsType?.Name}" +
                    $" must implement CommandLineTool.Attributes.AppAttribute");
            claAttr ??= new AppAttribute(clsType.Name);

            Console.Title = claAttr.Description;
            root = new RootCommand(claAttr.Description);
            foreach (MethodInfo method in clsType.GetMethods(claAttr.BindingFlags))
            {
                CommandAttribute attribute = method.GetCustomAttribute<CommandAttribute>();
                if (attribute is null)
                    continue;

                Command cmd = new(attribute.Name, attribute.Description);
                cmd.Handler = CommandHandler.Create(method);
                foreach (ParameterInfo parameter in method.GetParameters().Where(p =>
                {
                    return p.GetCustomAttribute<ParamAttribute>() is not null;
                }))
                {
                    if (parameter.GetCustomAttribute<ParamArgumentAttribute>() is ParamArgumentAttribute argumentAttribute)
                    {
                        cmd.AddArgument(new Argument(parameter.Name)
                        {
                            ArgumentType = parameter.ParameterType,
                            Description = argumentAttribute.Description
                        });
                    }
                    else if (parameter.GetCustomAttribute<ParamOptionAttribute>() is ParamOptionAttribute optionAttribute)
                    {
                        Type constructedOption = typeof(Option<>).MakeGenericType(parameter.ParameterType);
                        Option inst = (Option)Activator.CreateInstance(constructedOption, optionAttribute.Aliases.ToArray(),
                                                  optionAttribute.Description);
                        inst.IsRequired = optionAttribute.IsMandatory;
                        inst.Name = parameter.Name;
                        cmd.AddOption(inst);
                    }
                }
                root.Add(cmd);
            }
        }
        public void QueueCommand(string commandStr) => CommandQueue.Enqueue(commandStr);
        public void QueueCommands(List<string> commandStrs)
            => commandStrs.ForEach(action: commandStr => QueueCommand(commandStr));

        public void SetCancellationKeys(List<string> keys)
        {
            keys.ForEach(x => CancellationKeys.Add(x.ToLower()));
            CancellationKeys = CancellationKeys.Distinct().ToList();
        }
        void CheckIfAlreadyRunning()
        {
            if (isRunning)
                throw new ConsoleAleadyRunningException("Already running");
        }
        public void Start()
        {
            CheckIfAlreadyRunning();
            isRunning = true;
            Console.WriteLine($"{Introduction}\n\n");
            LoopCommand(cts.Token);
        }
        public void StartAsync()
        {
            CheckIfAlreadyRunning();
            isRunning = true;
            Console.WriteLine($"{Introduction}\n\n");
            ThreadPool.QueueUserWorkItem(new WaitCallback(LoopCommand), cts.Token);
        }
        public void StopAsync()
        {
            cts.Cancel();
            cts.Dispose();
        }

        private void LoopCommand(object obj)
        {
            CancellationToken cancellationToken = (CancellationToken)obj;
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Write($"{PromptText} >");
                var input = (CommandQueue.Any()) ? CommandQueue.Dequeue() : Console.ReadLine();
                if (CancellationKeys.Contains(input.ToLower()))
                    break;
                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                    continue;
                root.Invoke(input);
            }
            Console.WriteLine("Terminating console...");
            isRunning = false;
        }
    }
}
