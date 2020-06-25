use SASDdb

select *
-- delete
from project
--where projectName='System Design Project'

select *
-- delete
from article
--where articleTitle = 'System Design Project'
order by createtime desc

/*
select *
from projectVersion

select *
from [user]

select *
--update article set priority=9
--update article set articleType='General'
--update article set articleStatus='New'
-- delete
from article
--where belongToArticleDirId='00000000-0000-0000-0000-000000000000'
--where DATEDIFF(day, deleteTime, getdate()) >= 7
where deleteTime is null
order by createtime desc

select *
-- delete
from article
where articleId='00000000-0000-0000-0000-000000000000'
*/
