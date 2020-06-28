CREATE TABLE [dbo].[db] (
    [dbId]                INT            IDENTITY (1, 1) NOT NULL,
    [databaseName]        VARCHAR (33)   NOT NULL,
    [databaseLocation]    NVARCHAR (99)  NULL,
    [createtime]          DATETIME       CONSTRAINT [DF_db_createtime] DEFAULT (getdate()) NOT NULL,
    [databaseDescription] NVARCHAR (999) NULL,
    CONSTRAINT [PK_db] PRIMARY KEY CLUSTERED ([dbId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_db]
    ON [dbo].[db]([databaseName] ASC);

