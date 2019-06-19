-- R$ per city
select ug.city, sum(bid.value) as R$
from FN3Bidding bid
natural join managementUnit ug
group by ug.city
order by R$ desc;