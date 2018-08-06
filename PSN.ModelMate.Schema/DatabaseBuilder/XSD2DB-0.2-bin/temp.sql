if exists (select * from dbo.sysobjects where id = object_id(N'[mytopelement]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) DROP TABLE [mytopelement];CREATE TABLE [mytopelement] ([identifier] text, [version] text, [mytopelement_Id] int NOT NULL);
if exists (select * from dbo.sysobjects where id = object_id(N'[mynames]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) DROP TABLE [mynames];CREATE TABLE [mynames] ([mynames_Id] int NOT NULL, [mytopelement_Id] int NULL);
if exists (select * from dbo.sysobjects where id = object_id(N'[myname]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) DROP TABLE [myname];CREATE TABLE [myname] ([lang] text, [myname_text] text, [mynames_Id] int NULL, [myname_Id] int NOT NULL);
ALTER TABLE [mytopelement] WITH NOCHECK ADD CONSTRAINT [PK_mytopelement] PRIMARY KEY CLUSTERED ([mytopelement_Id]);
ALTER TABLE [mynames] WITH NOCHECK ADD CONSTRAINT [PK_mynames] PRIMARY KEY CLUSTERED ([mynames_Id]);
ALTER TABLE [myname] WITH NOCHECK ADD CONSTRAINT [PK_myname] PRIMARY KEY CLUSTERED ([myname_Id]);
ALTER TABLE [myname] ADD CONSTRAINT [mynames_myname] FOREIGN KEY ([mynames_Id]) REFERENCES [mynames] ([mynames_Id]);
ALTER TABLE [mynames] ADD CONSTRAINT [mytopelement_mynames] FOREIGN KEY ([mytopelement_Id]) REFERENCES [mytopelement] ([mytopelement_Id]);
