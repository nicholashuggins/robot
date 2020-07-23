using System;
using static robot.Common.Commands_en_au;
using static robot.Common.NESW_en_au;

namespace robot.GeoLocators
{
    /// <summary>
    /// This GeoLocator subclass deals with the commands specified for the ToyRobot challenge in English and with a 2 dimensional landscape (ie a table)
    /// Table size can be configured via the dimensions section in appsettings.json 
    /// </summary>
    public class GeoLocator_2d_NESW : GeoLocatorBase
    {
        private enum  Directions { North=0, East=90, South=180, West=270,Unknown };
        private Directions _currentDirection;
        private Vector _xPosition;
        private Vector _yPosition;
        private bool _isPlaced;
        private const string CommandIgnored = "Command ignored,";
        private const string OutOfBounds = "position is out of bounds.";

        public GeoLocator_2d_NESW(int xDimension , int yDimension )
        {
            _xPosition = new Vector(xDimension);
            _yPosition = new Vector(yDimension);
        }
        public override bool Place(string[] arguments, ref string errorMessage)
        {
            var previousX = _xPosition.Position;
            var previousY = _yPosition.Position;
            if (arguments.Length < 3)
            {
                return false;
            }
            if (SetDirection(arguments[2]) == false)
            {
                errorMessage = $"{CommandIgnored} '{arguments[2]}' is not a valid direction";
                return false;
            }
            _xPosition.Position = ConvertToPosition(arguments[0]);
            _yPosition.Position = ConvertToPosition(arguments[1]);
            if (_xPosition.IsValidPosition() == false || _yPosition.IsValidPosition() == false)
            {
                _xPosition.Position = previousX;
                _yPosition.Position = previousY;
                errorMessage = $"{CommandIgnored} {OutOfBounds}";
                return false;

            }
            _isPlaced = true;
            return true;
        }
        public override bool Move(ref string errorMessage)
        {
            if (IsPlaced(ref errorMessage) == false)
            {
                return false;
            }
            var result = true;
            switch (_currentDirection)
            {
                case Directions.North:
                    result = _yPosition.ShiftPosition(1);
                    break;
                case Directions.West:
                    result = _xPosition.ShiftPosition(-1);
                    break;
                case Directions.South:
                    result = _yPosition.ShiftPosition(-1);
                    break;
                case Directions.East:
                    result = _xPosition.ShiftPosition(1);
                    break;
                default:
                    break;
            }
            errorMessage = result ? "" : $"{CommandIgnored} {OutOfBounds}";
            return result;
        }
        public override bool Left(ref string errorMessage)
        {
            if (IsPlaced(ref errorMessage) == false)
            {
                return false;
            }
            ShiftDirection(-90);
            return true;
        }
        public override bool Right(ref string errorMessage)
        {
            if (IsPlaced(ref errorMessage) == false)
            {
                return false;
            }
            ShiftDirection(90);
            return true;
        }
        public override string Report()
        {
            var errorMessage = string.Empty;
            if (IsPlaced(ref errorMessage) == false)
            {
                return errorMessage;
            }
            return $"Output: {_xPosition.Position},{_yPosition.Position},{_currentDirection.ToString().Substring(0,1)}";
        }
        private void ShiftDirection(int value)
        {
            int direction = (int)_currentDirection;
            direction += value;
            if (direction > 270)
            {
                direction = 0;
            }
            else if (direction < 0)
            {
                direction = 270;
            }
            _currentDirection =(Directions)direction;
        }
        private int ConvertToPosition(string value)
        {
            int result = 0;
            try
            {
                result = Int32.Parse(value);
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }
        private bool SetDirection(string value)
        {
            var result = true;
            switch (value.ToUpper())
            {
                case North:
                    _currentDirection = Directions.North;
                    break;
                case West:
                    _currentDirection = Directions.West;
                    break;
                case South:
                    _currentDirection = Directions.South;
                    break;
                case East:
                    _currentDirection = Directions.East;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }
        private bool IsPlaced(ref string errorMessage)
        {
            if (_isPlaced)
            {
                return true;
            }
            errorMessage = $"{CommandIgnored} use {PlaceCmd} to set a start position.";
            return false;
        }
    }
}
