using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace tunivisuals
{
    /// <summary>
    /// Interaction logic for Add_new_photographer.xaml
    /// </summary>
    public partial class Add_new_photographer : Window
    {
        //connexion avec bd
        MySqlConnection conn = new MySqlConnection(@"Data Source=localhost;port=3306;Initial Catalog=tunivisuals;User Id=root;password=''");

        public Add_new_photographer()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                //open databse
                conn.Open();

                //make a command for adding
                string adminnum = "1";
                MySqlCommand cmd = new MySqlCommand("Insert into photographer (code,photographername,photographerlastname,salary,expyear,code_admin) values('"+ Convert.ToInt32(this.codeText.Text) + "','" + this.nameText.Text + "','" + this.lastnameText.Text + "','" + Convert.ToDouble(this.salaryText.Text) + "','" + Convert.ToInt32(this.expText.Text) + "','" + Convert.ToInt32(adminnum) + "')  ", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Photographer saved");
                this.Close();
                conn.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
