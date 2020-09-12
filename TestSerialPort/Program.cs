using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace TestSerialPort
{
    class Program
    {
        static async Task Main(string[] args)
        {
            SerialPort mySerialPort = new SerialPort("COM3");

            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;

            byte i = 0;
            bool f = false;
            mySerialPort.Open();
            while (true)
            {
                if (i > 7)
                {
                    i = 0;
                    f = !f;
                }

                i++;
                if (!f)
                {
                    mySerialPort.Write(new byte[] { i, 255, 255, 255 }, 0, 4);
                }
                else
                {
                    mySerialPort.Write(new byte[] { i, 0, 0, 0 }, 0, 4);
                }

                Console.WriteLine(mySerialPort.ReadExisting());
                await Task.Delay(10);
            }
            mySerialPort.Close();

        }
    }
}
