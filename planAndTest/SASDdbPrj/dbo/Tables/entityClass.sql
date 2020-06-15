CREATE TABLE [dbo].[entityClass] (
    [entityClassId]    INT              IDENTITY (1, 1) NOT NULL,
    [createtime]       DATETIME         CONSTRAINT [DF_entityClass_createtime] DEFAULT (getdate()) NOT NULL,
    [className]        VARCHAR (33)     NOT NULL,
    [classDescription] NVARCHAR (999)   NULL,
    [namespace]        VARCHAR (99)     NULL,
    [moduleOrProject]  VARCHAR (99)     NULL,
    [stateMachineId]   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_entityClass] PRIMARY KEY CLUSTERED ([entityClassId] ASC)
);



