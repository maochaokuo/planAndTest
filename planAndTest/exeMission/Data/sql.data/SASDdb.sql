use SASDdb

select *
from systemGroup

/*
select *
from systems

SELECT 
    [Extent1].[systemGroupId] AS [systemGroupId], 
    [Extent1].[createtime] AS [createtime], 
    [Extent1].[systemGroupName] AS [systemGroupName], 
    [Extent1].[systemGroupDescription] AS [systemGroupDescription], 
    [Extent1].[projectId] AS [projectId]
    --,[Extent2].[projectName] AS [projectName]
    FROM  [dbo].[systemGroup] AS [Extent1]
    LEFT OUTER JOIN [dbo].[project] AS [Extent2] ON ([Extent2].[deleteTime] IS NULL) AND ([Extent1].[projectId] = [Extent2].[projectId])

select *
from projectVersion

select *
-- delete
from project
--where projectName='System Design Project'

select *
-- delete
from article
--where articleTitle = 'System Design Project'
order by createtime desc

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
