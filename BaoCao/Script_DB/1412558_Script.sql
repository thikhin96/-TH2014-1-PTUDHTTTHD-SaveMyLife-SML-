create table CT_KhuyenMaiMua (
	ID_KhuyenMai int,
	ID_SanPham int,
	SoLuong int
);

create table BaoCaoDoanhThu (
	ID_BaoCaoDoanhThu int identity(1,1) primary key,
	NgayBatDau datetime,
	NgayKetThuc datetime
);

create table CT_DoanhThu (
	ID_BaoCaoDoanhThu int,
	ID_LoaiSP int,
	DonGiaBan int,
	SoLuong int
);

create table DonGiaoHang(
	ID_GiaoHang int identity(1,1) primary key,
	NguoiNhan nvarchar(50),
	DiaChiGiao nvarchar(100),
	TongTien money,
	NgayGiao datetime,
	TinhTrang tinyint check(TinhTrang in (0,1,2,3,4)),
	NgayCapNhat datetime,
	GhiChu nvarchar(200),
	ID_DonDatHang int,
	ID_NhanVien int,
	ID_NPP int
);

create table CT_GiaoHang(
	ID_GiaoHang int,
	ID_SanPham int,
	SoLuong int,
	GhiChu nvarchar(200)
);

create table HoaDon(
	ID_HoaDon int identity(1,1) primary key,
	TongTien money,
	NgayLap datetime,
	LoaiHoaDon tinyint,
	NoiDung nvarchar(200),
	ID_GiaoHang int,
	ID_NhanVien int
);