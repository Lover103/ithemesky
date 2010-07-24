package com.nd.web.iphone.model
{
	import com.adobe.cairngorm.model.ModelLocator;
	import com.nd.web.iphone.view.assets.ThemeAssets;
	import com.nd.web.iphone.vo.IconVo;
	import com.nd.web.iphone.vo.ThemeVo;

	import flash.utils.ByteArray;

	import mx.collections.ArrayCollection;

	[Bindable]
	public class ThemeModelLocator implements ModelLocator
	{
		private static var modelLocator:ThemeModelLocator;

		//客户端图片资源索引
		public var assets:ThemeAssets;
		//应用图标集合
		public var appIconList:ArrayCollection;

		//当前主题类
		public var curTheme:ThemeVo;

		//当前修改的图标
		public var curEditIcon:IconVo;

		//图片类型集合用于绑定到视图
		public var iconTypeList:ArrayCollection;
		//壁纸分类集合
		public var wallpaperTypeList:ArrayCollection;
		//主题分类集合
		public var themeTypeList:ArrayCollection;
		//应用图标分类
		public var appIconTypeList:ArrayCollection;
		//壁纸搜索框是否可见
		public var iconSearchVisible:Boolean;

		//当前选择的工具栏viewstack索引（icondesinger右侧）,0工具栏，1图标分类，2在线图标
		public var curToolbarIndex:int=0;

		//当前壁纸分页结果
		public var curPageList:ArrayCollection;
		//当前壁纸总数
		public var curPageCount:int;
		//当前处于第几页
		public var curPageNumber:int;

		//当前请求壁纸类型
		public var curWallpaperType:int;
		//壁纸搜索关键字
		public var curWallpaperKeyword:String;
		//壁纸搜索分辨率 1:320x480,2:240x320,5:480x640,6:640x480,...
		public var curWallpaperResolution:int=1;
		//当前请求壁纸排序（4，按时间倒序 ）
		public var defaultWallpaperOrder:int=4;
		//当前请求图标的排序（2，id倒序,即时间倒序）
		public var defaultIconOrder:int=2;

		//当前请求应用图标分类
		public var curAppIconType:int;

		//默认一页多少条记录
		public var defaultPageSize:int=8;

		//当前在线应用图标使用状态， 0，应用图标，1，分类图标
		public var curOnlineAppIconState:int=0;
		//当前在线壁纸状态， 0，在线壁纸，1，关键字搜索壁纸
		public var curOnlineWallpaperState:int=0;

		//当前图标分类状态，0，壁纸分类，1，应用图标分类
		public var curIconTypeState:int=0;

		//图片请求服务域url           
		public var PIC_URL:String="http://192.168.54.136/Service/Getpic.ashx?url=";

		//当前主题id,-1代表新增主题
		public var curThemeId:int=-1;

		//当前主题名称
		public var curThemeName:String="";

		//当前主题作者名称
		public var curThemeAuthorName:String="";

		//当前主题分类
		public var curThemeThemeTypeId:int;

		//是提交当前是主题审核发布0草稿，1提交审核
		public var curThemeIsPublish:int;

		//当前主题的下载地址
		public var curThemeDownloadUrl:String;

		//当前主题保存状态 0：成功，-1：失败
		public var curThemeGenerateStatus:int=0;

		//当前主题状态信息
		public var curThemeGenerateInfo:String="";

		//当前主题状态悠是否显示
		public var isCurThemeInfoVisible:Boolean=false;

		//当前屏幕截图
		public var curIphoneScreenShot:ByteArray;

		//主题制作区界面切换
		public var themInfoCavSelectedIndex:int=0;

		public static function getInstance():ThemeModelLocator
		{
			if (modelLocator == null)
			{
				modelLocator=new ThemeModelLocator();
			}
			return modelLocator;
		}

		public function ThemeModelLocator()
		{
			if (modelLocator != null)
			{
				throw new Error("Only one ShopModelLocator instance should be instantiated");
			}
			assets=new ThemeAssets();
			curTheme=new ThemeVo();
			curTheme.fillDefault(this);
		}
	}
}