# School Management System
This repository contains a School Management System application developed in C#. The application is designed to streamline the management of students, teachers, courses, and user authentication within a school environment.


## Context
The School Management System is a Windows Forms application that aims to simplify the administration of a school by providing a user-friendly interface for managing key entities such as students, teachers, and courses. The system also includes robust user authentication features, allowing for secure access to the system. The project is ideal for educational institutions seeking a basic yet effective tool to manage their operations.


## Table of Contents
- [School Management System](#school-management-system)
- [Context](#context)
- [Table of Contents](#table-of-contents)
- [Overview](#overview)
- [Student Management](#student-management)
- [Teacher Management](#teacher-management)
- [Course Management](#course-management)
- [User Sign-Up and Login Interface](#user-sign-up-and-login-interface)
- [Considerations](#considerations)
- [Database Tables](#database-with-all-the-required-tables)
- [Demonstrating Functionality](#demonstrating-functionality)


## Overview
This project involves designing and implementing a Windows Forms application for a school management system. The application provides functionality for managing student and teacher information, as well as handling course enrollments and grading. It includes the following features:


### Student Management:
- Add new students to the system.
- Edit student details such as name, contact information, and grade level.
- Delete students from the system.
- Search for students by name or student ID.
- Create a corresponding table in the SchoolManagementSystem database called `StudentDetails`.
<br><br>  <img src="assets/images/SMS1.png" alt="Student Management System image 1">


### Teacher Management:
- Add new teachers to the system.
- Edit teacher details such as name, contact information, and subject specialization.
- Delete teachers from the system.
- Search for teachers by name or teacher ID.
- Create a corresponding table in the SchoolManagementSystem database called `TeacherDetails`.
<br><br>  <img src="assets/images/SMS2.png" alt="Student Management System image 2">


### Course Management:
- Create new courses with a title, description, and associated teacher.
- Edit course details such as title, description, and assigned teacher.
- Delete courses from the system.
- Search for courses by title or course ID.
- Create a corresponding table in the SchoolManagementSystem database called `CourseDetails`.
<br><br>  <img src="assets/images/SMS3.png" alt="Student Management System image 3">


### User Sign-Up and Login Interface:
- The application includes an intuitive and user-friendly interface with appropriate controls (textboxes, buttons, etc.) for each functionality.
- The user interface provides clear instructions and feedback to guide the user in performing the desired actions.
- Users can register their username and password, with details saved in a table in your SQL database called `UserDetails`.
<br><br>  <img src="assets/images/SMS4.png" alt="Student Management System image 4"> <br><br>
- The login interface allows users to authenticate based on credentials stored in the database. Appropriate messages are displayed for incorrect login attempts or missing credentials.
<br><br>  <img src="assets/images/SMS5.png" alt="Student Management System image 5">


### Considerations:
- Implement appropriate data structures and classes to represent students, teachers, courses, and their relationships.
- Use proper validation techniques to ensure data integrity and handle potential errors.
- Implement suitable search and filtering mechanisms to enhance the usability of the application.
- Follow good coding practices, such as naming conventions, organizing code into logical modules, and adding comments where necessary.
- Implement error handling and exception management to create a robust application.


### Database with all the required tables:
- `UserDetails`
- `StudentDetails`
- `TeacherDetails`
- `CourseDetails`
<br><br> <img src="assets/images/SMS6.png" alt="Student Management System image 6">


### Demonstrating functionality

When the application first runs, the user will be directed to the Sign-Up form to create an account:
<br> <img src="assets/images/SMS7.png" alt="Student Management System image 7">

The "Show Password" checkbox allows users to see the password they are entering:
<br> <img src="assets/images/SMS8.png" alt="Student Management System image 8">

If the passwords do not match, error handling alerts the user to enter the passwords correctly:
<br> <img src="assets/images/SMS9.png" alt="Student Management System image 9">

Appropriate messages will also appear if there is no username or if the user has already signed up previously:
<br> <img src="assets/images/SMS10.png" alt="Student Management System image 10">

Upon successful registration, the user is redirected to the Login page:
<br> <img src="assets/images/SMS11.png" alt="Student Management System image 11">

Error handling for invalid username or password:
<br> <img src="assets/images/SMS12.png" alt="Student Management System image 12">

Upon successful login, the user is redirected to the Student Management form:
<br> <img src="assets/images/SMS13.png" alt="Student Management System image 13">

The user can add a student:
<br> <img src="assets/images/SMS14.png" alt="Student Management System image 14">

The user can also search for a student:
<br> <img src="assets/images/SMS15.png" alt="Student Management System image 15">

The user can update a student’s details:
<br> <img src="assets/images/SMS16.png" alt="Student Management System image 16">

Updated student details:
<br> <img src="assets/images/SMS17.png" alt="Student Management System image 17" width=567px>

The user can delete a student:
<br> <img src="assets/images/SMS18.png" alt="Student Management System image 18">

Search for all students:
<br> <img src="assets/images/SMS19.png" alt="Student Management System image 19">

The same functionalities apply for the Teacher Management tab, where users can add, edit, delete, and search for teachers:
<br> <img src="assets/images/SMS20.png" alt="Student Management System image 20">

The same applies for the Course Management tab, where users can manage courses. The exit button will close the application:
<br> <img src="assets/images/SMS21.png" alt="Student Management System image 21">

Finally, the exit button on the Exit tab will close the application:
<br> <img src="assets/images/SMS22.png" alt="Student Management System image 22">
