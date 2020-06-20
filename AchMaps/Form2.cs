using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace AchMaps
{
    public partial class Form2 : Form
    {
        bool isUsed = false;
        Key key = new Key();
        public Form2()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Write URL");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Write your ID from your URL steam e.g: \nhttp://steamcommunity.com/id/phemocky/ = phemocky");
        }

        private void button1_Click(object sender, EventArgs e)//sprzawdzenie
        {
            if (checkBox1.Checked)
            {
                
                string accountD = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=" + key.GetKey() + "&vanityurl=" + textBox1.Text;             
                using (WebClient client = new WebClient())
                {
                    string htmlCode = client.DownloadString(accountD);
                    AccountContainer acc = JsonConvert.DeserializeObject<AccountContainer>(htmlCode);
                    if(acc.response.success == "1")
                    {
                        List<string> tmpAcc = File.ReadAllLines("Account.txt").ToList();
                        tmpAcc.Add(textBox2.Text + ":" + acc.response.steamid);
                        File.WriteAllLines("Account.txt", tmpAcc);
                        this.Close();
                        Application.Restart();
                    }
                    else
                        MessageBox.Show("Wrong ID");
                }
            }
            else
            {
                List<string> tmpAcc = File.ReadAllLines("Account.txt").ToList();
                string result = textBox2.Text + ":" + textBox1.Text;
                tmpAcc.Add(result);
                File.WriteAllLines("Account.txt", tmpAcc);
                Application.Restart();
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (isUsed == false)
            {
                textBox2.Text = textBox1.Text;
                isUsed = false;
            } 
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            isUsed = true;
        }
    }
}
