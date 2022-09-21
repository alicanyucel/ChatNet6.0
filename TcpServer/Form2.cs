using SuperSimpleTcp;
using System.Text;

namespace TcpClient
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SimpleTcpServer server;
        

        private void button1_Click(object sender, EventArgs e)
        {
            server.Start();
            txtInfo.Text+= $"Starting...{Environment.NewLine}";
            btnStart.Enabled = false;
            btnSend.Enabled = true;

                
        }

        private void txtIp_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            server = new SimpleTcpServer(txtIp.Text);
            server.Events.ClientConnected += Events_ClientConnected;
            server.Events.ClientConnected += Events_ClientConnected;
            server.Events.DataReceived += Events_DataReceived;



        }

        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            txtInfo.Text += $"{e.IpPort}: {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
        }

        private void Events_ClientConnected(object? sender, ConnectionEventArgs e)
        {
       
            txtInfo.Text += $"{e.IpPort}.connected.{Environment.NewLine}";
            listtClientIp.Items.Add(e.IpPort);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(server.IsListening)
            {
                if(!string.IsNullOrEmpty(txtMessage.Text) && listtClientIp.SelectedItem!=null)
                {
                    server.Send(listtClientIp.SelectedItem.ToString(), txtMessage.Text);
                    txtInfo.Text += $"Server:{txtMessage.Text} {Environment.NewLine}";
                    txtMessage.Text = String.Empty;
                }
            }
        }
    }
}