drop database if exists biddings;
create database if not exists biddings;
use biddings;

drop table if exists bidding;
create table bidding
(
	biddingId 			int,
    processId 			varchar(17), -- max: 17
    objectName 			varchar(520), -- max: 517
    bidType 			int,
    bidState 			int,
    superiorAgencyCode 	int,
    superiorAgencyName 	varchar(45), -- max: 45 (Cap)
    agencyCode			int,
    agencyName			varchar(45), -- max: 45 (Cap)
    managementUnitCode 	int,
    managementUnitName 	varchar(45), -- max: 45 (Cap)
    city 				varchar(30), -- max: 28
    publicationDate 	date,
    openingDate 		date null,
    value 				float,
    primary key(biddingId, bidType, managementUnitCode)
) engine = InnoDB default charset=latin1;

