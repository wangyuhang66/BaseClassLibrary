--1=1，1=2 的使用，在 SQL 语句组合时用的较多
--“where 1=1” 是表示选择全部    “where 1=2”全部不选， 
--如： 
if @strWhere !=''  
begin 
set @strSQL = 'select count(*) as Total from [' + @tblName + '] where ' + @strWhere  
end else  
begin 
set @strSQL = 'select count(*) as Total from [' + @tblName + ']'  
end 

--我们可以直接写成 
--错误！未找到目录项。 
set @strSQL = 'select count(*) as Total from [' + @tblName + '] where 1=1 安定 '+ @strWhere 
--2、收缩数据库 --重建索引 
DBCC REINDEX DBCC INDEXDEFRAG 
--收缩数据和日志 
DBCC SHRINKDB DBCC SHRINKFILE 

--压缩数据库
DBCC shrinkdatabase(dbname)

--转移数据库给新用户已存在用户圈权限
exec sp_change_users_login 'update_one', 'newname', 'oldname' go

--检查备份集
restore verifyonly from disk = 'E:\dvbbs.bak';

--修复数据库
Alter Database [dvbbs] set single_user
go
dbcc checkdb('dvbbs', repair_allow_data_loss) with tabllock
go
alter Database [dvbbs] set null_user
go

--日志清除
set nocount on
declare @logicalFilename sysname,
 @MaxMinutes INT,
 @NewSize INT


 use tablename --要操作的数据库名
 select @logicalFilename = 'tablename_log', --日志文件
 @MaxMinutes = 10, --Limit on time allowed to wrap log,
    @NewSize = 1 --你想设定的日志文件的大小（M）

Setup / initialize 
DECLARE @OriginalSize int 
SELECT @OriginalSize = size   
    FROM sysfiles 
     WHERE name = @LogicalFileName 
     SELECT 'Original Size of ' + db_name() + ' LOG is ' +   
     CONVERT(VARCHAR(30),@OriginalSize) + ' 8K pages or ' 
     +   CONVERT(VARCHAR(30),(@OriginalSize*8/1024)) + 'MB'  
     FROM sysfiles  
     WHERE name = @LogicalFileName 
     CREATE TABLE DummyTrans  
     (DummyColumn char (8000) not null) 
 
DECLARE @Counter    INT,  
    @StartTime DATETIME, 
    @TruncLog   VARCHAR(255) 
    SELECT @StartTime = GETDATE(),  
    @TruncLog = 'BACKUP LOG ' + db_name() + ' WITH TRUNCATE_ONLY'

DBCC SHRINKFILE (@LogicalFileName, @NewSize) 
EXEC (@TruncLog) -- Wrap the log if necessary. 
WHILE @MaxMinutes > DATEDIFF (mi, @StartTime, GETDATE()) -- time has not expired  
    AND @OriginalSize = (SELECT size FROM sysfiles WHERE name = @Logical FileName)    
    AND (@OriginalSize * 8 /1024) > @NewSize    
    BEGIN -- Outer loop. 
    SELECT @Counter = 0  
    WHILE   ((@Counter < @OriginalSize / 16) 
    AND (@Counter < 50000))  
    BEGIN -- update  
    INSERT DummyTrans VALUES ('Fill Log') DELETE DummyTrans 

SELECT @Counter = @Counter + 1  
END  
EXEC (@TruncLog)    
END 
SELECT 'Final Size of ' + db_name() + ' LOG is ' +  
CONVERT(VARCHAR(30),size) + ' 8K pages or ' +   
CONVERT(VARCHAR(30),(size*8/1024)) + 'MB'  
FROM sysfiles   
WHERE name = @LogicalFileName 
DROP TABLE DummyTrans 
SET NOCOUNT OF

--更改某个表
exec sp_changeobjectowner 'tablename', 'dbo'

--存储更改全部表

CREATE PROCEDURE dbo.User_ChangeObjectOwnerBatch 
@OldOwner as NVARCHAR(128), 
@NewOwner as NVARCHAR(128) 
AS 
DECLARE @Name as NVARCHAR(128) 
DECLARE @Owner as NVARCHAR(128) 
DECLARE @OwnerName   as NVARCHAR(128) 

DECLARE curObject CURSOR FOR  
select 'Name' = name, 'Owner' = user_name(uid) 
from sysobjects 
where user_name(uid)=@OldOwner 
order by name 

OPEN curObject 
FETCH NEXT FROM curObject INTO @Name, @Owner 
WHILE(@@FETCH_STATUS=0) 
BEGIN      
if @Owner=@OldOwner  
begin    
set @OwnerName = @OldOwner + '.' + rtrim(@Name)    exec sp_changeobjectowner @OwnerName, @NewOwner 
end -- select @name,@NewOwner,@OldOwner 
 9 
FETCH NEXT 
FROM curObject INTO @Name, @Owner 
END 

close curObject 
deallocate curObject 
GO 

--SQL SERVER 直接循环写入数据

declare @i int

set @i=1

WHILE @i<30 

begin 
    insert into test (userid) values(@i)

    set @i=@i+1
end

--案例： 
--有如下表，要求就裱中所有沒有及格的成績，在每次增長 0.1 的基礎上，使他們剛好及格: 
--Name     score  
--Zhangshan  80  
--Lishi      59 
--Wangwu     50  
--Songquan   69 

WHILE((select min(score) from tb_table) < 60)
begin
upadte tb_table set score = score * 1.01
where score < 60
if (select min(score) from tb_table) > 60
    break
else
    continue
end

