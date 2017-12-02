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


ALTER TABLE ChiTiet_DDH ADD 
	CONSTRAINT FK_ChiTietDDH_DonDatHang FOREIGN KEY (ID_DonHang) REFERENCES DonDatHang (ID_DonHang),
	CONSTRAINT FK_ChiTietDDH_SanPham FOREIGN KEY (ID_SanPham) REFERENCES SanPham (ID_SanPham)
go
ALTER TABLE Log_ChiTietDDH ADD 
	CONSTRAINT FK_LogChiTietDDH_DonDatHang FOREIGN KEY (ID_DonHang) REFERENCES DonDatHang(ID_DonHang),
	CONSTRAINT FK_LogChiTietDDH_SanPham FOREIGN KEY (ID_SanPham) REFERENCES SanPham(ID_SanPham)
	CONSTRAINT FK_LogChiTietDDH_NhanVien FOREIGN KEY (ID_NhanVien) REFERENCES NhanVien(ID_NhanVien)
go


ALTER TABLE CT_KhuyenMaiTang ADD 
	CONSTRAINT FK_CT_KhuyenMaiTang_KhuyenMai FOREIGN KEY (ID_KhuyenMai) REFERENCES KhuyenMai(ID_KhuyenMai),
	CONSTRAINT FK_CT_KhuyenMaiTang_SanPham FOREIGN KEY (ID_SanPham) REFERENCES SanPham(ID_SanPham)
go