using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Encoding = System.Text.Encoding;

namespace UpdateSQLPassword
{
    public partial class UpdateSQL : Form
    {
        private static string dbConnectionPath = "C:\\ProgramData\\Add-On Products\\Digital Sign Service\\DatabaseConnection\\Info.xml";
        public UpdateSQL()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            label3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                label3.Text = "Please fill out all the fields!";
                label3.ForeColor = System.Drawing.Color.Red;
                label3.Visible = true;
                button2.Enabled = false;
                return;
            }

            if (textBox1.Text != textBox2.Text)
            {
                label3.Text = "Password and password confirmation does not match!";
                label3.ForeColor = System.Drawing.Color.Red;
                label3.Visible = true;
                button2.Enabled = false;
                return;
            }

            var newPassword = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(textBox1.Text));
            var textInfoXml = File.ReadAllText(dbConnectionPath);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(textInfoXml);
            var passwordNodes = doc.SelectNodes("/DatabaseConnection/Password");
            passwordNodes[0].InnerText = newPassword;
            var loginIdNodes = doc.SelectNodes("/DatabaseConnection/LoginID");
            loginIdNodes[0].InnerText = textBox3.Text;
            doc.Save(dbConnectionPath);

            button1_Click(sender, e);

            label3.Text = "SQL credentials updated successfully.";
            label3.ForeColor = System.Drawing.Color.Green;
            label3.Visible = true;
            button2.Enabled = false;

            return;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            label3.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            label3.Visible = false;
        }
    }
}
