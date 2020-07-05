CREATE TABLE [dbo].[systems] (
    [systemId]          UNIQUEIDENTIFIER NOT NULL,
    [createtime]        DATETIME         CONSTRAINT [DF_systems_createtime] DEFAULT (getdate()) NOT NULL,
    [systemName]        NVARCHAR (33)    NOT NULL,
    [systemDescription] NVARCHAR (999)   NULL,
    [systemType]        NVARCHAR (99)    NULL,
    [projectId]         UNIQUEIDENTIFIER NOT NULL,
    [systemGroupName]   NVARCHAR (33)    NULL,
    [systemArticleId]   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_systems] PRIMARY KEY CLUSTERED ([systemName] ASC, [projectId] ASC)
);








GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'web,console,windows,window service,etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'systems', @level2type = N'COLUMN', @level2name = N'systemType';


GO
CREATE NONCLUSTERED INDEX [IX_systems_2]
    ON [dbo].[systems]([systemName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_systems_1]
    ON [dbo].[systems]([projectId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_systems]
    ON [dbo].[systems]([systemId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_systems_4]
    ON [dbo].[systems]([systemArticleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_systems_3]
    ON [dbo].[systems]([systemGroupName] ASC);

