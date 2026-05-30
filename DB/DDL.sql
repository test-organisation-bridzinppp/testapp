CREATE TABLE People(
  id int IDENTITY primary key ,
  name nvarchar(8) not null,
  lasatname nvarchar(20) not null,
  age int not null,
  CONSTRAINT chk_age CHECK (age >= 0 and age < 130)
)

CREATE TABLE Addresses(
  id int IDENTITY primary key,
  person_id int,
  address nvarchar(255),
  CONSTRAINT fk_person FOREIGN KEY (person_id) REFERENCES People(id)
)

CREATE TABLE Audit_Addresses(
	id int identity primary key,
	address_id int,
	person_id int,
	operation char(1),
	address nvarchar(255)
)

CREATE TRIGGER dbo.trg_Addresses
ON dbo.Addresses
AFTER INSERT, UPDATE, DELETE
AS
  SET NOCOUNT ON;

  --insert 
  IF EXISTS(SELECT 1 FROM inserted) AND NOT EXISTS (SELECT 1 FROM deleted)
  BEGIN
    INSERT INTO Audit_Addresses(address_id, person_id, address, operation) SELECT id as address_id, person_id, address, 'I' as operation FROM inserted
  END

  -- update
  if EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM deleted)
  BEGIN
    INSERT INTO Audit_Addresses(address_id, person_id, address, operation) SELECT id as address_id, person_id, address, 'U' as operation FROM inserted
  END

  if NOT EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM deleted)
  BEGIN
    INSERT INTO Audit_Addresses(address_id, person_id, address, operation) SELECT id as address_id, person_id, address, 'D' as operation FROM inserted
  END
GO

CREATE INDEX addr_people ON dbo.Addresses(person_id)