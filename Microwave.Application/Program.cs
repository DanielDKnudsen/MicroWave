using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Application
{
    class Program
    {
        private static Display _display;
        private static Timer _timer;
        private static CookController _cooker;
        private static Output _output;
        private static IPowerTube _powerTube;
        private static IUserInterface _ui;
        private static IButton _powerButton;
        private static IButton _timeButton;
        private static IButton _startCancelButton;
        private static IDoor _door;
        private static ILight _light;

        static void Main(string[] args)
        {
            _output = new Output();
            _powerTube = new PowerTube(_output);
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _door = new Door();
            _light = new Light(_output);
            _display = new Display(_output);
            _timer = new Timer();
            _cooker = new CookController(_timer, _display, _powerTube);
            _ui = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cooker);
            _cooker.UI = _ui;

            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();

            _timeButton.Press();
            _timeButton.Press();
            _timeButton.Press();
            _timeButton.Press();

            _startCancelButton.Press();

            System.Console.WriteLine("Tast enter når applikationen skal afsluttes");
            System.Console.ReadKey();
        }
    }
}
