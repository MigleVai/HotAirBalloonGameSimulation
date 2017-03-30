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
    public partial class PlaySettings : Form
    {
        public PlaySettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("You don't have a name :<");
            }
            else
            {
                CreatePlayer();
                button2.Enabled = true;
            }
        }

        private void CreatePlayer()
        {
            string name = textBox1.Text;
            Player player = new Player();
            player.FirmName = name;
            FirmRepository<Player> p = new FirmRepository<Player>();
            p.Add(player);
        }

        private void PlaySettings_Load(object sender, EventArgs e)
        {    
            FirmRepository<Balloon> balloons = new FirmRepository<Balloon>();
            List<Balloon> listBalloon = balloons.GetAll();
            Dictionary<int, IEnumerable<Balloon>> result = Helpers.GetPilotWithBalloon(FillPilots(), listBalloon);
            var column = listView1.Columns.Add("ID - number");
            foreach (var item in result)
            {
                Balloon bal = item.Value.FirstOrDefault(x => x.PilotId == item.Key);
                var viewItem = listView1.Items.Add(item.Key + " - " + bal.PassangerNumb);
                viewItem.BackColor = Color.FromName(bal.Color);
            }
            column.Width = 100;
        }

        private List<Pilot> FillPilots ()
        {
            FirmRepository<Pilot> pilots = new FirmRepository<Pilot>();
            List<Pilot> listPilot = pilots.GetAll();
            listBox1.Items.AddRange(listPilot.Select(x => x.PilotId + " - " + x.FirstName + " " + x.SecondName).ToArray());
            return listPilot;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlayView play = new PlayView();
            play.Show();
            Close();
        }
    }
} 
