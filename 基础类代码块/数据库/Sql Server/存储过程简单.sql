
SET ANSI_NULLS ONGO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[proc_UpdateUserinfo]
@userid VARCHAR(50),
@userpwd VARCHAR(50),
@realname VARCHAR(20),
@school VARCHAR(50),
@province VARCHAR(20),
@city  VARCHAR(20),
@town VARCHAR(100),
@sex VARCHAR(10),
@mark VARCHAR(100),
@QQ  VARCHAR(50),
@phone VARCHAR(100)
AS
BEGIN
         DECLARE @COUNT INT
         DECLARE @StrSQL VARCHAR(2000)
         SET @COUNT = (SELECT COUNT(*) FROM tr_userinfo WHERE userid  = @userid )

        IF (@COUNT>0)
             BEGIN
            UPDATE tr_userinfo SET userpwd =@userpwd,realname=@realname,school=@school,province=@province ,city=@city,town=@town,sex=@sex,mark=@mark ,
    QQ=@QQ ,phone=@phone WHERE userid  = @userid
             END
        ELSE
            BEGIN
                INSERT INTO tr_userinfo(userid) VALUES (@userid)
                UPDATE tr_userinfo SET userpwd =@userpwd,realname=@realname,school=@school,province=@province ,city=@city,town=@town,sex=@sex,mark=@mark ,
    QQ=@QQ ,phone=@phone
            END
                 END

----------------------------------------------------------------------------------------------------------------
USE [XZJYJ]GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[proc_InsertUserinfo]
@userid VARCHAR(50),
@userpwd VARCHAR(50),
@realname VARCHAR(20),
@school VARCHAR(50),
@province VARCHAR(20),
@city  VARCHAR(20),
@town VARCHAR(100),
@sex VARCHAR(10),
@mark VARCHAR(100),
@QQ  VARCHAR(50),
@phone VARCHAR(100),
@COUNT  INT OUTPUT

AS
BEGIN

  SELECT @COUNT = COUNT (*)  FROM tr_userinfo  AS u  WHERE   u.userid=@userid
IF  @COUNT=0
BEGIN
 INSERT INTO tr_userinfo (userid ,userpwd,realname,school,province,city,town,sex,mark,QQ,phone)
VALUES(@userid,@userpwd,@realname,@school, @province,@city ,@town,@sex,@mark,@QQ ,@phone)
PRINT @COUNT
END
ELSE
BEGIN
 SET  @COUNT=1
END
END
