--SQL Server 基本函数 
--字符串函数 长度与分析用  

datalength(Char_expr) 
--返回字符串包含字符数,但不包含后面的空格 


substring(expression,start,length) 
--取子串，字符串的下标是从“1”，
--start 为起始位置，length 为字符串长度，实际应用中以 len(expression)取得 其长度 


right(char_expr,int_expr) 
--返回字符串右边第 int_expr 个字符，还用 le
--ft 于之相反 


isnull( check _ expression , replacem ent_value )
--如果 check_expression 為空，則返回 replacement_value 的值，不為空，就返回 check_expression 字符操作类  


Sp_addtype 自定義數據類型 例如：EXEC sp_addtype birthday, datetime, 'NULL' 

--set nocount {on|off} 使返回的结果中不包含有关受 Transact-SQL 语句影响的行数的信息。如果存储 过程中包含的一些语句并不返回许多实际的数据，则该设置由于大量减少了网络 流量，因此可显著提高性能。SET NOCOUNT 设置是在执行或运行时设置，而不是 在分析时设置。 SET NOCOUNT 为 ON 时，不返回计数（表示受 Transact-SQL 语句影响的行数）

SET NOCOUNT 为 OFF 时，返回计数 