-- Number Of Biddings Per UG (displaying UG name, agency name and superior agency name)
select ug.managementUnitCode, ug.managementUnitName, count(*) as cnt
from FN3Bidding bid
natural join managementUnit ug
group by bid.managementUnitCode
order by cnt desc