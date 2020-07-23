
namespace robot.GeoLocators
{
    public abstract class GeoLocatorBase
    {
        /// <summary>
        /// This class is responsible for simulating the Toy Robot actions in response to CommandInterpreter events 
        /// It is invoked from the CommandController class.
        /// To allow for future variation in the Command Set, shape of the 'table' etc, derived class(es) implement the functionality
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public abstract bool Place(string[] arguments, ref string errorMessage);
        public abstract bool Move(ref string errorMessage);
        public abstract bool Left(ref string errorMessage);
        public abstract bool Right(ref string errorMessage);
        public abstract string Report();
    }
}
