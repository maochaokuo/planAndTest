CREATE TABLE [dbo].[stateMachineEvent2State] (
    [stateMachineEvent2StateId] INT              NOT NULL,
    [stateMachineId]            UNIQUEIDENTIFIER NOT NULL,
    [eventName]                 VARCHAR (33)     NOT NULL,
    [stateName]                 VARCHAR (33)     NOT NULL,
    [newStateName]              VARCHAR (33)     NOT NULL,
    [createtime]                DATETIME         CONSTRAINT [DF_stateMachineEvent2State_createtime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_stateMachineEvent2State] PRIMARY KEY CLUSTERED ([stateMachineId] ASC, [eventName] ASC, [stateName] ASC)
);



