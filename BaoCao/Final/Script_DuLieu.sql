use QL_NPP

--Account
insert into Account values('taikhoan1','123456789',1,2,0,GetDate(),getdate(),null);
insert into Account values('taikhoan2','123456789',1,3,0,GetDate(),getdate(),null);
insert into Account values('taikhoan3','123456789',1,1,0,GetDate(),getdate(),null);
insert into Account values('taikhoan4','123456789',1,2,0,GetDate(),getdate(),null);
insert into Account values('taikhoan5','123456789',0,1,0,GetDate(),getdate(),null);
insert into Account values('taikhoan6','123456789',1,2,0,GetDate(),getdate(),null);
insert into Account values('taikhoan7','123456789',1,3,0,GetDate(),getdate(),null);
insert into Account values('taikhoan8','123456789',1,1,0,GetDate(),getdate(),null);
insert into Account values('taikhoan9','123456789',1,2,0,GetDate(),getdate(),null);
insert into Account values('taikhoan10','123456789',0,1,0,GetDate(),getdate(),null);

--Log_Login


--PotentialDistributor

--Distributor
select * from Distributor
--delete from Distributor
insert into Distributor values(N'Nhà phân phối 1',N'Quận Phú Nhuận, TPHCM','0987464556','emai34l@sample.com',null,getdate(),getdate(),null,1,'taikhoan6');
insert into Distributor values(N'Nhà phân phối 2',N'Quận Phú Nhuận, TPHCM','0456045045','email1@sample.com',null,getdate(),getdate(),null,1,'taikhoan7');
insert into Distributor values(N'Nhà phân phối 3',N'Quận Phú Nhuận, TPHCM','0456045606','email2@sample.com',null,getdate(),getdate(),null,1,'taikhoan8');
insert into Distributor values(N'Nhà phân phối 4',N'Quận Phú Nhuận, TPHCM','0789760466','email3@sample.com',null,getdate(),getdate(),null,1,'taikhoan9');
insert into Distributor values(N'Nhà phân phối 5',N'Quận Phú Nhuận, TPHCM','0456786078','email4@sample.com',null,getdate(),getdate(),null,1,'taikhoan10');


--[Contract]

--Representative

--Staff
select * from staff
insert into Staff values(N'Nguyên Văn Linh','taikhoan1','123456789','098794654',N'Q12,Việt Nam');
insert into Staff values(N'Vũ Liễu Nguyên','taikhoan2','123456788','0879746541',N'Q12,Việt Nam');
insert into Staff values(N'Trang Hồng Hường','taikhoan3','123456787','098746189',N'Q12,Việt Nam');
insert into Staff values(N'Lương Minh Tú','taikhoan4','123456786','0897406545',N'Q12,Việt Nam');
insert into Staff values(N'Zingiheu Shao','taikhoan5','123456785','0485949546',N'Q12,Việt Nam');


--Assignment

--ProductType
insert into ProductType values(N'Sữa bột');
insert into ProductType values(N'Sữa chua');
insert into ProductType values(N'Sữa đặc');
insert into ProductType values(N'Sữa hộp');

--Unit
insert into Unit values(N'Thùng',24);
insert into Unit values(N'Lốc',6);
insert into Unit values(N'Lon',24);

--Product
insert into Product values(N'Sữa ông thọ',24000,0,3,3);
insert into Product values(N'Sữa cô gái Việt Nam',24000,0,4,1);
insert into Product values(N'Sữa trái cây',24000,0,4,1);
insert into Product values(N'Sữa En súa 3',24000,0,1,1);

--Batch

--BatchDetail

--Storage

--ReturnRequest

--ReturnRequestDetail

--ReturnBase

--ReturnDetail

--Debt

--PaySlip

--[Order]
insert into [Order] values(10000000,1,1,GETDATE(),0,getdate(),getdate(),1,1,1,null);
insert into [Order] values(35000000,0,0,GETDATE(),2,getdate(),getdate(),1,2,2,null);
insert into [Order] values(350000000,0,1,GETDATE(),3,getdate(),getdate(),3,3,4,null);
insert into [Order] values(98500000,0,0,GETDATE(),1,getdate(),getdate(),5,3,1,null);
insert into [Order] values(135000000,1,1,GETDATE(),2,getdate(),getdate(),2,2,3,null);
insert into [Order] values(546800000,0,1,GETDATE(),1,getdate(),getdate(),4,1,5,null);
insert into [Order] values(1239800000,0,1,GETDATE(),0,getdate(),getdate(),5,2,2,null);

--Consignee
select * from Consignee
insert into Consignee values(N'Trang Sĩ Dũ','089789431',3);
insert into Consignee values(N'Vũ Sĩ Giang','065495465',4);
insert into Consignee values(N'Trang Bảo Đặng','498403126',5);
insert into Consignee values(N'Trần Nhật Ahh','564654065',1);
insert into Consignee values(N'Hozima Kamwa','049846054',2);

--Log_Order

--OrderDetail
select * from [order]
select * from product

insert into OrderDetail values(15,1,10);
insert into OrderDetail values(15,4,10);
insert into OrderDetail values(15,5,10);
insert into OrderDetail values(15,6,10);

insert into OrderDetail values(18,1,30);
insert into OrderDetail values(18,4,50);
insert into OrderDetail values(18,5,10);
insert into OrderDetail values(18,6,5);

insert into OrderDetail values(22,1,10);
insert into OrderDetail values(22,4,10);
insert into OrderDetail values(22,5,10);
insert into OrderDetail values(22,6,10);

insert into OrderDetail values(23,1,30);
insert into OrderDetail values(23,4,50);
insert into OrderDetail values(23,5,10);
insert into OrderDetail values(23,6,5);

insert into OrderDetail values(25,1,10);
insert into OrderDetail values(25,4,10);
insert into OrderDetail values(25,5,10);
insert into OrderDetail values(25,6,10);

insert into OrderDetail values(26,1,30);
insert into OrderDetail values(26,4,50);
insert into OrderDetail values(26,5,10);
insert into OrderDetail values(26,6,5);

insert into OrderDetail values(27,1,10);
insert into OrderDetail values(27,4,10);
insert into OrderDetail values(27,5,10);
insert into OrderDetail values(27,6,10);



--Log_OrderDetail

--Log_Product

--Promotion
insert into Promotion values(GETDATE(),dateadd(dd,30,getdate()));
insert into Promotion values(GETDATE(),dateadd(dd,40,getdate()));
insert into Promotion values(GETDATE(),dateadd(dd,7,getdate()));


--PromotionGifts
insert into PromotionGifts values(1,1,30);
insert into PromotionGifts values(1,4,50);
insert into PromotionGifts values(1,5,10);

insert into PromotionGifts values(2,5,10);
insert into PromotionGifts values(2,6,5);

insert into PromotionGifts values(3,4,50);
insert into PromotionGifts values(3,5,10);


--PromotionProduct

insert into PromotionProduct values(1,1,10);
insert into PromotionProduct values(1,4,10);

insert into PromotionProduct values(2,6,10);
insert into PromotionProduct values(2,4,20);

insert into PromotionProduct values(3,1,10);
insert into PromotionProduct values(3,6,10);

--SalesReport

