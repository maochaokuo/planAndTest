CREATE TABLE [dbo].[systems] (
    [systemId]          UNIQUEIDENTIFIER NOT NULL,
    [projectVersionId]  INT              NULL,
    [createtime]        DATETIME         CONSTRAINT [DF_systems_createtime] DEFAULT (getdate()) NOT NULL,
    [systemName]        NVARCHAR (33)    NOT NULL,
    [systemDescription] NVARCHAR (999)   NULL,
    [systemType]        NVARCHAR (99)    NULL,
    CONSTRAINT [PK_systems] PRIMARY KEY CLUSTERED ([systemId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_systems]
    ON [dbo].[systems]([projectVersionId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'web,console,windows,window service,etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'systems', @level2type = N'COLUMN', @level2name = N'systemType';

