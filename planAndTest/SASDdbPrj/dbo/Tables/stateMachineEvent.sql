CREATE TABLE [dbo].[stateMachineEvent] (
    [stateMachineEventId]     UNIQUEIDENTIFIER CONSTRAINT [DF_stateMachineEvent_stateMachineEventId] DEFAULT (newid()) NOT NULL,
    [stateMachineId]          UNIQUEIDENTIFIER NOT NULL,
    [eventName]               VARCHAR (33)     NOT NULL,
    [createtime]              DATETIME         CONSTRAINT [DF_stateMachineEvent_createtime] DEFAULT (getdate()) NOT NULL,
    [eventParameterJson]      NVARCHAR (999)   NULL,
    [eventActionName]         NVARCHAR (33)    NULL,
    [actionDoneEvent]         NVARCHAR (33)    NULL,
    [actionDoneEventParaJson] NVARCHAR (999)   NULL,
    [globalEventId]           UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_stateMachineEvent] PRIMARY KEY CLUSTERED ([stateMachineId] ASC, [eventName] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_stateMachineEvent_2]
    ON [dbo].[stateMachineEvent]([eventName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_stateMachineEvent_1]
    ON [dbo].[stateMachineEvent]([stateMachineId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_stateMachineEvent_3]
    ON [dbo].[stateMachineEvent]([stateMachineEventId] ASC);

