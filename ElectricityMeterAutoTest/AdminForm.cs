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
using System.Runtime.Remoting.Contexts;
using System.Xml.Linq;
using BaylanElectricAutoTest.Entity;
using System.Data.Entity;
using System.Windows.Forms.VisualStyles;

namespace BaylanElectricAutoTest
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            UserTable user = new UserTable();
        }


        private void AddButton_Click(object sender, EventArgs e)
        {
            UserTable user = new UserTable();
            Guid guid = Guid.NewGuid();
            user.Guid = guid;
            user.Email = null;
            user.Name = UserName.Text;
                try
                {
                    user.Password = Convert.ToInt32(UserPw.Text);
                
            
            user.RoleId = 0;
            user.LanguageId = 0;
            user.EmailConfirmed = false;
            user.PhoneNumberConfirmed = false;
            user.ActiveStatus = 0;
            user.CreatedDate = DateTime.Now;
            user.LastEditDate = DateTime.Now;
            user.PasswordChangeDate = DateTime.Now;

            using (SqlConnection con = new SqlConnection("Server=ISMAILUYULMUSLA\\SQLEXPRESS01; " +
                                                  "Initial Catalog=MeterAutoTest; " +
                                                  "User id = sa;" +
                                                  "Password=1453;" ))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO UserTable (Name, Password,Guid,RoleId,LanguageId,EmailConfirmed,PhoneNumberConfirmed,ActiveStatus,CreatedDate,LastEditDate,PasswordChangeDate) VALUES (@Name, @Password, @Guid,@RoleId,@LanguageId,@EmailConfirmed,@PhoneNumberConfirmed,@ActiveStatus,@CreatedDate,@LastEditDate,@PasswordChangeDate)", con);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Guid", user.Guid);
                cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
                cmd.Parameters.AddWithValue("@LanguageId", user.LanguageId);
                cmd.Parameters.AddWithValue("@EmailConfirmed", user.EmailConfirmed);
                cmd.Parameters.AddWithValue("@PhoneNumberConfirmed", user.PhoneNumberConfirmed);
                cmd.Parameters.AddWithValue("@ActiveStatus", user.ActiveStatus);
                cmd.Parameters.AddWithValue("@CreatedDate", user.CreatedDate);
                cmd.Parameters.AddWithValue("@LastEditDate", user.LastEditDate);
                cmd.Parameters.AddWithValue("@PasswordChangeDate", user.PasswordChangeDate);
                cmd.ExecuteNonQuery();
                con.Close();
                
            }
            MessageBox.Show("Basarılı");
            }
            catch (Exception)
            {
                MessageBox.Show("şifre yanlış");
            }
        }

        private void UserPw_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserName_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
