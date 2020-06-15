CREATE TABLE [dbo].[entityClassVariable] (
    [entityClassVariableId] INT          IDENTITY (1, 1) NOT NULL,
    [entityClassId]         INT          NULL,
    [createtime]            DATETIME     CONSTRAINT [DF_entityClassVariable_createtime] DEFAULT (getdate()) NOT NULL,
    [variableName]          VARCHAR (33) NOT NULL,
    [variableType]          VARCHAR (33) NOT NULL,
    CONSTRAINT [PK_entityClassVariable] PRIMARY KEY CLUSTERED ([entityClassVariableId] ASC)
);

