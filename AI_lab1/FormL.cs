using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_lab1
{
    public partial class FormL : Form
    {
        public int L = 0;
        public FormL()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            L = (int)numericL.Value;
            Close();
        }
    }
}
