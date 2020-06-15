CREATE TABLE [dbo].[stateMachineState] (
    [stateMachineStateId] INT              IDENTITY (1, 1) NOT NULL,
    [stateMachineId]      UNIQUEIDENTIFIER NULL,
    [stateName]           VARCHAR (33)     NOT NULL,
    [stateDescription]    NVARCHAR (999)   NULL,
    [createtime]          DATETIME         CONSTRAINT [DF_stateMachineState_createtime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_stateMachineState] PRIMARY KEY CLUSTERED ([stateMachineStateId] ASC)
);

