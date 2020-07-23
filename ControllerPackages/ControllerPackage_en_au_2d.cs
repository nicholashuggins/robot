using Microsoft.Extensions.Configuration;
using robot.CommandInterpreters;
using robot.Controllers;
using robot.GeoLocators;

namespace robot.ControllerPackages
{
    /// <summary>
    /// This class provides an Abstract factory function, creating a package comprising 
    ///  - a CommandInterpreter
    ///  - a CommandController
    ///  - and a GeoLocator
    ///  to suit a given set of Commands, Language and Landscape.
    ///  Future packages for example might use a different language, target say a 3 dimensional landscape or incorporate more advanced commands.
    /// </summary>
    public class ControllerPackage_en_au_2d : IControllerPackage
    {
        private const int DefaultDimension = 5;
        private CommandInterpreterBase _commandInterpreter;
        private CommandControllerBase _commandController;
        private IConfigurationRoot _configuration;
        private GeoLocatorBase _geoLocator;
        private int _xDimension;
        private int _yDimension;
        public ControllerPackage_en_au_2d(IConfigurationRoot configuration)
        {
            _configuration = configuration;
            GetAndSetDimensions();
            _geoLocator = new GeoLocator_2d_NESW(_xDimension,_yDimension);
            _commandInterpreter = new CommandInterpreter_en_au();
            _commandController = new CommandController_en_au(_commandInterpreter,_geoLocator );
       }
        public CommandInterpreterBase GetInterpreter()
        {
            return _commandInterpreter;
        }
        public CommandControllerBase GetController()
        {
            return _commandController;
        }
        private void GetAndSetDimensions()
        {
            UseDefaultForMissingConfig(ref _xDimension, "dimensions:x", DefaultDimension);
            UseDefaultForMissingConfig(ref _yDimension, "dimensions:y", DefaultDimension);
        }
        private void UseDefaultForMissingConfig(ref int dimension ,string configPath, int DefaultValue)
        {
            try
            {
                var configValue = _configuration.GetValue<int>(configPath);
                dimension = configValue <= 0 ? DefaultDimension : configValue;
            }
            catch (System.Exception)
            {
                dimension = DefaultDimension;
            }
        }

    }
}
