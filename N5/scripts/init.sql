create database permissions
go

use permissions
go

create table Permissions
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    EmployeeForename NVARCHAR(MAX),
    EmployeeSurname NVARCHAR(MAX),
    PermissionType INT,
    PermissionDate DATETIME
);