using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class Microwave_Integration_TestStep8
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IDoor _door;
        private CookController _cooker;
        private Display _display;
        private Light _light;
        private IOutput _output;

        private UserInterface _driver;
        private ITimer _timer;
        private IPowerTube _powerTube;

        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cooker = new CookController(_timer, _display, _powerTube);
            _display = new Display(_output);
            _driver = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cooker);
            _cooker.UI = _driver;
        }

        [Test]
        public void StartCooking_StartTimer_TimerStarted()
        {

            ManualResetEvent pause = new ManualResetEvent(false);
            _driver.OnPowerPressed(this,EventArgs.Empty);
            _driver.OnTimePressed(this,EventArgs.Empty);
            _driver.OnStartCancelPressed(this,EventArgs.Empty);
            
            
            pause.WaitOne(61000);

            _output.Received().OutputLine("Display cleared");

        }
    }
}
