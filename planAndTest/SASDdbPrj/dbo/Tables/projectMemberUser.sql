CREATE TABLE [dbo].[projectMemberUser] (
    [projectId]       UNIQUEIDENTIFIER NOT NULL,
    [userId]          VARCHAR (33)     NOT NULL,
    [createtime]      DATETIME         CONSTRAINT [DF_projectMemberUser_createtime] DEFAULT (getdate()) NOT NULL,
    [roleDescription] NVARCHAR (999)   NULL,
    CONSTRAINT [PK_projectMemberUser] PRIMARY KEY CLUSTERED ([projectId] ASC, [userId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_projectMemberUser_1]
    ON [dbo].[projectMemberUser]([userId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_projectMemberUser]
    ON [dbo].[projectMemberUser]([projectId] ASC);

