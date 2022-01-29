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
using System.Xml.Linq;

namespace Server
{
    public partial class Form1 : Form
    {
        IPAddress ip;
        int port;
        IPEndPoint ep;
        TcpListener listener;
        Thread listenerThread;

        string path1;
        XDocument doc1;

        public Form1()
        {
            InitializeComponent();
            path1 = @"..\..\Data\accounts.xml";
            doc1 = XDocument.Load(path1);
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
                try
                {
                    if (parts.Length > 0)
                    {
                        string command = parts[0];
                        if (command == "auth")
                        {
                            Journal.Text += "AUTH-mode activated\r\n";
                            string login = parts[1];
                            string passw = parts[2];
                            Journal.Text += $"{login} / {passw}\r\n";

                            var account = doc1.Element("root").Elements("account").Where(a=>a.Attribute("login").Value==login && a.Attribute("password").Value==passw).FirstOrDefault();
                            string serverMessage = "";
                            if (account != null)
                                serverMessage = "yes";
                            else
                                serverMessage = "no";
                        }
                        else if (command == "get")
                        {

                        }
                        else if (command == "add")
                        {

                        }
                        else if (command == "edit")
                        {

                        }
                        else if (command == "delete")
                        {

                        }
                    }
                }catch(Exception er)
                {
                    MessageBox.Show($"Error!\r\n{er.Message}", "Reciving error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
