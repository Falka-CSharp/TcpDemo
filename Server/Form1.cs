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
using System.Threading;

namespace Server
{
    public partial class Form1 : Form
    {
        IPAddress ip;
        int port;
        IPEndPoint ep;
        TcpListener listener;
        Thread listenerThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                ip = IPAddress.Parse(ipAddressTextBox.Text);
                port = int.Parse(portTextBox.Text);
                ep = new IPEndPoint(ip, port);
                listener = new TcpListener(ep);
                listener.Start();
                listenerThread = new Thread(new ThreadStart(ListenerWorkItem));
                listenerThread.IsBackground = true;
                listenerThread.Start();
            }catch(Exception er)
            {
                MessageBox.Show($"Error\r\n{er.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void ListenerWorkItem()
        {
            while (true)
            {
                TcpClient acceptor = listener.AcceptTcpClient();
                NetworkStream ns = acceptor.GetStream();
                StreamReader sr = new StreamReader(ns);
                string clientMessage = sr.ReadToEnd();

                string[] parts = clientMessage.Split('#');
                if (parts.Length > 2) {
                    string command = parts[0];
                    if(command == "auth")
                    {
                        Journal.Text += "AUTH-mode activated\r\n";

                    }else if (command == "get")
                    {

                    }else if(command == "add")
                    {

                    }else if(command == "edit")
                    {

                    }else if (command == "delete")
                    {

                    }
                }
                sr.Close();
                ns.Close();
                acceptor.Close();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(listener != null)
            {
                listenerThread.Suspend();
                listener.Stop();
            }
        }
    }
}
