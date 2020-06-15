CREATE TABLE [dbo].[domain] (
    [domainId]          UNIQUEIDENTIFIER CONSTRAINT [DF_domain_domainId] DEFAULT (newid()) NOT NULL,
    [createtime]        DATETIME         CONSTRAINT [DF_domain_createtime] DEFAULT (getdate()) NOT NULL,
    [domainName]        VARCHAR (33)     NOT NULL,
    [domainDescription] NVARCHAR (999)   NULL,
    [baseType]          VARCHAR (33)     NOT NULL,
    CONSTRAINT [PK_domain] PRIMARY KEY CLUSTERED ([domainId] ASC)
);

