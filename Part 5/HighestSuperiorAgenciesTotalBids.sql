-- Superior Agencies which bid more than R$100,000,000
select superiorAgency.superiorAgencyName, sum(FN3Bidding.value) as R$
from FN3Bidding
natural join managementUnit
natural join agency
natural join superiorAgency
group by superiorAgency.superiorAgencyCode
having R$ > 100000000
order by R$ desc;

