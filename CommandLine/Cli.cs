using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace CommandLine
{
    public delegate void ParamsAction(params string[] arguments);
    public class Cli
    {
        /// <summary>
        /// Recodrd to hold the registed command with it's method
        /// </summary>
        public record Command(string CommandName, ParamsAction Method);
        /// <summary>
        /// text to be used in CLI prompt.
        /// Example: CLI will be displayed as 'CLI >'
        /// </summary>
        public string PromptText { get; init; } = "CLI";
        /// <summary>
        /// A text which will be displayed during the launch of the console.
        /// </summary>
        public string Introduction { get; init; } = "";

        readonly Queue<string> CommandQueue;
        readonly List<Command> commandList = new();
        List<string> CancellationKeys;
        readonly CancellationTokenSource cts;
        bool isRunning = false;
        readonly bool isCaseSensitive = false;
        public Cli()
        {
            cts = new();
            CancellationKeys = new();
            CommandQueue = new();
        }
        public void QueueCommand(string commandStr) => CommandQueue.Enqueue(commandStr);
        public void QueueCommands(List<string> commandStrs) => commandStrs.ForEach(action: commandStr => QueueCommand(commandStr));

        private Command GetCommand(string commandName,
            ParamsAction method) => new((isCaseSensitive) ? commandName : commandName.ToLower(), method);
        public void SetBackroundColor(string color) => SetBackroundColor((ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true));
        public void SetBackroundColor(ConsoleColor color) => Console.BackgroundColor = color;
        public void RegisterCommands(List<Command> commands) => 
            commands.ForEach(c => RegisterCommand(c.CommandName, c.Method));
        public void RegisterCommand(string commandName, ParamsAction method)
        {
            var cmd = GetCommand(commandName, method);
            if (commandList.Contains(cmd))
            {
                LogWarning($"Command {commandName} is already registered with same method!");
                return;
            }

            if (commandList.Where(c => c.CommandName == commandName) is Command existingCmd)
                commandList[commandList.IndexOf(existingCmd)] = cmd;
            else
                commandList.Add(cmd);
        }
        public void RegisterCommand(Command command) => RegisterCommand(command.CommandName, command.Method);
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
            ThreadPool.QueueUserWorkItem(new WaitCallback(LoopCommand),cts.Token);
        }
        public void StopAsync()
        {
            cts.Cancel();
            cts.Dispose();
        }
        void LoopCommand(object obj)
        {
            CancellationToken cancellationToken = (CancellationToken)obj;
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Write($"{PromptText} >");
                var input =(CommandQueue.Any())? CommandQueue.Dequeue() : Console.ReadLine();
                if (CancellationKeys.Contains(input))
                    break;
                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                    continue;

                var inputList = input.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var cmd = commandList.FirstOrDefault(c => c.CommandName == inputList[0].ToLower());
                if (cmd is not null)
                {
                    cmd.Method.Invoke(inputList.Skip(1).ToArray());
                }
                else LogError($"unknown command {inputList[0]}");
            }
            Console.WriteLine("Terminating console...");
        }
        void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Log(message);
            Console.ResetColor();
        }
        void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Log(message);
            Console.ResetColor();
        }
        void Log(string Message) => Console.WriteLine(Message);
        
    }

    [Serializable]
    public sealed class ConsoleAleadyRunningException : Exception
    {
        public ConsoleAleadyRunningException() { }
        public ConsoleAleadyRunningException(string message) : base(message)
        {
        }

        private ConsoleAleadyRunningException(SerializationInfo serializationInfo, StreamingContext streamingContext):base(serializationInfo, streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
