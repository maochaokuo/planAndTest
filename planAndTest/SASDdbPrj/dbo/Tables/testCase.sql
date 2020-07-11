CREATE TABLE [dbo].[testCase] (
    [testCaseId]          UNIQUEIDENTIFIER CONSTRAINT [DF_testCase_testCaseId] DEFAULT (newid()) NOT NULL,
    [projectId]           UNIQUEIDENTIFIER NULL,
    [version]             NVARCHAR (33)    NULL,
    [createtime]          DATETIME         CONSTRAINT [DF_testCase_createtime] DEFAULT (getdate()) NOT NULL,
    [testCaseDescription] NVARCHAR (999)   NULL,
    [extraPassCondition]  NVARCHAR (999)   NULL,
    [extraSkipCondition]  NVARCHAR (999)   NULL,
    [extraFailCondition]  NVARCHAR (999)   NULL,
    [parentTestCaseId]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_testCase] PRIMARY KEY CLUSTERED ([testCaseId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_testCase_2]
    ON [dbo].[testCase]([parentTestCaseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_testCase_1]
    ON [dbo].[testCase]([version] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_testCase]
    ON [dbo].[testCase]([projectId] ASC);

