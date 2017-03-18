1.C#连接连接Access程序代码: 

------------------------------------------------------------------------------- 
using System.Data;
using System.Data.OleDb;

......

string strConnection="Provider=Microsoft.Jet.OleDb.4.0;";
strConnection+=@"Data Source=C:\BegASPNET\Northwind.mdb";

OleDbConnection objConnection=new OleDbConnection(strConnection);

......

objConnection.Open();
objConnection.Close();

......

--------------------------------------------------------------------------------

解释:

　连接Access数据库需要导入额外的命名空间,所以有了最前面的两条using命令,这是必不可少的!

　strConnection这个变量里存放的是连接数据库所需要的连接字符串,他指定了要使用的数据提供者和要使用的数据源.

　"Provider=Microsoft.Jet.OleDb.4.0;"是指数据提供者,这里使用的是Microsoft Jet引擎,也就是Access中的数据引擎,asp.net就是靠这个和Access的数据库连接的.

　"Data Source=C:\BegASPNET\Northwind.mdb"是指明数据源的位置,他的标准形式是"Data Source=MyDrive:MyPath\MyFile.MDB".

PS:
　1."+="后面的"@"符号是防止将后面字符串中的"\"解析为转义字符.
　2.如果要连接的数据库文件和当前文件在同一个目录下,还可以使用如下的方法连接:
　　strConnection+="Data Source=";
　　strConnection+=MapPath("Northwind.mdb");
　　这样就可以省得你写一大堆东西了!
　3.要注意连接字符串中的参数之间要用分号来分隔.

　"OleDbConnection objConnection=new OleDbConnection(strConnection);"这一句是利用定义好的连接字符串来建立了一个链接对象,以后对数据库的操作我们都要和这个对象打交道.

　"objConnection.Open();"这用来打开连接.至此,与Access数据库的连接完成.

--------------------------------------------------------------------------------

2.C#连接SQL Server程序代码: 

--------------------------------------------------------------------------------

using System.Data;
using System.Data.SqlClient;

...

string strConnection="user id=sa;password=;";
strConnection+="initial catalog=Northwind;Server=YourSQLServer;";
strConnection+="Connect Timeout=30";

SqlConnection objConnection=new SqlConnection(strConnection);

...

objConnection.Open();
objConnection.Close();

...

--------------------------------------------------------------------------------

解释:

连接SQL Server数据库的机制与连接Access的机制没有什么太大的区别,只是改变了Connection对象和连接字符串中的不同参数.

首先,连接SQL Server使用的命名空间不是"System.Data.OleDb",而是"System.Data.SqlClient".

其次就是他的连接字符串了,我们一个一个参数来介绍(注意:参数间用分号分隔):
　"user id=sa":连接数据库的验证用户名为sa.他还有一个别名"uid",所以这句我们还可以写成"uid=sa".
　"password=":连接数据库的验证密码为空.他的别名为"pwd",所以我们可以写为"pwd=".
　这里注意,你的SQL Server必须已经设置了需要用户名和密码来登录,否则不能用这样的方式来登录.如果你的SQL Server设置为Windows登录,那么在这里就不需要使用"user id"和"password"这样的方式来登录,而需要使用"Trusted_Connection=SSPI"来进行登录.
　"initial catalog=Northwind":使用的数据源为"Northwind"这个数据库.他的别名为"Database",本句可以写成"Database=Northwind".
　"Server=YourSQLServer":使用名为"YourSQLServer"的服务器.他的别名为"Data Source","Address","Addr".如果使用的是本地数据库且定义了实例名,则可以写为"Server=(local)\实例名";如果是远程服务器,则将"(local)"替换为远程服务器的名称或IP地址.
　"Connect Timeout=30":连接超时时间为30秒.

　在这里,建立连接对象用的构造函数为:SqlConnection.

--------------------------------------------------------------------------------

3.C#连接Oracle程序代码: 

--------------------------------------------------------------------------------

using System.Data.OracleClient;
using System.Data;

//在窗体上添加一个按钮，叫Button1，双击Button1，输入以下代码
private void Button1_Click(object sender, System.EventArgs e)
{
string ConnectionString="Data Source=sky;user=system;password=manager;";//写连接串
OracleConnection conn=new OracleConnection(ConnectionString);//创建一个新连接
try
{
conn.Open();
OracleCommand cmd=conn.CreateCommand();

cmd.CommandText="select * from MyTable";//在这儿写sql语句
OracleDataReader odr=cmd.ExecuteReader();//创建一个OracleDateReader对象
while(odr.Read())//读取数据，如果odr.Read()返回为false的话，就说明到记录集的尾部了                
{
Response.Write(odr.GetOracleString(1).ToString());//输出字段1，这个数是字段索引，具体怎么使用字段名还有待研究
}
odr.Close();
}
catch(Exception ee)
{
Response.Write(ee.Message); //如果有错误，输出错误信息
}
finally
{
conn.Close(); //关闭连接
}
}

--------------------------------------------------------------------------------

4.C#连接MySQL程序代码: 

--------------------------------------------------------------------------------

using MySQLDriverCS; 

// 建立数据库连接
MySQLConnection DBConn;
DBConn = new MySQLConnection(new MySQLConnectionString("localhost","mysql","root","",3306).AsString);
DBConn.Open(); 

// 执行查询语句
MySQLCommand DBComm;
DBComm = new MySQLCommand("select Host,User from user",DBConn); 

// 读取数据
MySQLDataReader DBReader = DBComm.ExecuteReaderEx(); 

// 显示数据
try
{
while (DBReader.Read())
{
Console.WriteLine("Host = {0} and User = {1}", DBReader.GetString(0),DBReader.GetString(1));
}
}
finally
{
DBReader.Close();
DBConn.Close();
} 

//关闭数据库连接
DBConn.Close();

--------------------------------------------------------------------------------

5.C#连接IBM DB2程序代码: 

--------------------------------------------------------------------------------

OleDbConnection1.Open();
//打开数据库连接
OleDbDataAdapter1.Fill(dataSet1,"Address");
//将得来的数据填入dataSet
DataGrid1.DataBind();
//绑定数据
OleDbConnection1.Close();
//关闭连接 

//增加数据库数据
在Web Form上新增对应字段数量个数的TextBox，及一个button，为该按键增加Click响应事件代码如下：

this.OleDbInsertCommand1.CommandText = "INSERTsintosADDRESS(NAME,
EMAIL, AGE, ADDRESS) VALUES
('"+TextBox1.Text+"','"+TextBox2.Text+"','"+TextBox3.Text+"','"+TextBox4.Text+"')";
OleDbInsertCommand1.Connection.Open();
//打开连接
OleDbInsertCommand1.ExecuteNonQuery();
//执行该SQL语句
OleDbInsertCommand1.Connection.Close();
//关闭连接 

--------------------------------------------------------------------------------

6.C#连接SyBase程序代码: (OleDb)

--------------------------------------------------------------------------------

Provider=Sybase.ASEOLEDBProvider.2;Initial Catalog=数据库名;User ID=用户名;Data Source=数据源;Extended Properties="";Server Name=ip地址;Network Protocol=Winsock;Server Port Address=5000;

--------------------------------------------------------------------------------


