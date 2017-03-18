1: select top 10 * from A where id not in (select top 30 id from A) 

select top 10 * from A where id not in (select top 30 id from A)
演变步骤：
1）select top 30 id from T_FilterWords--取前条

2）select * from T_FilterWords 
where id not in (select top 30 id from T_FilterWords)--取id不等于前三十条的
--也就是把前条排除在外

3）select top 10 * from T_FilterWords 
where id not in (select top 30 id from T_FilterWords)
--取把前条排除在外的前条，也就是-40条

解2: select top 10 * from A where id > (select max(id) from (select top 30 id from A )as A) 

select top 10 * from A where id > (select max(id) from (select top 30 id from A) as A)

解答3：用ROW_NUMBER实现