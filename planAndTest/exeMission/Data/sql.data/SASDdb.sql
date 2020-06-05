use SASDdb

select *
from article
where DATEDIFF(day, deleteTime, getdate()) >= 7