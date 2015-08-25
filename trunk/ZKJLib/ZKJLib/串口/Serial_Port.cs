using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ZKJLib.串口
{
    class Serial_Port
    {
        private SerialPort port1;

        public void foo()
        {
            port1 = new SerialPort("COM1");  //串口名
            port1.ReceivedBytesThreshold = 9; //一次读入多少字节
            port1.RtsEnable = true; 

            port1.DataReceived +=new SerialDataReceivedEventHandler(port1_DataReceived);

            if (port1.IsOpen == false)
                    port1.Open();   ///打开串口
                                    ///
            port1.Write("dddd");
            port1.Close();
      
    }

    void  port1_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
         byte[] readBuffer = new byte[9];
         int count = port1.Read(readBuffer, 0, 9);

 	   // throw new NotImplementedException()
    }


}
