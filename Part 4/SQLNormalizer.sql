-- SQL script to convert from denormalized database (biddings) to fully normalized (FN3) database (biddingsFN3)

-- Select distinct superior agencies
insert into SuperiorAgency (superiorAgencyCode, superiorAgencyName)
select distinct superiorAgencyCode, superiorAgencyName
from bidding;

-- Select distinct agencies while also refering a superior agency
insert into Agency (agencyCode, agencyName, superiorAgencyCode)
select distinct agencyCode, agencyName, superiorAgencyCode
from bidding;

-- Select distinct management units while also refering an agency
insert into ManagementUnit (managementUnitCode, managementUnitName, agencyCode)
select distinct managementUnitCode, managementUnitName, agencyCode
from bidding;

-- Create Bidding (FN3) table (horizontal cut)
-- Must preserve columns (biddingId, processId, object, biddingType, biddingStatus, managementUnitCode, publicationDate, openingDate, value)
insert into FN3Bidding (biddingId, processId, objectName, bidType, bidState, managementUnitCode, publicationDate, openingDate, value)
select biddingId, processId, objectName, bidType, bidState, managementUnitCode, publicationDate, openingDate, value
from bidding