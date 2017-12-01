CREATE TABLE Account
(
	ID_Account int identity (1,1) primary key,
	UserName varchar(30) unique,
	Password varchar(30) check (len(Password) between 8 and 30),
	DaKichHoat bit,
	PhanQuyen tinyint check (PhanQuyen in (1,2,3)),
	DaKhoa bit,
	NgayTao datetime,
	NgayCapNhat datetime,
	GhiChu nvarchar(Max)
)
go

CREATE TABLE LogDangNhap
(
	ID_Log bigint identity primary key,
	ID_Account int,
	ThoiGian datetime,
	TinhTrang bit
)
go

CREATE TABLE DoiTac
(
	ID_DoiTac int primary key identity(1,1),
	TenCT nvarchar(50) unique,
	DiaChi nvarchar(100),
	SoDT varchar(11) unique,
	Email varchar(100) unique,
	NgayTao datetime,
	NgayCapNhat datetime,
	GhiChu nvarchar(max),
	TinhTrang tinyint check(TinhTrang in (0,1,2,3,4)),
	NguoiDaiDien int 
)
go

CREATE TABLE NhaPhanPhoi
(
	ID_NPP int identity (1,1) primary key,
	TenNPP nvarchar(50) unique,
	DiaChi nvarchar(100),
	SoDT varchar(11) unique,
	Email varchar(100) unique,
	NgayTao datetime,
	NgayCapNhat datetime,
	GhiChu nvarchar(max),
	TrangThai bit,
	CongNo money,
	UserName int
)
go

CREATE TABLE HopDong
(
	ID_HopDong int identity (1,1) primary key,
	TGBatDau datetime,
	TGKetThuc datetime,
	GTDonHangNhoNhat money,
	CongNoToiDa money,
	TienHoaHong tinyint check( TienHoaHong between 0 and 100),
	LoaiPhanPhoi bit,
	KhuVuc nvarchar(30),
	TinhTrang bit,
	GhiChu nvarchar(max),
	NhaPhanPhoi int,
	NguoiDaiDien int,
	NhanVien int
)
go


ALTER TABLE DoiTac ADD 
	CONSTRAINTS FK_DoiTac_NDD FOREIGN KEY (NguoiDaiDien) REFERENCES NguoiDaiDien(Id_NDD)
go

ALTER TABLE NhaPhanPhoi ADD 
	CONSTRAINTS FK_NPP_Account FOREIGN KEY (UserName) REFERENCES Account(ID_Account)
go

ALTER TABLE HopDong ADD 
	CONSTRAINTS FK_HopDong_NDD FOREIGN KEY (NguoiDaiDien) REFERENCES NguoiDaiDien(Id_NDD), 
	CONSTRAINTS FK_HopDong_NhaPhanPhoi FOREIGN KEY (NhaPhanPhoi) REFERENCES NhaPhanPhoi(ID_NPP),
	CONSTRAINTS FK_HopDong_NhanVien FOREIGN KEY (NhanVien) REFERENCES NhanVien(ID_NV)
go