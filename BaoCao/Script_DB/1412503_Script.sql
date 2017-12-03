create table Product
(
	IdProduct int identity (1,1) primary key,
	ProductName nvarchar(50),
	Price money,
	IsDisabled bit,
	ProductType int,
	Unit int
)
go

create table Batch
(
	IdBatch int identity (1,1) primary key,
	ManufacturedDate datetime
)

create table BatchDetail
(
	IdBatch int,
	IdProduct int,
	Quantity int check(Quantity > 0),
	ExipreDate datetime,
	primary key (IdBatch, IdProduct)
)
go

create table Storage
(
	IdStorage int identity (1,1) primary key,
	HouseNumber_Street nvarchar(50),
	Ward_Commune nvarchar(50),
	District nvarchar(50),
	City nvarchar(50),
	"Description" nvarchar(100),
	Distributor int
)
go

create table ReturnRequest
(
	IdReturnRequest int identity (1,1) primary key,
	DateCreate datetime,
	"Status" int check("Status" in (0,1,2)),
	Note nvarchar(max),
	ModeOfReturn bit,
	Distributor int,
	Storage int,
	Staff int
)
go

create table ReturnRequestDetail
(
	IdReturnRequest int,
	IdProduct int,
	Quantity int check(Quantity > 0),
	Reason nvarchar(max),
	primary key (IdReturnRequest, IdProduct)
)
go

--Product
ALTER TABLE Product 
ADD CONSTRAINT FK_Product_ProductType 
FOREIGN KEY (ProductType) 
REFERENCES ProductType(IdProductType)

ALTER TABLE Product
ADD CONSTRAINT FK_Product_Unit 
FOREIGN KEY (Unit) 
REFERENCES Unit(IdUnit)

--BatchDetail
ALTER TABLE BatchDetail 
ADD CONSTRAINT FK_BatchDetail_Batch 
FOREIGN KEY (IdBatch) 
REFERENCES Batch(IdBatch)

ALTER TABLE BatchDetail 
ADD	CONSTRAINT FK_BatchDetail_Product 
FOREIGN KEY (IdProduct) 
REFERENCES Product(IdProduct)

--Storage
ALTER TABLE Storage 
ADD CONSTRAINT FK_Storage_Distributor
FOREIGN KEY (Distributor) 
REFERENCES Distributor(IdDistributor)

--ReturnRequest
ALTER TABLE ReturnRequest 
ADD CONSTRAINT FK_ReturnRequest_Distributor
FOREIGN KEY (Distributor) 
REFERENCES Distributor(IdDistributor)

ALTER TABLE ReturnRequest 
ADD CONSTRAINT FK_ReturnRequest_Storage 
FOREIGN KEY (Storage) 
REFERENCES Storage(IdStorage)

ALTER TABLE ReturnRequest 
ADD CONSTRAINT FK_ReturnRequest_Staff
FOREIGN KEY (Staff) 
REFERENCES Staff(IdStaff)

--ReturnRequestDetail
ALTER TABLE ReturnRequestDetail 
ADD CONSTRAINT FK_ReturnRequestDetail_ReturnRequest 
FOREIGN KEY (IdReturnRequest) 
REFERENCES ReturnRequest(IdReturnRequest)

ALTER TABLE ReturnRequestDetail 
ADD CONSTRAINT FK_ReturnRequestDetail_Product 
FOREIGN KEY (IdProduct) 
REFERENCES Product(IdProduct)

