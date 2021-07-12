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
using System.Configuration;
using System.Web;

namespace tunivisuals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            
            //connexion avec bd
            MySqlConnection conn = new MySqlConnection(@"Data Source=localhost;port=3306;Initial Catalog=tunivisuals;User Id=root;password=''");
            

            try
            {
                if (emailText.Text.Length == 0)
                {
                    errormessage.Text = "Enter an email.";
                    emailText.Focus();
                }
                else if (passwordText.Password.Length == 0)
                {
                    errormessage.Text = "Enter a password.";
                    passwordText.Focus();
                }

                else
                {
                    //recupere ladress et le passwrd et encrypt psswrd
                    string email = emailText.Text;
                    string password2 = passwordText.Password;
                    string password = MD5Hash(password2);
                    //open databse
                    conn.Open();
                    //commande check email & psswrd
                    MySqlCommand cmd = new MySqlCommand("Select * from admin where admin_email='" + email + "'  and admin_pw='" + password + "'", conn);
                    cmd.CommandType = CommandType.Text;
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    //commande recupere id
                    MySqlCommand cmdID = new MySqlCommand("Select admin_code from admin where admin_email='" + email + "'  and admin_pw='" + password + "'", conn);
                    cmdID.CommandType = CommandType.Text;
                    MySqlDataAdapter adapterID = new MySqlDataAdapter();
                    adapterID.SelectCommand = cmdID;

                    //fetching data
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    //testing
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        interfaceAdmin iAdmin = new interfaceAdmin();
                        string adminname = dataSet.Tables[0].Rows[0]["admin_name"].ToString();
                        string id = dataSet.Tables[0].Rows[0]["admin_code"].ToString();
                        iAdmin.ccAdmin.Text = adminname;
                        iAdmin.codeText.Text = id;
                        iAdmin.nameText.Text = adminname;
                        
                        iAdmin.Show();
                        this.Close();

                    }
                    else
                    {
                        errormessage.Text = "Wrong admin email/password.";
                    }
                    conn.Close();
                    
                }
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


        private void close_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        

        private static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        
    }
}
