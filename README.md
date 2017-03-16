RGB Shield for Netduino
=============
![RGB LCD Shield Kit](https://www.adafruit.com/images/1200x900/714-07.jpg)
This is a port of the Adafruit driver for the [Adafruit RGB LCD Shield](https://www.adafruit.com/products/714)

## License
Check the included [LICENSE.md](https://github.com/giawa/NetduinoRGBLCDShield/blob/master/LICENSE.md) file for the license associated with this code.

## Building the Project
The project includes a .sln and 2 .csproj files which will create a Netduino 2 program and a class library.  Both the .sln and .csproj are compatible with Visual Studio 2013 and later.

## Random Notes
I wrote this library a long time ago and only recently dusted it off.  I seem to remember getting a reference for the I2CDevice from somewhere, but I cannot track down where I found it.  I think I've modified it enough to avoid any copyright issues, and I remember it being open sourced.  If you are the original author or recognize your work please let me know for proper attribution.

## Alternatives
This [branch](https://github.com/JakeLardinois/rgbshield4netduino/tree/EventDrivenRGBShield4Netduino) provides an example on how to utilize Events (as opposed to a loop) to monitor button press feedback. It allows Main() to exit so that other events (such as Serial Port Data Recieved) can be fired.  Simply solder a wire to Pin 20 (INTA) of the [MCP23017](https://www.adafruit.com/products/732) chip and then connect it to a digital I/O port on the netduino.  The example code uses GPIO_PIN_D10 to register the interrupt and read the data containing which button was pressed.