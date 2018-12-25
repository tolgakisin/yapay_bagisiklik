using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yapay_bagisiklik {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        public double PI = 3.14;
        public double x = 0;
        public double y = 0;
        public double fonksiyon = 0;
        public double fitness = 0;

        public int klon = 0;
        public int mutasyon = 0;

        public int populasyonValue = 0;
        public int nValue = 0;
        public int betaValue = 0;


        Random random = new Random();

        public double GetRandomNumber(double minimum, double maximum) {

            return random.NextDouble() * (maximum - minimum) + minimum;
        }



        private void Form1_Load(object sender, EventArgs e) {






        }


        private void button1_Click(object sender, EventArgs e) {

            //nValue = Convert.ToInt32(textBox2.Text);
            //populasyonValue = Convert.ToInt32(textBox1.Text);
            //betaValue = Convert.ToInt32(textBox3.Text);

            for (int i = 0; i < 49; i++) {
                dataGridView1.Rows.Add();
                dataGridView2.Rows.Add();
                if (i < 19) {
                    dataGridView3.Rows.Add();
                    //dataGridView4.Rows.Add();


                }
            }

            for (int i = 0; i < 49; i++) {

                x = GetRandomNumber(-3.0, 12.1);
                y = GetRandomNumber(4.1, 5.8);

                fonksiyon = 21.5 + x * Math.Sin(4 * PI) + y * Math.Sin(20 * PI * y);
                fitness = fonksiyon;



                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].Value = x;
                dataGridView1.Rows[i].Cells[2].Value = y;
                dataGridView1.Rows[i].Cells[3].Value = fitness;


            }

            for (int i = 0; i < 49; i++) {
                for (int j = 1; j < 4; j++) {

                    dataGridView2.Rows[i].Cells[j].Value = dataGridView1.Rows[i].Cells[j].Value;
                }
            }

            dataGridView2.Sort(dataGridView2.Columns["dataGridViewTextBoxColumn4"], ListSortDirection.Descending);

            for (int i = 0; i < 20; i++) {
                for (int j = 1; j < 4; j++) {

                    dataGridView3.Rows[i].Cells[j].Value = dataGridView2.Rows[i].Cells[j].Value;
                }
            }



            for (int i = 0; i < 49; i++) {
                dataGridView2.Rows[i].Cells[0].Value = i + 1;
                if (i < 20) {
                    dataGridView3.Rows[i].Cells[0].Value = i + 1;
                }
            }

            

            int indexJ = 0;
            for (int i = 0; i < 20; i++) {

                double klonIslemi = 0;

                klonIslemi = 5 * 20 / (i + 1);

                klon = (int)Math.Round(klonIslemi);

               

                for (int j = 0; j < klon; j++) {
                    dataGridView4.Rows.Add();
                    dataGridView5.Rows.Add();
                    dataGridView4.Rows[indexJ].Cells[0].Value = indexJ + 1;
                    dataGridView4.Rows[indexJ].Cells[1].Value = dataGridView3.Rows[i].Cells[1].Value;
                    dataGridView4.Rows[indexJ].Cells[2].Value = dataGridView3.Rows[i].Cells[2].Value;
                    dataGridView4.Rows[indexJ].Cells[3].Value = dataGridView3.Rows[i].Cells[3].Value;

                    indexJ++;
                }

            }

            double mutasyonIslemi = 0;
            Random rnd = new Random();

            for (int i = 0; i < dataGridView4.Rows.Count-1; i++) {

                mutasyonIslemi =Math.Pow(Math.E,(-10-Convert.ToDouble(dataGridView4.Rows[i].Cells[3].Value)) / Convert.ToDouble(dataGridView4.Rows[0].Cells[3].Value))*2;
                mutasyon = (int)Math.Round(mutasyonIslemi);
                //dataGridView5.Rows[i].Cells[3].Value = mutasyon;
                double yeniX = (double)dataGridView4.Rows[i].Cells[1].Value;
                double yeniY = (double)dataGridView4.Rows[i].Cells[2].Value;
                int randomXY = 0;
                if (mutasyon==1) {
                    randomXY = rnd.Next(0, 2);
                    if (randomXY == 0) {

                        yeniX = GetRandomNumber(-3.0, 12.1);
                    }
                    else if(randomXY == 1) {
                        yeniY = GetRandomNumber(4.1, 5.8);
                    }

                }
                else if (mutasyon == 2) {
                    yeniX = GetRandomNumber(-3.0, 12.1);
                    yeniY = GetRandomNumber(4.1, 5.8);
                }

                fonksiyon = 21.5 + yeniX * Math.Sin(4 * PI) + yeniY * Math.Sin(20 * PI * yeniY);
                fitness = fonksiyon;
                dataGridView5.Rows[i].Cells[0].Value = mutasyon + " - " + randomXY;
                dataGridView5.Rows[i].Cells[1].Value = yeniX;
                dataGridView5.Rows[i].Cells[2].Value = yeniY;
                dataGridView5.Rows[i].Cells[3].Value = fitness;


            }



        }
    }
}
