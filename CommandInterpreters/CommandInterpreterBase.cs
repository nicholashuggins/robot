using System;
using robot.Common;

namespace robot.CommandInterpreters
{
    /// <summary>
    /// This class is responsible for interpreting user input and mapping it to a event based execution path.
    /// It is invoked from the RobotApplication class.
    /// To allow for future variation in the command set, language etc,  derived class(es) implement the fucntionality.
    /// </summary>
    public abstract class CommandInterpreterBase
    {
        public event EventHandler<CommandEventArgs> CommandMove;
        public event EventHandler<CommandEventArgs> CommandLeft;
        public event EventHandler<CommandEventArgs> CommandRight;
        public event EventHandler<CommandEventArgs> CommandPlace;
        public event EventHandler<CommandEventArgs> CommandReport;
        public abstract bool Execute(string command, ref string errorMsg);

        protected virtual void OnCommandMove(CommandEventArgs e)
        {
            CommandMove?.Invoke(this, e);
        }
        protected virtual void OnCommandPlace(CommandEventArgs e)
        {
            CommandPlace?.Invoke(this, e);
        }
        protected virtual void OnCommandLeft(CommandEventArgs e)
        {
            CommandLeft?.Invoke(this, e);
        }
        protected virtual void OnCommandRight(CommandEventArgs e)
        {
            CommandRight?.Invoke(this, e);
        }
        protected virtual void OnCommandReport(CommandEventArgs e)
        {
            CommandReport?.Invoke(this, e);
        }
    }
}
