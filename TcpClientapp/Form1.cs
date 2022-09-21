using SuperSimpleTcp;
using System.Text;

namespace TcpClientapp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SimpleTcpClient client;
        private void Form1_Load(object sender, EventArgs e)
        {
            client = new(txtIp.Text);
            client.Events.Connected += Events_Connected;
            client.Events.DataReceived += Events_DataReceived;
            client.Events.Disconnected += Events_Disconnected;
            btnSend.Enabled = false;

        }

        private void Events_Disconnected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtBaslik.Text += $"Server.diconnected.{Environment.NewLine}";
            });
            }

        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {

                txtBaslik.Text += $"Server:{Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
            });
            }

        private void Events_Connected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtBaslik.Text += $"Server connected. {Environment.NewLine}";
            });
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (client.IsConnected)
            {
                if (!string.IsNullOrEmpty(TxtMesaj.Text))
                {
                    client.Send(TxtMesaj.Text);
                    txtBaslik.Text += $"ME: {TxtMesaj.Text}{Environment.NewLine}";
                    TxtMesaj.Text = String.Empty;

                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // client da baðlanabiliyor mesaj gönderebiliyor
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
    }
}