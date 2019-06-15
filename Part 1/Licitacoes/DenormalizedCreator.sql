drop database if exists biddings;
create database if not exists biddings;
use biddings;

drop table if exists bidding;
create table bidding
(
	biddingId int,
    processId int,
    objectName varchar(200),
    bidType int,
    bidState int,
    superiorAgencyCode int,
    superiorAgencyName varchar(200),
    codeUG int,
    nameUG varchar(200),
    city varchar(200),
    publicationDate date,
    openingDate date,
    value float,
    primary key(biddingId)
) engine = InnoDB default charset=latin1;

