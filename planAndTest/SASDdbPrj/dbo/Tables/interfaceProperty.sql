CREATE TABLE [dbo].[interfaceProperty] (
    [interfacePropertyId] INT              IDENTITY (1, 1) NOT NULL,
    [systemInterfaceId]   UNIQUEIDENTIFIER NOT NULL,
    [propertyName]        VARCHAR (33)     NOT NULL,
    [propertyValue]       NVARCHAR (99)    NULL,
    [propertyDescription] NVARCHAR (999)   NULL,
    CONSTRAINT [PK_interfaceProperty] PRIMARY KEY CLUSTERED ([systemInterfaceId] ASC, [propertyName] ASC)
);

