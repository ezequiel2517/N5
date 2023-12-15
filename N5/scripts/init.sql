create database permissions
go

use permissions
go

create table PermissionTypes
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Description NVARCHAR(MAX)
)
go

INSERT INTO PermissionTypes (Description) VALUES 
('Escritura'),
('Eliminar'),
('Modificar')

create table Permissions
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    EmployeeForename NVARCHAR(MAX),
    EmployeeSurname NVARCHAR(MAX),
    PermissionType INT,
    PermissionDate DATETIME
);

ALTER TABLE Permissions
ADD CONSTRAINT FK_Permissions_PermissionTypes
FOREIGN KEY (PermissionType)
REFERENCES PermissionTypes(Id);