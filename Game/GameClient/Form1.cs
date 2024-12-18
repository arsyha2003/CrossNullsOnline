using Microsoft.VisualBasic.ApplicationServices;
using Pratice3Server;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpLientPratice
{
    public partial class Form1 : Form
    {
        public int[,] board = new int[3, 3];
        public TcpClient client;
        public NetworkStream stream;

        public IPEndPoint point;
        public int side;
        public Button[,] buttons;
        public int x;
        public int y;
        public Action<IPEndPoint> connectToServer;
        public Form2 connectionForm = null;
        public Form1()
        {
            InitializeComponent();
            point = new IPEndPoint(IPAddress.Loopback, 7777);
            client = new TcpClient();
            buttons = new Button[,]
            {
                {button1,button2, button3},
                {button4,button5, button6},
                {button7,button8, button9}
            };
            connectToServer = (IPEndPoint point) =>
            {
                try
                {
                    client.Connect(point);
                    stream = client.GetStream();
                    Task.Run(() => ReceiveMessagesAsync());
                    MessageBox.Show($"Подключено к {point}");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); return; }
            };
            button11.Visible = false;
            button15.Visible = false;
            button16.Visible = false;
        }
        public void SelectServer(object sender, EventArgs e)
        {
            connectionForm = new Form2(connectToServer);
            connectionForm.Show();
            button12.Visible = false;
        }
        public void ChangeButtonsValue(string[] tmp)
        {
            board = new int[,]
            {
                {int.Parse(tmp[0]),int.Parse(tmp[1]),int.Parse(tmp[2])},
                {int.Parse(tmp[3]), int.Parse(tmp[4]), int.Parse(tmp[5])},
                {int.Parse(tmp[6]), int.Parse(tmp[7]), int.Parse(tmp[8])}
            };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 2)
                    {
                        buttons[i, j].Text = "X";
                    }
                    if (board[i, j] == 1)
                    {
                        buttons[i, j].Text = "0";
                    }
                    if (board[i, j] == 0)
                    {
                        buttons[i, j].Text = string.Empty;
                    }
                }
            }
        }
        public void OfferADraw(object sender, EventArgs e)
        {
            stream.Write(Encoding.UTF8.GetBytes("/draw" + '\n'));
        }
        public void AgreeDraw(object sender, EventArgs e)
        {
            stream.Write(Encoding.UTF8.GetBytes("/agree" + '\n'));
            MessageBox.Show("Обе стороны приняли ничью");
        }
        public void DisagreeDraw(object sender, EventArgs e)
        {
            stream.Write(Encoding.UTF8.GetBytes("/disagree" + '\n'));
            button11.Visible = false;
            button15.Visible = false;
            button16.Visible = false;
        }
        public void GetLose(object sender, EventArgs e)
        {
            stream.Write(Encoding.UTF8.GetBytes("/loseклиент" + '\n'));
        }
        public void SendMessage(string message)
        {
            stream.Write(Encoding.UTF8.GetBytes(message + '\n'));
        }
        public async Task ReceiveMessagesAsync()
        {
            List<byte> buffer = new List<byte>();
            while (true)
            {
                int readedByte = stream.ReadByte();
                if (readedByte == -1) continue;
                if (readedByte == '\n')
                {
                    var message = Encoding.UTF8.GetString(buffer.ToArray(), 0, buffer.Count).Replace("\n", string.Empty);
                    if (message.Contains("/lose"))
                    {
                        MessageBox.Show(message.Replace("/lose", string.Empty)+" признал поражение");
                        button12.Visible = true;
                        client.Close();
                    }
                    else if (message.Contains("/draw"))
                    {
                        button13.Visible = true;
                        button14.Visible = true;
                        button15.Visible = true;
                    }
                    else if (message.Contains("/agree"))
                    {
                        MessageBox.Show("Обе стороны приняли ничью");
                    }
                    else if (message.Contains("/disagree"))
                    {
                        button11.Visible = false;
                        button15.Visible = false;
                        button16.Visible = false;
                    }
                    else if (message.Contains("/board"))
                    {
                        ChangeButtonsValue(message.Replace("/board", string.Empty).Split());
                    }
                    else if (message.Contains("/win"))
                    {
                        MessageBox.Show(message.Replace("/win", string.Empty));
                    }
                    buffer.Clear();
                }
                buffer.Add((byte)readedByte);
            }
        }
        public void Button00CLick(object sender, EventArgs e)
        {
            (x, y) = (0, 0);
            SendMessage($"/move{x} {y}");
        }
        public void Button01CLick(object sender, EventArgs e)
        {
            (x, y) = (0, 1);
            SendMessage($"/move{x} {y}");
        }
        public void Button02CLick(object sender, EventArgs e)
        {
            (x, y) = (0, 2);
            SendMessage($"/move{x} {y}");
        }
        public void Button10CLick(object sender, EventArgs e)
        {
            (x, y) = (1, 0);
            SendMessage($"/move{x} {y}");
        }
        public void Button11CLick(object sender, EventArgs e)
        {
            (x, y) = (1, 1);
            SendMessage($"/move{x} {y}");
        }
        public void Button12CLick(object sender, EventArgs e)
        {
            (x, y) = (1, 2);
            SendMessage($"/move{x} {y}");
        }
        public void Button20CLick(object sender, EventArgs e)
        {
            (x, y) = (2, 0);
            SendMessage($"/move{x} {y}");
        }
        public void Button21CLick(object sender, EventArgs e)
        {
            (x, y) = (2, 1);
            SendMessage($"/move{x} {y}");
        }
        public void Button22CLick(object sender, EventArgs e)
        {
            (x, y) = (2, 2);
            SendMessage($"/move{x} {y}");
        }
    }
}
