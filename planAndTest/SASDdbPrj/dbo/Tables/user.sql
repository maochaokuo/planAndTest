CREATE TABLE [dbo].[user] (
    [userId]              VARCHAR (33)  NOT NULL,
    [userPassword]        VARCHAR (33)  NULL,
    [createtime]          DATETIME      CONSTRAINT [DF_user_createtime] DEFAULT (getdate()) NOT NULL,
    [userCommentsPublic]  NVARCHAR (99) NULL,
    [userCommentsPrivate] NVARCHAR (99) NULL,
    [lastLoginTime]       DATETIME      NULL,
    [modifytime]          DATETIME      CONSTRAINT [DF_user_modifytime] DEFAULT (getdate()) NOT NULL,
    [hintQuestion]        NVARCHAR (99) NULL,
    [hintAnswer]          NVARCHAR (99) NULL,
    [deleteTime]          DATETIME2 (7) NULL,
    [deleteBy]            VARCHAR (33)  NULL,
    CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED ([userId] ASC)
);





