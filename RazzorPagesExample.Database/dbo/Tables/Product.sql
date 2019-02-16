CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	 Name            NVarChar(25) Not Null,  
	 Model	         NVarChar(50) Not Null,
	 Price	         Int Default 0

)
