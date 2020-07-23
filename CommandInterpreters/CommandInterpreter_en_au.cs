using System;
using System.Linq;
using System.Text;
using robot.Common;
using static robot.Common.Commands_en_au;

namespace robot.CommandInterpreters
{
    /// <summary>
    /// This CommandInterpreter subclass deals with the commands specified for the ToyRobot challenge in English
    /// </summary>
    public class CommandInterpreter_en_au : CommandInterpreterBase
    {
        private string[] _args = new string[3];
        private string _command;

        private string[] ValidCommands = new string[5] { MoveCmd, LeftCmd, RightCmd, PlaceCmd, ReportCmd };
        public override bool Execute(string command, ref string errorMsg)
        {
            if (ParseCommand(command) == false)
            {
                errorMsg = GetCommandHelp();
                return false;
            }
            if (CheckCommandName() == false)
            {
                errorMsg = GetCommandHelp();
                return false;
            }
            if (ExecuteCommand() == false)
            {
                errorMsg = GetCommandHelp();
                return false;
            }
            return true;
        }
        protected override void OnCommandMove(CommandEventArgs e)
        {
            base.OnCommandMove(e);
        }
        protected override void OnCommandPlace(CommandEventArgs e)
        {
            base.OnCommandPlace(e);
        }
        protected override void OnCommandLeft(CommandEventArgs e)
        {
            base.OnCommandLeft(e);
        }
        protected override void OnCommandRight(CommandEventArgs e)
        {
            base.OnCommandRight(e);
        }
        protected override void OnCommandReport(CommandEventArgs e)
        {
            base.OnCommandReport(e);
        }
        private bool ExecuteCommand()
        {
            var result = true;
            if (_command==PlaceCmd)
            {
                if (CheckArgsArePresent() == false)
                {
                    return false;
                }
                var args = new CommandEventArgs { CommandArguments = _args };
                OnCommandPlace(args);
            }
            else if (_command == MoveCmd)
            {
                if (CheckArgsAreNull()== false)
                {
                    return false;
                }
                OnCommandMove(null);
            }
            else if (_command == LeftCmd)
            {
                if (CheckArgsAreNull() == false)
                {
                    return false;
                }
                OnCommandLeft(null);
            }
            else if (_command == RightCmd)
            {
                if (CheckArgsAreNull() == false)
                {
                    return false;
                }
                OnCommandRight(null);

            }
            else if (_command == ReportCmd)
            {
                if (CheckArgsAreNull() == false)
                {
                    return false;
                }
                OnCommandReport(null);
            }
            else
            {
                result = false;
            }
            return result;
        }

        private bool ParseCommand(string command)
        {
            var words = command.Trim().Replace(",", " ").Split(" ",StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0 || words.Length > 4)
            {
                return false;
            }
            _command = GetArgument(words, 0);
            _args[0] = GetArgument(words, 1);
            _args[1] = GetArgument(words, 2);
            _args[2] = GetArgument(words, 3);
            return true;
        }
        private string GetArgument(string[] values, int index)
        {
            if( index < values.Length )
            {
                return values[index].ToLower();
            }
            return string.Empty;
        }
        private bool CheckCommandName()
        {
            if (string.IsNullOrEmpty(_command))
            {
                return false;
            }
            if (ValidCommands.FirstOrDefault(c => c == _command)== null)
            {
                return false;
            }
            return true;
        }
        private bool CheckArgsAreNull()
        {
            return _args.Any(a => string.IsNullOrEmpty(a) == false) ? false : true;
        }
        private bool CheckArgsArePresent()
        {
            return _args.Any(a => string.IsNullOrEmpty(a) == true) ? false : true;
        }

        private string GetCommandHelp()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Please provide a valid command");
            sb.AppendLine("Valid commands:");
            sb.AppendLine($"\t{PlaceCmd} X,Y,F");
            sb.AppendLine($"\t{MoveCmd}");
            sb.AppendLine($"\t{LeftCmd}");
            sb.AppendLine($"\t{RightCmd}");
            sb.AppendLine($"\t{ReportCmd}");
            return sb.ToString();
        }

    }
}
