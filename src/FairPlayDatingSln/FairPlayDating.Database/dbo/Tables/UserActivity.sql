CREATE TABLE [dbo].[UserActivity]
(
	[UserActivityId] BIGINT NOT NULL  CONSTRAINT PK_UserActivity PRIMARY KEY IDENTITY, 
    [ApplicationUserId] BIGINT NOT NULL, 
    [ActivityId] SMALLINT NOT NULL, 
    [FrequencyId] SMALLINT NOT NULL, 
    CONSTRAINT [FK_UserActivity_ApplicationUser] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser]([ApplicationUserId]), 
    CONSTRAINT [FK_UserActivity_Activity] FOREIGN KEY ([ActivityId]) REFERENCES [Activity]([ActivityId]), 
    CONSTRAINT [FK_UserActivity_Frequency] FOREIGN KEY ([FrequencyId]) REFERENCES [Frequency]([FrequencyId])
)

GO

CREATE UNIQUE INDEX [UI_UserActivity_ApplicationUserId_ActivityId] ON [dbo].[UserActivity] ([ApplicationUserId], [ActivityId])
