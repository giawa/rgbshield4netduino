using Microsoft.SPOT.Hardware;
using System;
using System.Threading;

namespace NetduinoRGBLCDShield
{
    public class Program
    {
        private static MCP23017 mcp23017;
        private static RGBLCDShield lcdBoard;

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

            Button currentButtons = 0;

            while (true)
            {
                lcdBoard.SetPosition(1, 0);

                // calculate the number of seconds since power on
                var seconds = (DateTime.Now - time).Ticks / TimeSpan.TicksPerSecond;
                lcdBoard.Write(seconds.ToString());

                Button buttons = lcdBoard.ReadButtons();

                // only update the screen if a new button is pressed
                if (buttons != currentButtons && buttons != 0)
                {
                    lcdBoard.Clear();
                    lcdBoard.SetPosition(0, 0);

                    if ((buttons & Button.Up) != 0)
                    {
                        lcdBoard.Write("UP ");
                        if (buttons == Button.Up) lcdBoard.SetBacklight(BacklightColor.Red);
                    }
                    if ((buttons & Button.Down) != 0)
                    {
                        lcdBoard.Write("DOWN ");
                        if (buttons == Button.Down) lcdBoard.SetBacklight(BacklightColor.Yellow);
                    }
                    if ((buttons & Button.Right) != 0)
                    {
                        lcdBoard.Write("RIGHT ");
                        if (buttons == Button.Right) lcdBoard.SetBacklight(BacklightColor.Green);
                    }
                    if ((buttons & Button.Left) != 0)
                    {
                        lcdBoard.Write("LEFT ");
                        if (buttons == Button.Left) lcdBoard.SetBacklight(BacklightColor.Teal);
                    }
                    if ((buttons & Button.Select) != 0)
                    {
                        lcdBoard.Write("SELECT ");
                        if (buttons == Button.Select) lcdBoard.SetBacklight(BacklightColor.Violet);
                    }

                    currentButtons = buttons;
                }
            }
        }
    }
}
