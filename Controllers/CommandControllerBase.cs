using System;
using robot.CommandInterpreters;
using robot.Common;
using robot.GeoLocators;

namespace robot.Controllers
{
    /// <summary>
    /// This class handle events raised by the CommandInterpreter and delegates the respective actions to the GeoLocator class
    /// To allow for future variation in the command set etc, derived class(es) implement the fucntionality.
    /// </summary>
    public abstract class CommandControllerBase
    {
        protected CommandInterpreterBase _commandInterpreter;
        protected GeoLocatorBase _geoLocator;
        public CommandControllerBase(CommandInterpreterBase commandInterpreter, GeoLocatorBase geoLocator)
        {
            _geoLocator = geoLocator;
            _commandInterpreter = commandInterpreter;
            _commandInterpreter.CommandMove += HandleMoveEvent;
            _commandInterpreter.CommandLeft += HandleLeftEvent;
            _commandInterpreter.CommandRight += HandleRightEvent;
            _commandInterpreter.CommandPlace += HandlePlaceEvent;
            _commandInterpreter.CommandReport += HandleReportEvent;
        }
        ~CommandControllerBase()
        {
            _commandInterpreter.CommandMove -= HandleMoveEvent;
            _commandInterpreter.CommandLeft -= HandleLeftEvent;
            _commandInterpreter.CommandRight -= HandleRightEvent;
            _commandInterpreter.CommandPlace -= HandlePlaceEvent;
            _commandInterpreter.CommandReport -= HandleReportEvent;
        }
        protected abstract void HandleMoveEvent(Object source, CommandEventArgs e);
        protected abstract void HandleLeftEvent(Object source, CommandEventArgs e);
        protected abstract void HandleRightEvent(Object source, CommandEventArgs e);
        protected abstract void HandlePlaceEvent(Object source, CommandEventArgs e);
        protected abstract void HandleReportEvent(Object source, CommandEventArgs e);
        protected virtual void LogMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
