use master
go
if DB_ID('QLNhaHang') is not null
	drop database QLNhaHang

go
create database QLNhaHang
go
use QLNhaHang
go
------------------------------------------
-------------BANG BAN AN------------------
create table BanAn(
	MaBanAn int identity(1,1) not null primary key,
	TenBan nvarchar(10),
	SoGhe int not null,
	TinhTrang nvarchar(20) not null
)
--------------NHAP BAN AN---------------------------
insert into BanAn values
(N'Bàn 1',4,N'Trống'),
(N'Bàn 2',4,N'Trống'),
(N'Bàn 3',4,N'Trống'),
(N'Bàn 4',4,N'Trống'),
(N'Bàn 5',4,N'Trống'),
(N'Bàn 6',4,N'Trống'),
(N'Bàn 7',4,N'Trống'),
(N'Bàn 8',4,N'Trống'),
(N'Bàn 9',4,N'Trống')

-------------BANG MENU------------------------
create table Menu(
	MaMenu int identity(1,1) not null primary key,
	TenMenu nvarchar(50) not null,
	MoTa text,
)

insert into Menu values
(N'Món khai vị',''),
(N'Món chính',''),
(N'Món tráng miệng',''),
(N'Tiệc sinh nhật',''),
(N'Tiệc mừng thọ',''),
(N'Buffet lẩu thái',''),
(N'Buffet lẩu hải sản','')


-------------BANG MON AN--------------
create table MonAn(
	MaMonAn int identity(1,1) not null primary key,
	TenMonAn nvarchar(50) not null,
	Gia int not null,
	MoTa nvarchar(200),
	Anh nvarchar(200),
	MaMenu int not null,
	constraint fk_monan_menu foreign key (MaMenu)
	references Menu(MaMenu) on delete cascade

)
insert into MonAn values
(N'Cá kho',20000,N'Cá','Image\1',2),
(N'Chuối nấu',10000,N'Nấu','Image\2',2),
(N'Khoai tây nấu',22000,N'Cấu','Image\3',2),
(N'Canh ngó khoai',12000,N'Canh','Image\4',2),
(N'Canh mùng',25000,N'Canh','Image\5',2),
(N'Phở',25000,N'Truyền thống','Image\pho',2),
(N'Bún bò Huế',25000,N'Truyền thống','Image\7',2),
(N'Gỏi cuốn',25000,N'Truyền thống','Image\8',2),
(N'Bánh xèo',25000,N'Truyền thống','Image\9',1),
(N'Chè khúc bạch',25000,N'Ăn vặt','Image\10',5),
(N'Cơm cháy',25000,N'Ăn vặt','Image\11',1),
(N'Bánh gạo lắc phô mai',25000,N'Ăn vặt','Image\12',5),
(N'Khoai lang kén',22000,N'Ăn vặt','Image\13',6),
(N'Trà sữa',25000,N'Ăn vặt','Image\14',4),
(N'Thạch dừa',25000,N'Ăn vặt','Image\15',4),
(N'Chè khoai lang',25000,N'Ăn vặt','Image\16',4),
(N'Chân gà sả ớt',25000,N'Ăn vặt','Image\17',1),
(N'Bánh gối',25000,N'Ăn vặt','Image\18',1),
(N'Khoai tây chiên',25000,N'Ăn vặt','Image\19',1),
(N'Bánh Xèo',25000,N'Ăn vặt','Image\20',6),
(N'Bánh bao nhân thịt',25000,N'Ăn vặt','Image\21',5),
(N'Bánh rán mặn',25000,N'Ăn vặt','Image\22',1),
(N'Tokbokki',25000,N'Ăn vặt','Image\23',5),
(N'Nem chua rán',25000,N'Ăn vặt','Image\24',1),
(N'Phô mai que',25000,N'Ăn vặt','Image\25',6),
(N'Salad xoài nấm',25000,N'Món chay','Image\26',1),
(N'Đậu phụ xào nấm',25000,N'Món chay','Image\27',2),
(N'Súp khoai tây',25000,N'Món chay','Image\28',2),
(N'Há cảo chay',25000,N'Món chay','Image\29',2),
(N'Salad chay',25000,N'Món chay','Image\30',1),
(N'Lagu chay',25000,N'Món chay','Image\31',2),
(N'Cơm chiên bát bửu chay',25000,N'Món chay','Image\32',5),
(N'Alfajores',25000,N'Tráng miệng','Image\Alfajores',4),
(N'Cornes de Gazelle',25000,N'Tráng miệng','Image\Cornes de Gazelle',4),
(N'Coconut Cake',25000,N'Tráng miệng','Image\Coconut Cake',4),
(N'Chocolate Mousse',25000,N'Tráng miệng','Image\Chocolate Mousse',3),
(N'Cendol',25000,N'Tráng miệng','Image\Cendol',5),
(N'Cardamom Bun',25000,N'Tráng miệng','Image\Cardamom Bun',3),
(N'Cannoli',25000,N'Tráng miệng','Image\Cannoli',6),
(N'Borma',25000,N'Tráng miệng','Image\Borma',6),
(N'Black Forest Cake',25000,N'Tráng miệng','Image\Black Forest Cake',3),
(N'Baklava',25000,N'Tráng miệng','Image\Baklava',3),
(N'Bánh chuối nướng',25000,N'Tráng miệng','Image\33',3),
(N'Bánh trôi tàu',25000,N'Tráng miệng','Image\34',3),
(N'Chè hương cốm lá dứa',25000,N'Tráng miệng','Image\35',3),
(N'Chè trái cây',25000,N'Tráng miệng','Image\36',5),
(N'Bánh cuộn tinh than tre',25000,N'Tráng miệng','Image\37',4),
(N'Bánh bông lan chanh dây',25000,N'Tráng miệng','Image\38',3),
(N'Bánh nếp bí đỏ',25000,N'Tráng miệng','Image\39',3),
(N'Sương sáo mật ong',25000,N'Tráng miệng','Image\40',3),
(N'Tào phớ',25000,N'Tráng miệng','Image\41',3),
(N'Chè bưởi',25000,N'Tráng miệng','Image\42',3),
(N'Bánh đậu đỏ hấp',25000,N'Tráng miệng','Image\43',7),
(N'Bánh đậu xanh',25000,N'Tráng miệng','Image\44',7),
(N'Chè hạt sen thạch rau câu',25000,N'Tráng miệng','Image\45',3),
(N'Súp ngô xay thịt hầm',125000,N'Khai vị','Image\46',5),
(N'Súp gà xé nấm',120000,N'Khai vị','Image\47',5),
(N'Súp cua măng tây',180000,N'Khai vị','Image\48',1),
(N'Súp rau',100000,N'Khai vị','Image\49',1),
(N'Cải chíp sốt nấm',40000,N'Khai vị','Image\50',1),
(N'Rau củ quả luộc',35000,N'Khai vị','Image\51',6),
(N'Khoai lang chiên',65000,N'Khai vị','Image\52',1),
(N'Ngon su su xào tỏi',50000,N'Khai vị','Image\53',6),
(N'Nộm ngó sen hoa chuối',70000,N'Khai vị','Image\54',1),
(N'Nộm rau tiến vua',90000,N'Khai vị','Image\55',1),
(N'Nộm bê bóp thấu',170000,N'Khai vị','Image\56',7),
(N'Khoai tây chiên',55000,N'Khai vị','Image\57',7),
(N'Salad',65000,N'Khai vị','Image\58',6),
(N'Salat dưa chuột cà chua',65000,N'Khai vị','Image\59',7),
(N'Chả mực',225000,N'Món chính','Image\60',1),
(N'Mực ống nhồi thịt',225000,N'Món chính','Image\61',7),
(N'Mực xào cần tỏi',250000,N'Món chính','Image\62',7),
(N'Mực xào ngũ sắc',225000,N'Món chính','Image\63',5),
(N'Dê tái chanh',325000,N'Món chính','Image\64',4),
(N'Dê xào lăn',325000,N'Món chính','Image\65',5),
(N'Gà hấp lá chanh',225000,N'Món chính','Image\66',5),
(N'Gà hấp mã não',225000,N'Món chính','Image\67',5),
(N'Gà hấp dầu mè',225000,N'Món chính','Image\68',4),
(N'Gà hấp muối',225000,N'Món chính','Image\69',4),
(N'Tôm chao dầu',325000,N'Món chính','Image\70',7),
(N'Tôm hấp',325000,N'Món chính','Image\71',7),
(N'Tôm chiên cốm',325000,N'Món chính','Image\72',7),
(N'Tôm xốt chua ngọt',325000,N'Món chính','Image\73',1),
(N'Lẩu Thái hải sản',525000,N'Món chính','Image\74',6),
(N'Lẩu hải sản',525000,N'Món chính','Image\75',7),
(N'Lẩu thập cẩm',525000,N'Món chính','Image\76',6),
(N'Bò hầm tiêu đen',250000,N'Món chính','Image\77',6),
(N'Bò chiên Thái Lan',205000,N'Món chính','Image\78',6),
(N'Bò sốt tiêu đen + Bánh bao',205000,N'Món chính','Image\79',1),
(N'Bò bít tết',205000,N'Món chính','Image\80',1)

-------------BANG KHACH HANG----------------------
create table KhachHang(
	MaKhachHang int identity(1,1) not null primary key,
	TenKhachHang nvarchar(60) not null,
	DiaChi nvarchar(100),
	SoDienThoai nchar(12) not null,
	constraint unique_khachhang unique(TenKhachHang,DiaChi)
)

insert into KhachHang values
(N'Nguyễn Văn Tuấn',N'Hà Nội','091234'),
(N'Nguyễn Văn Nam',N'Hà Nam','0382'),
(N'Nguyễn Thị Vân',N'Hà Tĩnh','0123'),
(N'Trần Văn Tuấn',N'Sài Gòn','0987'),
(N'Lại Văn Tuấn',N'Hai Dương','01123'),
(N'Hà Văn Tuấn',N'Hà Nội','0914234')

---------------BANG NHAN VIEN--------------------------
create table NhanVien(
	MaNhanVien int identity(1,1) not null primary key,
	TenNhanVien nvarchar(60) not null,
	GioiTinh nvarchar(10),
	Tuoi int not null,
	DiaChi nvarchar(100),
	SoDienThoai nchar(12) not null,
	tendangnhap varchar(50),
	matkhau varchar(50) not null,
	quyen nvarchar(50)
	constraint unique_username unique(tendangnhap)
)

insert into NhanVien values
(N'Hứa Văn Duy',N'Nam',20,N'Hà Nội','0912363523','huavanduy','271020',N'Quản lý'),
(N'Nguyễn Tiến Dũng',N'Nam',20,N'Hà Nam','0912363523','nguyentiendung','271020',N'Nhân viên'),
(N'Trần Văn Tuấn',N'Nam',20,N'Hà Nội','0912363523','account1','271020',N'Nhân viên'),
(N'Trần Thị Ly',N'Nữ',20,N'Hà Nội','09123523','account2','271020',N'Nhân viên'),
(N'Nguyễn Minh Châu',N'Nam',20,N'Hà Nội','0912523','account3','271020',N'Nhân viên'),
(N'Nguyễn Như Trọng',N'Nam',20,N'Hà Nội','0912523','account4','271020',N'Nhân viên'),
(N'Tạ Văn Tấn',N'Nam',20,N'Hà Nội','0912523','account5','271020',N'Nhân viên')
----------------BANG HOA DON---------------------------
create table HoaDon(
	MaHoaDon int identity(1,1) not null primary key,
	NgayLap date not null,
	MaNhanVien int not null,
	MaKhachHang int not null,
	MaBanAn int not null,
	constraint fk_hoadon_banan foreign key (MaBanAn)
	references BanAn(MaBanAn) on delete cascade,
	constraint fk_hoadon_khachhang foreign key (MaKhachHang)
	references KhachHang(MaKhachHang),
	constraint fk_hoadon_nhanvien foreign key (MaNhanVien)
	references NhanVien(MaNhanVien) on delete cascade
)

insert into HoaDon values
('12/12/2021',1,2,1),
('11/12/2021',2,3,2),
('9/12/2021',1,2,5),
('10/12/2021',3,5,4),
('7/12/2021',2,1,4)
-------------BANG CHI TIET HOA DON------------------------

create table ChiTietHoaDon(
	MaHoaDon int not null,
	MaMonAn int not null,
	SoLuong int,
	constraint pk_chitiethoadon primary key (MaHoaDon, MaMonAn),
	constraint fk_chitiethoadon_hoadon foreign key (MaHoaDon)
	references HoaDon(MaHoaDon) on delete cascade,
	constraint fk_chitiethoadon_monan foreign key (MaMonAn)
	references MonAn(MaMonAn) on delete cascade
)

insert into ChiTietHoaDon values
(1,1,3),  --1
(1,2,3),	--2
(1,3,3),	--2
(2,1,3),--1
(3,2,3),--2
(4,2,3),--2
(4,1,3),--1
(5,4,3)--4
go

SELECT * FROM MonAn
select * from HoaDon
select * from ChiTietHoaDon
select * from Menu
select * from banan
select * from KhachHang
select * from NhanVien
