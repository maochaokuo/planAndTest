CREATE TABLE [dbo].[articleRelation] (
    [articleId]                 UNIQUEIDENTIFIER NOT NULL,
    [relatedArticleId]          UNIQUEIDENTIFIER NOT NULL,
    [relationToOriginalArticle] NVARCHAR (99)    NULL,
    [createtime]                DATETIME         CONSTRAINT [DF_articleRelation_createtime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_articleRelation] PRIMARY KEY CLUSTERED ([articleId] ASC, [relatedArticleId] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_articleRelation_1]
    ON [dbo].[articleRelation]([relatedArticleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_articleRelation]
    ON [dbo].[articleRelation]([articleId] ASC);

