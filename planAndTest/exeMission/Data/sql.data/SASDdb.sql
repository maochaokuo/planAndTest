use SASDdb

select *
--update article set priority=9
--update article set articleType='General'
--update article set articleStatus='New'
-- delete
from article
--where belongToArticleDirId='00000000-0000-0000-0000-000000000000'
--where DATEDIFF(day, deleteTime, getdate()) >= 7
order by createtime desc

/*
select *
-- delete
from article
where articleId='00000000-0000-0000-0000-000000000000'
*/
