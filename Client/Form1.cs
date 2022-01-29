using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace Client
{
    public partial class Form1 : Form
    {
        int port;
        IPAddress ip;
        IPEndPoint ep;
        TcpClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            ip = IPAddress.Parse(ipAddressTextBox.Text);
            port = int.Parse(portTextBox.Text);
            ep = new IPEndPoint(ip, port);
            try
            {
                string clientMessage = $"auth#{loginTextBox.Text}#{passwordTextBox.Text}";
                client = new TcpClient();
                client.Connect(ep);
                NetworkStream ns = client.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                sw.Write(clientMessage);
                sw.Close();
                ns.Close();
                client.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error!\r\n {err.Message}", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
