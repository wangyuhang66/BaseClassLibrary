-------基础

--创建数据库
CREATE DATABASE database-name

--删除数据库
DROP DATABASE dbname

--备份数据库
use　master
EXEC sp_addumpdevice 'disk', 'testBack','c:\mssql17backup\MyNwind_1.dat'
----开始备份
Backup database pubs TO testBack

--创建新表
create table tableName(col1 type1 [not null] [primary key], col2 type2 [not null] [primary key])

--根据已有的表创建新表
 A: create table tab_new like tab_old
 B: create table tab_new as select coll, col2 from tab_old definition only

 --删除新表
 drop table tabname

 --增加一个列
 Alter table tabname add column col type1
 --注意增加列后将不能删除， DB2中列加上后数据类型也不能改变，唯一能改变的是增加var char类型的长度

 --添加主键
 Alter table tabname add primary key(col)
--删除主键
Alter table tabname drop primary key(col)

--创建索引
create [unique] index idxname on tabname (col...)
--删除索引
drop view viewname

--选择
select * from table where  范围

--插入
insert into tabname (field1,field2) values(value1, vbalue2)

--删除
delete from table where 范围

--更新
update tabname set field1 = value1 where范围

--查找
select * from tableName where field like '%value%^' -- 模糊查询

--排序
select * from tableName order by field, field1, field2

--总数
select count(*) as totalcount from tableName

--求和
select sum(field) as sumvalue from tableName

--平均
select avg(field) as avgvalue from tableName

--最大
select max(field) as maxvalue from tableName

--最小
select min(field) as minvalue from tableName


----============高级查询

--A： UNION 运算符通过组合其他两个结果表（例如 TABLE1 和 TABLE2）并消去表中 任何重复行而派生出一个结果表。
--当 ALL 随 UNION 一起使用时（即 UNION AL L），不消除重复行。
--两种情况下，派生表的每一行不是来自 TABLE1 就是来自 TABLE2。  


--B： EXCEPT 运算符  
--EXCEPT 运算符通过包括所有在 TABLE1 中但不在 TABLE2 中的行并消除所有重 复行而派生出一个结果表。
--当 ALL 随 EXCEPT 一起使用时 (EXCEPT ALL)，不消 除重复行。 

--C： INTERSECT 运算符 
--INTERSECT 运算符通过只包括 TABLE1 和 TABLE2 中都有的行并消除所有重复 行而派生出一个结果表。
--当 ALL 随 INTERSECT 一起使用时 (INTERSECT ALL)， 不消除重复行。 

--===== 注：使用运算词的几个查询结果行必须是一致的


-----===外连接
--A、left （outer） join：  
--左外连接（左连接）：结果集几包括连接表的匹配行，也包括左连接表的所有行。 

select a.a, a.b, a.c , b.c, b.d. b.f from a LEFT OUT JOIN b on a.a = b.c 

--B：right （outer） join:  
--右外连接(右连接)：结 果集既包括连接表的匹配连接行，也包括右连接表的所有 行。

--C：full/cross （outer） join：  
--全外连接：不仅包括符号连接表的匹配行，还包括两个连接表中的所有记录。 

--分组 GROUP BY
--分组:Group by:   
--一张表，一旦分组 完成后，查询后只能得到组相关的信息。  
--组相关的信息：（统计信息） count,sum,max,min,avg  分组的标准)     
--在 SQLServer 中分组时：不能以 text,ntext,image 类型的字段作为分组依 据  
--在 selecte 统计函数中的字段，不能和普通的字段放在一起


--对数据库进行操作：  
--分离数据库： sp_detach_db; 
--附加数据库：sp_attach_db 后接表明，附加 需要完整的路径名 

--修改数据库的名称: 
sq_renamedb 'old_name', 'new_name'











