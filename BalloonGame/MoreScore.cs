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
    public partial class MoreScore : Form
    {
        public MoreScore()
        {
            InitializeComponent();
        }

        private void MoreScore_Load(object sender, EventArgs e)
        {
            var play = Helpers.GetScores().OrderByDescending(x => x.Score);
            var player = play.Skip(5).ToList(); // Skip
            int i = 6;
            listBox1.Items.AddRange(player.Select(x => (i++) + " " + x.FirmName + " - " + x.Score).ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
