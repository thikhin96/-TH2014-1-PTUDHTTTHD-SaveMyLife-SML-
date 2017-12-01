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

--SanPham
ALTER TABLE SanPham 
ADD CONSTRAINT FK_SanPham_LoaiSanPham 
FOREIGN KEY (LoaiSP) 
REFERENCES LoaiSanPham(ID_LoaiSP)

ALTER TABLE SanPham
ADD CONSTRAINT FK_SanPham_DonViTinh 
FOREIGN KEY (DonViTinh) 
REFERENCES DonViTinh(ID_DVT)

--ChiTietDotHang
ALTER TABLE ChiTietDotHang 
ADD CONSTRAINT FK_ChiTietDotHang_DotHang 
FOREIGN KEY (ID_DotHang) 
REFERENCES DotHang(ID_DotHang)

ALTER TABLE ChiTietDotHang 
ADD	CONSTRAINT FK_ChiTietDotHang_SanPham 
FOREIGN KEY (ID_SanPham) 
REFERENCES SanPham(ID_SanPham)

--Kho
ALTER TABLE Kho 
ADD CONSTRAINT FK_Kho_NhaPhanPhoi 
FOREIGN KEY (NPP) 
REFERENCES NhaPhanPhoi(ID_NPP)

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

