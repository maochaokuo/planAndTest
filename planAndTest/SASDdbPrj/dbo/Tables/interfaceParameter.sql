CREATE TABLE [dbo].[interfaceParameter] (
    [interfaceParameterId] INT              IDENTITY (1, 1) NOT NULL,
    [systemInterfaceId]    UNIQUEIDENTIFIER NOT NULL,
    [parameterName]        VARCHAR (33)     NOT NULL,
    [parameterType]        VARCHAR (33)     NULL,
    [createtime]           DATETIME         CONSTRAINT [DF_interfaceParameter_createtime] DEFAULT (getdate()) NOT NULL,
    [parameterComments]    NVARCHAR (999)   NULL,
    CONSTRAINT [PK_interfaceParameter] PRIMARY KEY CLUSTERED ([systemInterfaceId] ASC, [parameterName] ASC)
);

