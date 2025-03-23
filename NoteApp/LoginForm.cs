using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace AppNote
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            Database.Initialize();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (Database.Login(username, password))
            {
                this.Hide();
                MainForm mainForm = new MainForm(username);
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Błędne dane logowania lub brak konta.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (Database.Register(username, password))
            {
                MessageBox.Show("Rejestracja zakończona sukcesem. Możesz się teraz zalogować.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Błąd rejestracji! Użytkownik już istnieje.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
