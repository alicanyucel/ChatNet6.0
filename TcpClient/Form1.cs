using SuperSimpleTcp;
using System.Text;
namespace TcpClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SimpleTcpClient client;

        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                client.Connect();
                btnConnect.Enabled = false;
                btnSend.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client =new(txtIp.Text);
            client.Events.Connected += Events_Connected;
            client.Events.DataReceived += Events_DataReceived;
            client.Events.Disconnected += Events_Disconnected;
            btnSend.Enabled = false;

        }

        private void Events_Disconnected(object? sender, ConnectionEventArgs e)
        {

            txtInfo.Text += $"Server.diconnected.{Environment.NewLine}";

        }

        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            txtInfo.Text += $"Server:{Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";

        }

        private void Events_Connected(object? sender, ConnectionEventArgs e)
        {
            txtInfo.Text += $"Server connected. {Environment.NewLine}";

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (client.IsConnected)
            {
                if (!string.IsNullOrEmpty(txtMessage.Text))
                {
                    client.Send(txtMessage.Text);
                    txtInfo.Text+=$"ME: {txtMessage.Text}{Environment.NewLine}";
                    txtMessage.Text = String.Empty;

                }
            }

        }
    }
}