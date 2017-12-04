CREATE TABLE Log_Order
(
	ID_Log int identity primary key,
	CreatedDate Datetime,
	OldPrice int,
	NewPrice int,
)
go

CREATE TABLE OrderDetail
(
	ID_Order int,
	ID_Product int,
	Quantity int check (Quantity > 0)
)
go

CREATE TABLE Log_OrderDetail
(
	ID_Log int identity primary key,
	CreatedDate datetime,
	OldQuantity int,
	NewQuantity int,
	Statu nvarchar(100),
	ID_Order int,
	ID_Product int,
	ID_Staff int,
)
go

CREATE TABLE Log_Product
(
	ID_Log_Product int identity primary key,
	CreatedDate Datetime,
	ID_Staff int,
	OldPrice int,
	NewPrice int,
	Statu nvarchar(max),
)
go

CREATE TABLE Promotion
(
	ID_Promotion int identity primary key,
	StartDate Datetime,
	EndDate Datetime,
)
go

CREATE TABLE PromotionGifts
(
	ID_Promotion int,
	ID_Product int,
	Quantity int check (Quantity > 0),
)
go


ALTER TABLE OrderDetail ADD 
	CONSTRAINT FK_OrderDetail_Order FOREIGN KEY (ID_Order) REFERENCES Order (ID_Order),
	CONSTRAINT FK_OrderDetail_Product FOREIGN KEY (ID_Product) REFERENCES Product (ID_Product)
go
ALTER TABLE Log_OrderDetail ADD 
	CONSTRAINT FK_Log_OrderDetail_Order FOREIGN KEY (ID_Order) REFERENCES Order(ID_Order),
	CONSTRAINT FK_Log_OrderDetail_Product FOREIGN KEY (ID_Product) REFERENCES Product(ID_Product)
	CONSTRAINT FK_Log_OrderDetail_Staff FOREIGN KEY (ID_Staff) REFERENCES Staff(ID_Staff)
go


ALTER TABLE PromotionGifts ADD 
	CONSTRAINT FK_PromotionGifts_Promotion FOREIGN KEY (ID_Promotion) REFERENCES Promotion(ID_Promotion),
	CONSTRAINT FK_PromotionGifts_Product FOREIGN KEY (ID_Product) REFERENCES Product(ID_Product)
go