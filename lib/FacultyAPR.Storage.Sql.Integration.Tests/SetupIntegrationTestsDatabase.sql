use test;

drop table if exists GroupReviewContent, SectionContent, FormReviewContent, Form, SectionRegister, SectionTypeTable, Section, FormGroup, FormRegister, UserInfo, Faculty, Department, SpotScoreFormQuestions, SpotScoreContent, SpotScoreSectionMetadata;

create table SectionContent(
    FormId varchar (36) NOT NULL,
    SectionId varchar(36),
    UserId varchar(36),
    Content Text,
    Modified Text,
    primary key (FormId, SectionId, UserId)
);

create table GroupReviewContent(
    FormId varchar (36) NOT NULL,
    GroupId varchar(36),
    UserId varchar(36),
    Review Text,
    Modified Text,
    primary key (FormId, GroupId, UserId)
);

create table FormReviewContent(
    FormId varchar (36) NOT NULL,
    UserId varchar(36) NOT NULL,
    TopLevelReview Text,
    Modified Text,
    FormState Enum("Draft","Review","FacultyAck","ToBeSigned","Completed","Submitted"),
    primary key (FormId, UserId)
);

create table Form(
    FormId varchar (36) NOT NULL,
    FacRank ENUM('Lecturer', 'Instructor', 'SeniorInstructor', 'AssistantProfessor','AssociateProfessor', 'Professor'),
    FormYear Text,
    Primary key (FormId)
);

create table SpotScoreFormQuestions(
    FormId varchar (36) NOT NULL,
    Question varchar(1000) NOT NULL,
    Primary key (FormId, Question)
);

create table SpotScoreContent(
    FormId varchar (36) NOT NULL,
    UserId varchar (36) NOT NULL,
    Question varchar (1000) NOT NULL,
    Course Text,
    PercentResponded DECIMAL,
    Mean DECIMAL,
    Primary key (FormId, UserId, Question)
);

create table SpotScoreSectionMetadata (
  FormId varchar (36) NOT NULL,
  UserId varchar (36) NOT NULL,
  FacultyComment Text,
  Review Text,
  Primary key (FormId, UserId)  
);

INSERT INTO Form(FormId, FacRank, FormYear)
Values ('1a94e847-00ff-4096-8f75-94bd08019549', 'Professor', '2020');

CREATE TABLE FormRegister (
  FacultyId varchar(36) NOT NULL,
  FormId VARCHAR(36) NOT NULL,
  ReviewerId VARCHAR(36) NOT NULL,
  PRIMARY KEY (FacultyId, FormId)
);

CREATE TABLE SectionTypeTable (
  Id int NOT NULL,
  TypeName text,
  PRIMARY KEY(Id)
);

Insert INTO SectionTypeTable(Id, TypeName) 
Values
('1', 'TextBox'),
('2', 'CompundTextBox'),
('3', 'MultiSelect'),
('4', 'Radio');

CREATE TABLE Section (
  FormId varchar (36) NOT NULL,
  SectionId varchar(36) NOT NULL,
  SectionType int,
  SectionTitle Text NOT NULL,
  SectionDescription Text NOT NULL,
  GroupId varchar(36),
  Options Text,
  OrderIndex int,
  PRIMARY KEY (FormId, SectionId)
);

create table FormGroup(
    FormId varchar (36) NOT NULL,
    GroupId varchar(36) NOT NULL,
    Title Text,
    DescriptionText Text,
    OrderIndex int,
    primary key (FormId, GroupId)
);

CREATE TABLE UserInfo (
  UserId varchar(36) NOT NULL,
  FirstName Text NOT NULL,
  LastName Text NOT NULL,
  EmailAddress Text NOT NULL,
  UserType ENUM('Admin', 'Dean', 'Faculty','FacultyChair'),
  PRIMARY KEY (UserId, UserType)
);

CREATE TABLE Faculty (
  UserId varchar(36) NOT NULL,
  FacultyRank ENUM('Lecturer', 'Instructor', 'SeniorInstructor', 'AssistantProfessor',
   'AssociateProfessor', 'Professor'),
  DepartmentId int NOT NULL,
  PRIMARY KEY (UserId)
);

CREATE TABLE Department (
  DepartmentId int NOT NULL,
  DepartmentName ENUM('Biology', 'Chemistry', 'CivilAndEnvironmentalEngineering',
                        'ComputerScience', 'ElectricalAndComputerEngineering',
                        'Mathematics', 'MechanicalEngineering', 'Physics'),
  DepartmentChair Text,
  PRIMARY KEY (DepartmentId)
);