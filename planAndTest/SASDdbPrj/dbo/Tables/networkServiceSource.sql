CREATE TABLE [dbo].[networkServiceSource] (
    [networkServiceSourceId]    UNIQUEIDENTIFIER CONSTRAINT [DF_networkServiceSource_networkServiceSourceId] DEFAULT (newid()) NOT NULL,
    [createtime]                DATETIME         CONSTRAINT [DF_networkServiceSource_createtime] DEFAULT (getdate()) NOT NULL,
    [serverId]                  UNIQUEIDENTIFIER NULL,
    [externalSourceDescription] NVARCHAR (999)   NULL,
    CONSTRAINT [PK_networkServiceSource] PRIMARY KEY CLUSTERED ([networkServiceSourceId] ASC)
);

