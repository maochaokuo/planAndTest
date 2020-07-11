CREATE TABLE [dbo].[testResult] (
    [testResultId]          UNIQUEIDENTIFIER CONSTRAINT [DF_testResult_testResultId] DEFAULT (newid()) NOT NULL,
    [testStartTime]         DATETIME         NULL,
    [testEndTime]           DATETIME         NULL,
    [testResultDescription] NVARCHAR (999)   NULL,
    [passNum]               INT              CONSTRAINT [DF_testResult_passNum] DEFAULT ((0)) NOT NULL,
    [skipNum]               INT              CONSTRAINT [DF_testResult_skipNum] DEFAULT ((0)) NOT NULL,
    [failNum]               INT              CONSTRAINT [DF_testResult_failNum] DEFAULT ((0)) NOT NULL,
    [createtime]            DATETIME         CONSTRAINT [DF_testResult_createtime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_testResult] PRIMARY KEY CLUSTERED ([testResultId] ASC)
);

