CREATE TABLE [dbo].[systemInterface] (
    [systemInterfaceId]    UNIQUEIDENTIFIER NOT NULL,
    [systemTemplateId]     INT              NULL,
    [createtime]           DATETIME         CONSTRAINT [DF_systemInterface_createtime] DEFAULT (getdate()) NOT NULL,
    [interfaceName]        NVARCHAR (33)    NOT NULL,
    [interfaceDescription] NVARCHAR (999)   NULL,
    [namespace]            VARCHAR (99)     NULL,
    [moduleOrProject]      VARCHAR (99)     NULL,
    CONSTRAINT [PK_systemInterface] PRIMARY KEY CLUSTERED ([systemInterfaceId] ASC)
);

