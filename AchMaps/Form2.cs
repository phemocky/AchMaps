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
        bool czyruszane = false;
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
                
                string kontoD = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=" + key.GetKey() + "&vanityurl=" + textBox1.Text;             
                using (WebClient client = new WebClient())
                {
                    string htmlCode = client.DownloadString(kontoD);
                    AccountContainer acc = JsonConvert.DeserializeObject<AccountContainer>(htmlCode);
                    if(acc.response.success == "1")
                    {
                        List<string> tmpKonto = File.ReadAllLines("konto.txt").ToList();
                        tmpKonto.Add(textBox2.Text + ":" + acc.response.steamid);
                        File.WriteAllLines("konto.txt", tmpKonto);
                        this.Close();
                        Application.Restart();
                    }
                    else
                        MessageBox.Show("Wrong ID");
                }
            }
            else
            {
                List<string> tmpKonto = File.ReadAllLines("konto.txt").ToList();
                string wynik = textBox2.Text + ":" + textBox1.Text;
                tmpKonto.Add(wynik);
                File.WriteAllLines("konto.txt", tmpKonto);
                Application.Restart();
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (czyruszane == false)
            {
                textBox2.Text = textBox1.Text;
                czyruszane = false;
            } 
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            czyruszane = true;
        }
    }
}
