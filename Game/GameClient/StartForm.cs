using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pratice3Server
{
    public partial class Form2 : Form
    {
        public IPEndPoint serverPoint = null;
        private IPAddress ip;
        private int port;
        private Action<IPEndPoint> getPoint;
        public Form2(Action<IPEndPoint> getPoint)
        {
            InitializeComponent();
            textBox1.Text = "127.0.0.1";
            textBox2.Text = "7777";
            this.getPoint = getPoint;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ip = IPAddress.Parse(textBox1.Text);
                port = int.Parse(textBox2.Text);
                serverPoint = new IPEndPoint(ip, port);
                getPoint.Invoke(serverPoint);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Данные введены неверно!");
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
            }
        }
    }
}
