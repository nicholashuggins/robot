using robot.CommandInterpreters;
using robot.Controllers;

namespace robot.ControllerPackages

{
    public interface IControllerPackage
    {
        public CommandInterpreterBase GetInterpreter();
        public CommandControllerBase GetController();
    }
}
