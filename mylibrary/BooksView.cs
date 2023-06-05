using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace mylibrary
{
    public partial class BooksView : Form
    {

        SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=almaarifa;Integrated Security=True");
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        string[] filters = { "اسم الكتاب", "اسم المؤلف", "التصنيف", "الناشر", "السعر" };
        Int32 rowId;
        Int32 rowCount;
        Int32 rowCountViewing;



        public BooksView()
        {
            InitializeComponent();
            comboBox1.DataSource = filters;
        }

        private void clearTxt()
        {
            txtBookName.Clear();
            txtAuthorName.Clear();
            txtCategory.Clear();
            txtPublisher.Clear();
            txtPrice.Clear();
            richTxtDescription.Clear();
            txtSeerch.Clear();
        }




        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            clearTxt();
        }



        private void BooksView_Load(object sender, EventArgs e)
        {
            var commandText = "SELECT * FROM books";
            command.Connection = connection;
            command.CommandText = commandText;
            DataTable dataTable = new DataTable();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            rowCount = dataGridView1.Rows.Count - 1;
        }


        private void refrish()
        {

            var commandText = "SELECT * FROM books";
            command.Connection = connection;
            command.CommandText = commandText;
            DataTable dataTable = new DataTable();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            panel1.Visible = false;
            clearTxt();
            rowCount = dataGridView1.Rows.Count - 1;
        }

        private void btnRefrish_Click(object sender, EventArgs e)
        {

            //var commandText = "SELECT * FROM books";
            //command.Connection = connection;
            //command.CommandText = commandText;
            //DataTable dataTable = new DataTable();

            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            //DataSet dataSet = new DataSet();
            //sqlDataAdapter.Fill(dataSet);
            //dataGridView1.DataSource = dataSet.Tables[0];
            //panel1.Visible = false;
            //clearTxt();
            //rowCount = dataGridView1.Rows.Count - 1;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int bookId;
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    // MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    ///الطريقة الأولى
                    //txtBookName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    //txtAuthorName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    //txtCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    //txtPublisher.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    //txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    //richTxtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();


                    ///الطريقة الثانية
                    bookId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                    var commandText = $"SELECT * FROM books where book_id= {bookId}";
                    command.Connection = connection;
                    command.CommandText = commandText;
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);
                    rowId = int.Parse(dataSet.Tables[0].Rows[0][0].ToString());
                    panel1.Visible = true;
                    txtBookName.Text = dataSet.Tables[0].Rows[0][1].ToString();
                    txtAuthorName.Text = dataSet.Tables[0].Rows[0][2].ToString();
                    txtCategory.Text = dataSet.Tables[0].Rows[0][3].ToString();
                    txtPublisher.Text = dataSet.Tables[0].Rows[0][4].ToString();
                    txtPrice.Text = dataSet.Tables[0].Rows[0][5].ToString();
                    richTxtDescription.Text = dataSet.Tables[0].Rows[0][6].ToString();
                }
            }
            catch
            {

            }
        }

        private void txtSeerch_TextChanged(object sender, EventArgs e)
        {
            string filter;
            switch (comboBox1.Text)
            {
                case "اسم الكتاب":
                    filter = "book_name";
                    break;

                case "اسم المؤلف":
                    filter = "author_name";
                    break;

                case "التصنيف":
                    filter = "category_name";
                    break;

                case "الناشر":
                    filter = "publisher_name";
                    break;

                case "السعر":
                    filter = "price";
                    break;
                default:
                    filter = "book_name";
                    break;

            }
            var commandText = $"SELECT * FROM books where {filter} Like '{txtSeerch.Text}%'";
            command.Connection = connection;
            command.CommandText = commandText;
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            rowCountViewing = dataGridView1.Rows.Count - 1;
            booksCount2.Text = rowCountViewing.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("سيتم تعديل الكتاب، هل تريد متابعة الإجراء", "تعديل كتب", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    string book_name = txtBookName.Text;
                    string author_name = txtAuthorName.Text;
                    string category_name = txtCategory.Text;
                    string publisher_name = txtPublisher.Text;
                    int price = int.Parse(txtPrice.Text);
                    string description = richTxtDescription.Text;

                    if (book_name != "" && author_name != "")
                    {

                        var commandText = $"UPDATE books SET book_name = '{book_name}', author_name = '{author_name}', category_name = '{category_name}', publisher_name= '{publisher_name}', price = {price}, description = '{description}' WHERE book_id = {rowId}";

                        command.Connection = connection;
                        command.CommandText = commandText;
                        // DataTable dataTable = new DataTable();

                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                        DataSet dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        MessageBox.Show("تم التعديل بنجاح");
                        refrish();
                    }
                    else
                    {
                        MessageBox.Show("لا يمكن ترك حقل اسم الكتاب أو المؤلف فارغ");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



      

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("سيتم حذف الكتاب، هل تريد متابعة الإجراء", "حذف كتاب", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {

                    var commandText = $"delete from books WHERE book_id = {rowId}";

                    command.Connection = connection;
                    command.CommandText = commandText;
                    // DataTable dataTable = new DataTable();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);
                   MessageBox.Show("تم الحذف بنجاح"); 
                   refrish();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BooksView_Activated(object sender, EventArgs e)
        {
            var commandText = "SELECT * FROM books";
            command.Connection = connection;
            command.CommandText = commandText;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            rowCount = dataSet.Tables[0].Rows.Count;
            booksCount1.Text = rowCount.ToString();
        }
    }
}
