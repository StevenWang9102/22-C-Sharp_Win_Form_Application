using PlayersManagementSystemClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayersManagementSystem
{
    public partial class Form1 : Form
    {
        List<Player> players = new List<Player>();
        List<Team> teams = new List<Team>();

        public Form1()
        {
            InitializeComponent();
        }

        private void showPlayersList(List<Player> players)
        {
            lst_players.Items.Clear();
            foreach (var player in players)
            {
                var listitem = new ListViewItem(new string[] {
                    player.PlayerId,
                    player.FirstName+ " "+player.LastName,
                    player.BirthDay.ToString("d/M/yyyy"),
                    player.BirthPlace,
                    player.Height.ToString(),
                    player.Weight.ToString(),
                    player.TeamName
                });
                listitem.Tag = player;
                lst_players.Items.Add(listitem);
            }
        }

        private void showTeamsList(List<Team> teams)
        {
            lst_teams.Items.Clear();
            foreach (var team in teams)
            {
                var listitem = new ListViewItem(new string[] {
                    team.TeamName,
                    team.Ground,
                    team.Coach,
                    team.FoundYear.ToString(),
                    team.Region
                });
                listitem.Tag = team;
                lst_teams.Items.Add(listitem);
            }
        }

        private void loadPlayers(string filename)
        {
            players.Clear();
            StreamReader sr = new StreamReader(filename, Encoding.Default);
            string line = sr.ReadLine();
            while (line != null)
            {
                var arr = line.Split(';');
                var player = new Player();
                if(arr.Length==7)
                {
                    player.PlayerId = arr[0].Trim();
                    player.FirstName = arr[1].Trim();
                    player.LastName= arr[2].Trim();
                    player.BirthDay = DateTime.ParseExact(arr[3].Trim(), "d/M/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                    player.Height = Convert.ToInt32(arr[4].Trim());
                    player.Weight = Convert.ToInt32(arr[5].Trim());
                    player.BirthPlace = arr[6].Trim();
                    players.Add(player);
                }
                if (arr.Length==8)
                {
                    player.PlayerId = arr[0].Trim();
                    player.FirstName = arr[1].Trim();
                    player.LastName = arr[2].Trim();
                    player.BirthDay = DateTime.ParseExact(arr[3].Trim(), "d/M/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                    player.Height = Convert.ToInt32(arr[4].Trim());
                    player.Weight = Convert.ToInt32(arr[5].Trim());
                    player.BirthPlace = arr[6].Trim();
                    player.TeamName = arr[7].Trim();
                    players.Add(player);
                }
                line = sr.ReadLine();
            }
            sr.Close();
        }

        private void savePlayers(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);

            foreach (var player in players)
            {
                var line = string.Format("{0};{1};{2};{3};{4};{5};{6};{7}",
                    player.PlayerId, player.FirstName, player.LastName,
                    player.BirthDay.ToString("d/M/yyyy"), player.Height, player.Weight, player.BirthPlace, player.TeamName==null?"": player.TeamName);
                sw.WriteLine(line);
            }

            sw.Flush();
            sw.Close();

        }

        private void loadTeams(string filename)
        {
            teams.Clear();

            StreamReader sr = new StreamReader(filename, Encoding.Default);
            string line = sr.ReadLine();
            while (line != null)
            {
                var arr = line.Split(';');
                if (arr.Length == 4)
                {
                    var team = new Team();
                    string yearstr = arr[3].Split(',')[0].Trim();
                    string regionstr = arr[3].Split(',')[1].Trim();
                    team.TeamName= arr[0].Trim();
                    team.Ground = arr[1].Trim();
                    team.Coach = arr[2].Trim();
                    team.FoundYear = Convert.ToInt32(yearstr.Split(' ')[1]);
                    team.Region = regionstr;
                    teams.Add(team);
                }
                line = sr.ReadLine();
            }
            sr.Close();
        }

        private void saveTeams(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            foreach (var team in teams)
            {
                var line = string.Format("{0};{1};{2};Founded {3}, {4}",
                    team.TeamName, team.Ground, team.Coach, team.FoundYear, team.Region);
                sw.WriteLine(line);
            }
            sw.Flush();
            sw.Close();
        }

        private int dateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            var diff= DateTime1.Year - DateTime2.Year;
            if (DateTime1.Month > DateTime2.Month)
            {
            }
            else if(DateTime1.Month == DateTime2.Month&&
                DateTime1.Day >= DateTime2.Day)
            {
            }
            else
            {
                diff -= 1;
            }
            return diff;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            List<Player> result = new List<Player>();
            int age = Convert.ToInt32(textBox1.Text);
            foreach (var item in players)
            {
                if(dateDiff(DateTime.Now, item.BirthDay)==age)
                {
                    result.Add(item);
                }
            }
            showPlayersList(result);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Player> result = new List<Player>();
            foreach (var item in players)
            {
                if (item.BirthPlace==textBox2.Text)
                {
                    result.Add(item);
                }
            }
            showPlayersList(result);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(lst_players.SelectedItems.Count==1&& lst_teams.SelectedItems.Count==1)
            {
                var player = (Player)lst_players.SelectedItems[0].Tag;
                var team = (Team)lst_teams.SelectedItems[0].Tag;
                player.TeamName = team.TeamName;
                showPlayersList(players);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(new AddPlayerForm(players).ShowDialog() == DialogResult.OK)
            {
                showPlayersList(players);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(new AddTeamForm(teams).ShowDialog()==DialogResult.OK)
            {
                showTeamsList(teams);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<string> names = new List<string>();
            List<int> weights = new List<int>();
            List<int> heights = new List<int>();
            foreach (var item in players)
            {
                names.Add(item.FirstName + " " + item.LastName);
                weights.Add(item.Weight);
                heights.Add(item.Height);
            }

            chart1.Series.Clear();
            chart1.Series.Add("Weight");
            chart1.Series.Add("Height");
            chart1.Series["Weight"].Points.DataBindXY(names, weights);
            chart1.Series["Height"].Points.DataBindXY(names, heights);
        }

        private void openPlayersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filename = openFileDialog1.FileName;
                loadPlayers(filename);
                showPlayersList(players);
            }
        }

        private void openTeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filename = openFileDialog1.FileName;
                loadTeams(filename);
                showTeamsList(teams);
            }
        }

        private void savePlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog1.FileName;
                savePlayers(filename);
            }
        }

        private void saveTeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog1.FileName;
                saveTeams(filename);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            showPlayersList(players);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(lst_teams.SelectedItems.Count==1)
            {
                Team team = (Team)lst_teams.SelectedItems[0].Tag;
                List<Player> plist = new List<Player>();
                foreach (var item in players)
                {
                    if(item.TeamName== team.TeamName)
                    {
                        plist.Add(item);
                    }
                }
                showPlayersList(plist);
            }
        }
    }
}
