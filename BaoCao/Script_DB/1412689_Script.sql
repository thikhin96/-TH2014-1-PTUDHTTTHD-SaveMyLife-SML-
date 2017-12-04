CREATE TABLE Log_Order
(
	idLog int identity primary key,
	createdDate Datetime,
	oldPrice int,
	newPrice int,
)
go

CREATE TABLE OrderDetail
(
	idOrder int,
	idProduct int,
	quantity int check (quantity > 0)
)
go

CREATE TABLE Log_OrderDetail
(
	idLog int identity primary key,
	createdDate datetime,
	oldQuantity int,
	newQuantity int,
	'description' nvarchar(100),
	idOrder int,
	idProduct int,
	idStaff int,
)
go

CREATE TABLE Log_Product
(
	idLog_Product int identity primary key,
	createdDate Datetime,
	idStaff int,
	oldPrice int,
	newPrice int,
	'description' nvarchar(max),
)
go

CREATE TABLE Promotion
(
	idPromotion int identity primary key,
	startDate Datetime,
	endDate Datetime,
)
go

CREATE TABLE PromotionGifts
(
	idPromotion int,
	idProduct int,
	quantity int check (quantity > 0),
)
go


ALTER TABLE OrderDetail ADD 
	CONSTRAINT FK_OrderDetail_Order FOREIGN KEY (idOrder) REFERENCES 'Order' (idOrder),
	CONSTRAINT FK_OrderDetail_Product FOREIGN KEY (idProduct) REFERENCES Product (idProduct)
go
ALTER TABLE Log_OrderDetail ADD 
	CONSTRAINT FK_Log_OrderDetail_Order FOREIGN KEY (idOrder) REFERENCES 'Order'(idOrder),
	CONSTRAINT FK_Log_OrderDetail_Product FOREIGN KEY (idProduct) REFERENCES Product(idProduct)
	CONSTRAINT FK_Log_OrderDetail_Staff FOREIGN KEY (idStaff) REFERENCES Staff(idStaff)
go


ALTER TABLE PromotionGifts ADD 
	CONSTRAINT FK_PromotionGifts_Promotion FOREIGN KEY (idPromotion) REFERENCES Promotion(idPromotion),
	CONSTRAINT FK_PromotionGifts_Product FOREIGN KEY (idProduct) REFERENCES Product(idProduct)
go