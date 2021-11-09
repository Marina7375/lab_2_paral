using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        static int[] a;
        static int[] b;
        static int k;
        static int min2, min1;

        public Form1()
        {
            InitializeComponent();
          
        }
        

         private void textBox6_TextChanged(object sender, EventArgs e)
         {

         }

         private void label1_Click(object sender, EventArgs e)
         {

         }

         private void label3_Click(object sender, EventArgs e)
         {

         }
        
        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            int col = Convert.ToInt32(textBox1.Text);
            k = Convert.ToInt32(textBox2.Text);
           int  M = Convert.ToInt32(textBox3.Text);
            a = new int[col];
            b = new int[col];
            var R = new Random();
            for (int i = 0; i < col; i++)
                a[i] = R.Next(99);
            DateTime dt1 = DateTime.Now;
            //поиск минимальных элементов
            static void Min(object o)
            {
                min1 = 0;
                for (; min1 < a.Length && a[min1] < 0; min1++) ;
                min2 = min1;
                for (int i = min1 + 1; i < a.Length; i++)
                    if (a[i] >= 0)
                        if (a[i] < a[min1])
                        {
                            if (a[min1] != a[min2])
                                min2 = min1;

                            min1 = i;
                        }
                        else if (a[i] < a[min2])
                            min2 = i;
            }
            if (M == 1)
            {
                for (int i = 0; i < 1000; i++)
                {
                    var thr = new Thread(Min);
                    thr.Start(new int[] { 0, col });
                    thr.Join();
                }
            }
            else
            {
                //массив потоков 
                for (int i = 0; i < 1000; i++)
                {
                    //стартовые и конечные позиции, шаг (равное кол-о элементов)
                    int Step = col/ M;
                    int Start = -Step;
                    int End = 0;
                    Thread[] arrThr = new Thread[M];
                    //инициализация и запуск потоков в цикле
                    for (int j = 0; j < M; j++)
                    {
                        arrThr[j] = new Thread(Min);
                        arrThr[j].Start(new int[] { Start += Step, End += Step });
                    }
                    //последовательное завершение(блокировка потоков)
                    for (int j = 0; j < M; j++)
                        arrThr[j].Join();
                }
            }
            
            DateTime dt2 = DateTime.Now;
           
           
            textBox4.Text = ((dt2 - dt1).TotalMilliseconds / 1000).ToString();
            textBox5.Text += (string.Join(" ", a).ToString());
            textBox6.Text += (string.Join(" ", a[min1],a[min2]).ToString());
            a = null;
            b = null;
            ;
        }

    }
}
