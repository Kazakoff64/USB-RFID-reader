using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

namespace rfid
{
    public partial class Form1 : Form
    {

        SerialPort sp = new SerialPort();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                     
            sp.PortName = comboComList.Text;
            sp.BaudRate = 38400;
            sp.Parity = Parity.None;
            sp.DataBits = 8;
            sp.StopBits = StopBits.One;
            sp.Open();

            //---------------------------------------------------------------------------------------

            Thread.Sleep(40);

            byte[] bytestosend = { 0xAA, 0xDD, 0x00, 0x03, 0x01, 0x02, 0x03 };
            sp.Write(bytestosend, 0, bytestosend.Length);

            Thread.Sleep(1);

            byte[] bytestosend2 = { 0xAA, 0xDD, 0x00, 0x04, 0x01, 0x03, 0x0A, 0x08 };
            sp.Write(bytestosend2, 0, bytestosend2.Length);


        }


        void RFRead() {
        

        
        }

        private void button2_Click(object sender, EventArgs e)
        {


            while (true)
            {

                Thread.Sleep(100);

                byte[] bytestosend4 = { 0xAA, 0xDD, 0x00, 0x03, 0x01, 0x0C, 0x0D };
                sp.Write(bytestosend4, 0, bytestosend4.Length);

                Thread.Sleep(60);

                byte[] buffer = new byte[56];
                int recv = sp.Read(buffer, 0, buffer.Length);

                if (buffer[0].ToString() == "170" && buffer[1].ToString() == "221" && buffer[2].ToString() == "0" && buffer[3].ToString() == "9")
                {
                    if (buffer[7].ToString() != "0" && buffer[12].ToString() != "0" && buffer[11].ToString() != "0" && buffer[10].ToString() != "0")
                    {

                        string line = buffer[7].ToString() + " " + buffer[8].ToString() + " " + buffer[9].ToString() + " " + buffer[10].ToString() + " " + buffer[11].ToString() + " " + buffer[12].ToString();
                        textBox1.Text = line;


                        byte[] bytestosend2 = { 0xAA, 0xDD, 0x00, 0x04, 0x01, 0x03, 0x0A, 0x08 };
                        sp.Write(bytestosend2, 0, bytestosend2.Length);


                        break;

                    }
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
     
            //sp.Close();
            //sp.Dispose();
            //sp = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();

            foreach (String port in ports) {

                comboComList.Items.Add(port);

            }

        }
    }
}
