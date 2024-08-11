using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();

            // Hide the password by default
            txtPassword.PasswordChar = '*';
            txtConfirmPassword.PasswordChar = '*';
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Check if the username is empty
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
                return;
            }

            // Check if the password is empty
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a password.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }

            // Check if the username already exists in the database
            // Query the UserDetails table in the database

            // Example code using ADO.NET for SQL Server
            string connectionString = "Connection String Here";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM UserDetails WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Check if the password and confirm password match
            if (password != confirmPassword)
            {
                MessageBox.Show("Password and Confirm Password do not match. Please re-enter the passwords.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return;
            }

            // Insert the new user into the UserDetails table
            // Example code using ADO.NET for SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO UserDetails (Username, Password) VALUES (@Username, @Password)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Registration successful. Please log in.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Hide the signup form
                    this.Hide();

                    // Show the login form
                    Login loginForm = new Login();
                    loginForm.Show();
                }
                else
                {
                    MessageBox.Show("Registration failed. Please try again.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSignUpForm();
        }

        private void ClearSignUpForm()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            chkShowPassword.Checked = false;
        }

        private void linkBackToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Close the current form (sign up form)
            this.Hide(); // Hide the SignUp form instead of closing it

            // Instantiate the login form
            Login loginForm = new Login();

            // Show the login form
            loginForm.Show();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                // Show the password
                txtPassword.PasswordChar = '\0'; // Set the PasswordChar to '\0' (null character) to display the actual password characters
                txtConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                // Hide the password
                txtPassword.PasswordChar = '*'; // Set the PasswordChar back to '*' to display asterisks
                txtConfirmPassword.PasswordChar = '*';
            }
        }
    }
}
