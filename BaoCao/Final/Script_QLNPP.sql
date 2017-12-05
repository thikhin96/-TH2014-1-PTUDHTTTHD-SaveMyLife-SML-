CREATE DATABASE QL_NPP
USE QL_NPP

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
	UserName varchar(30)
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

----------------------------------------

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
----------------------------

create table SanPham
(
	ID_SanPham int identity (1,1) primary key,
	TenSP nvarchar(50),
	DonGia money,
	TinhTrang bit,
	LoaiSP int,
	DonViTinh int
)
go

create table DotHang
(
	ID_DotHang int identity (1,1) primary key,
	NgaySX datetime
)

create table ChiTietDotHang
(
	ID_DotHang int,
	ID_SanPham int,
	SoLuong int check(SoLuong > 0),
	HSD datetime,
	primary key (ID_DotHang, ID_SanPham)
)
go

create table Kho
(
	ID_Kho int identity (1,1) primary key,
	SoNha_Duong nvarchar(50),
	PhuongXa nvarchar(50),
	QuanHuyen nvarchar(50),
	ThanhPho nvarchar(50),
	MoTa nvarchar(100),
	NPP int
)
go

create table DonYCDoiTra
(
	ID_DonYCDT int identity (1,1) primary key,
	NgayLapDon datetime,
	TinhTrang int check(TinhTrang in (0,1,2)),
	GhiChu nvarchar(max),
	HinhThuc bit,
	NPP int,
	Kho int,
	NhanVien int
)
go

create table CTDonYCDoiTra
(
	ID_DonYCDT int,
	ID_SanPham int,
	SoLuong int check(SoLuong > 0),
	LyDoDoiTra nvarchar(max),
	primary key (ID_DonYCDT, ID_SanPham)
)
go
------------------------------------------

CREATE TABLE PhieuDoiTra
(
	ID_PhieuDoiTra int identity(1,1) primary key,
	TongTienDoiTra money,
	NgayLapPhieu datetime,
	HinhThuc bit,				-- 0: đổi, 1: trả
	NPP int,
	Kho int,
	NhanVien int
)
go

CREATE TABLE CTPhieuDoiTra
(
	ID_PhieuDoiTra int,
	ID_SanPham int,
	SoLuong int check (SoLuong > 0),
	TienSPDoiTra money,
	primary key (ID_PhieuDoiTra, ID_SanPham)
)
go

CREATE TABLE PhieuCongNo
(
	ID_PCN int identity(1,1) primary key,
	TienTraCN money,
	NgayLapPhieu datetime,
	NPP int,
	NhanVien int
)
go

CREATE TABLE PhieuChi
(
	ID_PhieuChi int identity(1,1) primary key,
	TienChiTra money,
	LyDoChi nvarchar(200),
	NgayLapPhieu datetime,
	NPP int,
	NhanVien int
)
go

CREATE TABLE DonDatHang
(
	ID_DonHang int identity(1,1) primary key,
	TongTien money,
	HinhThucGiaoHang bit,			-- 0: tự túc, 1: vitamilk giao
	HinhThucThanhToan bit,			-- 0: tiền mặt, 1: thẻ
	NgayGiaoDuKien datetime,
	TinhTrang tinyint check (TinhTrang in (0,1,2,3)),	-- 0: chưa duyệt, 1: đã duyệt, 2: không duyệt, 3: đã giao
	NgayLap datetime,
	NgayCapNhat datetime,
	ID_NPP int,
	ID_NguoiLienHe int,
	ID_NhanVien int,
	MoTa nvarchar(100)
)
go

CREATE TABLE NguoiLienHeGiaoHang
(
	ID_NguoiLienHe int identity(1,1) primary key,
	HoTen nvarchar(100),
	SDT varchar(20),
	ID_NPP int,
	ID_DonHang int
)
go
-------------------------------------------

CREATE TABLE Log_DonDatHang
(
	ID_Log int identity primary key,
	ThoiGian Datetime,
	GiaCu int,
	GiaMoi int,
)
go

CREATE TABLE ChiTiet_DDH
(
	ID_DonHang int,
	ID_SanPham int,
	SoLuong int check (SoLuong > 0)
)
go

CREATE TABLE Log_ChiTietDDH
(
	ID_Log int identity primary key,
	ThoiGian datetime,
	SoLuongCu int,
	SoLuongMoi int,
	LyDo nvarchar(100),
	ID_DonHang int,
	ID_SanPham int,
	ID_NhanVien int,
)
go

CREATE TABLE Log_SanPham
(
	ID_Log_SanPham int identity primary key,
	ThoiGian Datetime,
	ID_NhanVien int,
	DonGiaCu int,
	DonGiaMoi int,
	LyDo nvarchar(max),
)
go

CREATE TABLE KhuyenMai
(
	ID_KhuyenMai int identity primary key,
	NgayBatDau Datetime,
	NgayKetThuc Datetime,
)
go

CREATE TABLE CT_KhuyenMaiTang
(
	ID_KhuyenMai int,
	ID_SanPham int,
	SoLuong int check (Soluong > 0),

)
go
-------------------------------

create table CT_KhuyenMaiMua (
	ID_KhuyenMai int,
	ID_SanPham int,
	SoLuong int,
	PRIMARY KEY (ID_KhuyenMai, ID_SanPham)
);
go

create table BaoCaoDoanhThu (
	ID_BaoCaoDoanhThu int identity(1,1) primary key,
	NgayBatDau datetime,
	NgayKetThuc datetime
);
go

create table CT_DoanhThu (
	ID_BaoCaoDoanhThu int,
	ID_LoaiSP int,
	DonGiaBan int,
	SoLuong int,
	PRIMARY KEY (ID_BaoCaoDoanhThu, ID_LoaiSP)
);
go

create table DonGiaoHang(
	ID_GiaoHang int identity(1,1) primary key,
	NguoiNhan nvarchar(50),
	DiaChiGiao nvarchar(100),
	TongTien money,
	NgayGiao datetime,
	TinhTrang tinyint check(TinhTrang in (1,2,3,4,5)),
	NgayCapNhat datetime,
	GhiChu nvarchar(200),
	ID_DonDatHang int,
	ID_NhanVien int,
	ID_NPP int
);
go

create table CT_GiaoHang(
	ID_GiaoHang int,
	ID_SanPham int,
	SoLuong int,
	GhiChu nvarchar(200),
	PRIMARY KEY (ID_GiaoHang, ID_SanPham)
);
go

create table HoaDon(
	ID_HoaDon int identity(1,1) primary key,
	TongTien money,
	NgayLap datetime,
	LoaiHoaDon tinyint check(LoaiHoaDon in (1,2)),
	NoiDung nvarchar(200),
	ID_GiaoHang int,
	ID_NhanVien int,
	ID_NPP int
);
go

--------------------

ALTER TABLE DoiTac ADD 
	CONSTRAINT FK_DoiTac_NDD FOREIGN KEY (NguoiDaiDien) REFERENCES NguoiDaiDien(ID_NDD)
go

ALTER TABLE NhaPhanPhoi ADD 
	CONSTRAINT FK_NPP_Account FOREIGN KEY (UserName) REFERENCES Account(UserName)
go

ALTER TABLE HopDong ADD 
	CONSTRAINT FK_HopDong_NDD FOREIGN KEY (NguoiDaiDien) REFERENCES NguoiDaiDien(ID_NDD), 
	CONSTRAINT FK_HopDong_NhaPhanPhoi FOREIGN KEY (NhaPhanPhoi) REFERENCES NhaPhanPhoi(ID_NPP),
	CONSTRAINT FK_HopDong_NhanVien FOREIGN KEY (NhanVien) REFERENCES NhanVien(ID_NV)
go

ALTER TABLE NguoiDaiDien ADD
	CONSTRAINT FK_NguoiDaiDien_DoiTac FOREIGN KEY (DoiTac) REFERENCES DoiTac(ID_DoiTac),
	CONSTRAINT FK_NguoiDaiDien_NhaPhanPhoi FOREIGN KEY (NhaPhanPhoi) REFERENCES NhaPhanPhoi(ID_NPP)
go

ALTER TABLE NhanVien ADD 
	CONSTRAINT FK_NhanVien_Account FOREIGN KEY (UserName) REFERENCES Account(UserName)
go

ALTER TABLE PhanCongTraoDoi ADD 
	CONSTRAINT FK_PhanCongTraoDoi_NhanVien FOREIGN KEY (ID_NV) REFERENCES NhanVien(ID_NV),
	CONSTRAINT FK_PhanCongTraoDoi_DoiTac FOREIGN KEY (ID_DoiTac) REFERENCES DoiTac(ID_DoiTac)
go

ALTER TABLE PhieuDoiTra ADD
	CONSTRAINT FK_PhieuDoiTra_NhaPhanPhoi FOREIGN KEY (NPP) REFERENCES NhaPhanPhoi (ID_NPP),
	CONSTRAINT FK_PhieuDoiTra_Kho FOREIGN KEY (Kho) REFERENCES Kho (ID_Kho),
	CONSTRAINT FK_PhieuDoiTra_NhanVien FOREIGN KEY (NhanVien) REFERENCES NhanVien (ID_NV)
go

ALTER TABLE CTPhieuDoiTra ADD
	CONSTRAINT FK_CTPhieuDoiTra_PhieuDoiTra FOREIGN KEY (ID_PhieuDoiTra) REFERENCES PhieuDoiTra (ID_PhieuDoiTra),
	CONSTRAINT FK_CTPhieuDoiTra_SanPham FOREIGN KEY (ID_SanPham) REFERENCES SanPham (ID_SanPham)
go

ALTER TABLE PhieuCongNo ADD
	CONSTRAINT FK_PhieuCongNo_NhaPhanPhoi FOREIGN KEY (NPP) REFERENCES NhaPhanPhoi (ID_NPP),
	CONSTRAINT FK_PhieuCongNo_NhanVien FOREIGN KEY (NhanVien) REFERENCES NhanVien (ID_NV)
go

ALTER TABLE PhieuChi ADD
	CONSTRAINT FK_PhieuChi_NhaPhanPhoi FOREIGN KEY (NPP) REFERENCES NhaPhanPhoi (ID_NPP),
	CONSTRAINT FK_PhieuChi_NhanVien FOREIGN KEY (NhanVien) REFERENCES NhanVien (ID_NV)
go

ALTER TABLE DonDatHang ADD
	CONSTRAINT FK_DonDatHang_NhaPhanPhoi FOREIGN KEY (ID_NPP) REFERENCES NhaPhanPhoi (ID_NPP),
	CONSTRAINT FK_DonDatHang_NguoiLienHeGiaoHang FOREIGN KEY (ID_NguoiLienHe) REFERENCES NguoiLienHeGiaoHang (ID_NguoiLienHe),
	CONSTRAINT FK_DonDatHang_NhanVien FOREIGN KEY (ID_NhanVien) REFERENCES NhanVien (ID_NV)
go

--SanPham
ALTER TABLE SanPham 
ADD CONSTRAINT FK_SanPham_LoaiSanPham 
FOREIGN KEY (LoaiSP) 
REFERENCES LoaiSanPham(ID_LoaiSP)

ALTER TABLE SanPham
ADD CONSTRAINT FK_SanPham_DonViTinh 
FOREIGN KEY (DonViTinh) 
REFERENCES DonViTinh(ID_DVT)
go

--ChiTietDotHang
ALTER TABLE ChiTietDotHang 
ADD CONSTRAINT FK_ChiTietDotHang_DotHang 
FOREIGN KEY (ID_DotHang) 
REFERENCES DotHang(ID_DotHang)
go

ALTER TABLE ChiTietDotHang 
ADD	CONSTRAINT FK_ChiTietDotHang_SanPham 
FOREIGN KEY (ID_SanPham) 
REFERENCES SanPham(ID_SanPham)
go

--Kho
ALTER TABLE Kho 
ADD CONSTRAINT FK_Kho_NhaPhanPhoi 
FOREIGN KEY (NPP) 
REFERENCES NhaPhanPhoi(ID_NPP)
go

--DonYCDoiTra
ALTER TABLE DonYCDoiTra 
ADD CONSTRAINT FK_DonYCDoiTra_NhaPhanPhoi 
FOREIGN KEY (NPP) 
REFERENCES NhaPhanPhoi(ID_NPP)

ALTER TABLE DonYCDoiTra 
ADD CONSTRAINT FK_DonYCDoiTra_Kho 
FOREIGN KEY (Kho) 
REFERENCES Kho(ID_Kho)

ALTER TABLE DonYCDoiTra 
ADD CONSTRAINT FK_DonYCDoiTra_NhanVien 
FOREIGN KEY (NhanVien) 
REFERENCES NhanVien(ID_NV)

--CTDonYCDoiTra
ALTER TABLE CTDonYCDoiTra 
ADD CONSTRAINT FK_CTDonYCDoiTra_DonYCDoiTra 
FOREIGN KEY (ID_DonYCDT) 
REFERENCES DonYCDoiTra(ID_DonYCDT)

ALTER TABLE CTDonYCDoiTra 
ADD CONSTRAINT FK_CTDonYCDoiTra_SanPham 
FOREIGN KEY (ID_SanPham) 
REFERENCES SanPham(ID_SanPham)


ALTER TABLE NguoiLienHeGiaoHang ADD
	CONSTRAINT FK_NguoiLienHeGiaoHang_NhaPhanPhoi FOREIGN KEY (ID_NPP) REFERENCES NhaPhanPhoi (ID_NPP),
	CONSTRAINT FK_NguoiLienHeGiaoHang_DonDatHang FOREIGN KEY (ID_DonHang) REFERENCES DonDatHang (ID_DonHang)
go

ALTER TABLE ChiTiet_DDH ADD 
	CONSTRAINT FK_ChiTietDDH_DonDatHang FOREIGN KEY (ID_DonHang) REFERENCES DonDatHang (ID_DonHang),
	CONSTRAINT FK_ChiTietDDH_SanPham FOREIGN KEY (ID_SanPham) REFERENCES SanPham (ID_SanPham)
go

ALTER TABLE Log_ChiTietDDH ADD 
	CONSTRAINT FK_LogChiTietDDH_DonDatHang FOREIGN KEY (ID_DonHang) REFERENCES DonDatHang(ID_DonHang),
	CONSTRAINT FK_LogChiTietDDH_SanPham FOREIGN KEY (ID_SanPham) REFERENCES SanPham(ID_SanPham),
	CONSTRAINT FK_LogChiTietDDH_NhanVien FOREIGN KEY (ID_NhanVien) REFERENCES NhanVien(ID_NV)
go

ALTER TABLE CT_KhuyenMaiTang ADD 
	CONSTRAINT FK_CT_KhuyenMaiTang_KhuyenMai FOREIGN KEY (ID_KhuyenMai) REFERENCES KhuyenMai(ID_KhuyenMai),
	CONSTRAINT FK_CT_KhuyenMaiTang_SanPham FOREIGN KEY (ID_SanPham) REFERENCES SanPham(ID_SanPham)
go

ALTER TABLE CT_KhuyenMaiMua ADD 
	CONSTRAINT FK_CT_KhuyenMaiMua_KhuyenMai FOREIGN KEY (ID_KhuyenMai) REFERENCES KhuyenMai(ID_KhuyenMai),
	CONSTRAINT FK_CT_KhuyenMaiMua_SanPham FOREIGN KEY (ID_SanPham) REFERENCES SanPham(ID_SanPham)
go

ALTER TABLE CT_DoanhThu ADD 
	CONSTRAINT FK_CT_DoanhThu_BaoCaoDoanhThu FOREIGN KEY (ID_BaoCaoDoanhThu) REFERENCES BaoCaoDoanhThu(ID_BaoCaoDoanhThu),
	CONSTRAINT FK_CT_DoanhThu_LoaiSP FOREIGN KEY (ID_LoaiSP) REFERENCES LoaiSanPham(ID_LoaiSP)
go

ALTER TABLE DonGiaoHang ADD 
	CONSTRAINT FK_DonGiaoHang_DonDatHang FOREIGN KEY (ID_DonDatHang) REFERENCES DonDatHang(ID_DonHang),
	CONSTRAINT FK_DonGiaoHang_NhanVien FOREIGN KEY (ID_NhanVien) REFERENCES NhanVien(ID_NV),
	CONSTRAINT FK_DonGiaoHang_NhaPhanPhoi FOREIGN KEY (ID_NPP) REFERENCES NhaPhanPhoi(ID_NPP)
go

ALTER TABLE CT_GiaoHang ADD 
	CONSTRAINT FK_CT_GiaoHang_DonGiaoHang FOREIGN KEY (ID_GiaoHang) REFERENCES DonGiaoHang(ID_GiaoHang),
	CONSTRAINT FK_CT_GiaoHang_SanPham FOREIGN KEY (ID_SanPham) REFERENCES SanPham(ID_SanPham)
go

ALTER TABLE HoaDon ADD 
	CONSTRAINT FK_HoaDon_DonGiaoHang FOREIGN KEY (ID_GiaoHang) REFERENCES DonGiaoHang(ID_GiaoHang),
	CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY (ID_NhanVien) REFERENCES NhanVien(ID_NV),
	CONSTRAINT FK_HoaDon_NhaPhanPhoi FOREIGN KEY (ID_NPP) REFERENCES NhaPhanPhoi(ID_NPP)
go
