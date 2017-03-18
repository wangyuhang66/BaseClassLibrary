frist:  id  name   cors   class   keming
second: id  class


--每门课的总成绩，平均成绩
SELECT
	keming AS '课程名字',
	sum(cors) AS '总成绩',
	avg(cors) AS '平均成绩'
FROM
	frist
GROUP BY
	keming;

--每个人的总成绩，平均成绩，最高分
SELECT
	NAME AS '姓名',
	avg(cors) AS '平均成绩',
	sum(cors) AS '总成绩' ,
    max(cors) AS '最高分'
FROM
	frist
GROUP BY
	NAME;

--查询每个人所在的班级
SELECT
	f. NAME AS '姓名',
	s.class AS '班级'
FROM
	frist AS f
LEFT JOIN SECOND AS s ON f.class = s.id;

--每个人的总成绩，平均成绩，最高分 所在班级
SELECT
	f. NAME AS '姓名',
	avg(f.cors) AS '平均成绩',
	sum(f.cors) AS '总成绩',
	max(f.cors) AS '最高分',
	s.class AS '班级'
FROM
	frist AS f
LEFT JOIN SECOND AS s ON f.class = s.id
GROUP BY
	f. NAME,
	s.class;
--每个班的总成绩  平均成绩
SELECT
	avg(f.cors) AS '平均成绩',
	sum(f.cors) AS '总成绩',
	max(f.cors) AS '最高分',
	s.class AS '班级'
FROM
	frist AS f
LEFT JOIN SECOND AS s ON f.class = s.id
GROUP BY
	f.cors,
	s.class;、
--每个班的最高成绩是谁
-----------------------------------------------------------------------------------
insert into frist (id, name)  values ('wyh');
insert into frist(name) select name from second;
 
update frist set name = 'wyh' where id = 1;