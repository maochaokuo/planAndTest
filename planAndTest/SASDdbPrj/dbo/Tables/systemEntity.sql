CREATE TABLE [dbo].[systemEntity] (
    [systemEntityId]        UNIQUEIDENTIFIER CONSTRAINT [DF_systemEntity_systemEntityId] DEFAULT (newid()) NOT NULL,
    [createtime]            DATETIME         CONSTRAINT [DF_systemEntity_createtime] DEFAULT (getdate()) NOT NULL,
    [entityName]            NVARCHAR (33)    NOT NULL,
    [entityDescription]     NVARCHAR (999)   NULL,
    [systemTemplateId]      UNIQUEIDENTIFIER NULL,
    [systemId]              UNIQUEIDENTIFIER NOT NULL,
    [parentEntityId]        UNIQUEIDENTIFIER NULL,
    [stateMachineId]        UNIQUEIDENTIFIER NULL,
    [stateMachineStateName] VARCHAR (33)     NULL,
    CONSTRAINT [PK_systemEntity] PRIMARY KEY CLUSTERED ([entityName] ASC, [systemId] ASC)
);














GO



GO
CREATE NONCLUSTERED INDEX [IX_systemEntity_3]
    ON [dbo].[systemEntity]([systemId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_systemEntity_2]
    ON [dbo].[systemEntity]([entityName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_systemEntity]
    ON [dbo].[systemEntity]([stateMachineId] ASC);

