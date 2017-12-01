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

ALTER TABLE PhieuDoiTra ADD
	CONSTRAINT FK_PhieuDoiTra_NhaPhanPhoi FOREIGN KEY (NPP) REFERENCES NhaPhanPhoi (Id_NPP),
	CONSTRAINT FK_PhieuDoiTra_Kho FOREIGN KEY (Kho) REFERENCES Kho (ID_Kho),
	CONSTRAINT FK_PhieuDoiTra_NhanVien FOREIGN KEY (NhanVien) REFERENCES NhanVien (Id_NV)
go

ALTER TABLE CTPhieuDoiTra ADD
	CONSTRAINT FK_CTPhieuDoiTra_PhieuDoiTra FOREIGN KEY (ID_PhieuDoiTra) REFERENCES PhieuDoiTra (ID_PhieuDoiTra),
	CONSTRAINT FK_CTPhieuDoiTra_SanPham FOREIGN KEY (ID_SanPham) REFERENCES SanPham (ID_SanPham)
go

ALTER TABLE PhieuCongNo ADD
	CONSTRAINT FK_PhieuCongNo_NhaPhanPhoi FOREIGN KEY (NPP) REFERENCES NhaPhanPhoi (Id_NPP),
	CONSTRAINT FK_PhieuCongNo_NhanVien FOREIGN KEY (NhanVien) REFERENCES NhanVien (Id_NV)
go

ALTER TABLE PhieuChi ADD
	CONSTRAINT FK_PhieuChi_NhaPhanPhoi FOREIGN KEY (NPP) REFERENCES NhaPhanPhoi (Id_NPP),
	CONSTRAINT FK_PhieuChi_NhanVien FOREIGN KEY (NhanVien) REFERENCES NhanVien (Id_NV)
go

ALTER TABLE DonDatHang ADD
	CONSTRAINT FK_DonDatHang_NhaPhanPhoi FOREIGN KEY (ID_NPP) REFERENCES NhaPhanPhoi (Id_NPP),
	CONSTRAINT FK_DonDatHang_NguoiLienHeGiaoHang FOREIGN KEY (ID_NguoiLienHe) REFERENCES NguoiLienHeGiaoHang (ID_NguoiLienHe),
	CONSTRAINT FK_DonDatHang_NhanVien FOREIGN KEY (ID_NhanVien) REFERENCES NhanVien (Id_NV)
go

ALTER TABLE NguoiLienHeGiaoHang ADD
	CONSTRAINT FK_NguoiLienHeGiaoHang_NhaPhanPhoi FOREIGN KEY (ID_NPP) REFERENCES NhaPhanPhoi (Id_NPP),
	CONSTRAINT FK_NguoiLienHeGiaoHang_DonDatHang FOREIGN KEY (ID_DonHang) REFERENCES DonDatHang (ID_DonHang)









