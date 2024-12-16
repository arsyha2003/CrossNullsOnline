using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;

namespace Pratice3Server
{
    public partial class Form1 : Form
    {
        public TcpListener listener;
        public TcpClient client;
        public Game game;
        public NetworkStream stream;

        public Button[,] buttons;
        public int x;
        public int y;

        public int xSide = 0;
        public int zeroSide = 0;
        public bool isGameEnded = false;
        public string whoWin;
        public bool clientMove = false;
        public Form1()
        {
            InitializeComponent();
            game = new Game();  
            listener = new TcpListener(IPAddress.Any, 7777);
            buttons = new Button[,]
            {
                {button1,button2, button3},
                {button4,button5, button6},
                {button7,button8, button9}
            };
        }
        public int ChoseSide()
        {
            return new Random().Next(1, 3);
        }
        public void Button00CLick(object sender, EventArgs e)
        {
            (x, y) = (0, 0);
            Button b = (Button)sender;
            if (clientMove == false)
            {
                game.Move(x, y, xSide); 
                b.Text = "X";
                clientMove = true;
            }
            SendBoard();
        }
        public void Button01CLick(object sender, EventArgs e)
        {
            (x, y) = (0, 1);
            Button b = (Button)sender;
            if (clientMove == false)
            {
                game.Move(x, y, xSide);
                b.Text = "X";
                clientMove = true;
            }
            SendBoard();
        }
        public void Button02CLick(object sender, EventArgs e)
        {
            (x, y) = (0, 2);
            Button b = (Button)sender;
            if (clientMove == false)
            {
                game.Move(x, y, xSide);
                b.Text = "X";
                clientMove = true;
            }
            SendBoard();
        }
        public void Button10CLick(object sender, EventArgs e)
        {
            (x, y) = (1, 0);
            Button b = (Button)sender;
            if (clientMove == false)
            {
                game.Move(x, y, xSide);
                b.Text = "X";
                clientMove = true;
            }
            SendBoard();
        }
        public void Button11CLick(object sender, EventArgs e)
        {
            (x, y) = (1, 1);
            Button b = (Button)sender;
            if (clientMove == false)
            {
                game.Move(x, y, xSide);
                b.Text = "X";
                clientMove = true;
            }
            SendBoard();
        }
        public void Button12CLick(object sender, EventArgs e)
        {
            (x, y) = (1, 2);
            Button b = (Button)sender;
            if (clientMove == false)
            {
                game.Move(x, y, xSide);
                b.Text = "X";
                clientMove = true;
            }
            SendBoard();
        }
        public void Button20CLick(object sender, EventArgs e)
        {
            (x, y) = (2, 0);
            Button b = (Button)sender;
            if (clientMove == false)
            {
                game.Move(x, y, xSide);
                b.Text = "X";
                clientMove = true;
            }
            SendBoard();
        }
        public void Button21CLick(object sender, EventArgs e)
        {
            (x, y) = (2, 1);
            Button b = (Button)sender;
            if (clientMove == false)
            {
                game.Move(x, y, xSide);
                b.Text = "X";
                clientMove = true;
            }
            SendBoard();
        }
        public void Button22CLick(object sender, EventArgs e)
        {
            (x, y) = (2, 2);
            Button b = (Button)sender;
            if (clientMove == false)
            {
                game.Move(x, y, xSide);
                b.Text = "X";
                clientMove = true;
            }
            SendBoard();
        }
        public async void StartGame(object sender, EventArgs e)
        {
            try
            {
                xSide = ChoseSide();
                if(xSide == 1)
                {
                    Text = "Ты ходишь ноликами";
                    zeroSide = 2;
                }
                else if(xSide == 2)
                {
                    Text = "Ты ходишь крестиками";
                    zeroSide = 1;
                }
                listener.Start();
                client = listener.AcceptTcpClient();
                stream = client.GetStream();
                _=Task.Run(() => ClientMoves());
                _=Task.Run(() => CheckGameStatus());
                MessageBox.Show(client.Client.RemoteEndPoint.ToString());
            }
            catch(Exception ex) {MessageBox.Show(ex.Message); return; }

        }
        public void SendBoard()
        {
            string board = string.Empty;
            foreach (var b in game.board)
            {
                board += b.ToString() + " ";
            }
            stream.Write(Encoding.UTF8.GetBytes("/board" + board + '\n'));
        }
        public async Task CheckGameStatus()
        {
            while (isGameEnded != true)
            {
                int gameState = game.CheckWinSituations();
                if (gameState == 1)
                {
                    stream.Write(Encoding.UTF8.GetBytes("/win" + "Победили нолики!" + '\n'));
                    isGameEnded = true;
                    whoWin = "нолики";
                }
                else if (gameState == 2)
                {
                    stream.Write(Encoding.UTF8.GetBytes("/win" + "Победили крестики!" + '\n'));
                    isGameEnded = true;
                    whoWin = "крестики";
                }
            }
            game.board = new int[,]
            {
                {0,0,0},
                {0,0,0},
                {0,0,0},
            };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i,j].Text = string.Empty;
                }
            }
            MessageBox.Show("Игра закончена, победили " + whoWin);
            SendBoard();
        }
        public async Task ClientMoves()
        {
            try
            {
                
                if(zeroSide == 1) stream.Write(Encoding.UTF8.GetBytes("/side1" + '\n'));       
                else if(zeroSide == 2) stream.Write(Encoding.UTF8.GetBytes("/side2" + '\n'));

                List<byte> buffer = new List<byte>();
                while (true)
                {
                    int readedByte = stream.ReadByte();
                    if (readedByte == -1) continue;
                    if (readedByte == '\n')
                    {
                        var message = Encoding.UTF8.GetString(buffer.ToArray(), 0, buffer.Count).Replace("\n", string.Empty);
                        Text = message;
                        if (message.Contains("/move"))
                        {
                            if(clientMove == true)
                            {
                                var tmp = message.Replace("/move", string.Empty).Split();
                                int x = int.Parse(tmp[0]);
                                int y = int.Parse(tmp[1]);
                                game.Move(x, y, xSide);
                                clientMove = false;
                                for (int i = 0; i < 3; i++)
                                {
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (game.board[i, j] == 2)
                                        {
                                            buttons[i, j].Text = "X";
                                        }
                                        if (game.board[i, j] == 1)
                                        {
                                            buttons[i, j].Text = "0";
                                        }
                                    }
                                }
                                SendBoard();
                            }
                        }
                        buffer.Clear();
                    }
                    buffer.Add((byte)readedByte);
                }
            }
            catch (Exception)
            {
            }
        }
    }

    public class Game
    {
        public int[,] board;
        public bool canMove = false;
        public Game()
        {
            board = new int[,]
            {
                {0,0,0},
                {0,0,0},
                {0,0,0},
            };
        }
        public void Move(int x, int y, int var)
        {
            board[x,y] = var;
        }
        public int CheckWinSituations()
        {
            if(board[0, 0] == 1 && board[1, 1] == 1 && board[2, 2] == 1) return 1;
            else if(board[0, 0] == 1 && board[1, 0] == 1 && board[2, 0] == 1) return 1;
            else if(board[0, 1] == 1 && board[1, 1] == 1 && board[2, 1] == 1) return 1;
            else if(board[0, 2] == 1 && board[1, 2] == 1 && board[2, 2] == 1) return 1;
            else if(board[0, 2] == 1 && board[1, 1] == 1 && board[2, 0] == 1) return 1;
            else if(board[0, 2] == 1 && board[1, 1] == 1 && board[2, 0] == 1) return 1;
            else if(board[0, 0] == 1 && board[0, 1] == 1 && board[0, 2] == 1) return 1;
            else if(board[1, 0] == 1 && board[1, 1] == 1 && board[1, 2] == 1) return 1;
            else if(board[2, 0] == 1 && board[2, 1] == 1 && board[2, 2] == 1) return 1;
            
            else if(board[0, 0] == 2 && board[1, 1] == 2 && board[2, 2] == 2) return 2;
            else if(board[0, 0] == 2 && board[1, 0] == 2 && board[2, 0] == 2) return 2;
            else if(board[0, 1] == 2 && board[1, 1] == 2 && board[2, 1] == 2) return 2;
            else if(board[0, 2] == 2 && board[1, 2] == 2 && board[2, 2] == 2) return 2;
            else if(board[0, 2] == 2 && board[1, 1] == 2 && board[2, 0] == 2) return 2;
            else if(board[0, 2] == 2 && board[1, 1] == 2 && board[2, 0] == 2) return 2;
            else if(board[0, 0] == 2 && board[0, 1] == 2 && board[0, 2] == 2) return 2;
            else if(board[1, 0] == 2 && board[1, 1] == 2 && board[1, 2] == 2) return 2;
            else if(board[2, 0] == 2 && board[2, 1] == 2 && board[2, 2] == 2) return 2;
            return 0;
            //8 для крестов, 8 для нулей

            //{ 1,0,0}, 
            //{ 0,1,0},
            //{ 0,0,1}

            //{ 1,0,0},
            //{ 1,0,0},
            //{ 1,0,0},

            //{ 0,1,0},
            //{ 0,1,0},
            //{ 0,1,0},

            //{ 0,0,1},
            //{ 0,0,1},
            //{ 0,0,1},

            //{ 0,0,1},
            //{ 0,1,0},
            //{ 1,0,0},

            //{ 1,1,1},
            //{ 0,0,0},
            //{ 0,0,0},

            //{ 0,0,0},
            //{ 1,1,1},
            //{ 0,0,0},

            //{ 0,0,0},
            //{ 0,0,0},
            //{ 1,1,1},
        }
    }
}
