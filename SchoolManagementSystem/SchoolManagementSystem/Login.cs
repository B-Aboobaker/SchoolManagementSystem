using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            // Hide the password by default
            txtPassword.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Check if the username and password match the database records
            // Query the UserDetails table in the database

            // Example code using ADO.NET for SQL Server
            string connectionString = "Connection String Here";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM UserDetails WHERE Username = @Username AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    // Login successful
                    // Navigate to the management form

                    // Instantiate the StudentManagement form
                    StudentManagement studentManagementForm = new StudentManagement();

                    // Show the StudentManagement form
                    studentManagementForm.Show();

                    // Close the current form (login form)
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearLoginForm();
        }

        private void ClearLoginForm()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            chkShowPassword.Checked = false;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                // Show the password
                txtPassword.PasswordChar = '\0'; // Set the PasswordChar to '\0' (null character) to display the actual password characters
            }
            else
            {
                // Hide the password
                txtPassword.PasswordChar = '*'; // Set the PasswordChar back to '*' to display asterisks
            }
        }

        private void lblCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Hide the current login form
            this.Hide();

            // Show the sign-up form
            SignUp signUpForm = new SignUp();
            signUpForm.Show();
        }
    }
}
