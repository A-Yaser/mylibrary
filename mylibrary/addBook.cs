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
    public partial class AddBook : Form
    {
        //sql connection
        SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=almaarifa;Integrated Security=True");
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public AddBook()
        {
            InitializeComponent();
        }

        private void clearTxt()
        {
            txtBookName.Text = "";
            txtAuthorName.Text = "";
            txtCategory.Text = "";
            txtPublisher.Text = "";
            txtPrice.Text = "";
            richTxtDescription.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            

            if (txtBookName.Text.Trim() != "" && txtAuthorName.Text.Trim() != "" && txtPrice.Text.Trim() != "")
            {
                
                try
                {
                    string book_name = txtBookName.Text.Trim();
                    string author_name = txtAuthorName.Text.Trim();
                    string category_name = txtCategory.Text.Trim();
                    string publisher_name = txtPublisher.Text.Trim();
                    int price = int.Parse(txtPrice.Text.Trim());
                    string description = richTxtDescription.Text.Trim();



                    connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandText = $"INSERT INTO books (book_name, author_name, category_name, publisher_name, price, description) VALUES ('{ book_name }','{author_name}','{category_name}','{publisher_name}','{price}','{description}')";
                    MessageBox.Show("تمت الإضافة بنجاح");
                    clearTxt();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                    connection.Close();
                }
            }

            else
            {
                MessageBox.Show("لا يمكن ترك حقل اسم الكتاب أو اسم المؤلف أو السعر فارغ");
            }


            }
        

        private void cansleSave_Click(object sender, EventArgs e)
        {
            this.Close();
            clearTxt();
        }
    }
}
