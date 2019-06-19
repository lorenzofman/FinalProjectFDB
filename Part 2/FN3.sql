create table SuperiorAgency
(	
    superiorAgencyCode 	int,
    superiorAgencyName 	varchar(45), 	-- max: 45 (Cap)
    primary key(superiorAgencyCode)
);

create table Agency
(	
	agencyCode			int,
    agencyName			varchar(45), 	-- max: 45 (Cap)
    superiorAgencyCode 	int,
    primary key(agencyCode),
    foreign key(superiorAgencyCode) references SuperiorAgency(superiorAgencyCode)
);

create table ManagementUnit
(	
	managementUnitCode 	int,
    managementUnitName 	varchar(45), 	-- max: 45 (Cap)
	agencyCode			int,
    city 				varchar(30),	-- max: 28
    primary key(managementUnitCode),
    foreign key(agencyCode) references Agency(agencyCode)
);

create table FN3Bidding
(
	biddingId 			int,
    processId 			varchar(20), 	-- max: 17
    objectName 			varchar(520), 	-- max: 517
    bidType 			ENUM(
		'Inexigibilidade de Licitação', 
		'Dispensa de Licitação', 
		'Pregão - Registro de Preço', 
		'Pregão',
        'Concorrência',
        'Tomada de Preços',
        'Convite',
        'Concorrência - Registro de Preço',
        'Concurso'
    ),
    bidState 			ENUM(
		'Encerrado',
		'Publicado',
		'Evento de Suspensão Divulgado',
		'Evento de Revogação Publicado',
		'Divulgado',
		'Evento de Resultado de Julgame',
		'Evento de Retificação Publicad',
		'Evento de Suspensão Publicado',
		'Evento de Anulação Publicado',
		'Evento de Alteração Divulgado',
		'Revogação',
		'Evento de Reabertura de Prazo ',
		'Evento de Adiamento Divulgado',
		'Evento de Retificação Divulgad',
		'Evento de Alteração Publicado',
		'Evento de Adiamento Publicado',
		'Retificação',
		'Evento de Revogação Divulgado',
		'Evento de Habilitação Publicad',
		'Anulação',
		'Inválido',
		'Evento de Alteração de Resulta',
        'Evento de Anulação Divulgado',
		'Evento de Revogação Devolvido'
    ),
    managementUnitCode 	int,
    publicationDate 	date,
    openingDate 		date null,
    value 				decimal,
    primary key(biddingId, bidType, managementUnitCode),
    foreign key(managementUnitCode) references ManagementUnit(managementUnitCode)
) engine = InnoDB default charset=latin1;

