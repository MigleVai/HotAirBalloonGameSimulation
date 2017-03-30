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
    public partial class ScoreBoard : Form
    {
        public ScoreBoard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ScoreBoard_Load(object sender, EventArgs e)
        {
            var play = Helpers.GetScores().OrderByDescending(x => x.Score);
            var players = play.Take(5).ToList();  //Take
            int i = 1;
            listBox1.Items.AddRange(players.Select(x => (i++) + " " + x.FirmName + " - " + x.Score).ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MoreScore more = new MoreScore();
            more.Show();
        }
    }
}
