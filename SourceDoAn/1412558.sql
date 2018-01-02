/*use master
if exists(select * from sys.databases where name='WebApplication1')
drop DATABASE WebApplication1
go
create DATABASE WebApplication1
go*/
use QL_NPP
go


insert into Account values('taikhoan1','123456789',1,2,0,GetDate(),getdate(),null);
insert into Account values('taikhoan2','123456789',1,3,0,GetDate(),getdate(),null);
insert into Account values('taikhoan3','123456789',1,1,0,GetDate(),getdate(),null);
insert into Account values('taikhoan4','123456789',1,0,0,GetDate(),getdate(),null);
insert into Account values('taikhoan5','123456789',0,1,0,GetDate(),getdate(),null);



insert into DonGiaoHang(NguoiNhan, DiaChiGiao, TongTien, NgayGiao, TinhTrang, NgayCapNhat, GhiChu, ID_DonDatHang, ID_NhanVien, ID_NPP)
	values(N'Nguyễn Văn A', N'123 Tân Hóa, Bình Đinh', 50000000,'2010/01/01',2,'2010/01/01', N'Đã giao thành công',1,1,1);
insert into DonGiaoHang(NguoiNhan, DiaChiGiao, TongTien, NgayGiao, TinhTrang, NgayCapNhat, GhiChu, ID_DonDatHang, ID_NhanVien, ID_NPP)
	values(N'Cao Hoài B', N'18Bis Hòa Hưng, F11, Q11, TP HCM', 10000000,'2010/01/01',2,'2010/01/01',  N'Đã giao thành công',2,1,1);
insert into DonGiaoHang(NguoiNhan, DiaChiGiao, TongTien, NgayGiao, TinhTrang, NgayCapNhat, GhiChu, ID_DonDatHang, ID_NhanVien, ID_NPP)
	values(N'Cù Văn C', N'18 QL1A, Trảng Bom, Đồng Nai', 10000000,'2010/01/01',2,'2010/01/01',  N'Đã giao thành công',3,1,2);
insert into DonGiaoHang(NguoiNhan, DiaChiGiao, TongTien, NgayGiao, TinhTrang, NgayCapNhat, GhiChu, ID_DonDatHang, ID_NhanVien, ID_NPP)
	values(N'Chuyên BD', N'200 QL1A, Thống Nhất, Đồng Nai', 20000000,'2010/01/01',2,'2010/01/01',  N'Đã giao thành công',4,1,2);
insert into DonGiaoHang(NguoiNhan, DiaChiGiao, TongTien, NgayGiao, TinhTrang, NgayCapNhat, GhiChu, ID_DonDatHang, ID_NhanVien, ID_NPP)
	values(N'Lùn Thế Tuyệt', N'123 Tân Hóa, Tân Phú', 12000000,'2010/01/01',2,'2010/01/01', N'Đã giao thành công',5,1,3);
insert into DonGiaoHang(NguoiNhan, DiaChiGiao, TongTien, NgayGiao, TinhTrang, NgayCapNhat, GhiChu, ID_DonDatHang, ID_NhanVien, ID_NPP)
	values(N'Mạnh Trường', N'123 Tân Hóa, Bình Đinh', 50000000,'2010/01/01',2,'2010/01/01', N'Đã giao thành công',6,1,3);
insert into DonGiaoHang(NguoiNhan, DiaChiGiao, TongTien, NgayGiao, TinhTrang, NgayCapNhat, GhiChu, ID_DonDatHang, ID_NhanVien, ID_NPP)
	values(N'Dương Quá', N'100 đường 3/2, F11, Q11, TP HCM ', 50000000,'2010/01/01',2,'2010/01/01', N'Đã giao thành công',7,1,4);
insert into DonGiaoHang(NguoiNhan, DiaChiGiao, TongTien, NgayGiao, TinhTrang, NgayCapNhat, GhiChu, ID_DonDatHang, ID_NhanVien, ID_NPP)
	values(N'Bùi Đạt', N'123 Tân Hóa, Bình Đinh', 50000000,'2010/01/01',2,'2010/01/01', N'Đã giao thành công',8,1,4);
insert into DonGiaoHang(NguoiNhan, DiaChiGiao, TongTien, NgayGiao, TinhTrang, NgayCapNhat, GhiChu, ID_DonDatHang, ID_NhanVien, ID_NPP)
	values(N'Bùi Đạt', N'123 Tân Hóa, Bình Đinh', 50000000,'2010/01/01',0,'2010/01/01', N'Không đủ hàng trong kho',9,1,4);
insert into CT_GiaoHang(ID_GiaoHang,ID_SanPham, SoLuong, GhiChu) values();

/*
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
	recipientPhone nvarchar(50),
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

-- cập nhật lại FK_DeliveryOrder_Order  do sai khóa
ALTER TABLE DeliveryOrder ADD 
	CONSTRAINT FK_DeliveryOrder_Order FOREIGN KEY (idOrder) REFERENCES "Order"(idOrder),
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
*/