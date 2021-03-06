﻿using System;
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
    public class Microwave_IntegrationTestStep1
    {
        // DENNE TESTKLASSE BLEV OVER-DO'ET EN DEL FORDI VI KASTEDE OS UD I DET FØR VI HAVDE FORSTÅET OPGAVEN KORREKT. Men Light virker i hvert fald! :)
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IDoor _door;
        private ICookController _cookController;
        private IDisplay _uut;
        private Light _light;
        private IOutput _output;

        private UserInterface _driver;


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
            _uut = Substitute.For<IDisplay>();
            _driver = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _uut, _light,
                _cookController);
        }

        [Test]
        public void Ready_DoorOpen_LightOn()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void Ready_DoorOpenDoorClosed_LightOff()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned off");
        }

        [Test]
        public void SetPower_DoorOpened_LightOn()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void SetTime_DoorOpened_LightOn()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void SetTime_StartButton_LightOn()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void SetTime_StartButton2Times_LightOff()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned off");
        }
    }
}
