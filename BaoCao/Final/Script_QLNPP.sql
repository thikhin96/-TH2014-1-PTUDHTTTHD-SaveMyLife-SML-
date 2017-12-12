create database QL_NPP
use QL_NPP

CREATE TABLE Account
(
	idUser int identity (1,1) primary key,
	UserName varchar(30) unique,
	Password varchar(30) check (len(Password) between 8 and 30),
	activated bit,
	decentralization tinyint check (decentralization in (1,2,3)),
	locked bit,
	dateCreate datetime,
	dateUpdate datetime,
	note nvarchar(Max)
)
go

CREATE TABLE Log_Login
(
	ID_Log bigint identity primary key,
	ID_Account int,
	at_time datetime,
	status bit,
	idLog bigint identity primary key,
	idAccount int,
	at_time datetime,
	[status] bit
)
go

CREATE TABLE PotentialDistributor
(
	idDistributor int primary key identity(1,1),
	name nvarchar(50) unique,
	address nvarchar(100),
	phone varchar(11) unique,
	Email varchar(100) unique,
	createdDate datetime,
	updatedDate datetime,
	note nvarchar(max),
	[status] tinyint check(status in (0,1,2,3,4))
)
go

CREATE TABLE Distributor
(
	idDistributor int primary key identity(1,1),
	name nvarchar(50) unique,
	address nvarchar(100),
	phone varchar(11) unique,
	Email varchar(100) unique,
	debt money,
	createdDate datetime,
	updatedDate datetime,
	note nvarchar(max),
	[status] bit,
	UserName varchar(30)
)
go

CREATE TABLE [Contract]
(
	idContract int identity (1,1) primary key,
	beginDate datetime,
	expiredDate datetime,
	minOrderTotalValue money,
	maxDebt money,
	commission tinyint check( commission between 0 and 100),
	disType bit,
	area nvarchar(30),
	status bit,
	note nvarchar(max),
	distributor int,
	representative int,
	staff int
)
go

------------
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

create table Product
(
	IdProduct int identity (1,1) primary key,
	ProductName nvarchar(50),
	Price money,
	IsDisabled bit,
	Quantity int check(Quantity > 0),
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
	[Description] nvarchar(100),
	Distributor int
)
go

create table ReturnRequest
(
	IdReturnRequest int identity (1,1) primary key,
	DateCreate datetime,
	[Status] int check([Status] in (0,1,2)),
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

CREATE TABLE ReturnBase
(
	idReturn int identity(1,1) primary key,
	Total money,
	DateCreate datetime,
	ModeOfReturn bit,				-- 0: đổi, 1: trả
	idDistributor int,
	idStorage int,
	idStaff int
)
go

CREATE TABLE ReturnDetail
(
	idReturn int,
	idProduct int,
	Quantity int check (Quantity > 0),
	ProductMoneyRefunding money,
	primary key (idReturn, idProduct)
)
go

CREATE TABLE Debt
(
	idDebt int identity(1,1) primary key,
	Purchase money,
	CreatedDate datetime,
	idDistributor int,
	idStaff int
)
go

CREATE TABLE PaySlip
(
	idPaySlip int identity(1,1) primary key,
	AmountSpent money,
	SpendingReasons nvarchar(200),
	CreatedDate datetime,
	idDistributor int,
	idStaff int
)
go

CREATE TABLE [Order]
(
	idOrder int identity(1,1) primary key,
	Total money,
	DeliveryType bit,			-- 0: tự túc, 1: vitamilk giao
	PaymentType bit,			-- 0: tiền mặt, 1: thẻ
	EstimateDateOfDelivery datetime,
	Statuses tinyint check (Statuses in (0,1,2,3)),	-- 0: chưa duyệt, 1: đã duyệt, 2: không duyệt, 3: đã giao
	CreatedDate datetime,
	UpdatedDate datetime,
	idDistributor int,
	idConsignee int,
	idStaff int,
	Descriptions nvarchar(100)
)
go

CREATE TABLE Consignee
(
	idConsignee int identity(1,1) primary key,
	Name nvarchar(100),
	PhoneNumber varchar(20),
	idDistributor int
)
go

CREATE TABLE Log_Order
(
	idLog int identity(1,1) primary key,
	createdDate Datetime,
	oldPrice int,
	newPrice int
)
go

CREATE TABLE OrderDetail
(
	idOrder int,
	idProduct int,
	quantity int check (quantity > 0),
	primary key (idOrder, idProduct)
)
go

CREATE TABLE Log_OrderDetail
(
	idLog int identity(1,1) primary key,
	createdDate datetime,
	oldQuantity int,
	newQuantity int,
	[description] nvarchar(100),
	idOrder int,
	idProduct int,
	idStaff int
)
go

CREATE TABLE Log_Product
(
	idLog_Product int identity(1,1) primary key,
	createdDate Datetime,
	idStaff int,
	oldPrice int,
	newPrice int,
	[description] nvarchar(max)
)
go

CREATE TABLE Promotion
(
	idPromotion int identity(1,1) primary key,
	startDate Datetime,
	endDate Datetime
)
go

CREATE TABLE PromotionGifts
(
	idPromotion int,
	idProduct int,
	quantity int check (quantity > 0),
	primary key (idPromotion, idProduct)

)
go

create table PromotionProduct (
	idPromotion int,
	idProduct int,
	Quantity int,
	primary key ( idPromotion, idProduct)
);
go

create table SalesReport (
	idSalesReport int identity(1,1) primary key,
	beginDate datetime,
	endDate datetime
);
go

create table SalesReportDetail (
	idSalesReport int,
	idProductType int,
	price int,
	quantity int,
	primary key (idSalesReport, idProductType)
);
go

create table DeliveryOrder(
	idDeliveryOrder int identity(1,1) primary key,
	recipient nvarchar(50),
	deliveryAdd nvarchar(100),
	totalPurchase money,
	deliveryDate datetime,
	[status] tinyint check(status in (1,2,3,4,5)),
	updateDate datetime,
	[description] nvarchar(200),
	idOrder int,
	idStaff int,
	idDistributor int
);
go

create table DetailedDeliveryOrder(
	idDeliveryOrder int,
	idProduct int,
	quantity int,
	note nvarchar(200),
	PRIMARY KEY (idDeliveryOrder, idProduct)
);
go

/* cần bổ sung thêm idDistributor*/
create table Bill(
	idBill int identity(1,1) primary key,
	purchase money,
	createdDate datetime,
	[types] tinyint check([types] in (1,2)),
	description nvarchar(200),
	idDeliveryOrder int,
	idStaff int,
	idDistributor int
);
go

-------------------------------

ALTER TABLE Distributor ADD 
	CONSTRAINT FK_NPP_Account FOREIGN KEY (UserName) REFERENCES Account(UserName)
go

ALTER TABLE Contract ADD 
	CONSTRAINT FK_HopDong_NDD FOREIGN KEY (representative) REFERENCES Representative(idRepresentative), 
	CONSTRAINT FK_HopDong_NhaPhanPhoi FOREIGN KEY (Distributor) REFERENCES Distributor(idDistributor),
	CONSTRAINT FK_HopDong_NhanVien FOREIGN KEY (staff) REFERENCES Staff(idStaff)
go

ALTER TABLE Representative ADD
	CONSTRAINT FK_Representative_PDistributor FOREIGN KEY (PDistributor) REFERENCES PotentialDistributor(idDistributor),
	CONSTRAINT FK_Representative_Distributor FOREIGN KEY (Distributor) REFERENCES Distributor(idDistributor)
go

ALTER TABLE Staff ADD 
	CONSTRAINT FK_Staff_Account FOREIGN KEY (account) REFERENCES Account(UserName)
go

ALTER TABLE Assignment ADD 
	CONSTRAINT FK_Assignment_Staff FOREIGN KEY (staff) REFERENCES Staff(idStaff),
	CONSTRAINT FK_Assignment_PDistributor FOREIGN KEY (PDistributor) REFERENCES PotentialDistributor(idDistributor)
go

--Product
ALTER TABLE Product 
ADD CONSTRAINT FK_Product_ProductType 
FOREIGN KEY (ProductType) 
REFERENCES ProductType(IdProductType)
go

ALTER TABLE Product
ADD CONSTRAINT FK_Product_Unit 
FOREIGN KEY (Unit) 
REFERENCES Unit(IdUnit)
go

--BatchDetail
ALTER TABLE BatchDetail 
ADD CONSTRAINT FK_BatchDetail_Batch 
FOREIGN KEY (IdBatch) 
REFERENCES Batch(IdBatch)
go

ALTER TABLE BatchDetail 
ADD	CONSTRAINT FK_BatchDetail_Product 
FOREIGN KEY (IdProduct) 
REFERENCES Product(IdProduct)
go

--Storage
ALTER TABLE Storage 
ADD CONSTRAINT FK_Storage_Distributor
FOREIGN KEY (Distributor) 
REFERENCES Distributor(IdDistributor)
go

--ReturnRequest
ALTER TABLE ReturnRequest 
ADD CONSTRAINT FK_ReturnRequest_Distributor
FOREIGN KEY (Distributor) 
REFERENCES Distributor(IdDistributor)
go

ALTER TABLE ReturnRequest 
ADD CONSTRAINT FK_ReturnRequest_Storage 
FOREIGN KEY (Storage) 
REFERENCES Storage(IdStorage)
go

ALTER TABLE ReturnRequest 
ADD CONSTRAINT FK_ReturnRequest_Staff
FOREIGN KEY (Staff) 
REFERENCES Staff(IdStaff)
go

--ReturnRequestDetail
ALTER TABLE ReturnRequestDetail 
ADD CONSTRAINT FK_ReturnRequestDetail_ReturnRequest 
FOREIGN KEY (IdReturnRequest) 
REFERENCES ReturnRequest(IdReturnRequest)
go

ALTER TABLE ReturnRequestDetail 
ADD CONSTRAINT FK_ReturnRequestDetail_Product 
FOREIGN KEY (IdProduct) 
REFERENCES Product(IdProduct)
go

ALTER TABLE ReturnBase ADD
	CONSTRAINT FK_ReturnBase_Distributor FOREIGN KEY (idDistributor) REFERENCES Distributor (idDistributor),
	CONSTRAINT FK_ReturnBase_Storage FOREIGN KEY (idStorage) REFERENCES Storage (idStorage),
	CONSTRAINT FK_ReturnBase_Staff FOREIGN KEY (idStaff) REFERENCES Staff (idStaff)
go

ALTER TABLE ReturnDetail ADD
	CONSTRAINT FK_ReturnDetail_ReturnBase FOREIGN KEY (idReturn) REFERENCES ReturnBase (idReturn),
	CONSTRAINT FK_ReturnDetail_Product FOREIGN KEY (idProduct) REFERENCES Product (idProduct)
go

ALTER TABLE Debt ADD
	CONSTRAINT FK_Debt_Distributor FOREIGN KEY (idDistributor) REFERENCES Distributor (idDistributor),
	CONSTRAINT FK_Debt_Staff FOREIGN KEY (idStaff) REFERENCES Staff (idStaff)
go

ALTER TABLE PaySlip ADD
	CONSTRAINT FK_PaySlip_Distributor FOREIGN KEY (idDistributor) REFERENCES Distributor (idDistributor),
	CONSTRAINT FK_PaySlip_Staff FOREIGN KEY (idStaff) REFERENCES Staff (idStaff)
go

ALTER TABLE [Order] ADD
	CONSTRAINT FK_Order_Distributor FOREIGN KEY (idDistributor) REFERENCES Distributor (idDistributor),
	CONSTRAINT FK_Order_Consignee FOREIGN KEY (idConsignee) REFERENCES Consignee (idConsignee),
	CONSTRAINT FK_Order_Staff FOREIGN KEY (idStaff) REFERENCES Staff (idStaff)
go

ALTER TABLE Consignee ADD
	CONSTRAINT FK_Consignee_Distributor FOREIGN KEY (idDistributor) REFERENCES Distributor (idDistributor)
go

ALTER TABLE OrderDetail ADD 
	CONSTRAINT FK_OrderDetail_Order FOREIGN KEY (idOrder) REFERENCES [Order] (idOrder),
	CONSTRAINT FK_OrderDetail_Product FOREIGN KEY (idProduct) REFERENCES Product (idProduct)
go

ALTER TABLE Log_OrderDetail ADD 
	CONSTRAINT FK_Log_OrderDetail_Order FOREIGN KEY (idOrder) REFERENCES [Order](idOrder),
	CONSTRAINT FK_Log_OrderDetail_Product FOREIGN KEY (idProduct) REFERENCES Product(idProduct),
	CONSTRAINT FK_Log_OrderDetail_Staff FOREIGN KEY (idStaff) REFERENCES Staff(idStaff)
go

ALTER TABLE PromotionGifts ADD 
	CONSTRAINT FK_PromotionGifts_Promotion FOREIGN KEY (idPromotion) REFERENCES Promotion(idPromotion),
	CONSTRAINT FK_PromotionGifts_Product FOREIGN KEY (idProduct) REFERENCES Product(idProduct)
go

ALTER TABLE PromotionProduct ADD 
	CONSTRAINT FK_PromotionProduct_Promotion FOREIGN KEY (idPromotion) REFERENCES Promotion(idPromotion),
	CONSTRAINT FK_PromotionProduct_Product FOREIGN KEY (idProduct) REFERENCES Product(IdProduct)
go

ALTER TABLE SalesReportDetail ADD 
	CONSTRAINT FK_SalesReportDetail_SalesReport FOREIGN KEY (idSalesReport) REFERENCES SalesReport(idSalesReport),
	CONSTRAINT FK_SalesReportDetail_ProductType FOREIGN KEY (idProductType) REFERENCES ProductType(idProductType)
go

ALTER TABLE DeliveryOrder ADD 
	CONSTRAINT FK_DeliveryOrder_Oder FOREIGN KEY (idDeliveryOrder) REFERENCES [Order](idOrder),
	CONSTRAINT FK_DeliveryOrder_Staff FOREIGN KEY (idStaff) REFERENCES Staff(idStaff),
	CONSTRAINT FK_DeliveryOrder_Distributor FOREIGN KEY (idDistributor) REFERENCES Distributor(idDistributor)
go

ALTER TABLE DetailedDeliveryOrder ADD 
	CONSTRAINT FK_DetailedDeliveryOrder_DeliveryOrder FOREIGN KEY (idDeliveryOrder) REFERENCES DeliveryOrder(idDeliveryOrder),
	CONSTRAINT FK_DetailedDeliveryOrder_Product FOREIGN KEY (idProduct) REFERENCES Product(idProduct)
go

ALTER TABLE Bill ADD 
	CONSTRAINT FK_Bill_DeliveryOrder FOREIGN KEY (idDeliveryOrder) REFERENCES DeliveryOrder(idDeliveryOrder),
	CONSTRAINT FK_Bill_Staff FOREIGN KEY (idStaff) REFERENCES Staff(idStaff),
	CONSTRAINT FK_Bill_Distributor FOREIGN KEY (idDistributor) REFERENCES Distributor(idDistributor)
go