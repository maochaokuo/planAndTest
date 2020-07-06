CREATE TABLE [dbo].[systemTemplate] (
    [systemTemplateId]     UNIQUEIDENTIFIER CONSTRAINT [DF_systemTemplate_systemTemplateId] DEFAULT (newid()) NOT NULL,
    [createtime]           DATETIME         CONSTRAINT [DF_systemTemplate_createtime] DEFAULT (getdate()) NOT NULL,
    [templateName]         NVARCHAR (33)    NOT NULL,
    [templateDescription]  NVARCHAR (999)   NULL,
    [baseSystemTemplateId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_systemTemplate_1] PRIMARY KEY CLUSTERED ([systemTemplateId] ASC)
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_systemTemplate]
    ON [dbo].[systemTemplate]([templateName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_systemTemplate_1]
    ON [dbo].[systemTemplate]([baseSystemTemplateId] ASC);

