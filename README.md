# School Management System
This repository showcases a School Management System application programmed using C#


## Context



## Table of Contents
- [School Management System](#school-management-system)
- [Context](#table-of-contents)
- [Table of Contents](#table-of-contents)
- [School Management System](#school-management-system-1)



## School Management System
### Overview
Design and implement a Windows Forms application for a school management system. The application should provide functionality for managing student and teacher information, as well as handling course enrolments and grading. It should include the following features:


#### Student Management:
- Ability to add new students to the system. 
- Ability to edit student details such as name, contact information, and grade level. 
- Ability to delete students from the system. 
- Ability to search for students by name or student ID. 
- Create a corresponding table in the SchoolManagementSystem Database called StudentDetails.
<br><br>  <img src="assets/images/SMS1.png" alt="">

#### Teacher Management:
- Ability to add new teachers to the system. 
- Ability to edit teacher details such as name, contact information, and subject specialization. 
- Ability to delete teachers from the system. 
- Ability to search for teachers by name or teacher ID. 
- Create a corresponding table in the SchoolManagementSystem Database called TeacherDetails. 
<br><br>  <img src="assets/images/SMS2.png" alt="">

#### Course Management:
- Ability to create new courses with a title, description, and associated teacher. 
- Ability to edit course details such as title, description, and assigned teacher. 
- Ability to delete courses from the system. 
- Ability to search for courses by title or course ID. 
- Create a corresponding table in the SchoolManagementSystem Database called CourseDetails. 
<br><br>  <img src="assets/images/SMS3.png" alt="">

#### User Sign-Up and Login Interface:
- The application should have an intuitive and user-friendly interface with appropriate controls (textboxes, buttons, etc.) for each functionality. 
- The user interface should provide clear instructions and feedback to guide the user in performing the desired actions. 
- The user can register their username and password and the details should be saved to a table in your SQL Database called UserDetails. 
- The Signup interface should look like the figure below:
<br><br>  <img src="assets/images/SMS4.png" alt="">
- The Login interface should look like the figure below. 
- To log in, the details used should match the credentials in the Database to provide access to the School Management System. Appropriate messages should be displayed if a user logs in with a Wrong Password and or Username or tries to log in without entering either the Username or Password (Message Box showing, enter Username or Password). 
<br><br>  <img src="assets/images/SMS5.png" alt="">

#### Considerations:
- Implement appropriate data structures and classes to represent students, teachers, courses, and their relationships. 
- Use proper validation techniques to ensure data integrity and handle potential errors. 
- Implement suitable search and filtering mechanisms to enhance the usability of the application. 
- Apply good coding practices, such as following naming conventions, organizing code into logical modules, and commenting where necessary. 
- Implement error handling and exception management to provide a robust application. 

#### Database with all the required tables:
- UserDetails
- StudentDetails
- TeacherDetails
- CourseDetails
<br><br> <img src="assets/images/SMS6.png" alt="">



### Demonstrating functionality
<img src="assets/images" alt="">