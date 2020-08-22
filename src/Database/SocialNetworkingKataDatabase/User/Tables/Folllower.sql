CREATE TABLE [User].[Folllower] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [UserId]     INT      NOT NULL,
    [FollowerId] INT      NOT NULL,
    [DateFrom]   DATETIME NULL,
    CONSTRAINT [PK_Folllower] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Folllower_User] FOREIGN KEY ([FollowerId]) REFERENCES [User].[User] ([Id]),
    CONSTRAINT [FK_Folllower_User1] FOREIGN KEY ([UserId]) REFERENCES [User].[User] ([Id])
);

