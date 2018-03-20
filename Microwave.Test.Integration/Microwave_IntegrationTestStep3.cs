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
    [TestFixture]
    public class Microwave_IntegrationTestStep3
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IDoor _door;
        private CookController _uut;
        private Display _display;
        private Light _light;
        private IOutput _output;

        private UserInterface _driver;
        private ITimer _timer;
        private IPowerTube _powerTube;


        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();
            _uut = new CookController(_timer, _display, _powerTube, _driver);
            _display = new Display(_output);
            _driver = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _uut);
        }

        [Test]
        public void StartCooking_StartButton_CookingTimerIsCalled()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timer.Received().Start(60);
        }

        [Test]
        public void StartCooking_StartButton_PowerTubeIsCalled()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerTube.Received().TurnOn(50);
        }

        [Test]
        public void StopCooking_StopButton_CookingTimerStopped()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _timer.Received().Stop();
        }

        [Test]
        public void StopCooking_StopButton_CookingPowerTubeIsStopped()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _powerTube.Received().TurnOff();
        }
    }
}
