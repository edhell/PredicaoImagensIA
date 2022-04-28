using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoTestesSoftware
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        // Botão Entrar
        private void button1_Click(object sender, EventArgs e)
        {
            if(LoginInApp(textBox1.Text, textBox2.Text))
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos!");
            }
        }

        public static bool LoginInApp(String login, String senha)
        {
            if (login == "admin" && senha == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        // Botão Sair
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Link Cadastrar
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Em desenvolvimento");
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            //button1.PerformClick();
                    }
    }
}
