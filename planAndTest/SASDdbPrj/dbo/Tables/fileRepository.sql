CREATE TABLE [dbo].[fileRepository] (
    [fileRepositoryId]          UNIQUEIDENTIFIER CONSTRAINT [DF_fileRepository_fileRepositoryId] DEFAULT (newid()) NOT NULL,
    [createtime]                DATETIME         CONSTRAINT [DF_fileRepository_createtime] DEFAULT (getdate()) NOT NULL,
    [url]                       VARCHAR (99)     NOT NULL,
    [serverId]                  UNIQUEIDENTIFIER NULL,
    [externalSourceDescription] NVARCHAR (999)   NULL,
    CONSTRAINT [PK_fileRepository] PRIMARY KEY CLUSTERED ([fileRepositoryId] ASC)
);

