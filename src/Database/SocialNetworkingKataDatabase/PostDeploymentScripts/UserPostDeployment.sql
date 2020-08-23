IF NOT EXISTS(SELECT * FROM [User].[User] WHERE [Name] = 'Alice') INSERT INTO [User].[User] ([Name]) VALUES ('Alice')
GO

IF NOT EXISTS(SELECT * FROM [User].[User] WHERE [Name] = 'Bob') INSERT INTO [User].[User] ([Name]) VALUES ('Bob')
GO

IF NOT EXISTS(SELECT * FROM [User].[User] WHERE [Name] = 'Charlie') INSERT INTO [User].[User] ([Name]) VALUES ('Charlie')
GO

IF NOT EXISTS(SELECT * FROM [User].[User] WHERE [Name] = 'Luca') INSERT INTO [User].[User] ([Name]) VALUES ('Luca')
GO

-- Insert Alice fake messages
DECLARE @UserId INT
DECLARE @MessageDate datetime
DECLARE @MessageContent nvarchar(MAX)

SELECT @UserId = Id  FROM [User].[User] WHERE [Name] = 'Alice'
SELECT @MessageDate = CAST('2020-08-23 10:15' AS datetime)
SET @MessageContent = 'Hello, I''m Alice!'

IF NOT EXISTS(SELECT * FROM [User].[Message] WHERE [Content] = @MessageContent and [Date] = @MessageDate)
INSERT INTO [User].[Message]
           ([UserId]
           ,[Content]
           ,[Date])
     VALUES
           (@UserId
           ,@MessageContent
           ,@MessageDate)

SELECT @MessageDate = CAST('2020-08-23 10:16' AS datetime)
SET @MessageContent = 'How are you ?'
IF NOT EXISTS(SELECT * FROM [User].[Message] WHERE [Content] = @MessageContent and [Date] = @MessageDate)
INSERT INTO [User].[Message]
           ([UserId]
           ,[Content]
           ,[Date])
     VALUES
           (@UserId
           ,@MessageContent
           ,@MessageDate)


SELECT @UserId = Id  FROM [User].[User] WHERE [Name] = 'Luca'
SELECT @MessageDate = CAST('2020-08-23 10:35' AS datetime)
SET @MessageContent = 'Ciao ragazzi, mi chiamo Luca'
IF NOT EXISTS(SELECT * FROM [User].[Message] WHERE [Content] = @MessageContent and [Date] = @MessageDate)
INSERT INTO [User].[Message]
           ([UserId]
           ,[Content]
           ,[Date])
     VALUES
           (@UserId
           ,@MessageContent
           ,@MessageDate)

GO

