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
    public partial class AddPlayerForm : Form
    {
        private List<Player> players;

        public AddPlayerForm(List<Player> players)
        {
            InitializeComponent();
            this.players = players;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Player player = new Player();
                player.PlayerId = txt_playerid.Text;
                player.FirstName = txt_firstname.Text;
                player.LastName = txt_lastname.Text;
                player.BirthDay = txt_birthday.Value;
                player.BirthPlace = txt_birthplace.Text;
                player.Height = Convert.ToInt32(txt_height.Text);
                player.Weight = Convert.ToInt32(txt_weight.Text);
                foreach (var item in players)
                {
                    if(item.PlayerId== player.PlayerId)
                    {
                        MessageBox.Show("Dumplicated player!");
                        return;
                    }
                }
                players.Add(player);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show("Please check your input!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
