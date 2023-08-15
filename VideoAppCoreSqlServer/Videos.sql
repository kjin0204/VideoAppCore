CREATE TABLE [dbo].[Table1]
(
	[Id] INT NOT NULL identity(1,1) PRIMARY KEY
	,Created DateTimeOffset(7) Default(SysDateTimeOffset() AT TIME ZONE 'Korea Standard Time')
	,Title nvarchar(255) not null -- 제목
	,Url nvarchar(max) not null -- URL
	,Name nvarchar(50) null --이름
	,Company nvarchar(255) null --회사
)
