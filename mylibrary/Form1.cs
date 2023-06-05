using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mylibrary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBookAdd_Click(object sender, EventArgs e)
        {
            Form addbook = new AddBook();
            addbook.Show();
        }

        private void btnBookview_Click(object sender, EventArgs e)
        {
            Form bookview = new BooksView();
            bookview.Show();
        }
    }
}
