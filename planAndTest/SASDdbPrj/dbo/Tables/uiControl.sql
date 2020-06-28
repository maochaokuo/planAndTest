CREATE TABLE [dbo].[uiControl] (
    [uiControlId]        INT            IDENTITY (1, 1) NOT NULL,
    [uiFormId]           INT            NOT NULL,
    [controlName]        NVARCHAR (33)  NOT NULL,
    [controlDescription] NVARCHAR (999) NULL,
    [createtime]         DATETIME       CONSTRAINT [DF_uiControl_createtime] DEFAULT (getdate()) NOT NULL,
    [controlType]        VARCHAR (33)   CONSTRAINT [DF_uiControl_controlType] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_uiControl] PRIMARY KEY CLUSTERED ([uiFormId] ASC, [controlName] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_uiControl_2]
    ON [dbo].[uiControl]([controlName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_uiControl_1]
    ON [dbo].[uiControl]([uiFormId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_uiControl]
    ON [dbo].[uiControl]([uiControlId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'textbox, dropdown, checkbox, button', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'uiControl', @level2type = N'COLUMN', @level2name = N'controlType';

