CREATE TABLE [dbo].[systemEntity] (
    [systemEntityId]    UNIQUEIDENTIFIER CONSTRAINT [DF_systemEntity_systemEntityId] DEFAULT (newid()) NOT NULL,
    [createtime]        DATETIME         CONSTRAINT [DF_systemEntity_createtime] DEFAULT (getdate()) NOT NULL,
    [entityName]        NVARCHAR (33)    NOT NULL,
    [entityDescription] NVARCHAR (999)   NULL,
    [systemTemplateId]  INT              NULL,
    [systemId]          UNIQUEIDENTIFIER NOT NULL,
    [parentEntityId]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_systemEntity] PRIMARY KEY CLUSTERED ([entityName] ASC, [systemId] ASC)
);








GO
CREATE NONCLUSTERED INDEX [IX_systemEntity]
    ON [dbo].[systemEntity]([systemTemplateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_systemEntity_3]
    ON [dbo].[systemEntity]([systemId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_systemEntity_2]
    ON [dbo].[systemEntity]([entityName] ASC);


GO


