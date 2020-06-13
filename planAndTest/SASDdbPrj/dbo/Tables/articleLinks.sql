CREATE TABLE [dbo].[articleLinks] (
    [articleId]  UNIQUEIDENTIFIER NOT NULL,
    [linkurl]    VARCHAR (900)    NOT NULL,
    [createtime] DATETIME         CONSTRAINT [DF_articleLinks_createtime] DEFAULT (getdate()) NOT NULL,
    [linkDesc]   VARCHAR (999)    NULL,
    [linkType]   NVARCHAR (99)    NULL,
    CONSTRAINT [PK_articleLinks] PRIMARY KEY CLUSTERED ([linkurl] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_articleLinks]
    ON [dbo].[articleLinks]([articleId] ASC);

