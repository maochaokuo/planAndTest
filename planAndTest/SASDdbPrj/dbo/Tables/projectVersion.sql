CREATE TABLE [dbo].[projectVersion] (
    [projectVersionId]   INT              IDENTITY (1, 1) NOT NULL,
    [projectId]          UNIQUEIDENTIFIER NOT NULL,
    [version]            NVARCHAR (33)    NOT NULL,
    [versionDescription] NVARCHAR (99)    NULL,
    [createtime]         DATETIME         CONSTRAINT [DF_projectVersion_createtime] DEFAULT (getdate()) NOT NULL,
    [versionArticleId]   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_projectVersion] PRIMARY KEY CLUSTERED ([projectId] ASC, [version] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_projectVersion]
    ON [dbo].[projectVersion]([projectVersionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_projectVersion_1]
    ON [dbo].[projectVersion]([versionArticleId] ASC);

