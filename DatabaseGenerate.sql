
CREATE TABLE [Player](
	[AccountId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
    CONSTRAINT [PK_Player] PRIMARY KEY ([AccountId])
 );
CREATE TABLE [Wager] (
          [WagerId] uniqueidentifier NOT NULL,
          [Theme] nvarchar(max) NOT NULL,
          [Provider] nvarchar(max) NOT NULL,
          [GameName] nvarchar(max) NOT NULL,
          [Amount] decimal(18,2) NOT NULL,
          [CreationDate] datetime2 NOT NULL,
          [AccountId] uniqueidentifier NOT NULL,
          [PlayerAccountId] uniqueidentifier NOT NULL,
          CONSTRAINT [PK_Wager] PRIMARY KEY ([WagerId]),
          CONSTRAINT [FK_Wager_Player_PlayerAccountId] FOREIGN KEY ([PlayerAccountId]) REFERENCES [Player] ([AccountId]) ON DELETE CASCADE
      );



CREATE INDEX [IX_Player_AccountId] ON [Player] ([AccountId]);

CREATE INDEX [IX_Player_AccountId] ON [Player] ([AccountId]);

CREATE INDEX [IX_Wager_AccountId] ON [Wager] ([AccountId]);

CREATE INDEX [IX_Wager_AccountId] ON [Wager] ([AccountId]);

CREATE INDEX [IX_Wager_PlayerAccountId] ON [Wager] ([PlayerAccountId]);

CREATE INDEX [IX_Wager_PlayerAccountId] ON [Wager] ([PlayerAccountId]);

CREATE INDEX [IX_Wager_WagerId] ON [Wager] ([WagerId]);

CREATE INDEX [IX_Wager_WagerId] ON [Wager] ([WagerId]);