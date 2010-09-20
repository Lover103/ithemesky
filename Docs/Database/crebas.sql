/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2010/8/11 11:40:39                           */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ThemeComment') and o.name = 'FK_THEMECOM_REFERENCE_THEME')
alter table dbo.ThemeComment
   drop constraint FK_THEMECOM_REFERENCE_THEME
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ThemeDownloadHistory') and o.name = 'FK_THEMEDOW_REFERENCE_THEME')
alter table dbo.ThemeDownloadHistory
   drop constraint FK_THEMEDOW_REFERENCE_THEME
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ThemeImages') and o.name = 'FK_THEMEIMA_REFERENCE_THEME')
alter table dbo.ThemeImages
   drop constraint FK_THEMEIMA_REFERENCE_THEME
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ThemeRateHistory') and o.name = 'FK_THEMERAT_REFERENCE_THEME')
alter table dbo.ThemeRateHistory
   drop constraint FK_THEMERAT_REFERENCE_THEME
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ThemeTag') and o.name = 'FK_THEMETAG_REFERENCE_THEMETAGCategory')
alter table dbo.ThemeTag
   drop constraint FK_THEMETAG_REFERENCE_THEMETAGCategory
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ThemeTagMap') and o.name = 'FK_THEMETAG_REFERENCE_THEME')
alter table dbo.ThemeTagMap
   drop constraint FK_THEMETAG_REFERENCE_THEME
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ThemeTagMap') and o.name = 'FK_THEMETAG_REFERENCE_THEMETAG')
alter table dbo.ThemeTagMap
   drop constraint FK_THEMETAG_REFERENCE_THEMETAG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Theme')
            and   type = 'U')
   drop table dbo.Theme
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ThemeCategory')
            and   type = 'U')
   drop table dbo.ThemeCategory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ThemeComment')
            and   type = 'U')
   drop table dbo.ThemeComment
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ThemeDownloadHistory')
            and   type = 'U')
   drop table dbo.ThemeDownloadHistory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ThemeImages')
            and   type = 'U')
   drop table dbo.ThemeImages
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ThemeRateHistory')
            and   type = 'U')
   drop table dbo.ThemeRateHistory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ThemeTag')
            and   type = 'U')
   drop table dbo.ThemeTag
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ThemeTagCategory')
            and   type = 'U')
   drop table dbo.ThemeTagCategory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ThemeTagMap')
            and   type = 'U')
   drop table dbo.ThemeTagMap
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Theme')
            and   type = 'U')
   drop table dbo.tmp_Theme
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_ThemeCategory')
            and   type = 'U')
   drop table dbo.tmp_ThemeCategory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_ThemeComment')
            and   type = 'U')
   drop table dbo.tmp_ThemeComment
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_ThemeDownloadHistory')
            and   type = 'U')
   drop table dbo.tmp_ThemeDownloadHistory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_ThemeImages')
            and   type = 'U')
   drop table dbo.tmp_ThemeImages
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_ThemeRateHistory')
            and   type = 'U')
   drop table dbo.tmp_ThemeRateHistory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_ThemeTag')
            and   type = 'U')
   drop table dbo.tmp_ThemeTag
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_ThemeTagCategory')
            and   type = 'U')
   drop table dbo.tmp_ThemeTagCategory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_ThemeTagMap')
            and   type = 'U')
   drop table dbo.tmp_ThemeTagMap
go

execute sp_revokedbaccess dbo
go

/*==============================================================*/
/* Table: Theme                                                 */
/*==============================================================*/
create table Theme (
   ThemeId              int                  identity,
   CategoryId2          int                  null,
   ParentCategoryId     int                  null,
   Title                varchar(300)         null,
   FileSize             bigint               null,
   Description          varchar(1000)        null,
   DisplayState         smallint             null default 0,
   CheckState           smallint             null default 0,
   AuthorId             int                  null default 0,
   CheckerId            int                  null,
   CommendIndex         int                  null default 0,
   ThumbnailName        varchar(100)         null,
   AddTime              datetime             null default getdate(),
   UpdateTime           datetime             null default getdate(),
   RateScore            int                  null,
   RateNumbers          int                  null,
   Comments             int                  null,
   Downloads            int                  null,
   Views                int                  null,
   LastWeekDownloads    int                  null,
   LastMonthDownloads   int                  null,
   Source               int                  null,
   DownloadUrl          varchar(300)         null,
   constraint PK_THEME primary key (ThemeId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ThemeId',
   'user', @CurrentUser, 'table', 'Theme', 'column', 'ThemeId'
go

/*==============================================================*/
/* Index: Index_CategoryId                                      */
/*==============================================================*/
create index Index_CategoryId on Theme (
CategoryId2 ASC
)
go

/*==============================================================*/
/* Index: Index_ParentCategoryId                                */
/*==============================================================*/
create index Index_ParentCategoryId on Theme (
ParentCategoryId ASC
)
go

/*==============================================================*/
/* Table: ThemeCategory                                         */
/*==============================================================*/
create table ThemeCategory (
   CategoryId           int                  identity,
   CategoryName         varchar(300)         null,
   ParentId             int                  null,
   CategoryIcon         varchar(300)         null,
   SortNumber           int                  null,
   BindTagCategories    varchar(500)         null,
   constraint PK_THEMECATEGORY primary key (CategoryId)
)
go

/*==============================================================*/
/* Table: ThemeComment                                          */
/*==============================================================*/
create table ThemeComment (
   CommentId            bigint               identity,
   ThemeId              int                  null,
   RateType             int                  null,
   Title                varchar(300)         null,
   Content              text                 null,
   UserId               int                  null,
   UserIp               varchar(40)          null,
   AddTime              datetime             null,
   UpdateTime           datetime             null,
   DiggNumber           int                  null,
   BuryNumber           int                  null,
   UserName             varchar(100)         null,
   UserMail             varchar(300)         null,
   constraint PK_THEMECOMMENT primary key (CommentId)
)
go

/*==============================================================*/
/* Index: Index_ThemeId                                         */
/*==============================================================*/
create index Index_ThemeId on ThemeComment (
ThemeId ASC
)
go

/*==============================================================*/
/* Table: ThemeDownloadHistory                                  */
/*==============================================================*/
create table ThemeDownloadHistory (
   HistoryId            bigint               identity,
   ThemeId              int                  null,
   AddTime              datetime             null default getdate(),
   UserId               int                  null default 0,
   UserIp               varchar(40)          null,
   UserOs               varchar(300)         null,
   UserBrowser          varchar(300)         null,
   constraint PK_THEMEDOWNLOADHISTORY primary key (HistoryId)
)
go

/*==============================================================*/
/* Index: Index_ThemeId                                         */
/*==============================================================*/
create index Index_ThemeId on ThemeDownloadHistory (
ThemeId ASC
)
go

/*==============================================================*/
/* Table: ThemeImages                                           */
/*==============================================================*/
create table ThemeImages (
   ThemeId              int                  null,
   ImageId              int                  identity,
   ImageUrl             varchar(300)         null,
   AddTime              datetime             null default getdate(),
   constraint PK_THEMEIMAGES primary key (ImageId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ThemeId',
   'user', @CurrentUser, 'table', 'ThemeImages', 'column', 'ThemeId'
go

/*==============================================================*/
/* Table: ThemeRateHistory                                      */
/*==============================================================*/
create table ThemeRateHistory (
   HistoryId            bigint               identity,
   ThemeId              int                  null,
   RateScore            int                  null default 0,
   AddTime              datetime             null default getdate(),
   UserId               int                  null default 0,
   UserIp               varchar(40)          null,
   constraint PK_THEMERATEHISTORY primary key (HistoryId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ThemeId',
   'user', @CurrentUser, 'table', 'ThemeRateHistory', 'column', 'ThemeId'
go

/*==============================================================*/
/* Index: Index_ThemeId                                         */
/*==============================================================*/
create index Index_ThemeId on ThemeRateHistory (
ThemeId ASC
)
go

/*==============================================================*/
/* Table: ThemeTag                                              */
/*==============================================================*/
create table ThemeTag (
   TagId                int                  identity,
   TagCategoryId        int                  null,
   TagName              varchar(40)          null,
   TagIcon              varchar(300)         null,
   TagDescription       text                 null,
   IsSystem             smallint             null default 0,
   AddTime              datetime             null default getdate(),
   UsedTimes            int                  null default 0,
   constraint PK_THEMETAG primary key (TagId)
)
go

/*==============================================================*/
/* Table: ThemeTagCategory                                      */
/*==============================================================*/
create table ThemeTagCategory (
   TagCategoryId        int                  identity,
   TagCategoryName      varchar(300)         null,
   TagCategoryIcon      varchar(300)         null,
   constraint PK_THEMETAGCATEGORY primary key (TagCategoryId)
)
go

/*==============================================================*/
/* Table: ThemeTagMap                                           */
/*==============================================================*/
create table ThemeTagMap (
   ThemeId              int                  not null,
   TagId                int                  not null,
   SortNumber           int                  null default 0,
   constraint PK_THEMETAGMAP primary key (ThemeId, TagId)
)
go

/*==============================================================*/
/* View: View_TagTheme                                          */
/*==============================================================*/
create view View_TagTheme as
SELECT 
    Theme.*, 
    ISNULL(CategoryName, 'Unknow Category') CategoryName, 
    ThemeTagMap.SortNumber as TagSortNumber,
    ThemeTag.TagId,ThemeTag.TagCategoryId,ThemeTag.TagName
    FROM Theme 
    LEFT JOIN ThemeCategory ON Theme.CategoryId = ThemeCategory.CategoryId
    INNER JOIN ThemeTagMap ON Theme.ThemeId = ThemeTagMap.ThemeId
    INNER JOIN ThemeTag ON ThemeTagMap.TagId = ThemeTag.TagId
go

/*==============================================================*/
/* View: View_Theme                                             */
/*==============================================================*/
create view View_Theme as
SELECT 
    Theme.*, ISNULL(CategoryName, 'Unknow Category') CategoryName
    FROM Theme 
    LEFT JOIN ThemeCategory ON Theme.CategoryId = ThemeCategory.CategoryId
go

alter table ThemeComment
   add constraint FK_THEMECOM_REFERENCE_THEME foreign key (ThemeId)
      references Theme (ThemeId)
go

alter table ThemeDownloadHistory
   add constraint FK_THEMEDOW_REFERENCE_THEME foreign key (ThemeId)
      references Theme (ThemeId)
go

alter table ThemeImages
   add constraint FK_THEMEIMA_REFERENCE_THEME foreign key (ThemeId)
      references Theme (ThemeId)
go

alter table ThemeRateHistory
   add constraint FK_THEMERAT_REFERENCE_THEME foreign key (ThemeId)
      references Theme (ThemeId)
go

alter table ThemeTag
   add constraint FK_THEMETAG_REFERENCE_THEMETAGCategory foreign key (TagCategoryId)
      references ThemeTagCategory (TagCategoryId)
go

alter table ThemeTagMap
   add constraint FK_THEMETAG_REFERENCE_THEME foreign key (ThemeId)
      references Theme (ThemeId)
go

alter table ThemeTagMap
   add constraint FK_THEMETAG_REFERENCE_THEMETAG foreign key (TagId)
      references ThemeTag (TagId)
go


/****** 对象:  StoredProcedure [dbo].[PR_GetDataByPageIndex]    脚本日期: 10/15/2009 18:47:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_GetDataByPageIndex]
	@SelectList            VARCHAR(2000),    --欲选择字段列表
    @TableSource        VARCHAR(255),    --表名或视图表 
	@PrimaryKeyField	VARCHAR(255) = '',	--主键字段名称
    @SearchCondition    VARCHAR(2000),    --查询条件
    @OrderExpression    VARCHAR(1000),    --排序表达式
    @PageIndex            INT = 1,        --页号,从1开始
    @PageSize            INT = 10,        --页尺寸
	@RecordNum            INT = 0 OUTPUT        --记录数
AS
BEGIN
    IF @SearchCondition <> ''
    BEGIN
        SET @SearchCondition = ' WHERE ' + @SearchCondition
    END
  
    IF @OrderExpression <> ''
    BEGIN
        SET @OrderExpression = ' ORDER BY ' + @OrderExpression
    END
    --PRINT @OrderExpression

    IF @PageIndex IS NULL OR @PageIndex < 1
    BEGIN
        SET @PageIndex = 1
    END
    --PRINT @PageIndex
    IF @PageSize IS NULL OR @PageSize < 1
    BEGIN
        SET @PageSize = 10
    END
    --PRINT  @PageSize

    DECLARE @SqlQuery NVARCHAR(4000)

	IF @PrimaryKeyField <> ''
	BEGIN
		SET @SqlQuery=
		'SELECT '+@SelectList+' FROM (
			SELECT ' + @PrimaryKeyField + ' AS PaginationTablePrimayKey FROM 
				(SELECT '+@PrimaryKeyField+',ROW_NUMBER() OVER( '+ @OrderExpression +') AS RowNumber FROM '+ @TableSource + ' '+ @SearchCondition +') RowNumberTable
			WHERE RowNumber BETWEEN ' + CAST(((@PageIndex - 1)* @PageSize+1) AS VARCHAR) + ' AND ' + CAST((@PageIndex * @PageSize) AS VARCHAR)+')
		 AS PrimaryKeyTable INNER JOIN '+ @TableSource + ' ON PaginationTablePrimayKey = ' + @PrimaryKeyField
	END
	ELSE
	BEGIN
		SET @SqlQuery='SELECT '+@SelectList+' 
		FROM 
			(SELECT '+@SelectList+',ROW_NUMBER() OVER( '+ @OrderExpression +') AS RowNumber 
			  FROM '+ @TableSource+' '+ @SearchCondition +') AS RowNumberTableSource 
		WHERE RowNumber BETWEEN ' + CAST(((@PageIndex - 1)* @PageSize+1) AS VARCHAR) 
		+ ' AND ' + 
		CAST((@PageIndex * @PageSize) AS VARCHAR) 
	END

    --PRINT @SqlQuery
    --SET NOCOUNT ON
    EXECUTE(@SqlQuery)
    --SET NOCOUNT OFF
    IF @RecordNum <= 0
	BEGIN
		SET @SqlQuery='SELECT  @R = COUNT(1) from ' + @TableSource  + ' ' + @SearchCondition
	    
		EXECUTE sp_executesql @SqlQuery, N'@R int output',
						  @RecordNum output
	END
END
go

