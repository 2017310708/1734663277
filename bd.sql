CREATE DATABASE SW_Parcial_Yon;
USE SW_Parcial_Yon;

CREATE TABLE `Projects` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NOT NULL,
    `Description` longtext NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `IsActive` tinyint(1) NOT NULL,
    `Status` longtext NOT NULL,
    PRIMARY KEY (`Id`)
);
CREATE TABLE `Tasks` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NOT NULL,
    `Description` longtext NOT NULL,
    `DueDate` datetime(6) NOT NULL,
    `IsActive` tinyint(1) NOT NULL,
    `Status` longtext NOT NULL,
    `Priority` longtext NOT NULL,
    `ProjectId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Tasks_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`Id`) ON DELETE CASCADE
);

INSERT INTO SW_Parcial_Yon.Projects
(Id, Name, Description, StartDate, EndDate, IsActive, Status)
VALUES(1, 'proy. 1', 'desc. proy 1', '2024-12-14 19:51:15.164000', '2024-12-25 19:51:15.165000', 0, 'Pendiente');
INSERT INTO SW_Parcial_Yon.Projects
(Id, Name, Description, StartDate, EndDate, IsActive, Status)
VALUES(2, 'Proyecto #2', 'Descripcion del Proyecto #2', '2024-12-14 20:22:21.903000', '2025-01-20 20:22:21.903000', 1, 'Pendiente');
INSERT INTO SW_Parcial_Yon.Projects
(Id, Name, Description, StartDate, EndDate, IsActive, Status)
VALUES(3, 'Proyecto #3', 'Descripcion del Proyecto #3', '2024-12-14 20:22:21.903000', '2025-01-20 20:22:21.903000', 1, 'En Progreso');
INSERT INTO SW_Parcial_Yon.Projects
(Id, Name, Description, StartDate, EndDate, IsActive, Status)
VALUES(4, 'Proyecto 4', 'Descripcion del proy. #4', '2024-12-14 21:47:51.123000', '2024-12-15 21:47:51.123000', 1, 'Pendiente');

INSERT INTO SW_Parcial_Yon.Tasks
(Id, Name, Description, DueDate, IsActive, Status, Priority, ProjectId)
VALUES(1, 'Tarea 1', 'desc. Tarea 1', '2024-12-14 19:53:05.772000', 1, 'Pendiente', 'Alta', 1);
INSERT INTO SW_Parcial_Yon.Tasks
(Id, Name, Description, DueDate, IsActive, Status, Priority, ProjectId)
VALUES(2, 'Tarea 2 proy 1', 'desc. Tarea 2 proy 1', '2024-12-14 19:53:05.772000', 1, 'Pendiente', 'Alta', 1);
INSERT INTO SW_Parcial_Yon.Tasks
(Id, Name, Description, DueDate, IsActive, Status, Priority, ProjectId)
VALUES(3, 'Tarea 3 proy 1', 'desc. Tarea 3 proy 1', '2024-12-14 19:53:05.772000', 1, 'Pendiente', 'Alta', 1);
INSERT INTO SW_Parcial_Yon.Tasks
(Id, Name, Description, DueDate, IsActive, Status, Priority, ProjectId)
VALUES(4, 'Tarea 4 proy 1', 'desc. Tarea 4 proy 1', '2024-12-14 19:53:05.772000', 1, 'Pendiente', 'Media', 1);
INSERT INTO SW_Parcial_Yon.Tasks
(Id, Name, Description, DueDate, IsActive, Status, Priority, ProjectId)
VALUES(5, 'Tarea 1 proy 2aasa', 'Descripcion Tarea 1 proy 2', '2024-12-14 20:12:05.234000', 1, 'Pendiente', 'Media', 2);