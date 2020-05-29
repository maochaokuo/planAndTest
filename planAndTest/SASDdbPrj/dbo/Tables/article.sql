CREATE TABLE [dbo].[article] (
    [articleId]            UNIQUEIDENTIFIER CONSTRAINT [DF_article_articleId] DEFAULT (newid()) NOT NULL,
    [createtime]           DATETIME         CONSTRAINT [DF_article_createtime] DEFAULT (getdate()) NOT NULL,
    [articleTitle]         NVARCHAR (999)   NULL,
    [articleContent]       NVARCHAR (MAX)   NULL,
    [isDir]                BIT              CONSTRAINT [DF_article_isDir] DEFAULT ((0)) NOT NULL,
    [belongToArticleDirId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_article] PRIMARY KEY CLUSTERED ([articleId] ASC)
);

