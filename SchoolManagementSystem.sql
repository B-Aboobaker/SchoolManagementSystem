CREATE DATABASE SchoolManagementSystem;

USE SchoolManagementSystem;

CREATE TABLE UserDetails (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);

CREATE TABLE StudentDetails (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    CourseEnrolled NVARCHAR(100) NOT NULL,
    ContactNumber NVARCHAR(20) NOT NULL,
    Address NVARCHAR(MAX) NOT NULL
);

CREATE TABLE TeacherDetails (
    TeacherID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Course NVARCHAR(100) NOT NULL,
    Department NVARCHAR(100) NOT NULL,
    ContactNumber NVARCHAR(20) NOT NULL
);

CREATE TABLE CourseDetails (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(100) NOT NULL,
    Category NVARCHAR(100) NOT NULL
);
