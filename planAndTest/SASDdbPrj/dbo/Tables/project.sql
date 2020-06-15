CREATE TABLE [dbo].[project] (
    [projectId]          UNIQUEIDENTIFIER CONSTRAINT [DF_project_projectId] DEFAULT (newid()) NOT NULL,
    [projectName]        NVARCHAR (99)    NULL,
    [createtime]         DATETIME         CONSTRAINT [DF_project_createtime] DEFAULT (getdate()) NOT NULL,
    [ownUserId]          VARCHAR (33)     NOT NULL,
    [projectDescription] NVARCHAR (999)   NULL,
    CONSTRAINT [PK_project] PRIMARY KEY CLUSTERED ([projectId] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_project]
    ON [dbo].[project]([ownUserId] ASC);

