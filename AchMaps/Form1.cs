using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace AchMaps
{
    public partial class Form1 : Form
    {
        List<AccountData> LAccounts = new List<AccountData>();
        List<Games> LGames = new List<Games>();
        Games ActualGame = new Games();
        CategoryOfAchievement ActualCoA = new CategoryOfAchievement();
        Key key = new Key();

        public Form1()
        {
            InitializeComponent();

            //TEST(); 

            if (!File.Exists("Account.txt"))
            {
                File.Create("Account.txt");
                MessageBox.Show("Missing txt, incoming restart application");
                Application.Restart();
            }              
            string[] who = File.ReadAllLines("Account.txt");
            if (!IsSomeoneExist(who))
            {
                NewAccountForm();
            }
            AccountToCB();
            GamesToCB();
            label4.ForeColor = Color.MediumPurple;
            label5.ForeColor = Color.Orange;
            label6.ForeColor = Color.CadetBlue;
            label8.ForeColor = Color.MediumPurple;
            label7.ForeColor = Color.Orange;
            
        }
        bool IsSomeoneExist(string[] who)
        {
            if (who.Length == 0)
            {
                MessageBox.Show("Add accout!");
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewAccountForm();
        }
        
        private void NewAccountForm()
        {
            Form2 abc = new Form2();
            abc.ShowDialog();                   
        }
        private void AccountToCB()
        {
            comboBox1.Items.Clear();
            string[] kto = File.ReadAllLines("Account.txt");
            
            foreach(string a in kto)
            {
                AccountData b = new AccountData();
                string[] c = a.Split(':');
                b.name = c[0];
                b.ID = c[1];
                LAccounts.Add(b);
            }
            comboBox1.DataSource = LAccounts;
            comboBox1.DisplayMember = "Name";
            comboBox1.Format += (s, e) => 
            {
                e.Value = ((AccountData)e.Value).name;
            };//skopiowanie, nie ogarniam ale dziala!!!



        }
        private void GamesToCB()
        {
            Games kf1 = new Games("killingfloor");
            Games kf2 = new Games("killingfloor2");
            Games pd2 = new Games("payday2");

            kf1.name = "Killing Floor";
            kf1.ID = "1250";
            kf1.underName.Add("Map\\Difficulty", 0);
            kf1.underName.Add("Map\\Collectable", 1);
            LGames.Add(kf1);

            kf2.name = "Killing Floor 2";
            kf2.ID = "232090";
            kf2.underName.Add("Map\\Difficulty", 0);
            kf2.underName.Add("Perk\\Difficulty", 1);
            kf2.underName.Add("Perk\\Lvl", 2);
            LGames.Add(kf2);

            pd2.name = "PAYDAY 2";
            pd2.ID = "218620";
            pd2.underName.Add("Map\\Difficulty", 0);
            pd2.underName.Add("Level", 1);
            pd2.underName.Add("Secret", 2);
            pd2.underName.Add("Holdout", 3);
            LGames.Add(pd2);


            comboBox2.DataSource = LGames;
            comboBox2.DisplayMember = "nazwa";
            comboBox2.Format += (s, e) =>
            {
                e.Value = ((Games)e.Value).name;
            };//skopiowanie, nie ogarniam ale dziala!!!
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dynamic item = comboBox2.SelectedItem;
                string id = item.ID;

                dynamic accountId = comboBox1.SelectedItem;
                string idk = accountId.ID;
                string adres = "http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=" + id + "&key=" +  key.GetKey() + "&steamid=" + idk;
                using (WebClient client = new WebClient())
                {
                    string htmlCode = client.DownloadString(adres);
                    JsonOutAchs achievements = JsonConvert.DeserializeObject<JsonOutAchs>(htmlCode);
                    //Dictionary<int, bool> dicToRead = new Dictionary<int, bool>();
                    Dictionary<string, bool> dicToRead = new Dictionary<string, bool>();
                    int ii = 0;
                    dynamic cb3 = comboBox3.SelectedItem;
                    ActualCoA = ActualGame.categories[cb3.Value];
                    foreach (var b in achievements.playerstats.achievements)
                    {
                        if (b.achieved == 1)
                            dicToRead.Add(b.apiname, true);
                        else
                            dicToRead.Add(b.apiname, false);
                        ii++;
                    }

                    List<string> tabtmp = new List<string>();
                    foreach(string ac in ActualCoA.upName)
                    {
                        tabtmp.Add(ac);
                    }
                    //bool ayy = false;
                    tabtmp.RemoveAt(0);
                    dataGridView1.ColumnCount = tabtmp.Count;
                    
                    dataGridView1.Rows.Add(tabtmp.ToArray());
                    int zz = 1;
                    foreach (var a in ActualCoA.maps)
                    {
                        List<string> datas = new List<string>();
                        datas.Add(a.name);

                        foreach (var c in a.id)
                        {
                            if (c == "NULL")
                                continue;
                 
                            datas.Add(c);
                        }
                        dataGridView1.Rows.Add(datas.ToArray());
                        if (a.type == TypeMap.Long || a.type == TypeMap.Waves)
                            dataGridView1[0, zz].Style.BackColor = Color.MediumPurple;
                        else if (a.type == TypeMap.Normal)
                            dataGridView1[0, zz].Style.BackColor = Color.Orange;
                        else if (a.type == TypeMap.Objective)
                            dataGridView1[0, zz].Style.BackColor = Color.CadetBlue;
                        else
                            dataGridView1[0, zz].Style.BackColor = Color.Orange;
                        int i = 1;
                        foreach (var c in a.id)
                        {
                            if (c == "NULL")
                            {
                                dataGridView1[i, zz].Value = "NULL";
                                dataGridView1[i, zz].Style.BackColor = Color.LightGray;
                                i++;
                                continue;
                            }

                            else if (dicToRead.ContainsKey(c))
                            {
                                if (dicToRead[c])
                                {
                                    dataGridView1[i, zz].Value = "Done";
                                    dataGridView1[i, zz].Style.BackColor = Color.GreenYellow;
                                }
                                else
                                {
                                    dataGridView1[i, zz].Value = "Unfinished";
                                    dataGridView1[i, zz].Style.BackColor = Color.OrangeRed;
                                }
                            }
                            else//For some reason "NULL" != "NULL" 
                            {
                                dataGridView1[i, zz].Value = "NULL";
                                dataGridView1[i, zz].Style.BackColor = Color.LightGray;
                                i++;
                                continue;
                            }
                            i++;
                        }
                        zz++;
                    }

                   

                    dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }


            catch (Exception)
            {
                MessageBox.Show("Wrong ID, private profile, account doesnt have that game or private games list (patch from 11.04.2018)", "Error");
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic item = comboBox2.SelectedItem;
            Dictionary<string, int> b = item.underName;
            comboBox3.DataSource = b.ToList();
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            if (item.name == "Killing Floor")
            {
                ActualGame = LGames[0];
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
            }
            else if (item.name == "Killing Floor 2")
            {
                ActualGame = LGames[1];
                label7.Visible = true;
                label8.Visible = true;
                label6.Visible = true;
            }
            else if (item.name == "PAYDAY 2")
            {
                ActualGame = LGames[2];
            }
        }
        
        
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void generateToolStripMenuItem_Click_1(object sender, EventArgs e)//generating stuff i did badly 
        {
            
            //DirectoryInfo d = new DirectoryInfo(@"D:\vs\AchMaps\AchMaps\bin\Debug\killingfloor");
            //string id = "1250";            
            //RapairThisGame(d,id);

            //d = new DirectoryInfo(@"D:\vs\AchMaps\AchMaps\bin\Debug\killingfloor2");
            //id = "232090";
            //RapairThisGame(d, id);

            //d = new DirectoryInfo(@"D:\vs\AchMaps\AchMaps\bin\Debug\payday2");
            //id = "218620";
            //RapairThisGame(d, id);

        }

        private void RapairThisGame(DirectoryInfo d, string id)
        {
            //dynamic accountId = comboBox1.SelectedItem;
            //string idk = accountId.ID;
            //foreach (var file in d.GetFiles())
            //{

            //    CategoryOfAchievement CoA = JsonConvert.DeserializeObject<CategoryOfAchievement>(File.ReadAllText(file.FullName));
            //    string adres = "http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=" + id + "&key=" + key.GetKey() + "&steamid=" + idk;
            //    using (WebClient client = new WebClient())
            //    {
            //        string htmlCode = client.DownloadString(adres);
            //        JsonOutAchs achievements = JsonConvert.DeserializeObject<JsonOutAchs>(htmlCode);
            //        int ii = 0;
            //        foreach (var b in achievements.playerstats.achievements)
            //        {
            //            var a = CoA.maps.Find(x => x.id.Contains(ii.ToString()));
            //            if (a != null)
            //            {
            //                a.id = a.id.Select(s => s.Replace(ii.ToString(), b.apiname)).ToList();
                            
            //            }
            //            ii++;

            //        }

            //    }
            //    string json = JsonConvert.SerializeObject(CoA);
            //    File.WriteAllText(file.FullName, json);

            //}
        }

        private void kF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FAddNewMap form = new FAddNewMap(LGames[1],0);
            form.ShowDialog();
            if(form.DialogResult == DialogResult.OK)
            {
                LGames[1] = form.Game;

                var path2 = Path.Combine(Directory.GetCurrentDirectory() + @"\" + "killingfloor2");
                DirectoryInfo d = new DirectoryInfo(path2);
                foreach (var file in d.GetFiles())
                {
                    if(file.Name == "0_MapsDifficulty.txt")
                    {
                        var CoA = LGames[1].categories[0];
                        File.WriteAllText(file.FullName, JsonConvert.SerializeObject(CoA));
                    }
                    

                }
            }
        }

        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Payday2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void MapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FAddNewMap form = new FAddNewMap(LGames[2],0);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                LGames[2] = form.Game;

                var path2 = Path.Combine(Directory.GetCurrentDirectory() + @"\" + "payday2");
                DirectoryInfo d = new DirectoryInfo(path2);
                foreach (var file in d.GetFiles())
                {
                    if (file.Name == "0_MapsDifficulty.txt")
                    {
                        var CoA = LGames[2].categories[0];
                        File.WriteAllText(file.FullName, JsonConvert.SerializeObject(CoA));
                    }
                }
            }
        }

        private void HoldoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FAddNewMap form = new FAddNewMap(LGames[2],3);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                LGames[2] = form.Game;

                var path2 = Path.Combine(Directory.GetCurrentDirectory() + @"\" + "payday2");
                DirectoryInfo d = new DirectoryInfo(path2);
                foreach (var file in d.GetFiles())
                {
                    if (file.Name == "3_Holdout.txt")
                    {
                        var CoA = LGames[2].categories[3];
                        File.WriteAllText(file.FullName, JsonConvert.SerializeObject(CoA));
                    }
                }
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
