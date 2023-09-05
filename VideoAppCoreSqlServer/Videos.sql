CREATE TABLE [dbo].[Videos]
(
	[Id] INT NOT NULL identity(1,1) PRIMARY KEY
	,Title nvarchar(255) not null -- 제목
	,Url nvarchar(max) not null -- URL
	,Name nvarchar(50) null --이름
	,Company nvarchar(255) null --회사
	,Created DateTime Default(GetDate()) --생성일(작성일)
	,CreateBy Nvarchar(255) null -- 생성자
	,Modified DateTime Default(getDate()) null -- 수정일
	,ModifiedBy Nvarchar(255) null -- 수정자
)
