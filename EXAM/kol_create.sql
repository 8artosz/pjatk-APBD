-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2021-05-13 12:08:39.536

-- tables
-- Table: Action
CREATE TABLE Action (
    IdAction int  NOT NULL,
    StartTime datetime  NOT NULL,
    EndTime datetime  NULL,
    NeedSpecialEquipment bit  NOT NULL,
    CONSTRAINT Action_pk PRIMARY KEY  (IdAction)
);

-- Table: FireTruck
CREATE TABLE FireTruck (
    IdFireTruck int  NOT NULL,
    OperationalNumber varchar(10)  NOT NULL,
    SpecialEquipment bit  NOT NULL,
    CONSTRAINT FireTruck_pk PRIMARY KEY  (IdFireTruck)
);

-- Table: FireTruck_Action
CREATE TABLE FireTruck_Action (
    IdFireTruckAction int  NOT NULL,
    IdFireTruck int  NOT NULL,
    IdAction int  NOT NULL,
    AssignmentDate datetime  NOT NULL,
    CONSTRAINT FireTruck_Action_pk PRIMARY KEY  (IdFireTruckAction)
);

-- Table: Firefighter
CREATE TABLE Firefighter (
    IdFirefighter int  NOT NULL,
    FirstName nvarchar(30)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    CONSTRAINT Firefighter_pk PRIMARY KEY  (IdFireFighter)
);

-- Table: Firefighter_Action
CREATE TABLE Firefighter_Action (
    IdFirefighter int  NOT NULL,
    IdAction int  NOT NULL,
    CONSTRAINT Firefighter_Action_pk PRIMARY KEY  (IdFirefighter,IdAction)
);

-- foreign keys
-- Reference: FireTruck_Action_Action (table: FireTruck_Action)
ALTER TABLE FireTruck_Action ADD CONSTRAINT FireTruck_Action_Action
    FOREIGN KEY (IdAction)
    REFERENCES Action (IdAction);

-- Reference: FireTruck_Action_FireTruck (table: FireTruck_Action)
ALTER TABLE FireTruck_Action ADD CONSTRAINT FireTruck_Action_FireTruck
    FOREIGN KEY (IdFireTruck)
    REFERENCES FireTruck (IdFireTruck);

-- Reference: Firefighter_Action_Action (table: Firefighter_Action)
ALTER TABLE Firefighter_Action ADD CONSTRAINT Firefighter_Action_Action
    FOREIGN KEY (IdAction)
    REFERENCES Action (IdAction);

-- Reference: Firefighter_Action_Firefighter (table: Firefighter_Action)
ALTER TABLE Firefighter_Action ADD CONSTRAINT Firefighter_Action_Firefighter
    FOREIGN KEY (IdFirefighter)
    REFERENCES Firefighter (IdFireFighter);

INSERT INTO Firefighter (IdFirefighter, FirstName, LastName) VALUES (1, 'Bartek', 'Wasilewski');
INSERT INTO Firefighter (IdFirefighter, FirstName, LastName) VALUES (2, 'Jan', 'Kowalski');
INSERT INTO Firefighter (IdFirefighter, FirstName, LastName) VALUES (3, 'Anna', 'Nowak');

INSERT INTO FireTruck(IdFireTruck, OperationalNumber, SpecialEquipment) VALUES (1, '123', 1);
INSERT INTO FireTruck(IdFireTruck, OperationalNumber, SpecialEquipment) VALUES (2, '456', 0);
INSERT INTO FireTruck(IdFireTruck, OperationalNumber, SpecialEquipment) VALUES (3, '789', 1);

INSERT INTO Action (IdAction, StartTime, EndTime, NeedSpecialEquipment) VALUES (1, '2021-05-12', '2021-05-13', 1); 
INSERT INTO Action (IdAction, StartTime, NeedSpecialEquipment) VALUES (2, '2021-05-13',1);
INSERT INTO Action (IdAction, StartTime, EndTime, NeedSpecialEquipment) VALUES (3, '2021-05-11', '2021-05-12', 0);


INSERT INTO FireTruck_Action(IdFireTruckAction, IdFireTruck, IdAction, AssignmentDate) VALUES (1, 1, 1,'2021-05-12');
INSERT INTO FireTruck_Action(IdFireTruckAction, IdFireTruck, IdAction, AssignmentDate) VALUES (2, 1, 2,'2021-05-13');
INSERT INTO FireTruck_Action(IdFireTruckAction, IdFireTruck, IdAction, AssignmentDate) VALUES (3, 2, 3,'2021-05-11');

INSERT INTO Firefighter_Action(IdFirefighter, IdAction) VALUES(1, 2);
INSERT INTO Firefighter_Action(IdFirefighter, IdAction) VALUES(1, 1);
INSERT INTO Firefighter_Action(IdFirefighter, IdAction) VALUES(2, 1);

-- End of file.

