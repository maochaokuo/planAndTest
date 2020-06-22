CREATE TABLE [dbo].[templateEntity] (
    [templateEntityId]          INT            IDENTITY (1, 1) NOT NULL,
    [createtime]                DATETIME       CONSTRAINT [DF_templateEntity_createtime] DEFAULT (getdate()) NOT NULL,
    [systemTemplateId]          INT            NOT NULL,
    [templateEntityName]        NVARCHAR (33)  NOT NULL,
    [templateEntityDescription] NVARCHAR (999) NULL,
    [parentTemplateEntityId]    INT            NULL,
    CONSTRAINT [PK_templateEntity] PRIMARY KEY CLUSTERED ([systemTemplateId] ASC, [templateEntityName] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_templateEntity_2]
    ON [dbo].[templateEntity]([parentTemplateEntityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_templateEntity_1]
    ON [dbo].[templateEntity]([templateEntityName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_templateEntity]
    ON [dbo].[templateEntity]([systemTemplateId] ASC);

