using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{

    public partial class Form1 : Form
    {
        public Action<string> showReceived;
        public Action<string> showSended;
        public Server server;
        public Form1()
        {
            InitializeComponent();
            Text = "server";
            showReceived = (string msg) =>
            {
                textBox1.Text += msg + Environment.NewLine;
            };
            showSended = (string msg) =>
            {
                textBox1.Text += msg + Environment.NewLine;
            };
            server = new Server();  
        }
        public async void StartReceivingButton(object sender, EventArgs e) => server.StartListening(showReceived);
        public async void SendMessageEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                server.SendMessageAsync(textBox2.Text, showSended);
                textBox2.Text =string.Empty;
            }
        }
    }
    public class Server
    {
        public UdpClient server;
        public List<IPEndPoint> clients;
        public Server()
        {
            server = new UdpClient(new IPEndPoint(IPAddress.Loopback, 8080));
            clients = new List<IPEndPoint>();
        }
        public async Task StartListening(Action<string> showMåssage)
        {
            await Task.Run(() =>
            {
                while(true)
                {
                    var result = server.ReceiveAsync();
                    if(!clients.Contains(result.Result.RemoteEndPoint)) clients.Add(result.Result.RemoteEndPoint);
                    var message = Encoding.UTF8.GetString(result.Result.Buffer);

                    if (message == "/start") continue;
                    else showMåssage.Invoke("Êëèåíò: " + message);

                    foreach(var clientEndPoint in clients)
                    {
                        if(clientEndPoint != result.Result.RemoteEndPoint) SendMessageAsync("Êëèåíò: "+message, clientEndPoint);
                    }
                }
            });
        }
        public async void SendMessageAsync(string message, Action<string> showMåssage)
        {
            var responce = Encoding.UTF8.GetBytes("Ñåðâåð: " + message);
            foreach(var clientEndPoint in clients)
            {
                _ = server.SendAsync(responce, clientEndPoint);
            }
            showMåssage.Invoke("Ñåðâåð: " + message);
        }
        public async void SendMessageAsync(string message, IPEndPoint clientEndPoint)
        {
            var responce = Encoding.UTF8.GetBytes(message);
            _ = server.SendAsync(responce, clientEndPoint);
        }
    }
}
