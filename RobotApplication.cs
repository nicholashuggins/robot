using System;
using robot.Controllers;
using robot.ControllerPackages;
using static robot.Common.Commands_en_au;

namespace robot
{
    /// <summary>
    /// The Console Application responsible for capturing user input and message output
    /// A built in test can alternatively be run by supplying a single command line parameter of 'script'
    /// </summary>
    public class RobotApplication
    {
        private IControllerPackage _controllerPackage;
        private CommandControllerBase _controller;
        public RobotApplication(IControllerPackage controllerPackage)
        {
            _controllerPackage = controllerPackage;
        }
        public void Run(string[] args)
        {
            var _controller = _controllerPackage.GetController();
            if (args.Length > 0 && args[0] == "script")
            {
                RunRobotScript();
                return;
            }
            RunRobotCmdLine();
        }
        private void RunRobotCmdLine()
        {
            Console.WriteLine("Welcome to Robot Controller, enter a command to continue, or Press Enter to exit.");
            var command = Console.ReadLine();
            while (string.IsNullOrEmpty(command) == false)
            {
                RunCommand(command);
                command = Console.ReadLine();
            }

        }
        private void RunRobotScript()
        {
            string errorMsg = string.Empty;
            Console.WriteLine("Placing at 0,0,E");
            RunCommand( $"{PlaceCmd} 0,0,E");
            RunCommand( ReportCmd);
            Console.WriteLine("Moving east to right side of table");
            MoveToEdge();
            Console.WriteLine("Trying to step off the table");
            RunCommand( MoveCmd);
            RunCommand( ReportCmd);
            RunCommand( LeftCmd);
            Console.WriteLine("Moving north to top of table");
            MoveToEdge();
            Console.WriteLine("Trying to step off the table");
            RunCommand( MoveCmd);
            RunCommand( ReportCmd);
            RunCommand( LeftCmd);
            Console.WriteLine("Moving west to left side of table");
            MoveToEdge();
            Console.WriteLine("Trying to step off the table");
            RunCommand( MoveCmd);
            RunCommand( ReportCmd);
            RunCommand( LeftCmd);
            Console.WriteLine("Moving south to starting point");
            MoveToEdge();
            Console.WriteLine("Trying to step off the table");
            RunCommand( MoveCmd);
            RunCommand( ReportCmd);
        }

        private void MoveToEdge()
        {
            string errorMsg = string.Empty;
            RunCommand( MoveCmd);
            RunCommand( MoveCmd);
            RunCommand( MoveCmd);
            RunCommand( MoveCmd);
            RunCommand( ReportCmd);
        }
        private void RunCommand(string command)
        {
            string errorMsg = string.Empty;
            if (Execute(command, ref errorMsg) == false || string.IsNullOrEmpty(errorMsg) == false)
            {
                Console.WriteLine(errorMsg);
            }
        }
        private bool Execute(string commandArgs, ref string errorMsg)
        {
            errorMsg = string.Empty;
            var commandInterpreter = _controllerPackage.GetInterpreter();
            return commandInterpreter.Execute(commandArgs, ref errorMsg);
        }
    }
}
