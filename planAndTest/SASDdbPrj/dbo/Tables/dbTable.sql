CREATE TABLE [dbo].[dbTable] (
    [dbTableId]        INT            IDENTITY (1, 1) NOT NULL,
    [databaseName]     VARCHAR (33)   NOT NULL,
    [tableName]        VARCHAR (33)   NOT NULL,
    [createtime]       DATETIME       CONSTRAINT [DF_dbTable_createtime] DEFAULT (getdate()) NOT NULL,
    [tableDescription] NVARCHAR (999) NULL,
    CONSTRAINT [PK_dbTable] PRIMARY KEY CLUSTERED ([databaseName] ASC, [tableName] ASC)
);

