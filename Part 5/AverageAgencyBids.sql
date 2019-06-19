-- Superior Agencies which bid more than R$100,000,000
select agency.agencyName, avg(FN3Bidding.value) as average
from FN3Bidding
natural join managementUnit
natural join agency
group by agency.agencyCode
order by average desc;

