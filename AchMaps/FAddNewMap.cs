using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AchMaps
{
    public partial class FAddNewMap : Form
    {
        public Games Game;
        int curpanel = -1;
        public FAddNewMap(Games game, int cat)
        {
            InitializeComponent();
            Game = game;
            if(Game.name == "Killing Floor 2" && cat == 0)
            {
                label1.Text = Game.name + " : " + "Map";
                panel1.Visible = true;
                curpanel = 1;
                comboBox1.Items.Add(TypeMap.Normal);
                comboBox1.Items.Add(TypeMap.Objective);
                comboBox1.Items.Add(TypeMap.Waves);

            }
            else if (Game.name == "PAYDAY 2" && cat == 0)
            {
                label1.Text = Game.name + " : " + "Map";
                panel2.Visible = true;
                curpanel = 2;
                //comboBox2.DataSource = Enum.GetValues(typeof(TypeMap));
                comboBox2.Items.Add(TypeMap.Bain);
                comboBox2.Items.Add(TypeMap.Vlad);
                comboBox2.Items.Add(TypeMap.Hector);
                comboBox2.Items.Add(TypeMap.Elephant);
                comboBox2.Items.Add(TypeMap.Event);
                comboBox2.Items.Add(TypeMap.Buther);
                comboBox2.Items.Add(TypeMap.Dentist);
                comboBox2.Items.Add(TypeMap.Jimmy);
                comboBox2.Items.Add(TypeMap.Locky);
                comboBox2.Items.Add(TypeMap.Classic);
                comboBox2.Items.Add(TypeMap.Continental);
            }
            else if (Game.name == "PAYDAY 2" && cat == 3)
            {
                label1.Text = Game.name + " : " + "Holdout";
                panel3.Visible = true;
                curpanel = 3;
            }
            else
            {
                this.Close();
            }
            
            refreshDataGrid(cat);
            this.DialogResult = DialogResult.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CMaps map = new CMaps();
            if(curpanel == 1)
            {
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Name Map");
                    return;
                }
                map.name = textBox1.Text;
                map.type = (TypeMap)comboBox1.SelectedItem;
                map.id = new List<string>();
                map.id.Add(textBox2.Text);
                map.id.Add(textBox3.Text);
                map.id.Add(textBox4.Text);
                map.id.Add(textBox5.Text);
                map.id.Add(textBox6.Text);
                Game.categories[0].maps.Add(map);
                refreshDataGrid(0);
                textBox1.Text = "";
            }
            else if(curpanel == 2)
            {
                if (String.IsNullOrEmpty(textBox7.Text))
                {
                    MessageBox.Show("Name Map");
                    return;
                }
                map.name = textBox7.Text;
                map.type = (TypeMap)comboBox2.SelectedItem;
                map.id = new List<string>();
                map.id.Add(textBox8.Text);
                map.id.Add(textBox9.Text);
                map.id.Add(textBox10.Text);
                map.id.Add(textBox11.Text);
                map.id.Add(textBox12.Text);
                map.id.Add(textBox13.Text);
                map.id.Add(textBox14.Text);
                map.id.Add(textBox15.Text);
                Game.categories[0].maps.Add(map);
                refreshDataGrid(0);
                textBox7.Text = "";
            }
            else if (curpanel == 3)
            {
                if (String.IsNullOrEmpty(textBox19.Text))
                {
                    MessageBox.Show("Name Map");
                    return;
                }
                map.name = textBox19.Text;
                map.type = TypeMap.Holdout;
                map.id = new List<string>();
                map.id.Add(textBox20.Text);
                map.id.Add(textBox21.Text);
                map.id.Add(textBox23.Text);
                map.id.Add(textBox24.Text);
                Game.categories[0].maps.Add(map);
                refreshDataGrid(3);
            }


        }
        private void refreshDataGrid(int val)
        {
            for(int i = dataGridView1.Rows.Count-1; i > 0; i--)
            {
                dataGridView1.Rows.RemoveAt(i);
            }
            var ActualCoA = Game.categories[val];
            List<string> tabtmp = new List<string>();
            foreach (string ac in ActualCoA.upName)
            {
                tabtmp.Add(ac);
            }
            //bool ayy = false;
            tabtmp.RemoveAt(0);
            dataGridView1.ColumnCount = tabtmp.Count;
            dataGridView1.Rows.Add(tabtmp.ToArray());
            foreach (var a in ActualCoA.maps)
            {

                List<string> datas = new List<string>();
                datas.Add(a.name);

                foreach (var c in a.id)
                {
                    if (c == "NOPE")
                        continue;

                    datas.Add(c);
                }
                dataGridView1.Rows.Add(datas.ToArray());
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void FAddNewMap_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
