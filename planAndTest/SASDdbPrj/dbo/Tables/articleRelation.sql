CREATE TABLE [dbo].[articleRelation] (
    [articleId]                 UNIQUEIDENTIFIER NOT NULL,
    [relatedArticleId]          UNIQUEIDENTIFIER NOT NULL,
    [relationToOriginalArticle] NVARCHAR (99)    NULL,
    [createtime]                DATETIME         CONSTRAINT [DF_articleRelation_createtime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_articleRelation] PRIMARY KEY CLUSTERED ([articleId] ASC, [relatedArticleId] ASC)
);

