using System.Net.Sockets;
using System.Net;
using System.Text;

namespace PPI_Client
{
    public partial class Form1 : Form
    {
        static public string str = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            str = vScrollBar1.Value.ToString()+","+vScrollBar2.Value.ToString()+","+vScrollBar3.Value.ToString();
            Task clientSend = new Task(async () =>
            {
                IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
                IPEndPoint ipEndPoint = new(ipAddress, 8080);
                using Socket client = new(
        ipEndPoint.AddressFamily,
        SocketType.Stream,
        ProtocolType.Tcp);

                await client.ConnectAsync(ipEndPoint);

                var message = str;
                var messageBytes = Encoding.UTF8.GetBytes(message);
                await client.SendAsync(messageBytes, SocketFlags.None);

                client.Shutdown(SocketShutdown.Both);
                return;
            });
            clientSend.Start();


        }
    }
}