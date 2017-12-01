CREATE TABLE NguoiDaiDien
(
	ID_NDD int identity(1,1) primary key,
	TenNDD nvarchar(30),
	SoDT varchar(11),
	Email varchar(100),
	ChucVu nvarchar(50),
	DoiTac int,
	NhaPhanPhoi int,
)
go

CREATE TABLE NhanVien
(
	ID_NV int identity(1,1) primary key,
	TenNV nvarchar(30),
	UserName varchar(30),
	CMND varchar(12) unique,
	SDT varchar(11) unique,
	DiaChi nvarchar(100)
)
go

CREATE TABLE PhanCongTraoDoi
(
	ID_NV int,
	ID_DoiTac int,
	ThoiGian datetime,
	DiaDiem nvarchar(100),
	HoanThanh bit,
	KetQua nvarchar(max),
	primary key (ID_NV, ID_DoiTac)
)
go

CREATE TABLE LoaiSanPham
(
	ID_LoaiSP int identity(1,1) primary key,
	TenLoaiSP nvarchar(50)
)
go

CREATE TABLE DonViTinh
(
	ID_DVT int identity(1,1) primary key,
	TenDVT nvarchar(30) unique,
	SoLuong int check (SoLuong > 0)
)
go

ALTER TABLE NguoiDaiDien ADD
	CONSTRAINTS FK_NguoiDaiDien_DoiTac FOREIGN KEY (DoiTac) REFERENCES DoiTac(ID_DoiTac),
	CONSTRAINTS FK_NguoiDaiDien_NhaPhanPhoi FOREIGN KEY (NhaPhanPhoi) REFERENCES DoiTac(ID_NPP)
go

ALTER TABLE NhanVien ADD 
	CONSTRAINTS FK_NhanVien_Account FOREIGN KEY (UserName) REFERENCES Account(ID_Account)
go

ALTER TABLE PhanCongTraoDoi ADD 
	CONSTRAINTS FK_PhanCongTraoDoi_NhanVien FOREIGN KEY (ID_NV) REFERENCES NhanVien(ID_NV),
	CONSTRAINTS FK_PhanCongTraoDoi_DoiTac FOREIGN KEY (ID_DoiTac) REFERENCES DoiTac(ID_DoiTac)
go