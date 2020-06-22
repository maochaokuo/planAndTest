CREATE TABLE [dbo].[systemTemplate] (
    [systemTemplateId]    INT            IDENTITY (1, 1) NOT NULL,
    [createtime]          DATETIME       CONSTRAINT [DF_systemTemplate_createtime] DEFAULT (getdate()) NOT NULL,
    [templateName]        NVARCHAR (33)  NOT NULL,
    [templateDescription] NVARCHAR (999) NULL,
    CONSTRAINT [PK_systemTemplate] PRIMARY KEY CLUSTERED ([systemTemplateId] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_systemTemplate]
    ON [dbo].[systemTemplate]([templateName] ASC);

