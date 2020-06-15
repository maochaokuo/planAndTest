CREATE TABLE [dbo].[domainCase] (
    [domainCaseId]    INT              IDENTITY (1, 1) NOT NULL,
    [domainId]        UNIQUEIDENTIFIER CONSTRAINT [DF_domainCase_domainId] DEFAULT (newid()) NOT NULL,
    [createtime]      DATETIME         CONSTRAINT [DF_domainCase_createtime] DEFAULT (getdate()) NOT NULL,
    [caseName]        VARCHAR (33)     NOT NULL,
    [caseDescription] NVARCHAR (999)   NULL,
    CONSTRAINT [PK_domainCase] PRIMARY KEY CLUSTERED ([domainCaseId] ASC)
);

