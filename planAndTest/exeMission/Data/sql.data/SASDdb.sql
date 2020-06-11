use SASDdb

select *
--update article set priority=9
--update article set articleType='GENERAL'
--update article set articleStatus='NEW'
from article
order by createtime desc
--where DATEDIFF(day, deleteTime, getdate()) >= 7

/*
select *
-- delete
from article
where articleId='00000000-0000-0000-0000-000000000000'
*/
