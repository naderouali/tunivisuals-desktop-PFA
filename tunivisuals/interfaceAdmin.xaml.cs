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
    /// Interaction logic for interfaceClient.xaml
    /// </summary>
    public partial class interfaceAdmin : Window
    {
        //connexion avec bd
        MySqlConnection conn = new MySqlConnection(@"Data Source=localhost;port=3306;Initial Catalog=tunivisuals;User Id=root;password=''");

        public interfaceAdmin()
        {
            InitializeComponent();
        }

        private void read_Click(object sender, EventArgs e)
        {
            try
            {
                //open databse
                conn.Open();

                //commande recupere 
                MySqlCommand cmd = new MySqlCommand("Select * from photographer", conn);
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;

                //fetching data
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                Photographers photogs = new Photographers();
                photogs.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void logout_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }

}
