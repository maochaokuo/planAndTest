CREATE TABLE [dbo].[testCaseResult] (
    [testCaseResultId] UNIQUEIDENTIFIER CONSTRAINT [DF_testCaseResult_testCaseResultId] DEFAULT (newid()) NOT NULL,
    [testCaseId]       UNIQUEIDENTIFIER NOT NULL,
    [testResultId]     UNIQUEIDENTIFIER NOT NULL,
    [createtime]       DATETIME         CONSTRAINT [DF_testCaseResult_createtime] DEFAULT (getdate()) NOT NULL,
    [testResult]       SMALLINT         CONSTRAINT [DF_testCaseResult_testResult] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_testCaseResult] PRIMARY KEY CLUSTERED ([testCaseId] ASC, [testResultId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_testCaseResult_2]
    ON [dbo].[testCaseResult]([testResultId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_testCaseResult_1]
    ON [dbo].[testCaseResult]([testCaseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_testCaseResult]
    ON [dbo].[testCaseResult]([testCaseResultId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 pass 0 fail -1 skip', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'testCaseResult', @level2type = N'COLUMN', @level2name = N'testResult';

