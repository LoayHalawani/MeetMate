CREATE TABLE Company (
	CompanyId INT PRIMARY KEY,
	Name VARCHAR(255),
	Description VARCHAR(MAX),
	EmailAddress VARCHAR(255)  UNIQUE,
	Logo VARCHAR(255),
	Active BIT,
	EmployeeId INT 
);

CREATE TABLE Employee (
	EmployeeId INT PRIMARY KEY,
	Name VARCHAR(255),
	DateOfBirth DATE,
	Gender VARCHAR(6) CHECK (Gender IN ('Male', 'Female')),
	Email VARCHAR(255) UNIQUE,
	Password VARCHAR(255) UNIQUE,
	Role VARCHAR(7) CHECK (Role IN ('Company', 'Admin')),
	CompanyId INT,
	FOREIGN KEY (CompanyId) REFERENCES Company (CompanyId)
);

CREATE TABLE Room (
	RoomId INT PRIMARY KEY,
	Name VARCHAR(255),
	Location VARCHAR(255),
	Capacity INT,
	Description VARCHAR(MAX),
	CompanyId INT,
	FOREIGN KEY (CompanyId) REFERENCES Company (CompanyId)
);

CREATE TABLE Meeting (
	MeetingId INT PRIMARY KEY,
	Date DATETIME,
	StartTime DATETIME,
	EndTime DATETIME,
	RoomId INT,
	NumberOfAttendees INT,
	Status BIT,
	FOREIGN KEY (RoomId) REFERENCES Room (RoomId)
);