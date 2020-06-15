CREATE TABLE [dbo].[stateMachine] (
    [stateMachineId]          UNIQUEIDENTIFIER NOT NULL,
    [createtime]              DATETIME         CONSTRAINT [DF_stateMachine_createtime] DEFAULT (getdate()) NOT NULL,
    [stateMachineName]        VARCHAR (33)     NOT NULL,
    [stateMachineDescription] NVARCHAR (999)   NULL,
    CONSTRAINT [PK_stateMachine] PRIMARY KEY CLUSTERED ([stateMachineId] ASC)
);

