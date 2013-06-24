/*
 *  C-Sharp Mono sample application that writes to the raspicomm's RS-485 Port
*/

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Rs485Write
{
  class Program
  {
    private const string Rs485Device = "/dev/ttyRPC0";
    private const string MessageToSend = "Hello World!";

    static int Main(string[] args)
    {
      Console.WriteLine("this sample application writes to the rs-485 port");

      using (SerialPort serialPort = new SerialPort(Rs485Device))
      {
        Console.Write(string.Format("opening device {0}...", Rs485Device));

        /* try to open the rs-485 port */
        try { serialPort.Open(); }
        catch
        {
          Console.WriteLine("failed.");
          Console.WriteLine("possible causes:");
          Console.WriteLine("1) the raspicomm device driver is not loaded. type 'lsmod' and verify that you 'raspicommrs485' is loaded.");
          Console.WriteLine("2) the raspicomm device driver is in use. Is another application using the device driver?");
          Console.WriteLine("3) something went wrong when loading the device driver. type 'dmesg' and check the kernel messages");
          return -1;
        }
        Console.WriteLine("successful.");

        /* configure the port */
        try { serialPort.BaudRate = 9600; }
        catch
        {
          Console.WriteLine("error configuring the rs-485 port.");
          return -2;
        };

        /* example write */
        Console.WriteLine(string.Format("writing the message: ({0}) {1} to the rs-485 port", MessageToSend.Length, MessageToSend));
        try { serialPort.Write(MessageToSend); }
        catch 
        { 
          Console.WriteLine("writing to the serial port failed."); 
          return -3; 
        }

        /* close the device again (end of 'using' statement) */
        Console.WriteLine("closing the device again");
      }

      return 0;
    }
  }
}
