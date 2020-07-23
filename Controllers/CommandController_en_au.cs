using System;
using robot.CommandInterpreters;
using robot.Common;
using robot.GeoLocators;

namespace robot.Controllers
{
    public class CommandController_en_au :CommandControllerBase
    {
        public CommandController_en_au(CommandInterpreterBase commandInterpreter, GeoLocatorBase geoLocator) : base(commandInterpreter, geoLocator) 
        {
        }

        protected override void HandleMoveEvent(Object source, CommandEventArgs e)
        {
            var errorMsg = string.Empty;
            if( _geoLocator.Move(ref errorMsg) == false)
            {
                LogMessage(errorMsg);
            }
        }
        protected override void HandleLeftEvent(Object source, CommandEventArgs e)
        {
            var errorMsg = string.Empty;
            if (_geoLocator.Left(ref errorMsg) == false)
            {
                LogMessage(errorMsg);
            }

        }
        protected override void HandleRightEvent(Object source, CommandEventArgs e)
        {
            var errorMsg = string.Empty;
            if (_geoLocator.Right(ref errorMsg) == false)
            {
                LogMessage(errorMsg);
            }

        }
        protected override void HandlePlaceEvent(Object source, CommandEventArgs e)
        {
            var errorMsg = string.Empty;
            if (_geoLocator.Place(e.CommandArguments,ref errorMsg) == false)
            {
                LogMessage(errorMsg);
            }

        }
        protected override void HandleReportEvent(Object source, CommandEventArgs e)
        {
            LogMessage(_geoLocator.Report());
        }
    }
}
