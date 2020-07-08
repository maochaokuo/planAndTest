CREATE TABLE [dbo].[stateMachine] (
    [stateMachineId]          UNIQUEIDENTIFIER NOT NULL,
    [createtime]              DATETIME         CONSTRAINT [DF_stateMachine_createtime] DEFAULT (getdate()) NOT NULL,
    [stateMachineName]        VARCHAR (33)     NOT NULL,
    [stateMachineDescription] NVARCHAR (999)   NULL,
    [receiveSelfEvent]        NVARCHAR (999)   NULL,
    [receiveParentEvent]      NVARCHAR (999)   NULL,
    [receiveChildEvent]       NVARCHAR (999)   NULL,
    [initialStateName]        VARCHAR (33)     NULL,
    CONSTRAINT [PK_stateMachine] PRIMARY KEY CLUSTERED ([stateMachineId] ASC)
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'multi value , delimited', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'stateMachine', @level2type = N'COLUMN', @level2name = N'receiveSelfEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'multi value , delimited', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'stateMachine', @level2type = N'COLUMN', @level2name = N'receiveParentEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'multi value , delimited', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'stateMachine', @level2type = N'COLUMN', @level2name = N'receiveChildEvent';

