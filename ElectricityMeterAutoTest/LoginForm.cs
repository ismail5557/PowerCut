using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BaylanElectricAutoTest
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void GirisButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Server=ISMAILUYULMUSLA\\SQLEXPRESS01; " +
                                                  "Initial Catalog=MeterAutoTest; " +
                                                  "User id = sa;" +
                                                  "Password=1453;"))
            {
                con.Open();
                string UserName = TboxUserName.Text;
                string UserPw = TboxUserPw.Text;
                /*SqlCommand command_UserName = new SqlCommand("SELECT [Name] FROM UserTable Where [Name] = @UserName", con);*/
                SqlCommand command = new SqlCommand("SELECT [Name] FROM UserTable WHERE [Name] = @UserName AND [Password] = @UserPw", con);
                command.Parameters.AddWithValue("@UserName", UserName);
                //object result_UserName = command_UserName.ExecuteScalar();
                //SqlCommand command_UserPw = new SqlCommand("SELECT [Name] FROM UserTable Where [Name] = @UserPw", con);
                command.Parameters.AddWithValue("@UserPw", UserPw);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    string isim = result.ToString();
                    label3.Text = isim + "  :Basarılı giris";
                    if (AdminCheckBox.Checked)
                    {
                        var form2 = new AdminForm();
                        form2.Show();

                    }
                    else
                    {
                        var form3 = new TesterInterface();
                        form3.Show();
                    }
                    
                }
                else
                {
                    label3.Text = "kullanıcı adı veya sifre hatalı";
                }
                con.Close();
                
            }   
            
        }

        private void TboxUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdminCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TboxUserPw_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
