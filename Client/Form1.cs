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
using Models;
using System.Text.Json;

namespace Client
{
    public partial class Form1 : Form
    {
        private readonly int Ntimeout = 1000;
        int port;
        IPAddress ip;
        IPEndPoint ep;
        TcpClient client;
        List<MyTask> tasks;
        public Form1()
        {
            InitializeComponent();
            tasks = new List<MyTask>();
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
                ns.ReadTimeout = Ntimeout;
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(clientMessage);
                sw.Flush();
                StreamReader sr = new StreamReader(ns);
                string serverMessage = sr.ReadLine();
                //MessageBox.Show(serverMessage, "Server message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (serverMessage == "yes")
                {
                    connectButton.Enabled = false;
                    disconnectButton.Enabled = true;
                    delete_button.Enabled = true;
                    edit_button.Enabled = true;
                    add_button.Enabled = true;
                    tasks_button.Enabled = true;
                }
                else
                    MessageBox.Show("Authorization failed!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                sr.Close();
                sw.Close();
                ns.Close();
                client.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error!\r\n {err.Message}", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void tasks_button_Click(object sender, EventArgs e)
        {
            try
            {
                string mess = $"get#{loginTextBox.Text}";
                client = new TcpClient();
                client.Connect(ep);
                NetworkStream ns = client.GetStream();
                ns.ReadTimeout = Ntimeout;
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(mess);
                sw.Flush();

                StreamReader sr = new StreamReader(ns);
                mess = await sr.ReadLineAsync();
                tasks = JsonSerializer.Deserialize<List<MyTask>>(mess);
                taskList.DataSource = tasks;
                taskList.DisplayMember = "Title";
                sr.Close();
                sw.Close();
                ns.Close();
                client.Close();
            }catch(Exception)
            {
                MessageBox.Show("Serializing error.\r\nMaybe you are not authorized", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void taskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyTask selectedTask = taskList.SelectedItem as MyTask;
            taskTextBox.Text = selectedTask.Title;
            DescriptionTextBox.Text = selectedTask.About;
            StatusTextBox.Text = selectedTask.Status;
            startDateTextBox.Value = selectedTask.Start;
            endDateTextBox.Value = selectedTask.Finish;
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = true;
            disconnectButton.Enabled = false;
            delete_button.Enabled = false;
            edit_button.Enabled = false;
            add_button.Enabled = false;
            tasks_button.Enabled = false;


            try
            {
                client = new TcpClient();
                client.Connect(ep);

                NetworkStream ns = client.GetStream();
                ns.ReadTimeout = Ntimeout;
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine("disconnect");
                sw.Flush();

                sw.Close();
                ns.Close();
                client.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show($"{err.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            try
            {

                client = new TcpClient();
                client.Connect(ep);

                NetworkStream ns = client.GetStream();
                ns.ReadTimeout = Ntimeout;
                StreamWriter sw = new StreamWriter(ns);

                sw.WriteLine($"add#{loginTextBox.Text}");
                sw.Flush();

                StreamReader sr = new StreamReader(ns);
                sr.ReadLine();

                sr.Close();
                sw.Close();
                ns.Close();
                client.Close();
                tasks_button.PerformClick();
            }
            catch(Exception err)
            {
                MessageBox.Show($"{err.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            if (taskList.SelectedIndex == -1) {
                MessageBox.Show("Item not selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string taskName = taskTextBox.Text;
            string taskDescription = DescriptionTextBox.Text;
            DateTime startDate = startDateTextBox.Value;
            DateTime endDate = endDateTextBox.Value;
            string status = StatusTextBox.Text;
            string sendString = $"edit#{loginTextBox.Text}#{(taskList.SelectedItem as MyTask).Id}#{taskName}#{startDate}#{endDate}#{status}#{DescriptionTextBox.Text}";
            try
            {
                client = new TcpClient();
                client.Connect(ep);
                NetworkStream ns = client.GetStream();
                ns.ReadTimeout = Ntimeout;
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(sendString);
                sw.Flush();

                StreamReader sr = new StreamReader(ns);
                sr.ReadLine();

                sr.Close();
                
                sw.Close();
                ns.Close();
                client.Close();
                Console.WriteLine(2);
                tasks_button.PerformClick();
                Console.WriteLine(1);


            }
            catch (Exception err)
            {
                MessageBox.Show($"{err.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
