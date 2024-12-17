using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        public Action<string> showReceived;
        public Action<string> showSended;
        public Client client;
        public Form1()
        {
            InitializeComponent();
            Text = "client";
            showReceived = (string msg) =>
            {
                textBox1.Text += msg + Environment.NewLine;
            };
            showSended = (string msg) =>
            {
                textBox1.Text += msg + Environment.NewLine;
            };
            client = new Client();
            client.Connect();
            client.SendMessage("/start", showSended);
        }
        public async void ConnectButtonEvent(object sender, EventArgs e)
        {
            client.StartReceiving(showReceived);
        }
        public void SendMessageEvent(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                client.SendMessage(textBox2.Text, showSended);
                textBox2.Text = string.Empty;
            }
        }

    }
    public class Client
    {

        public IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 8081);
        public UdpClient client = new UdpClient();
        public Client()
        {
            client = new UdpClient();
        }
        public async Task StartReceiving(Action<string> writeMsg)
        {
            await Task.Run(() =>
            {
                while(true)
                {
                    var result = client.ReceiveAsync();
                    var message = Encoding.UTF8.GetString(result.Result.Buffer);
                    writeMsg.Invoke(message);
                }
            });
        }
        public async void SendMessage(string message, Action<string> writeSended)
        {
            var requestBytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(requestBytes);
        }
        public async void Connect()
        {
            try
            {
                client.Connect(IPAddress.Loopback, 8080);
            }
            catch { }
        }
    }
}
