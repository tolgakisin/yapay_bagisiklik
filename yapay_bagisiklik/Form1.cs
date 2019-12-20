using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts; //Core of the library
using LiveCharts.Wpf; //The WPF controls
using LiveCharts.WinForms; //the WinForm wrappers


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

        public int baslangicPopulasyonu = 0;
        public int nValue = 0;
        public int betaValue = 0;
        public int iterasyonSayisi = 0;

        public double maxFitness = 0;
        public double maxX1 = 0;
        public double maxY1 = 0;
        public int maxIter = 0;

        Random random = new Random();

        public double GetRandomNumber(double minimum, double maximum) {

            return random.NextDouble() * (maximum - minimum) + minimum;
        }



        private void Form1_Load(object sender, EventArgs e) {

            label15.Text = "";
            label16.Text = "";
        }


        private void button1_Click(object sender, EventArgs e) {

            baslangicPopulasyonu = Convert.ToInt32(textBox1.Text);
            nValue = Convert.ToInt32(textBox2.Text);
            betaValue = Convert.ToInt32(textBox3.Text);
            iterasyonSayisi = Convert.ToInt32(textBox4.Text);


            for (int i = 0; i < iterasyonSayisi; i++) {
                dataGridView9.Rows.Add();
            }


            for (int iter = 0; iter < iterasyonSayisi; iter++) {


                if (dataGridView1.Rows.Count < baslangicPopulasyonu+1) {
                    for (int i = 0; i < baslangicPopulasyonu; i++) {
                        dataGridView1.Rows.Add();
                        dataGridView8.Rows.Add();
                    }
                }
                for (int i = 0; i < baslangicPopulasyonu; i++) {

                    dataGridView2.Rows.Add();

                    if (i < nValue-1) { //????
                        dataGridView3.Rows.Add();
                        //dataGridView4.Rows.Add();


                    }
                }

                for (int i = 0; i < baslangicPopulasyonu; i++) {

                    x = GetRandomNumber(-3.0, 12.1);
                    y = GetRandomNumber(4.1, 5.8);

                    fonksiyon = 21.5 + x * Math.Sin(4 * PI * x) + y * Math.Sin(20 * PI * y);
                    fitness = fonksiyon;



                    dataGridView1.Rows[i].Cells[0].Value = i + 1;
                    dataGridView1.Rows[i].Cells[1].Value = x;
                    dataGridView1.Rows[i].Cells[2].Value = y;
                    dataGridView1.Rows[i].Cells[3].Value = fitness;


                }

                for (int i = 0; i < baslangicPopulasyonu; i++) {
                    for (int j = 1; j < 4; j++) {

                        dataGridView2.Rows[i].Cells[j].Value = dataGridView1.Rows[i].Cells[j].Value;
                    }
                }

                dataGridView2.Sort(dataGridView2.Columns["dataGridViewTextBoxColumn4"], ListSortDirection.Descending);

                for (int i = 0; i < nValue; i++) {
                    for (int j = 1; j < 4; j++) {

                        dataGridView3.Rows[i].Cells[j].Value = dataGridView2.Rows[i].Cells[j].Value;
                    }
                }



                for (int i = 0; i < baslangicPopulasyonu; i++) {
                    dataGridView2.Rows[i].Cells[0].Value = i + 1;
                    if (i < nValue) {
                        dataGridView3.Rows[i].Cells[0].Value = i + 1;
                    }
                }



                int indexJ = 0;
                for (int i = 0; i < nValue; i++) {

                    double klonIslemi = 0;

                    klonIslemi = betaValue * nValue / (i + 1);

                    klon = (int)Math.Round(klonIslemi);



                    for (int j = 0; j < klon; j++) {
                        dataGridView4.Rows.Add();

                        dataGridView4.Rows[indexJ].Cells[0].Value = indexJ + 1;
                        dataGridView4.Rows[indexJ].Cells[1].Value = dataGridView3.Rows[i].Cells[1].Value;
                        dataGridView4.Rows[indexJ].Cells[2].Value = dataGridView3.Rows[i].Cells[2].Value;
                        dataGridView4.Rows[indexJ].Cells[3].Value = dataGridView3.Rows[i].Cells[3].Value;

                        indexJ++;
                    }

                }

                for (int j = 0; j < indexJ - 1; j++) {
                    dataGridView5.Rows.Add();
                }

                double mutasyonIslemi = 0;
                Random rnd = new Random();

                int hiperMutasyonCounter = 0;

                for (int i = 0; i < dataGridView4.Rows.Count - 1; i++) {

                    hiperMutasyonCounter++;

                    mutasyonIslemi = Math.Pow(Math.E, (-10 - Convert.ToDouble(dataGridView4.Rows[i].Cells[3].Value)) / Convert.ToDouble(dataGridView4.Rows[0].Cells[3].Value)) * 2;
                    mutasyon = (int)Math.Round(mutasyonIslemi);
                    //dataGridView5.Rows[i].Cells[3].Value = mutasyon;
                    double yeniX = (double)dataGridView4.Rows[i].Cells[1].Value;
                    double yeniY = (double)dataGridView4.Rows[i].Cells[2].Value;
                    int randomXY = 0;
                    if (mutasyon == 1) {
                        randomXY = rnd.Next(0, 2);
                        if (randomXY == 0) {

                            yeniX = GetRandomNumber(-3.0, 12.1);
                        } else if (randomXY == 1) {
                            yeniY = GetRandomNumber(4.1, 5.8);
                        }

                    } else if (mutasyon == 2) {
                        yeniX = GetRandomNumber(-3.0, 12.1);
                        yeniY = GetRandomNumber(4.1, 5.8);
                    }

                    fonksiyon = 21.5 + yeniX * Math.Sin(4 * PI * yeniX) + yeniY * Math.Sin(20 * PI * yeniY);
                    fitness = fonksiyon;
                    dataGridView5.Rows[i].Cells[0].Value = hiperMutasyonCounter; //mutasyon + " - " + randomXY;
                    dataGridView5.Rows[i].Cells[1].Value = yeniX;
                    dataGridView5.Rows[i].Cells[2].Value = yeniY;
                    dataGridView5.Rows[i].Cells[3].Value = fitness;

                }


                //7. adım

                for (int i = 0; i < hiperMutasyonCounter; i++) {
                    dataGridView6.Rows.Add();
                }
                for (int i = 0; i < hiperMutasyonCounter; i++) {
                    dataGridView6.Rows[i].Cells[0].Value = i + 1;
                }
                for (int i = 0; i < hiperMutasyonCounter; i++) {
                    for (int j = 1; j < 4; j++) {

                        dataGridView6.Rows[i].Cells[j].Value = dataGridView5.Rows[i].Cells[j].Value;
                    }
                }

                dataGridView6.Sort(dataGridView6.Columns["dataGridViewTextBoxColumn20"], ListSortDirection.Descending);

                for (int i = 0; i < nValue-1; i++) {
                    dataGridView7.Rows.Add();
                }

                for (int i = 0; i < nValue; i++) {
                    for (int j = 1; j < 4; j++) {
                        dataGridView7.Rows[i].Cells[0].Value = i + 1;
                        dataGridView7.Rows[i].Cells[j].Value = dataGridView6.Rows[i].Cells[j].Value;
                    }
                }


                //8. Adım

                for (int i = 0; i < nValue; i++) {
                    for (int j = 1; j < 4; j++) {
                        dataGridView8.Rows[i].Cells[0].Value = i + 1;
                        dataGridView8.Rows[i].Cells[j].Value = dataGridView7.Rows[i].Cells[j].Value;
                    }
                }

                for (int i = nValue; i < baslangicPopulasyonu; i++) {

                    x = GetRandomNumber(-3.0, 12.1);
                    y = GetRandomNumber(4.1, 5.8);

                    fonksiyon = 21.5 + x * Math.Sin(4 * PI * x) + y * Math.Sin(20 * PI * y);
                    fitness = fonksiyon;



                    dataGridView8.Rows[i].Cells[0].Value = i + 1;
                    dataGridView8.Rows[i].Cells[1].Value = x;
                    dataGridView8.Rows[i].Cells[2].Value = y;
                    dataGridView8.Rows[i].Cells[3].Value = fitness;

                }

                for (int i = 0; i < baslangicPopulasyonu; i++) {
                    for (int j = 1; j < 4; j++) {

                        dataGridView1.Rows[i].Cells[j].Value = dataGridView8.Rows[i].Cells[j].Value;
                    }
                }

                //for (int i = 0; i < dataGridView7.Rows.Count; i++) {
                //    if (maxFitness < (double)dataGridView7.Rows[iter].Cells[3].Value) {
                //            maxFitness = (double)dataGridView7.Rows[iter].Cells[3].Value;
                //            dataGridView8.Rows[i].Cells[0].Value = i + 1;
                //        }
                //}


                dataGridView9.Rows[iter].Cells[0].Value = iter + 1;
                dataGridView9.Rows[iter].Cells[1].Value = dataGridView7.Rows[0].Cells[1].Value;
                dataGridView9.Rows[iter].Cells[2].Value = dataGridView7.Rows[0].Cells[2].Value;
                dataGridView9.Rows[iter].Cells[3].Value = dataGridView7.Rows[0].Cells[3].Value;


                if (iter < iterasyonSayisi - 1) {
                    dataGridView2.Rows.Clear();
                    dataGridView2.Refresh();
                    dataGridView3.Rows.Clear();
                    dataGridView3.Refresh();
                    dataGridView4.Rows.Clear();
                    dataGridView4.Refresh();
                    dataGridView5.Rows.Clear();
                    dataGridView5.Refresh();
                    dataGridView6.Rows.Clear();
                    dataGridView6.Refresh();
                    dataGridView7.Rows.Clear();
                    dataGridView7.Refresh();
                }




            }

            for (int i = 0; i < dataGridView9.Rows.Count-1; i++) {
                if (maxFitness < (double)dataGridView9.Rows[i].Cells[3].Value) {
                    maxFitness = (double)dataGridView9.Rows[i].Cells[3].Value;
                    maxIter = (int)dataGridView9.Rows[i].Cells[0].Value;
                    maxX1 = (double)dataGridView9.Rows[i].Cells[1].Value;
                    maxY1 = (double)dataGridView9.Rows[i].Cells[2].Value;
                }
            }

            label15.Text = "Çözüm = " + maxFitness + " " + maxIter + ".Iterasyon";
            label16.Text = "x1 = " + maxX1 + "\n" + "x2 = " + maxY1;


            List<double> fitnessList = new List<double>();

            for (int i = 0; i < dataGridView9.Rows.Count-1; i++) {
                fitnessList.Add((double)dataGridView9.Rows[i].Cells[3].Value);
            }


            cartesianChart1.Series = new SeriesCollection {
                new LineSeries {
                    Title = "Yapay Bağışıklık Çözümleri",
                    Values = new ChartValues<double>(fitnessList)
                }
            };
            

        }

        private void button3_Click(object sender, EventArgs e) {

            foreach (Control c in Controls) {
                switch (c) {
                    case TextBox tb:
                        tb.Text = "";
                        break;
                    
                    case DataGridView dgt:
                        dgt.Rows.Clear();
                        dgt.Refresh();
                        break;
                }
            }
            label15.Text = "";
            label16.Text = "";

            x = 0;
            y = 0;
            fonksiyon = 0;
            fitness = 0;

            klon = 0;
            mutasyon = 0;

            baslangicPopulasyonu = 0;
            nValue = 0;
            betaValue = 0;
            iterasyonSayisi = 0;
            maxFitness = 0;
            maxX1 = 0;
            maxY1 = 0;
            maxIter = 0;

            cartesianChart1.Series = null;
        }
    }
}
