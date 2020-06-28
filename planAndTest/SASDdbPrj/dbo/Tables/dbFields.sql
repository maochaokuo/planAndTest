CREATE TABLE [dbo].[dbFields] (
    [dbFieldId]        INT            IDENTITY (1, 1) NOT NULL,
    [databaseName]     VARCHAR (33)   NOT NULL,
    [tableName]        VARCHAR (33)   NOT NULL,
    [fieldName]        VARCHAR (33)   NOT NULL,
    [createtime]       DATETIME       CONSTRAINT [DF_dbFields_createtime] DEFAULT (getdate()) NOT NULL,
    [fieldDescription] NVARCHAR (999) NULL,
    [primaryKeyOrder]  SMALLINT       CONSTRAINT [DF_dbFields_primaryKeyOrder] DEFAULT ((0)) NOT NULL,
    [fieldType]        VARCHAR (33)   CONSTRAINT [DF_dbFields_fieldType] DEFAULT ('') NOT NULL,
    [nullable]         BIT            CONSTRAINT [DF_dbFields_nullable] DEFAULT ((1)) NOT NULL,
    [maxlength]        SMALLINT       NULL,
    CONSTRAINT [PK_dbFields] PRIMARY KEY CLUSTERED ([databaseName] ASC, [tableName] ASC, [fieldName] ASC)
);

