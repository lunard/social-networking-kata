CREATE TABLE [User].[User] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_User.User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

