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
using NUnit.Framework.Internal;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class Microwave_IntegrationTestStep2
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IDoor _door;
        private ICookController _cookController;
        private Display _display;
        private Light _light;
        private IOutput _output;

        private UserInterface _uut;


        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _cookController = Substitute.For<ICookController>();
            _display = new Display(_output);
            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light,
                _cookController);
        }

        [Test]
        public void Test_ShowPower_PowerIsShown()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Display shows: 100 W");

        }

        [Test]
        public void Test_ShowTime_TimeIsShown()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Display shows: 01:00");
        }

        [Test]
        public void Test_ClearDisplay_DisplayIsClear()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Display cleared");
        }
    }
}
