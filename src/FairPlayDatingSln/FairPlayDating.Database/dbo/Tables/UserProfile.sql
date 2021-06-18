CREATE TABLE [dbo].[UserProfile]
(
	[UserProfileId] BIGINT NOT NULL CONSTRAINT PK_UserProfile PRIMARY KEY IDENTITY,
	[ApplicationUserId] BIGINT NOT NULL CONSTRAINT FK_ApplicationUserId_UserProfile FOREIGN KEY REFERENCES [dbo].[ApplicationUser]([ApplicationUserId]),
	[About] NVARCHAR(100) NOT NULL, 
    [HairColorId] SMALLINT NOT NULL, 
    [EyesColorId] SMALLINT NOT NULL, 
    CONSTRAINT [FK_UserProfile_HairColor] FOREIGN KEY ([HairColorId]) REFERENCES [HairColor]([HairColorId]), 
    CONSTRAINT [FK_UserProfile_EyesColor] FOREIGN KEY ([EyesColorId]) REFERENCES [EyesColor]([EyesColorId]) 
)
