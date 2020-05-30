CREATE TABLE [dbo].[article] (
    [articleId]            UNIQUEIDENTIFIER CONSTRAINT [DF_article_articleId] DEFAULT (newid()) NOT NULL,
    [createtime]           DATETIME         CONSTRAINT [DF_article_createtime] DEFAULT (getdate()) NOT NULL,
    [articleTitle]         NVARCHAR (999)   NULL,
    [articleHtmlContent]   NVARCHAR (MAX)   NULL,
    [articleContent]       NVARCHAR (MAX)   NULL,
    [isDir]                BIT              CONSTRAINT [DF_article_isDir] DEFAULT ((0)) NOT NULL,
    [belongToArticleDirId] UNIQUEIDENTIFIER NULL,
    [projectId]            UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_article] PRIMARY KEY CLUSTERED ([articleId] ASC)
);






GO
CREATE NONCLUSTERED INDEX [IX_article]
    ON [dbo].[article]([belongToArticleDirId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_article_1]
    ON [dbo].[article]([projectId] ASC);

