create table CT_KhuyenMaiMua (
	ID_KhuyenMai int,
	ID_SanPham int,
	SoLuong int
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
	SoLuong int
);
go

/* cần bổ sung thêm TinhTrang = 0 <=> vừa mới lập mà chưa được kiểm kê*/
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

/* cần bổ sung thêm ID_NPP*/
create table HoaDon(
	ID_HoaDon int identity(1,1) primary key,
	TongTien money,
	NgayLap datetime,
	LoaiHoaDon tinyint check(LoaiHoaDon in (1,2)),
	NoiDung nvarchar(200),
	ID_GiaoHang int,
	ID_NhanVien int,
	/*ID_NPP int*/
);
go

ALTER TABLE CT_KhuyenMaiMua ADD 
	CONSTRAINTS FK_CT_KhuyenMaiMua_KhuyenMai FOREIGN KEY (ID_KhuyenMai) REFERENCES KhuyenMai(ID_KhuyenMai),
	CONSTRAINTS FK_CT_KhuyenMaiMua_SanPham FOREIGN KEY (ID_SanPham) REFERENCES SanPham(ID_SanPham)
go

ALTER TABLE CT_DoanhThu ADD 
	CONSTRAINTS FK_CT_DoanhThu_BaoCaoDoanhThu FOREIGN KEY (ID_BaoCaoDoanhThu) REFERENCES BaoCaoDoanhThu(ID_BaoCaoDoanhThu),
	CONSTRAINTS FK_CT_DoanhThu_LoaiSP FOREIGN KEY (ID_LoaiSP) REFERENCES LoaiSanPham(ID_LoaiSP)
go

ALTER TABLE DonGiaoHang ADD 
	CONSTRAINTS FK_DonGiaoHang_DonDatHang FOREIGN KEY (ID_DonDatHang) REFERENCES DonDatHang(ID_DonHang),
	CONSTRAINTS FK_DonGiaoHang_NhanVien FOREIGN KEY (ID_NV) REFERENCES NhanVien(Id_NV),
	CONSTRAINTS FK_DonGiaoHang_NhaPhanPhoi FOREIGN KEY (ID_NPP) REFERENCES NhaPhanPhoi(Id_NPP)
go

ALTER TABLE CT_GiaoHang ADD 
	CONSTRAINTS FK_CT_GiaoHang_DonGiaoHang FOREIGN KEY (ID_GiaoHang) REFERENCES DonGiaoHang(ID_GiaoHang),
	CONSTRAINTS FK_CT_GiaoHang_SanPham FOREIGN KEY (ID_SanPham) REFERENCES SanPham(ID_SanPham)
go

ALTER TABLE HoaDon ADD 
	CONSTRAINTS FK_HoaDon_DonGiaoHang FOREIGN KEY (ID_GiaoHang) REFERENCES DonGiaoHang(ID_GiaoHang),
	CONSTRAINTS FK_HoaDon_NhanVien FOREIGN KEY (ID_NhanVien) REFERENCES NhanVien(Id_NV)
	/*CONSTRAINTS FK_HoaDon_NhaPhanPhoi FOREIGN KEY (ID_NPP) REFERENCES NhaPhanPhoi(Id_NPP)*/
go