CREATE TABLE [dbo].[stateMachineState] (
    [stateMachineStateId] INT              IDENTITY (1, 1) NOT NULL,
    [stateMachineId]      UNIQUEIDENTIFIER NOT NULL,
    [stateName]           VARCHAR (33)     NOT NULL,
    [stateDescription]    NVARCHAR (999)   NULL,
    [createtime]          DATETIME         CONSTRAINT [DF_stateMachineState_createtime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_stateMachineState] PRIMARY KEY CLUSTERED ([stateMachineId] ASC, [stateName] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_stateMachineState_2]
    ON [dbo].[stateMachineState]([stateName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_stateMachineState_1]
    ON [dbo].[stateMachineState]([stateMachineId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_stateMachineState]
    ON [dbo].[stateMachineState]([stateMachineStateId] ASC);

