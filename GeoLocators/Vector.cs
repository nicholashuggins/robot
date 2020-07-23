
namespace robot.GeoLocators
{
    /// <summary>
    /// This class encapsulates the logic for moving along a fixed length axis
    /// </summary>
    public class Vector
    {
        private int _maxBounds;
        public Vector(int VectorLength)
        {
            _maxBounds = VectorLength -1 ; // Zero based vector counting is used
        }
        public int Position { get; set; }
        public bool ShiftPosition(int value)
        {
            var result = true;
            Position += value;
            if (Position < 0)
            {
                Position = 0;
                result = false;
            }
            else if (Position > _maxBounds)
            {
                Position = _maxBounds;
                result = false;
            }
            return result;
        }
        public bool IsValidPosition()
        {
            return (Position >= 0 && Position <= _maxBounds) ? true : false;
        }

    }
}
