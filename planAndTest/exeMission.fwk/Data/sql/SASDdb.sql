use SASDdb

select *
from article
order by createtime desc
--where DATEDIFF(day, deleteTime, getdate()) >= 7