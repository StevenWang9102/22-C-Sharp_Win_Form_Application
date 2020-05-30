using PlayersManagementSystemClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayersManagementSystem
{
    public partial class AddTeamForm : Form
    {
        private List<Team> teams;

        public AddTeamForm(List<Team> teams)
        {
            InitializeComponent();
            this.teams = teams;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Team team = new Team();
                team.TeamName = txt_teamname.Text;
                team.Ground = txt_ground.Text;
                team.Coach = txt_coach.Text;
                team.FoundYear = Convert.ToInt32(txt_foundyear.Text);
                team.Region = txt_region.Text;
                foreach (var item in teams)
                {
                    if (item.TeamName == team.TeamName)
                    {
                        MessageBox.Show("Dumplicated team!");
                        return;
                    }
                }

                teams.Add(team);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception e2)
            {
                MessageBox.Show("Please check your input!");
            }
        }
    }
}
