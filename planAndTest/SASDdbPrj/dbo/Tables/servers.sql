CREATE TABLE [dbo].[servers] (
    [serverId]          UNIQUEIDENTIFIER NOT NULL,
    [createtime]        DATETIME         CONSTRAINT [DF_servers_createtime] DEFAULT (getdate()) NOT NULL,
    [hostnameOrIP]      VARCHAR (33)     NOT NULL,
    [serverDescription] NVARCHAR (999)   NULL,
    CONSTRAINT [PK_servers] PRIMARY KEY CLUSTERED ([serverId] ASC)
);

