use orders
go
select * from members
select * from products
select * from paytype
select * from posters

select * from products where productcode like '1001%'
--
select * from products where  substring(productcode,1,4) like '1001'

select *,allprice=price_pro*productcount from products

select *,allprice=convert(numeric(18,2),price_pro*productcount) from products
select *,allprice=cast(price_pro*productcount as numeric(18,2)) from products

create proc sp_InsertOrder
@memberid int,
@receivename nvarchar(20),
@receiveaddress nvarchar(50),
@receivezip char(6),
@receivetel char(11),
@productids nvarchar(200), --10011020$10011030$10011040
@productcount nvarchar(200),--1$1$2
@paytype int,
@poster int
as
	begin transaction-
		declare @errorsum int,@temproductcount int,@tempallprice numeric(18,2)
		set @errorsum = 0 -

		select id= identity(int,1,1), col into #tcount from f_split(@productcount,'$')
select id= identity(int,1,1),col into #tproductcode from f_split(@productids,'$')
select productid=b.id,a.counts,a.productcodes,b.price_shop,b.prodctname,
sumprice=convert(numeric(18,2),a.counts*b.price_shop) into #temproducts
 from
(select #tcount.col as counts,#tproductcode.col as productcodes from #tcount,#tproductcode
where #tproductcode.id=#tcount.id) a left join
 products b on a.productcodes=b.productcode
drop table #tcount
drop table #tproductcode
		--select * from #temproducts

		select @temproductcount =sum(counts) from #temproducts
		select @tempallprice = sum(sumprice) from #temproducts


		insert into [order] (memberid,receivename,receiveaddress,receivezip,receivetel,
		productcount,allprice,paytype,poster) values (@memberid,@receivename,@receiveaddress,@receivezip,@receivetel,
@temproductcount,@tempallprice,@paytype,@poster)
      set @errorsum=@errorsum+ @@error

		insert into [order] (memberid,receivename,receiveaddress,receivezip,receivetel,
		productcount,allprice,paytype,poster) values (@memberid,@receivename,@receiveaddress,@receivezip,@receivetel,
@temproductcount,@tempallprice,@paytype,@poster)

	insert into ordersub (orderid,subid,productid,productcode,productcount,productallcount)
	values ()
	select * from [order]
	select * from ordersub
go


create function f_split(@c varchar(2000),@split varchar(2))
  returns  @t  table(col varchar(20))
  as
    begin
      while(charindex(@split,@c)<>0)   
        begin
          insert   @t(col)   values   (substring(@c,1,charindex(@split,@c)-1))
          set   @c   =   stuff(@c,1,charindex(@split,@c),'')
        end
      insert   @t(col)   values   (@c)
      return
    end
go
