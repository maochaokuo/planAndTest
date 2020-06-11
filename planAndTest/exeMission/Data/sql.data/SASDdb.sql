use SASDdb

select *
from article
order by createtime desc
--where DATEDIFF(day, deleteTime, getdate()) >= 7

/*
select *
-- delete
from article
where articleId='00000000-0000-0000-0000-000000000000'
*/
