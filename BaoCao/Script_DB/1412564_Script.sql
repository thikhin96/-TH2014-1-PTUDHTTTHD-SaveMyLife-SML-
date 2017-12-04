CREATE TABLE ReturnBase
(
	ID_Return int identity(1,1) primary key,
	Total money,
	CreatedDate datetime,
	ModeOfReturn bit,				-- 0: đổi, 1: trả
	ID_Distributor int,
	ID_Storage int,
	ID_Staff int
)
go

CREATE TABLE ReturnDetail
(
	ID_Return int,
	ID_Product int,
	Quantity int check (Quantity > 0),
	ProductMoneyRefunding money,
	primary key (ID_Return, ID_Product)
)
go

CREATE TABLE DebtPayment
(
	ID_DebtPayment int identity(1,1) primary key,
	AmountOfDebtPayment money,
	CreatedDate datetime,
	ID_Distributor int,
	ID_Staff int
)
go

CREATE TABLE PaySlip
(
	ID_PaySlip int identity(1,1) primary key,
	AmountSpent money,
	SpendingReasons nvarchar(200),
	CreatedDate datetime,
	ID_Distributor int,
	ID_Staff int
)
go

CREATE TABLE OrderBase
(
	ID_Order int identity(1,1) primary key,
	Total money,
	DeliveryType bit,			-- 0: tự túc, 1: vitamilk giao
	PaymentType bit,			-- 0: tiền mặt, 1: thẻ
	EstimateDateOfDelivery datetime,
	Statuses tinyint check (Statuses in (0,1,2,3)),	-- 0: chưa duyệt, 1: đã duyệt, 2: không duyệt, 3: đã giao
	CreatedDate datetime,
	UpdatedDate datetime,
	ID_Distributor int,
	ID_Consignee int,
	ID_Staff int,
	Descriptions nvarchar(100)
)
go

CREATE TABLE Consignee
(
	ID_Consignee int identity(1,1) primary key,
	Name nvarchar(100),
	PhoneNumber varchar(20),
	ID_Distributor int,
	ID_Order int
)
go

ALTER TABLE ReturnBase ADD
	CONSTRAINT FK_ReturnBase_Distributor FOREIGN KEY (ID_Distributor) REFERENCES Distributor (ID_Distributor),
	CONSTRAINT FK_ReturnBase_Storage FOREIGN KEY (ID_Storage) REFERENCES Storage (ID_Storage),
	CONSTRAINT FK_ReturnBase_Staff FOREIGN KEY (ID_Staff) REFERENCES Staff (ID_Staff)
go

ALTER TABLE ReturnDetail ADD
	CONSTRAINT FK_ReturnDetail_ReturnBase FOREIGN KEY (ID_Return) REFERENCES ReturnBase (ID_Return),
	CONSTRAINT FK_ReturnDetail_Product FOREIGN KEY (ID_Product) REFERENCES Product (ID_Product)
go

ALTER TABLE DebtPayment ADD
	CONSTRAINT FK_DebtPayment_Distributor FOREIGN KEY (ID_Distributor) REFERENCES Distributor (ID_Distributor),
	CONSTRAINT FK_DebtPayment_Staff FOREIGN KEY (ID_Staff) REFERENCES Staff (ID_Staff)
go

ALTER TABLE PaySlip ADD
	CONSTRAINT FK_PaySlip_Distributor FOREIGN KEY (ID_Distributor) REFERENCES Distributor (ID_Distributor),
	CONSTRAINT FK_PaySlip_Staff FOREIGN KEY (ID_Staff) REFERENCES Staff (ID_Staff)
go

ALTER TABLE OrderBase ADD
	CONSTRAINT FK_OrderBase_Distributor FOREIGN KEY (ID_Distributor) REFERENCES Distributor (ID_Distributor),
	CONSTRAINT FK_OrderBase_Consignee FOREIGN KEY (ID_Consignee) REFERENCES Consignee (ID_Consignee),
	CONSTRAINT FK_OrderBase_Staff FOREIGN KEY (ID_Staff) REFERENCES Staff (ID_Staff)
go

ALTER TABLE Consignee ADD
	CONSTRAINT FK_Consignee_Distributor FOREIGN KEY (ID_Distributor) REFERENCES Distributor (ID_Distributor),
	CONSTRAINT FK_Consignee_OrderBase FOREIGN KEY (ID_Order) REFERENCES OrderBase (ID_Order)









