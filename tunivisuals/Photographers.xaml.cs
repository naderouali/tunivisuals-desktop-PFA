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
    /// Interaction logic for Photographers.xaml
    /// </summary>
    public partial class Photographers : Window
    {
        //connexion avec bd
        MySqlConnection conn = new MySqlConnection(@"Data Source=localhost;port=3306;Initial Catalog=tunivisuals;User Id=root;password=''");

        public Photographers()
        {
            InitializeComponent();
            fillCombo();
        }

        void fillCombo()
        {
            try
            {
                //open databse
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from photographer", conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    string code = dr.GetString(0);
                    comboPhotog.Items.Add(code);
                }
                conn.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void drop(object sender, EventArgs e)
        {
            try
            {
                //open databse
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from photographer where code='" + comboPhotog.Text + "' ", conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string code = dr.GetString(0);
                    string name = dr.GetString(1);
                    string lastname = dr.GetString(2);
                    string salary = dr.GetDouble(3).ToString();
                    string exp = dr.GetInt32(4).ToString();

                    codeText.Text = code;
                    nameText.Text = name;
                    lastnameText.Text = lastname;
                    salaryText.Text = salary;
                    expText.Text = exp;

                }
                conn.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            
            try
            {
                //open databse
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from photographer", conn);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter dataAdp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable("photographer");
                dataAdp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);

                conn.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                //open databse
                conn.Open();
                //make a command
                MySqlCommand cmd = new MySqlCommand("Update photographer set code='"+ this.codeText.Text +"',photographername='"+ this.nameText.Text + "',photographerlastname='" + this.lastnameText.Text + "',salary='" + this.salaryText.Text + "',expyear='" + this.expText.Text + "' where code='" + this.codeText.Text + "' ", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Photographer updated");
                conn.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                //open databse
                conn.Open();
                //make a command
                MySqlCommand cmd = new MySqlCommand("Delete from photographer where code='"+ this.codeText.Text +"' ", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Photographer deleted");
                conn.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            Add_new_photographer addPh = new Add_new_photographer();
            addPh.Show();
        }
    }
}
