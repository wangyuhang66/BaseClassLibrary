-----=======提升


--复制表（只复制结构， 源表名：a， 新表名：b ）（Access数据库 可用）
--一：
select * into b from a where 1<>1(仅用于SQLServer)
--二：
select top 0 * into b from a

--拷贝表（拷贝数据， 源表名：a， 目标表名：b）（Access数据库 可用）
insert into b（a, b, c） select d,e,f from a;

--跨数据库之间表得拷贝（具体数据使用绝对路径） （Access数据库 可用）
insert into b(a,b,c) select d,e,f from b in '具体数据库' where 条件

--子查询 （表名1：a， 表名2： b）
select a,b,c from a where a in(select d from b);
--或者：
select a,b,c from a where a in (1,2,3)

--显示文章 提交人 和最后回复时间
select a.title, a.username, b.adddate from table a, (select max(adddate) as adddate from table where table.title = a.title) b;

--外联结查询
select a.a, a.b, b.c, b.d from a LEFT JOIN b ON a.a = b.c;

--在线视图查询
select * from (select a,b,c from a ) T where t.a >1

--between 的用法:
--between 限制查询数据范围时包括了边界值,not between 不包括 
select * from table1 where time between time1 and time2 ;
select a,b,c, from table1 where a not between 数值 1 and 数值 2;

--in 的使用方法
 select * from table1 where a [not] in (‘值 1’,’值 2’,’值 4’,’值 6’) 

--两张关联表，删除主表中已经在副表中没有的信息  
delete from table1 where not exists(select * from table2 where table.field1 = table2.field1)

--四表联查问题： 
select * from a left 
inner join b on a.a=b.b right 
inner join c on a. a=c.c 
inner join d on a.a=d.d 
where 条件

--日程安排提前五分钟提醒
 select * from 日程安排 where datediff('minute',f 开始时间,getdate())>5 

--一条 sql 语句搞定数据库分页 
select top 10 b.* 
from (select top 20 主键字段,排序字段 from 表名 order by 排序 字段 desc) a,表名 b 
where b.主键字段 = a.主键字段 order by a.排序字段

--具体实现
 declare @start int,@end int @sql  nvarchar(600)   
 set @sql=’select top’+str(@end-@start+1)+’+
 from T where rid not in
 (select top’+str(@str-1)+’Rid from T where Rid>-1)’   
 exec sp_executesql @sql 

 --注意：在 top 后不能直接跟一个变量，
 --所以在实际应用中只有这样的进行特殊 的处理。
 --Rid 为一个标识列，如果 top 后还有具体的字段，这样做是非常有好处 的。
 --因为这样可以避免 top 的字段如果是逻辑索引的，查询的结果后实际表中的不一致
 --逻辑索引中的数据有可能和数据表中的不一致，而查询时如果处在 索引则首先查询索引） 
 
 --前10条记录
select top 10 * from tablename

--选择在每一组 b 值相同的数据中对应的 a 最大的记录的所有信息(类 似这样的用法可以用于论坛每月排行榜,每月热销产品分析,按科目成绩排名, 等等.)
select a ,b,c from tablename ta where a=(select max(a) from tablename tb where tb.b = ta.b)

--包括所有在 TableA 中但不在 TableB 和 TableC 中的行并消除所有 重复行而派生出一个结果表
(select a from tableA) except (select a from tableB) except (select a from tablkeC)

--随机取出10条数据
select top 10 * from tablename order by newid();

--随机选择记录 
select newid();

--删除重复记录 1)
delete from tablename where id not in (select max(id) from tablena me group by col1,col2,...); 
select distinct * into temp from tablename delete from tablename   insert into tablename select * from temp; 
--评价： 这种操作牵连大量的数据的移动，这种做法不适合大容量但数据操作 


--在一个外部表中导入数据，由于某些原因第一次只导入了一部分，但 很难判断具体位置，这样只有在下一次全部导入，这样也就产生好多重复的字 段，怎样删除重复字段 
alter table tablename 
--添加一个自增列 
add  column_b int identity(1,1)  delete from tablename where column_b not in( select max(column_b)  from tablename group by column1,column2,...) alter table tablename drop column column_b

--列出数据库里所有的表名 
select name from sysobjects where type='U' // U 代表用户 

--说明：列出表里的所有的列名 
select name from syscolumns where id=object_id('TableName') 

--说明：列示 type、vender、pcs 字段，以 type 字段排列，case 可以方便地 实现多重选择，类似 select 中的 case。 
select type,sum(case vender when 'A' then pcs else 0 end),sum(case ve nder when 'C' then pcs else 0 end),sum(case vender when 'B' then pcs else 0 end) FROM tablename group by type 显示结果： type vender pcs 电脑 A 1 电脑 A 1 光盘 B 2 光盘 A 2 手机 B 3 手机 C 3

--说明：初始化表 table1 
TRUNCATE TABLE table1 

--说明：选择从 10 到 15 的记录 
select top 5 * from (select top 15 * from table order by id asc) tabl e_别名 order by id desc 

