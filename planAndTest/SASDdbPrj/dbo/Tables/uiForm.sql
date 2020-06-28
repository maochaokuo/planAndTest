CREATE TABLE [dbo].[uiForm] (
    [uiFormId]        INT            IDENTITY (1, 1) NOT NULL,
    [createtime]      DATETIME       CONSTRAINT [DF_uiForm_createtime] DEFAULT (getdate()) NOT NULL,
    [systemEntityId]  INT            NOT NULL,
    [formName]        NVARCHAR (33)  NOT NULL,
    [formDescription] NVARCHAR (999) NULL,
    CONSTRAINT [PK_uiForm] PRIMARY KEY CLUSTERED ([systemEntityId] ASC, [formName] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_uiForm_2]
    ON [dbo].[uiForm]([formName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_uiForm_1]
    ON [dbo].[uiForm]([systemEntityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_uiForm]
    ON [dbo].[uiForm]([uiFormId] ASC);

