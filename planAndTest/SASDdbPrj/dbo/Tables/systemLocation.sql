CREATE TABLE [dbo].[systemLocation] (
    [systemLocationId]      INT              IDENTITY (1, 1) NOT NULL,
    [serverId]              UNIQUEIDENTIFIER NOT NULL,
    [systemId]              UNIQUEIDENTIFIER NOT NULL,
    [systemCopyDescription] NVARCHAR (999)   NULL,
    [createtime]            DATETIME         CONSTRAINT [DF_systemLocation_createtime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_systemLocation] PRIMARY KEY CLUSTERED ([systemLocationId] ASC)
);

