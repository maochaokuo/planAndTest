CREATE TABLE [dbo].[project] (
    [projectId]          UNIQUEIDENTIFIER CONSTRAINT [DF_project_projectId] DEFAULT (newid()) NOT NULL,
    [projectName]        NVARCHAR (99)    NULL,
    [createtime]         DATETIME         CONSTRAINT [DF_project_createtime] DEFAULT (getdate()) NOT NULL,
    [ownUserId]          VARCHAR (33)     NOT NULL,
    [projectDescription] NVARCHAR (999)   NULL,
    [deleteTime]         DATETIME2 (7)    NULL,
    [deleteBy]           VARCHAR (33)     NULL,
    CONSTRAINT [PK_project] PRIMARY KEY CLUSTERED ([projectId] ASC)
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_project]
    ON [dbo].[project]([projectName] ASC);



