using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TreciaTOPMigle;

namespace BalloonGame
{
    public partial class PlayView : Form
    {
        Timer timer = new Timer();
        public PlayView()
        {
            InitializeComponent();
        }

        private void PlayView_Load(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Start();
            timer.Interval = 1000; //120 000
            progressBar1.Maximum = 120;
            timer.Tick += new System.EventHandler(timer_Tick);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != 120)
            {
                progressBar1.Value++;
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Your time is up :<");
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool text1 = CheckNotEmpty(textBox1);
            bool text2 = CheckNotEmpty(textBox2);
            bool text3 = CheckNotEmpty(textBox3);
            bool text4 = CheckNotEmpty(textBox4);
            int number;
            int phone;
            if (text1 && text2 && text3 && text4)
            {
                if (Int32.TryParse(textBox4.Text, out number) && Int32.TryParse(textBox3.Text, out phone))
                {
                    var client = CreateClient();
                    CheckForPilot(number, client);
                }
                else
                {
                    MessageBox.Show("It had to be a integer or integers!");
                }
            }
            else
            {
                MessageBox.Show("No no... write everything!");
            }
        }
        private Client CreateClient()
        {
            Client client = new Client();
            client.FirstName = textBox1.Text;
            client.SecondName = textBox2.Text;
            client.TelephoneNumb = textBox3.Text;
            FirmRepository<Client> c = new FirmRepository<Client>();
            c.Add(client);
            return client;
        }

        private Dictionary<int, IEnumerable<Balloon>> ConnectPilotBalloon()
        {
            FirmRepository<Balloon> b = new FirmRepository<Balloon>();
            FirmRepository<Pilot> p = new FirmRepository<Pilot>();
            List<Pilot> listPilot = p.GetAll();
            List<Balloon> listBalloon = b.GetAll();
            return Helpers.GetPilotWithBalloon(listPilot, listBalloon);
        }

        private void CheckForPilot(int number, Client c)
        {
            FirmRepository<Order> o = new FirmRepository<Order>();
            List<Order> orders = new List<Order>();
            orders = o.GetAll();
            Order pilId = orders.LastOrDefault<Order>();
            if (number > FindMaxAmount())
            {
                MessageBox.Show("There is no balloon for the amount of people!!!");
            }
            else
            {
                bool created = false;
                var result = ConnectPilotBalloon().OrderBy(item => Math.Abs(number - item.Value.FirstOrDefault().PassangerNumb));
                foreach (var item in result)
                {
                    Balloon bal = item.Value.FirstOrDefault(x => x.PilotId == item.Key);
                    if (bal.PassangerNumb >= number)
                    {
                        if (pilId == null)
                        {
                            CreateOrder(number, c.ClientId, item.Key, bal.Color);
                            created = true;
                            break;
                        }
                        else if (item.Key != pilId.PilotId)
                        {
                            CreateOrder(number, c.ClientId, item.Key, bal.Color);
                            created = true;
                            break;
                        }
                    }
                }
                if (created == false)
                {
                    MessageBox.Show("The amount of passangers is a little bit crazy?!");
                }
            }
        }

        private int FindMaxAmount()
        {
            FirmRepository<Balloon> b = new FirmRepository<Balloon>();
            var balloons = b.GetAll();
            var amount = balloons.Select(x => x.PassangerNumb).Max();
            return amount;
        }
        private void CreateOrder(int number, int cId, int pId, string color)
        {
            FirmRepository<Order> o = new FirmRepository<Order>();
            Order order = new Order();
            order.OrderDate = DateTime.Now;
            order.PassangerAmount = number;
            order.ClientId = cId;
            order.PilotId = pId;
            o.Add(order);
            Create(color);
            ClearAll();
        }

        private void ClearAll()
        {
            ClearTextBox(textBox1);
            ClearTextBox(textBox2);
            ClearTextBox(textBox3);
            ClearTextBox(textBox4);
        }

        private void ClearTextBox(TextBox box)
        {
            box.Text = "";
        }

        private bool CheckNotEmpty(TextBox box)
        {
            if (string.IsNullOrEmpty(box.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Create(string color)
        {
            Random random = new Random();
            int x = random.Next(0, 404);
            int y = random.Next(0, 187);
            Graphics g = panel2.CreateGraphics();
            SolidBrush b = new SolidBrush(Color.FromName(color));
            g.FillEllipse(b, x, y, 80, 100);
        }

        private void PlayView_FormClosing(object sender, FormClosingEventArgs e)
        {
            FirmRepository<Player> p = new FirmRepository<Player>();
            List<Player> players = p.GetAll();
            Player player = players.LastOrDefault();
            if (progressBar1.Value == 120)
            {
                FirmRepository<Order> o = new FirmRepository<Order>();
                List<Order> orders = o.GetAll();
                long score = orders.Select(x => x.PassangerAmount).Aggregate((a, b) => a + b); //Agregate
                player.Score = score;
                p.Update(player, player.ID);
            }
            else
            {
                p.Delete(player.ID);
            }
            DeleteAllNotNeeded();
        }

        private void DeleteAllNotNeeded()
        {
            FirmRepository<Order> o = new FirmRepository<Order>();
            o.AllDelete();
            FirmRepository<Client> c = new FirmRepository<Client>();
            c.AllDelete();
        }
    }
}
