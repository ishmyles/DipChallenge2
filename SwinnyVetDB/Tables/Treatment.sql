﻿CREATE TABLE [dbo].[Treatment]
(
	[PetName] NVARCHAR(15) NOT NULL,
	[OwnerID] NVARCHAR(4) NOT NULL,
	[ProcedureID] NVARCHAR(3) NOT NULL,
	[TreatmentDate] DATE NOT NULL,
	[TreatmentNotes] NVARCHAR(50) NOT NULL,
	[TreatmentPrice] MONEY
	CONSTRAINT PK_TREATMENT PRIMARY KEY (PetName, OwnerID, ProcedureID, TreatmentDate)
	CONSTRAINT FK_PET_TREATMENT FOREIGN KEY (PetName, OwnerID) REFERENCES Pet(PetName, OwnerID),
	CONSTRAINT FK_PROCEDURE_TREATMENT FOREIGN KEY (ProcedureID) REFERENCES [Procedure](ProcedureID)
)
