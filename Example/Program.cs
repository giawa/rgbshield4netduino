using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System;
using System.Threading;

namespace NetduinoRGBLCDShield
{
    public class Program
    {
        private static MCP23017 mcp23017;
        private static RGBLCDShield lcdBoard;
        private static OutputPort onboardLED = new OutputPort(Pins.ONBOARD_LED, false);
        private static InterruptPort btnShield { get; set; }


        public static void Main()
        {
            // the MCP is what allows us to talk with the RGB LCD panel
            mcp23017 = new MCP23017();

            // and this is a class to help us chat with the LCD panel
            lcdBoard = new RGBLCDShield(mcp23017);

            // we'll follow the Adafruit example code
            lcdBoard.Write("Hello, world!");
            lcdBoard.SetBacklight(BacklightColor.White);

            // get the time so that we can track # of seconds since power up
            DateTime time = DateTime.Now;

            lcdBoard.SetPosition(1, 0);
            // calculate the number of seconds since power on
            var seconds = (DateTime.Now - time).Ticks / TimeSpan.TicksPerSecond;
            lcdBoard.Write(seconds.ToString());

            // Setup the interrupt port
            btnShield = new InterruptPort(Pins.GPIO_PIN_D10, true, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeLow);
            // Bind the interrupt handler to the pin's interrupt event.
            btnShield.OnInterrupt += new NativeEventHandler(btnShield_OnInterrupt);
        }

        public static void btnShield_OnInterrupt(UInt32 data1, UInt32 data2, DateTime time)
        {
            var ButtonPressed = mcp23017.ReadGpioAB();
            var InterruptBits = BitConverter.GetBytes(ButtonPressed);


            lcdBoard.Clear();
            lcdBoard.SetPosition(0, 0);

            switch (InterruptBits[0]) //the 0 value contains the button that was pressed...
            {
                case (int)Button.Up:
                    lcdBoard.Write("UP ");
                    lcdBoard.SetBacklight(BacklightColor.Red);
                    break;
                case (int)Button.Down:
                    lcdBoard.Write("Down ");
                    lcdBoard.SetBacklight(BacklightColor.Yellow);
                    break;
                case (int)Button.Left:
                    lcdBoard.Write("Left ");
                    lcdBoard.SetBacklight(BacklightColor.Green);
                    break;
                case (int)Button.Right:
                    lcdBoard.Write("Right ");
                    lcdBoard.SetBacklight(BacklightColor.Teal);
                    break;
                case (int)Button.Select:
                    lcdBoard.Write("Select ");
                    lcdBoard.SetBacklight(BacklightColor.Violet);
                    break;
            }
        }
    }
}
