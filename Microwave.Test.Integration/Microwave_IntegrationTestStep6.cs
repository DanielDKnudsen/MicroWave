using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    class Microwave_IntegrationTestStep6
    {
        private IDoor _door;
        private IButton _powerButton;
        private IButton _startCancelButton;
        private IButton _timeButton;
        private UserInterface _userInterface;
        private CookController _cookController;
        private Light _light;
        private Display _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IOutput _output;

        [SetUp]
        public void Setup()
        {
            _door = new Door();
            _powerButton = new Button();
            _startCancelButton = new Button();
            _timeButton = new Button();
            _output = Substitute.For<IOutput>();
            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();
            _display = new Display(_output);
            _light = new Light(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _userInterface;
        }

        [Test]
        public void CookController_TimerExpired_CookingIsDone()
        {
            _userInterface.OnPowerPressed(this, EventArgs.Empty);
            _userInterface.OnTimePressed(this, EventArgs.Empty);
            _userInterface.OnStartCancelPressed(this, EventArgs.Empty);

            
            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            _output.Received().OutputLine("Display cleared");
            _output.Received().OutputLine("Light is turned off");
        }
    }
}
