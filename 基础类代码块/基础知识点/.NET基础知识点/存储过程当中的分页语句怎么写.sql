create procedure page
	@pageIndex int,		--当前页		
	@pageSize int     --页面大小
  as
create table #temp
(
  EID int,
  tempId int identity(1,1)
)
insert into        #temp(EID) select EID from Employee  --将表Employee的EID作为一个新的列插入到虚表#temp
select * from (
select * from Emplyee inner join #temp.EId = Employee.EID --查询要执行分页的数据
)as temp
where tempId between(@pageIndex-1)*(@pageSize+1) and @pageIndex*@pageSize  