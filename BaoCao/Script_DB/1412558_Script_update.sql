create table PromotionGifts (
	idPromotion int,
	idProduct int,
	Quantity int
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
	quantity int
);
go

create table DeliveryOrder(
	idDeliveryOrder int identity(1,1) primary key,
	recipient nvarchar(50),
	deliveryAdd nvarchar(100),
	totalPurchase money,
	deliveryDate datetime,
	status tinyint check(status in (1,2,3,4,5)),
	updateDate datetime,
	description nvarchar(200),
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
	types tinyint check(types in (1,2)),
	description nvarchar(200),
	idDeliveryOrder int,
	idStaff int,
	idDistributor int
);
go

ALTER TABLE PromotionGifts ADD 
	CONSTRAINT FK_PromotionGifts_Promotion FOREIGN KEY (idPromotion) REFERENCES Promotion(idPromotion),
	CONSTRAINT FK_PromotionGifts_Product FOREIGN KEY (idProduct) REFERENCES Product(IdProduct)
go

ALTER TABLE SalesReportDetail ADD 
	CONSTRAINT FK_SalesReportDetail_SalesReport FOREIGN KEY (idSalesReport) REFERENCES SalesReport(idSalesReport),
	CONSTRAINT FK_SalesReportDetail_ProductType FOREIGN KEY (idProductType) REFERENCES ProductType(idProductType)
go

ALTER TABLE DeliveryOrder ADD 
	CONSTRAINT FK_DeliveryOrder_Oder FOREIGN KEY (idDeliveryOrder) REFERENCES "Order"(idOrder),
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