/*
 * C-Sharp mono sample application that reads from the raspicomm's RS-4864 
*/

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Rs485Read
{
  class Program
  {
    private const string Rs485Device = "/dev/ttyRPC0";
    private const int ReadBufferSize = 10;
    private static byte[] ReadBuffer = new byte[ReadBufferSize];

    static int Main(string[] args)
    {
      Console.WriteLine("this sample application reads from the rs-485 port");

      using (SerialPort serialPort = new SerialPort(Rs485Device))
      {
        /* try to open the port */
        Console.Write("opening device {0}...", Rs485Device);
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

        /* read in a loop */
        Console.WriteLine("start reading from the rs-485 port a maximum of {0} bytes", ReadBufferSize);
        int readByte, i = 0;
        while ((readByte = serialPort.ReadByte()) != -1 &&  /* try to read only 1 byte at once */
                i < ReadBufferSize)                         /* read until the buffer is full */
        {
          ReadBuffer[i++] = (byte)readByte; /* store it in the read buffer */
        }

        /* print the received bytes */
        Console.WriteLine("we received the following bytes:");
        for (i = 0; i < ReadBufferSize; i++)
        {
          Console.Write("[{0}]: 0x{1:X2}", i, ReadBuffer[i]);
          if (ReadBuffer[i] >= 32 && ReadBuffer[i] <= 126)    /* if the character is a printable ascii code, print it to the stdout */
            Console.Write(" - '{0}'", (char)ReadBuffer[i]);
          Console.WriteLine();
        }

        /* close the device again (end of 'using' statement) */
        Console.WriteLine("closing the device again");
      }

      return 0;
    }
  }
}
