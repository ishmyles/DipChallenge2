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