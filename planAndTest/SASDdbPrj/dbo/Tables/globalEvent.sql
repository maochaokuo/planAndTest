CREATE TABLE [dbo].[globalEvent] (
    [globalEventId]           UNIQUEIDENTIFIER CONSTRAINT [DF_Table_1_stateMachineEventId] DEFAULT (newid()) NOT NULL,
    [globalEventName]         VARCHAR (33)     NOT NULL,
    [createtime]              DATETIME         CONSTRAINT [DF_globalEvent_createtime] DEFAULT (getdate()) NOT NULL,
    [eventParameterJson]      NVARCHAR (999)   NULL,
    [eventActionName]         NVARCHAR (33)    NULL,
    [actionDoneEvent]         NVARCHAR (33)    NULL,
    [actionDoneEventParaJson] NVARCHAR (999)   NULL,
    [globalEventDescription]  NVARCHAR (99)    NULL,
    CONSTRAINT [PK_globalEvent] PRIMARY KEY CLUSTERED ([globalEventId] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_globalEvent]
    ON [dbo].[globalEvent]([globalEventName] ASC);

