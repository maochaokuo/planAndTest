CREATE TABLE [dbo].[systemGroup] (
    [systemGroupId]          INT              IDENTITY (1, 1) NOT NULL,
    [createtime]             DATETIME         CONSTRAINT [DF_systemGroup_createtime] DEFAULT (getdate()) NOT NULL,
    [systemGroupName]        NVARCHAR (33)    NOT NULL,
    [systemGroupDescription] NVARCHAR (999)   NULL,
    [projectId]              UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_systemGroup] PRIMARY KEY CLUSTERED ([systemGroupName] ASC, [projectId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_systemGroup_2]
    ON [dbo].[systemGroup]([projectId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_systemGroup_1]
    ON [dbo].[systemGroup]([systemGroupName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_systemGroup]
    ON [dbo].[systemGroup]([systemGroupId] ASC);

