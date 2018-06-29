﻿/*
Deployment script for 101571963SwinnyVETDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar LoadTestData "true"
:setvar DatabaseName "101571963SwinnyVETDB"
:setvar DefaultFilePrefix "101571963SwinnyVETDB"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
PRINT N'Dropping [dbo].[CHK_PRICE]...';


GO
ALTER TABLE [dbo].[Procedure] DROP CONSTRAINT [CHK_PRICE];


GO
PRINT N'Creating [dbo].[FK_PET_TREATMENT]...';


GO
ALTER TABLE [dbo].[Treatment] WITH NOCHECK
    ADD CONSTRAINT [FK_PET_TREATMENT] FOREIGN KEY ([PetName], [OwnerID]) REFERENCES [dbo].[Pet] ([PetName], [OwnerID]);


GO
PRINT N'Creating [dbo].[FK_PROCEDURE_TREATMENT]...';


GO
ALTER TABLE [dbo].[Treatment] WITH NOCHECK
    ADD CONSTRAINT [FK_PROCEDURE_TREATMENT] FOREIGN KEY ([ProcedureID]) REFERENCES [dbo].[Procedure] ([ProcedureID]);


GO
PRINT N'Creating [dbo].[CHK_PRICE]...';


GO
ALTER TABLE [dbo].[Procedure] WITH NOCHECK
    ADD CONSTRAINT [CHK_PRICE] CHECK (ProcedurePrice > $0);


GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF '$(LoadTestData)' = 'true'

BEGIN

DELETE FROM Treatment;
DELETE FROM Pet;
DELETE FROM [Procedure];
DELETE FROM [Owner];

INSERT INTO [Owner](OwnerID, GivenName, Surname, PhoneNo) VALUES
('1','Frank','Sinatra','0400111222'),
('2','Duke','Ellington','0400222333'),
('3','Ella','Fitzgerald','0400333444');

INSERT INTO [Procedure](ProcedureID, ProcedureDesc, ProcedurePrice) VALUES
('1','Rabies Vaccination', $24.00),
('10','Examine and Treat Wound', $30.00),
('5','Heart Worm Test', $25.00),
('8','Tetnus Vaccination', $17.00);

INSERT INTO Pet(PetName, OwnerID, PetType) VALUES
('Buster', '1', 'Dog'),
('Fluffy', '1', 'Cat'),
('Stew', '2', 'Rabbit'),
('Buster', '3', 'Dog'),
('Pooper', '3', 'Dog');

INSERT INTO [Treatment](PetName, OwnerID, ProcedureID, TreatmentDate, TreatmentNotes, TreatmentPrice) VALUES
(
	'Buster', '1', '1', 
	CONVERT(DATE, '20/6/2017', 103),
	'Routine Vaccination', $22.00
),
(
	'Buster', '1', '1', 
	CONVERT(DATE, '15/5/2018', 103),
	'Booster Shot', $24.00
),
(
	'Fluffy', '1', '10', 
	CONVERT(DATE, '10/5/2018', 103),
	'Wounds sustained in apparent cat fight', $30.00
),
(
	'Stew', '2', '10', 
	CONVERT(DATE, '10/5/2018', 103),
	'Wounds sustained during attempt to cook the stew', $30.00
),
(
	'Pooper', '3', '5', 
	CONVERT(DATE, '18/5/2018', 103),
	'Routine Test', $25.00
);

END
GO

GO
