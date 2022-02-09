﻿using System;
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
using Models;
using System.Text.Json;
using Server.Models;
namespace Server
{
    public partial class Form1 : Form
    {
        IPAddress ip;
        int port;
        IPEndPoint ep;
        TcpListener listener;
        Thread listenerThread;
        DataManager dm;
        string path1;
        XDocument doc1;
        List<ClientAuthorizationIpLogin> authorizedClients;
        public Form1()
        {
            InitializeComponent();
            path1 = @"..\..\Data\accounts.xml";
            doc1 = XDocument.Load(path1);
            authorizedClients = new List<ClientAuthorizationIpLogin>();
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

                dm = new DataManager();
                dm.InitData();
                Journal.Text += DateTime.Now + " > Data loaded!\r\n";
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
                StreamWriter sw = new StreamWriter(ns);
                //string clientMessage = sr.ReadToEnd();
                string clientMessage = sr.ReadLine();
                string clientIp = ((IPEndPoint)(acceptor.Client.RemoteEndPoint)).Address.ToString();
                string[] parts = clientMessage.Split('#');
                try
                {
                    if (parts.Length > 1)
                    {
                        string command = parts[0];
                        if (command == "auth")
                        {
                            if (Journal.InvokeRequired)
                                Journal.Invoke(new MethodInvoker(delegate { Journal.Text += "AUTH-mode activated\r\n"; }));
                            else
                                Journal.Text += "AUTH-mode activated\r\n";

                            string login = parts[1];
                            string passw = parts[2];

                            if (Journal.InvokeRequired)
                            {
                                Journal.Invoke(new Action(() =>
                                {
                                    Journal.Text += $"{login} / {passw}\r\n";
                                }));
                            }else
                                Journal.Text += $"{login} / {passw}\r\n";


                            var account = doc1.Element("root").Elements("account").Where(a=>a.Attribute("login").Value==login && a.Attribute("password").Value==passw).FirstOrDefault();
                            string serverMessage = "";

                            if (account != null)
                                serverMessage = "yes";
                            else
                                serverMessage = "no";

                            sw.WriteLine(serverMessage);
                            sw.Flush();
                            if (serverMessage == "yes")
                            {
                                Journal.Invoke(new MethodInvoker(delegate { Journal.Text += "Client authorized\r\n"; }));
                               
                                if (authorizedClients.Where(a => a.Ip == clientIp && parts[1] == a.Username).ToList().Count == 0)
                                    authorizedClients.Add(new ClientAuthorizationIpLogin() { Ip = clientIp,Username = parts[1] });
                            }
                            else
                                Journal.Invoke(new MethodInvoker(delegate { Journal.Text += "Authorization failed\r\n"; }));
                       
                        }
                        else
                        {
                            Console.WriteLine(authorizedClients.Count);
                            Console.WriteLine(authorizedClients[0].Ip);
                            Console.WriteLine(authorizedClients[0].Username);
                            if (authorizedClients.Where(a => clientIp == a.Ip && parts[1] == a.Username).ToList().Count == 0)
                            {
                                Journal.Invoke(new Action(() => { Journal.Text += $"{clientIp} - connection refused / authorizaion required\r\n"; }));
                                sw.WriteLine("You are not authorized!");
                                sw.Flush();
                                break;
                            }
                        
                            if (command == "get")
                            {
                                //Send tasks list
                                Journal.Invoke(new Action(() => { Journal.Text += $"GET-mode activated - {clientMessage}\r\n"; }));
                                List<MyTask> userTasks = dm.Repos.MyTasks.Where(t => t.User == parts[1]).ToList();
                                string json = JsonSerializer.Serialize(userTasks);
                                sw.WriteLine(json);
                                sw.Flush();
                            
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
                    }
                }catch(Exception er)
                {
                    MessageBox.Show($"Error!\r\n{er.Message}", "Reciving error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                sw.Close();
                sr.Close();
                ns.Close();
                acceptor.Close();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(listener != null)
            {
                try
                {
                    listenerThread.Suspend();
                    listener.Stop();
                }catch(Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
        }
    }
}
