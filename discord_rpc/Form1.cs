using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using DiscordRPC.Logging;

namespace discord_rpc
{
    public partial class Form1 : Form
    {
        public DiscordRpcClient client;
		public string nadpis = "";
		public string popis = "";
		public string id = "your_id_here";
        public Form1()
        {
            InitializeComponent();
        }

		void Initialize()
		{
			if (string.IsNullOrEmpty(textBox1.Text))
			{
				nadpis = "nadpis";
			}
			if (string.IsNullOrEmpty(textBox2.Text))
			{
				popis = "popis";
			}
			if (string.IsNullOrEmpty(textBox3.Text))
			{
				id = "your_id_here";
			}

			client = new DiscordRpcClient(id);

			//Set the logger
			client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

			//Subscribe to events
			client.OnReady += (sender, e) =>
			{
				Console.WriteLine("Received Ready from user {0}", e.User.Username);
			};

			client.OnPresenceUpdate += (sender, e) =>
			{
				Console.WriteLine("Received Update! {0}", e.Presence);
			};

			//Connect to the RPC
			client.Initialize();

			//Set the rich presence
			//Call this as many times as you want and anywhere in your code.

			DiscordRPC.Button but1 = new DiscordRPC.Button() { Label = "Hmm, co tohle může být...", Url = "https://www.youtube.com/watch?v=hvL1339luv0" };
			DiscordRPC.Button but2 = new DiscordRPC.Button() { Label = "Že by free robux?", Url = "https://www.youtube.com/watch?v=hvL1339luv0" };

			DiscordRPC.Button[] buttons = { but1, but2 };

			RichPresence rp = new RichPresence()
			{
				Details = nadpis,
				State = popis,
				Assets = new Assets()
				{
					LargeImageKey = "cat_mad",
					LargeImageText = "Co chceš?",
					SmallImageKey = " "
				},
				Buttons = buttons
			};

			//rp = rp.WithTimestamps(Timestamps.Now);

			client.SetPresence(rp);
		}

        private void button1_Click(object sender, EventArgs e)
        {
			Initialize();
			label4.Text = "Running";
        }

        private void button2_Click(object sender, EventArgs e)
        {
			client.Dispose();
			label4.Text = "Stopped";
		}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
		{
			nadpis = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
			popis = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
			id = textBox3.Text;
        }
    }
}
