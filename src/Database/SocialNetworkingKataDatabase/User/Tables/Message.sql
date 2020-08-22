CREATE TABLE [User].[Message] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [UserId]  INT            NOT NULL,
    [Content] NVARCHAR (MAX) NULL,
    [Date]    DATETIME       NOT NULL,
    CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Message_User] FOREIGN KEY ([UserId]) REFERENCES [User].[User] ([Id])
);

