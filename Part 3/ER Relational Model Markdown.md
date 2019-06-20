SuperiorAgency (<u>superiorAgencyCode</u>, superiorAgencyName)

Agency (<u>agencyCode</u>, agencyName, superiorAgencyCode)
	superiorAgencyCode references SuperiorAgency

ManagementUnit (<u>managementUnitCode</u>, managementUnitName, agencyCode, city)
	agencyCode references Agency

Bidding (<u>biddingId</u>, processId, objectName, <u>bidType</u>, bidState, <u>managementUnitCode</u>, publicationDate, openingDate, value)	
    managementUnitCode references ManagementUnit