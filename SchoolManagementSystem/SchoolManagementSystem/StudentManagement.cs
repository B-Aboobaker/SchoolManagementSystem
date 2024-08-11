using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class StudentManagement : Form
    {
        public StudentManagement()
        {
            InitializeComponent();

            // Populate the course combo box in the Student Management tab
            PopulateCourseComboBox(cmbCourseEnrolled);
            PopulateCourseComboBox(cmbCourseNameC);
        }

        private void PopulateCourseComboBox(ComboBox comboBox)
        {
            // Clear the combo box items before populating
            comboBox.Items.Clear();
            cmbCategoryC.Items.Clear();

            string connectionString = "Connection String Here";
            string query = "SELECT CourseName, Category FROM CourseDetails;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string courseName = reader["CourseName"].ToString();
                        string category = reader["Category"].ToString();

                        comboBox.Items.Add(courseName);
                        cmbCategoryC.Items.Add(category);
                    }
                }
            }

            // Set the default value as the selected item
            comboBox.SelectedIndex = 0;

            // Set the default value for category
            cmbCategoryC.SelectedIndex = 0;
        }


        // Student Management Form
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTabName = tabControl.SelectedTab.Text;
            this.Text = $"{selectedTabName}";

            if (selectedTabName == "Teacher Management")
            {
                // Populate the course and department combo boxes in the Teacher Management tab
                PopulateCourseComboBox(cmbCourse);
                PopulateDepartmentComboBox(cmbDepartment);

                // Set the default values for the combo boxes
                cmbCourse.SelectedIndex = 0;
                cmbDepartment.SelectedIndex = 0;
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            string connectionString = "Connection String Here";
            string studentID = txtStudentID.Text;
            string fullName = txtFullName.Text;
            string gender = rbtnMale.Checked ? "Male" : "Female";
            string courseEnrolled = cmbCourseEnrolled.Text;
            string contactNumber = txtContactNumber.Text;
            string address = rtxtAddress.Text;

            // Check if any of the required fields are empty
            if (string.IsNullOrWhiteSpace(studentID) || string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(courseEnrolled) ||
                string.IsNullOrWhiteSpace(contactNumber) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Insert the student details into the StudentDetails table
            string insertQuery = "INSERT INTO StudentDetails (FullName, Gender, CourseEnrolled, ContactNumber, Address) " +
                "VALUES (@FullName, @Gender, @CourseEnrolled, @ContactNumber, @Address);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@FullName", fullName);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@CourseEnrolled", courseEnrolled);
                command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                command.Parameters.AddWithValue("@Address", address);

                connection.Open();
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Student added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear the form controls
            ClearStudentForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = "Connection String Here";
            string searchText = txtSearch.Text;

            // Search for students in the StudentDetails table based on the provided search text
            string searchQuery = "SELECT * FROM StudentDetails WHERE FullName LIKE '%' + @SearchText + '%';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(searchQuery, connection);
                command.Parameters.AddWithValue("@SearchText", searchText);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Clear the previous search results from the rich text box
                    rtxtSearchResults.Text = string.Empty;

                    while (reader.Read())
                    {
                        // Append the search results to the rich text box
                        rtxtSearchResults.AppendText($"Student ID: {reader["StudentID"]}\n");
                        rtxtSearchResults.AppendText($"Full Name: {reader["FullName"]}\n");
                        rtxtSearchResults.AppendText($"Gender: {reader["Gender"]}\n");
                        rtxtSearchResults.AppendText($"Course Enrolled: {reader["CourseEnrolled"]}\n");
                        rtxtSearchResults.AppendText($"Contact Number: {reader["ContactNumber"]}\n");
                        rtxtSearchResults.AppendText($"Address: {reader["Address"]}\n");
                        rtxtSearchResults.AppendText("------------------------------------\n");
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = "Connection String Here";
            string studentID = txtStudentID.Text;

            // Check if a student is selected
            if (string.IsNullOrWhiteSpace(studentID))
            {
                MessageBox.Show("Please select a student ID to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve the existing student details from the database
            string selectQuery = "SELECT * FROM StudentDetails WHERE StudentID = @StudentID;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@StudentID", studentID);

                connection.Open();

                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Get the existing values from the database
                        string existingFullName = reader["FullName"].ToString();
                        string existingGender = reader["Gender"].ToString();
                        string existingCourseEnrolled = reader["CourseEnrolled"].ToString();
                        string existingContactNumber = reader["ContactNumber"].ToString();
                        string existingAddress = reader["Address"].ToString();

                        // Close the SqlDataReader before executing the update command
                        reader.Close();

                        // Get the new values from the form controls
                        string newFullName = txtFullName.Text;
                        string newGender = rbtnMale.Checked ? "Male" : "Female";
                        string newCourseEnrolled = cmbCourseEnrolled.Text;
                        string newContactNumber = txtContactNumber.Text;
                        string newAddress = rtxtAddress.Text;

                        // Create a list to store the updated fields
                        var updatedFields = new List<string>();

                        // Compare existing and new values for each field and add it to the updatedFields list if changed
                        if (!string.Equals(existingFullName, newFullName))
                            updatedFields.Add("FullName");

                        if (!string.Equals(existingGender, newGender))
                            updatedFields.Add("Gender");

                        if (!string.Equals(existingCourseEnrolled, newCourseEnrolled))
                            updatedFields.Add("CourseEnrolled");

                        if (!string.Equals(existingContactNumber, newContactNumber))
                            updatedFields.Add("ContactNumber");

                        if (!string.Equals(existingAddress, newAddress))
                            updatedFields.Add("Address");

                        // Check if any fields have been modified
                        if (updatedFields.Count > 0)
                        {
                            // Construct the SQL update statement dynamically based on the changed fields
                            string updateQuery = "UPDATE StudentDetails SET ";

                            for (int i = 0; i < updatedFields.Count; i++)
                            {
                                updateQuery += $"{updatedFields[i]} = @{updatedFields[i]}";

                                if (i < updatedFields.Count - 1)
                                    updateQuery += ", ";
                            }

                            // Append the condition for updating a specific student
                            updateQuery += " WHERE StudentID = @StudentID;";

                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                // Add parameters for the updated fields
                                foreach (string field in updatedFields)
                                {
                                    switch (field)
                                    {
                                        case "FullName":
                                            updateCommand.Parameters.AddWithValue($"@{field}", newFullName);
                                            break;
                                        case "Gender":
                                            updateCommand.Parameters.AddWithValue($"@{field}", newGender);
                                            break;
                                        case "CourseEnrolled":
                                            updateCommand.Parameters.AddWithValue($"@{field}", newCourseEnrolled);
                                            break;
                                        case "ContactNumber":
                                            updateCommand.Parameters.AddWithValue($"@{field}", newContactNumber);
                                            break;
                                        case "Address":
                                            updateCommand.Parameters.AddWithValue($"@{field}", newAddress);
                                            break;
                                    }
                                }

                                // Add the parameter for the student ID
                                updateCommand.Parameters.AddWithValue("@StudentID", studentID);

                                // Execute the update command
                                updateCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Student updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No changes were made to the student details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = "Connection String Here";
            string studentID = txtStudentID.Text;

            // Check if a student is selected
            if (string.IsNullOrWhiteSpace(studentID))
            {
                MessageBox.Show("Please select a student to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Delete the corresponding student record from the database
            string deleteQuery = "DELETE FROM StudentDetails WHERE StudentID = @StudentID;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@StudentID", studentID);

                connection.Open();
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Student deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearStudentForm();
            rbtnMale.Checked = false;
            rbtnFemale.Checked = false;
        }

        private void ClearStudentForm()
        {
            // Clear the form controls for adding a student
            txtStudentID.Text = string.Empty;
            txtFullName.Text = string.Empty;
            cmbCourseEnrolled.SelectedIndex = -1;
            txtContactNumber.Text = string.Empty;
            rtxtAddress.Text = string.Empty;
        }


        // Teacher Management Form
        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            string connectionString = "Connection String Here";
            string teacherID = txtTeacherID.Text;
            string fullName = txtFullNameT.Text;
            string gender = rbtnMaleT.Checked ? "Male" : "Female";
            string course = cmbCourse.Text;
            string department = cmbDepartment.Text; // Get the selected department
            string contactNumber = txtContactNumberT.Text;

            // Check if any of the required fields are empty
            if (string.IsNullOrWhiteSpace(teacherID) || string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(course) ||
                string.IsNullOrWhiteSpace(contactNumber))
            {
                MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Insert the teacher details into the TeacherDetails table
            string insertQuery = "INSERT INTO TeacherDetails (FullName, Gender, Course, Department, ContactNumber) " +
                "VALUES (@FullName, @Gender, @Course, @Department, @ContactNumber);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@FullName", fullName);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@Course", course);
                command.Parameters.AddWithValue("@Department", department); // Add department parameter
                command.Parameters.AddWithValue("@ContactNumber", contactNumber);

                connection.Open();
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Teacher added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear the form controls
            ClearTeacherForm();
        }

        private void btnClearT_Click(object sender, EventArgs e)
        {
            ClearTeacherForm();
            rbtnMaleT.Checked = false;
            rbtnFemaleT.Checked = false;
        }

        private void ClearTeacherForm()
        {
            // Clear the form controls for adding a teacher
            txtTeacherID.Text = string.Empty;
            txtFullNameT.Text = string.Empty;
            cmbCourse.SelectedIndex = -1;
            txtContactNumberT.Text = string.Empty;
        }

        private void PopulateDepartmentComboBox(ComboBox comboBox)
        {
            string connectionString = "Connection String Here";
            string query = "SELECT DISTINCT Department FROM TeacherDetails;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string department = reader["Department"].ToString();
                        comboBox.Items.Add(department);
                    }
                }
            }

            // Set the default value as the selected item
            comboBox.SelectedIndex = 0;
        }

        private void btnSearchT_Click(object sender, EventArgs e)
        {
            string connectionString = "Connection String Here";
            string searchText = txtSearchT.Text.Trim();

            // Check if search text is empty
            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Display all data from the TeacherDetails table
                string query = "SELECT TOP (1000) [TeacherID], [FullName], [Gender], [Course], [Department], [ContactNumber] FROM [SchoolManagementSystem].[dbo].[TeacherDetails];";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear the previous search results from the rich text box
                        rtxtSearchResultsT.Text = string.Empty;

                        while (reader.Read())
                        {
                            // Append the search results to the rich text box
                            rtxtSearchResultsT.AppendText($"Teacher ID: {reader["TeacherID"]}\n");
                            rtxtSearchResultsT.AppendText($"Full Name: {reader["FullName"]}\n");
                            rtxtSearchResultsT.AppendText($"Gender: {reader["Gender"]}\n");
                            rtxtSearchResultsT.AppendText($"Course: {reader["Course"]}\n");
                            rtxtSearchResultsT.AppendText($"Department: {reader["Department"]}\n");
                            rtxtSearchResultsT.AppendText($"Contact Number: {reader["ContactNumber"]}\n");
                            rtxtSearchResultsT.AppendText("------------------------------------\n");
                        }
                    }
                }
            }
            else
            {
                // Search for teachers in the TeacherDetails table based on the provided search text
                string searchQuery = "SELECT TOP (1000) [TeacherID], [FullName], [Gender], [Course], [Department], [ContactNumber] FROM [SchoolManagementSystem].[dbo].[TeacherDetails] WHERE [TeacherID] LIKE '%' + @SearchText + '%' OR [FullName] LIKE '%' + @SearchText + '%';";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(searchQuery, connection);
                    command.Parameters.AddWithValue("@SearchText", searchText);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear the previous search results from the rich text box
                        rtxtSearchResultsT.Text = string.Empty;

                        while (reader.Read())
                        {
                            // Append the search results to the rich text box
                            rtxtSearchResultsT.AppendText($"Teacher ID: {reader["TeacherID"]}\n");
                            rtxtSearchResultsT.AppendText($"Full Name: {reader["FullName"]}\n");
                            rtxtSearchResultsT.AppendText($"Gender: {reader["Gender"]}\n");
                            rtxtSearchResultsT.AppendText($"Course: {reader["Course"]}\n");
                            rtxtSearchResultsT.AppendText($"Department: {reader["Department"]}\n");
                            rtxtSearchResultsT.AppendText($"Contact Number: {reader["ContactNumber"]}\n");
                            rtxtSearchResultsT.AppendText("------------------------------------\n");
                        }
                    }
                }
            }
        }

        private void btnUpdateT_Click(object sender, EventArgs e)
        {
            string connectionString = "Connection String Here";
            string updateQuery = "UPDATE TeacherDetails SET FullName = @FullName, Course = @Course, Department = @Department, ContactNumber = @ContactNumber WHERE TeacherID = @TeacherID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@TeacherID", txtTeacherID.Text.Trim());
                command.Parameters.AddWithValue("@FullName", txtFullNameT.Text.Trim());
                command.Parameters.AddWithValue("@Course", cmbCourse.Text);
                command.Parameters.AddWithValue("@Department", cmbDepartment.Text);
                command.Parameters.AddWithValue("@ContactNumber", txtContactNumberT.Text.Trim());

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Teacher details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update teacher details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeleteT_Click(object sender, EventArgs e)
        {
            string connectionString = "Connection String Here";
            string deleteQuery = "DELETE FROM TeacherDetails WHERE TeacherID = @TeacherID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@TeacherID", txtTeacherID.Text.Trim());

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Teacher details deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to delete teacher details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExitT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        // Course Management Tab
        private void btnAddC_Click(object sender, EventArgs e)
        {
            string courseName = cmbCourseNameC.Text;
            string category = cmbCategoryC.Text;

            if (string.IsNullOrEmpty(courseName))
            {
                MessageBox.Show("Please enter a course name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Connection String Here";
            string insertQuery = "INSERT INTO CourseDetails (CourseName, Category) VALUES (@CourseName, @Category)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@CourseName", courseName);
                command.Parameters.AddWithValue("@Category", category);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Course added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to add course.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClearC_Click(object sender, EventArgs e)
        {
            txtCourseIdC.Text = string.Empty;
            cmbCourseNameC.SelectedIndex = -1;
            cmbCourseNameC.Text = "Select a course"; // Set a default value for the combo box
            cmbCategoryC.SelectedIndex = -1;
            cmbCategoryC.Text = string.Empty;
            richTextBoxDescriptionC.Text = string.Empty;
        }

        private void btnSearchC_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Connection String Here";
                string searchQuery = "SELECT TOP (1000) CourseID, CourseName, Category FROM CourseDetails";
                bool isSearch = !string.IsNullOrWhiteSpace(txtSearch.Text);

                if (isSearch)
                {
                    searchQuery += " WHERE CourseID = @CourseID";
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(searchQuery, connection);

                    if (isSearch)
                    {
                        command.Parameters.AddWithValue("@CourseID", txtSearch.Text.Trim());
                    }

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        StringBuilder resultBuilder = new StringBuilder();

                        while (reader.Read())
                        {
                            string courseId = reader["CourseID"].ToString();
                            string courseName = reader["CourseName"].ToString();
                            string category = reader["Category"].ToString();

                            resultBuilder.AppendLine($"Course ID: {courseId}");
                            resultBuilder.AppendLine($"Course Name: {courseName}");
                            resultBuilder.AppendLine($"Category: {category}");
                            resultBuilder.AppendLine();
                        }

                        richTextBoxResults.Text = resultBuilder.ToString();
                    }
                    else
                    {
                        richTextBoxResults.Text = "No course found.";
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while searching for the course: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditC_Click(object sender, EventArgs e)
        {
            string connectionString = "Connection String Here";
            string updateQuery = "UPDATE CourseDetails SET CourseName = @CourseName, Category = @Category WHERE CourseID = @CourseID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@CourseID", txtCourseIdC.Text.Trim());
                command.Parameters.AddWithValue("@CourseName", cmbCourseNameC.Text.Trim());
                command.Parameters.AddWithValue("@Category", cmbCategoryC.Text.Trim());

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Course details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update course details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeleteC_Click(object sender, EventArgs e)
        {
            string connectionString = "Connection String Here";
            string deleteQuery = "DELETE FROM CourseDetails WHERE CourseID = @CourseID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@CourseID", txtCourseIdC.Text.Trim());

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Course details deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to delete course details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExitC_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
