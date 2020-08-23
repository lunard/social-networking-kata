CREATE TABLE [User].[Follower] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [FollowerId] INT      NOT NULL,
    [FollowedId] INT      NOT NULL,
    [DateFrom]   DATETIME NULL,
    CONSTRAINT [PK_Folllower] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Folllower_User] FOREIGN KEY ([FollowedId]) REFERENCES [User].[User] ([Id]),
    CONSTRAINT [FK_Folllower_User1] FOREIGN KEY ([FollowerId]) REFERENCES [User].[User] ([Id])
);



