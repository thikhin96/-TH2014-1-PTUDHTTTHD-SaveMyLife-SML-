CREATE TABLE Representative
(
	idRepresentative int identity(1,1) primary key,
	name nvarchar(30),
	phone varchar(11),
	email varchar(100),
	title nvarchar(50),
	PDistributor int,
	Distributor int,
)
go

CREATE TABLE Staff
(
	idStaff int identity(1,1) primary key,
	staffName nvarchar(30),
	account varchar(30),
	idCard varchar(12) unique,
	phone varchar(11) unique,
	adress nvarchar(100)
)
go

CREATE TABLE Assignment
(
	staff int,
	PDistributor int,
	date datetime,
	place nvarchar(100),
	isComplete bit,
	result nvarchar(max),
	primary key (staff, PDistributor)
)
go

CREATE TABLE ProductType
(
	idProductType int identity(1,1) primary key,
	productTypeName nvarchar(50)
)
go

CREATE TABLE Unit
(
	idUnit int identity(1,1) primary key,
	unitName nvarchar(30) unique,
	quantity int check (quantity > 0)
)
go

ALTER TABLE Representative ADD
	CONSTRAINTS FK_Representative_PDistributor FOREIGN KEY (PDistributor) REFERENCES PDistributor(idPDistributor),
	CONSTRAINTS FK_Representative_Distributor FOREIGN KEY (Distributor) REFERENCES Distributor(idDistributor)
go

ALTER TABLE Staff ADD 
	CONSTRAINTS FK_Staff_Account FOREIGN KEY (account) REFERENCES Account(idUser)
go

ALTER TABLE Assignment ADD 
	CONSTRAINTS FK_Assignment_Staff FOREIGN KEY (staff) REFERENCES Staff(idStaff),
	CONSTRAINTS FK_Assignment_PDistributor FOREIGN KEY (PDistributor) REFERENCES PDistributor(idPDistributor)
go