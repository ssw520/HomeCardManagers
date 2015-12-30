using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string pwd = txtPassword.Text;
            if (name.Trim() == "" || pwd == "")
            {
                MessageBox.Show("用户名和密码都不能为空");
            }
            else
            {
                Users user = new Users();
                user.LoginName = name;
                user.Password = pwd;

                UsersService service = new UsersService();
                user = service.CheckLogin(user);
                if (user == null)
                {
                    MessageBox.Show("登录失败,可能的错误消息是："+DBHelper.Message);
                }
                else
                {
                    MessageBox.Show("登录成功");
                    MainForm main = new MainForm();
                    ThirdData.user = user;
                    main.Show();
                    this.Hide(); 
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
