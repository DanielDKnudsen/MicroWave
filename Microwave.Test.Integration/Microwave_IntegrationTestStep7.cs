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
    public class Microwave_IntegrationTestStep7
    {
        private IDoor _door;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IUserInterface _userInterface;
        private ICookController _driver;
        private ILight _light;
        private IDisplay _display;
        private IPowerTube _uut;
        private ITimer _timer;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _door = Substitute.For<IDoor>();
            _powerButton = new Button();
            _timeButton = new Button();;
            _startCancelButton = new Button();
            _timer = Substitute.For<ITimer>();
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
            _display = new Display(_output);
            _uut = new PowerTube(_output);
            _driver = new CookController(_timer, _display, _uut);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light,
                _driver);
        }

        [Test]
        public void StartCooking_CookControllerOnPowerTube()
        {
            _driver.StartCooking(50,60);
            double power = (50 * 100) / 700;
            _output.Received().OutputLine("PowerTube works with "+ power +" %");
        }

        [Test]
        public void CookControllerOnPowerTube_MaxPower()
        {
            _driver.StartCooking(700,60);
            double power = 700 * 100 / 700;
            _output.Received().OutputLine("PowerTube works with " + power + " %");
        }

        [Test]
        public void CookControllerOnPowerTube_RangeException()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => _driver.StartCooking(750, 60));
        }

        [Test]
        public void CookControllerOnPowerTube_ApplicationException()
        {
            _driver.StartCooking(50,60);
            Assert.Throws(typeof(ApplicationException), () => _driver.StartCooking(50, 60));
        }

        [Test]
        public void CookControllerOnPowerTube_TurnOffWasCalledCorrect()
        {
            _driver.StartCooking(50,60);
            _driver.Stop();
            _output.Received().OutputLine("PowerTube turned off");
        }
    }
}
