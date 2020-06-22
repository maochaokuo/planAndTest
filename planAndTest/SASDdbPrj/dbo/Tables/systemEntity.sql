CREATE TABLE [dbo].[systemEntity] (
    [systemEntityId]    INT              IDENTITY (1, 1) NOT NULL,
    [createtime]        DATETIME         CONSTRAINT [DF_systemEntity_createtime] DEFAULT (getdate()) NOT NULL,
    [entityName]        NVARCHAR (33)    NOT NULL,
    [entityDescription] NVARCHAR (999)   NULL,
    [systemTemplateId]  INT              NULL,
    [parentEntityId]    INT              NULL,
    [systemId]          UNIQUEIDENTIFIER NOT NULL,
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
CREATE NONCLUSTERED INDEX [IX_systemEntity_1]
    ON [dbo].[systemEntity]([parentEntityId] ASC);

