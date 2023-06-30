using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool startStatus = true;
        const double mu = 0.2, sigma = 0.35;

        Random rnd = new Random();
        double normalRV1, normalRV2;
        double W1 = 0, W2 = 0;
        double l1, l2;
        const double k = 0.1;

        double euro, dollar;

        int day = 0;

        private void StartStopBtn_Click(object sender, EventArgs e)
        {
            euro = (double)edEuro.Value;
            dollar = (double)edDollar.Value;
            if (startStatus)
            {
                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                timer1.Start();
                startStatus = false;
            }
            else
            {
                timer1.Stop();
                startStatus = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            day += 1;

            l1 = rnd.NextDouble();
            l2 = rnd.NextDouble();

            normalRV1 = Math.Sqrt((-2) * Math.Log(l1)) * Math.Sin(2 * Math.PI * l2);
            W1 = /*W1 +*/ (double)(Math.Sqrt(k) * normalRV1 * 0.25);
            euro = euro * Math.Exp((mu - (double)((sigma * sigma) / 2)) * k * 0.25 + (double)(sigma * W2));

            normalRV2 = Math.Sqrt((-2) * Math.Log(l1)) * Math.Cos(2 * Math.PI * l2);
            W2 = /*W2 +*/ (double)(Math.Sqrt(k) * normalRV2 * 0.25);
            dollar = dollar * Math.Exp((mu - (double)((sigma * sigma) / 2)) * k * 0.25 + (double)(sigma * W1));

            chart1.Series[0].Points.AddXY(day, euro);
            chart1.Series[1].Points.AddXY(day, dollar);
            edEuro.Text = euro.ToString();
            edDollar.Text = dollar.ToString();
        }
    }
}
