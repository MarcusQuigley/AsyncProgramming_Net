using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Data_Parallelism
{
    public partial class PLinQ : Form
    {
        public PLinQ()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            Go();
        }
        
        private  void Go()
        {
            int[] source = Enumerable.Range(1, 30000).ToArray();

            int[] modThreeIsZero = (from num in source.AsParallel() where num % 3 == 0 
                                    orderby num descending select num).ToArray();

            foreach (int i in modThreeIsZero)
                textBox1.AppendText(i + ", ");


        }
    }
}
