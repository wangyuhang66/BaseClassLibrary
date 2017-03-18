--按姓氏壁画排序
select * from tablename order by customerName collate chinese_PRC_Stroke_ci_as  --从少到多

--数据库加密
select encrypt('原始密码') 
select pwdencrypt('原始密码') 
select pwdcompare('原始密码','加密后密码') = 1--相同；否则不相同 encr ypt('原始密码') 
select pwdencrypt('原始密码') 
select pwdcompare('原始密码','加密后密码') = 1--相同；否则不相同 

--取回表中字段
declare @list varchar(1000),
@sql nvarchar(1000)
select @list=@list+','+b.name 
    from sysboject a, syscolumns b 
    where a.id = b.id anda.name= '表a'
set @sql = 'select ' + right(@list, len(@list) - 1)+'from表a'
exec (@sql)

--查看硬盘分区: 
EXEC master..xp_fixeddrives

--比较 A,B 表是否相等: 
if (select checksum_agg(binary_checksum(*)) from A)      
=     
(select checksum_agg(binary_checksum(*)) from B) 

print '相等' 
else 
print '不相等' 

--杀掉所有的事件探察器进程:
DECLARE hcforeach CURSOR GLOBAL FOR 
SELECT 'kill '+RTRIM(spid) FROM m aster.dbo.sysprocesses 
WHERE program_name IN('SQL profiler',N'SQL 事件探查器') 
EXEC sp_msforeach_worker '?' 

--=================================================================================================
--记录搜索: 开头到 N 条记录 
Select Top N * From 表 
------------------------------- N 到 M 条记录(要有主索引 ID) 
Select Top M-N * From 表 Where ID in (Select Top M ID From 表) Order by ID   Desc 
 11 
---------------------------------- N 到结尾记录 
Select Top N * From 表 Order by ID Desc

 --案例 例如 1：一张表有一万多条记录，表的第一个字段 RecID 是自增长字段， 写一个 SQL 语句，
 --找出表的第 31 到第 40 个记录。  
 select top 10 recid from A where recid not  in(select top 30 recid from A) 
 --分析：如果这样写会产生某些问题，如果 recid 在表中存在逻辑索引。  
 select top 10 recid from A where„„
 --是从索引中查找，而后面的 
 select top 30 recid from A 
 --则在数据表中查找，这样由于索引中的顺序有可能和数据表中的不一致，这样就导 致查询到的不是本来的欲得到的数据。 
 --解决方案 1， 用 
 order by select top 30 recid from A order by ricid 
 --如果该字段不是自 增长，就会出现问题 
 --2， 在那个子查询中也加条件：
 select top 30 recid from A where recid>-1 
 --例 2：查询表中的最后以条记录，并不知道这个表共有多少数据,以及表结构。 
 set @s = 'select top 1 * from T   where pid not in (select top ' + str(@count-1) + ' pid  from  T)' print @s      
 exec  sp_executesql  @s 

--=================================================================================================



--获取当前数据库中的所有用户表 
select Name from sysobjects where xtype='u' and status>=0 

--获取某一个表的所有字段 
select name from syscolumns where id=object_id('表名');
select name from syscolumns where id in (select id from sysobjects wh ere type = 'u' and name = '表名'); 

--查看与某一个表相关的视图、存储过程、函数 
select a.* from sysobjects a, syscomments b where a.id = b.id and b.t ext like '%表名%'

--查看当前数据库中所有存储过程 
 select name as 存储过程名称 from sysobjects where xtype='P' 

--查询用户创建的所有数据库 
select * from master..sysdatabases D where sid not in(select sid from master..syslogins where name='sa')
 --或者 
 select dbid, name AS DB_NAME from master..sysdatabases where sid <> 0 x01 

--查询某一个表的字段和数据类型 
select column_name,data_type from information_schema.columns where table_name = '表名'

 --不同服务器数据库之间的数据操作 
--创建链接服务器  
exec sp_addlinkedserver   'ITSV ', ' ', 'SQLOLEDB ', '远程服务器名或 ip 地址 '  
exec sp_addlinkedsrvlogin  'ITSV ', 'false ',null, '用户名 ', '密码 '  

--查询示例  
select * from ITSV.数据库名.dbo.表名  

--导入示例  
select * into 表 from ITSV.数据库名.dbo.表名  

--以后不再使用时删除链接服务器  
exec sp_dropserver  'ITSV ', 'droplogins '  
 
--连接远程/局域网数据(openrowset/openquery/opendatasource)  
--1、openrowset  
--查询示例  
select * from openrowset( 'SQLOLEDB ', 'sql 服务器名 '; '用户名 '; '密码 ',数据 库名.dbo.表名)  

--生成本地表  
select * into 表 from openrowset( 'SQLOLEDB ', 'sql 服务器名 '; '用户名 '; '密 码 ',数据库名.dbo.表名)  
 
--把本地表导入远程表  
insert openrowset( 'SQLOLEDB ', 'sql 服务器名 '; '用户名 '; '密码 ',数据库名.db o.表名)  
 13 

select *from 本地表  

--更新本地表  
update b  
set b.列 A=a.列 A  
 from openrowset( 'SQLOLEDB ', 'sql 服务器名 '; '用户名 '; '密码 ',数据库名.dbo. 表名)as a inner join 本地表 b  
on a.column1=b.column1  
--openquery 用法需要创建一个连接  
--首先创建一个连接创建链接服务器  
exec sp_addlinkedserver   'ITSV ', ' ', 'SQLOLEDB ', '远程服务器名或 ip 地址 '  

--查询  
select *  
FROM openquery(ITSV,  'SELECT *  FROM 数据库.dbo.表名 ')  

--把本地表导入远程表  
insert openquery(ITSV,  'SELECT *  FROM 数据库.dbo.表名 ')  
select * from 本地表  

--更新本地表  
update b  
set b.列 B=a.列 B  
FROM openquery(ITSV,  'SELECT * FROM 数据库.dbo.表名 ') as a   
inner join 本地表 b on a.列 A=b.列 A  
 
--3、opendatasource/openrowset  
SELECT   *  
 14 
FROM   opendatasource( 'SQLOLEDB ',  'Data Source=ip/ServerName;User ID=登陆名; Password=密码 ' ).test.dbo.roy_ta  
--把本地表导入远程表  
insert opendatasource( 'SQLOLEDB ',  'Data Source=ip/ServerName;User ID=登陆名; Password=密码 ').数据库.dbo.表名  
select * from 本地表 