﻿CREATE TABLE [dbo].[Owner]
(
	[OwnerID] NVARCHAR(4) NOT NULL,
	[GivenName] NVARCHAR(15) NOT NULL,
	[Surname] NVARCHAR(15) NOT NULL,
	[PhoneNo] NVARCHAR(10) NOT NULL
	CONSTRAINT PK_OWNER PRIMARY KEY (OwnerID)
)
