using System;

namespace robot.Common
{
    public class CommandEventArgs : EventArgs
    {
        public string[] CommandArguments { get; set; }
    }
}
