package com.nd.web.iphone.event
{
	import com.adobe.cairngorm.control.CairngormEvent;

	import flash.events.Event;

	public class GetIconsEvent extends CairngormEvent
	{
		public static var EVENT_GET_ICONS:String="getIcons";

		public static var EVENT_GET_WALPAPERS:String="getWallpapers";

		public static var EVENT_GET_KEYWORD_WALLPAPER:String="getKeywordWallpaper";

		public static var EVENT_GET_TYPEICON:String="getTypeIcons";

		//壁纸类型
		public var wallpaperType:int;

		//搜索壁纸关键字
		public var wallpaperKeyword:String;

		//壁纸分辨率
		public var resolution:int;

		//应用图标分类
		public var appIconType:int;

		//应用图标标签
		public var iconTag:String;

		//结果排序顺序
		public var order:int=0;

		//第几页
		public var page:int;

		//每页大小
		public var size:int;

		//事件类型
		public var eventType:String;

		/**
		 * Constructor.
		 */
		public function GetIconsEvent(type:String)
		{
			eventType=type;
			super(eventType);
		}

		/**
		 * Override the inherited clone() method, but don't return any state.
		 */
		override public function clone():Event
		{
			return new GetIconsEvent(eventType);
		}
	}

}