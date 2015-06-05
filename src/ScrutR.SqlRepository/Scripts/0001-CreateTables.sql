

if not Exists(Select name from sysobjects where name = 'ScrutR_Notification' and xtype = 'U')
Begin
	create table dbo.ScrutR_Notification (
		Id varchar(36) not null,
		EventName varchar(100) not null,
		Entity varchar(max) not null,
		FullTypeName varchar(512) not null,
		CreationDate datetime not null,
		Priority int not null,
		Creator varchar(512) null
	)

	alter table dbo.ScrutR_Notification add constraint PK_ScrutR_Notification_Id primary key (Id)
End
Go

if not Exists(Select name from sysobjects where name = 'ScrutR_Subscription' and xtype = 'U')
Begin
	create table dbo.ScrutR_Subscription (
		Id varchar(36) not null,
		EventName varchar(100) not null,
		FullTypeName varchar(512) not null,
		Recipient varchar(512) not null,
		Collector int not null,
		ConditionList varchar(1024) null,
		PublisherList varchar(1024) not null,
		SubjectFormat varchar(512) not null,
		BodyFormat varchar(max) not null,
		CreationDate datetime not null
	)

	alter table dbo.ScrutR_Subscription add constraint PK_ScrutR_Subscription_Id primary key (Id)
End
Go
