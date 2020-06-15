CREATE TABLE [dbo].[systemEntity] (
    [systemEntity]      INT            IDENTITY (1, 1) NOT NULL,
    [createtime]        DATETIME       CONSTRAINT [DF_systemEntity_createtime] DEFAULT (getdate()) NOT NULL,
    [entityName]        NVARCHAR (33)  NOT NULL,
    [entityDescription] NVARCHAR (999) NULL,
    [systemTemplateId]  INT            NOT NULL,
    CONSTRAINT [PK_systemEntity] PRIMARY KEY CLUSTERED ([systemEntity] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_systemEntity]
    ON [dbo].[systemEntity]([systemTemplateId] ASC);

