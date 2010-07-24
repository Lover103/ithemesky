package com.nd.web.iphone.command
{
	import com.adobe.cairngorm.commands.ICommand;
	import com.adobe.cairngorm.control.CairngormEvent;
	import com.nd.web.iphone.business.PicDelegate;
	import com.nd.web.iphone.business.ThemeDelegate;
	import com.nd.web.iphone.event.GetIconsEvent;
	import com.nd.web.iphone.model.ThemeModelLocator;

	import mx.rpc.IResponder;

	/**
	 * @version	$Revision: $
	 */
	public class GetIconsCommand implements ICommand, IResponder
	{
		[Bindable]
		private var model:ThemeModelLocator=ThemeModelLocator.getInstance();

		public function GetIconsCommand()
		{
		}

		public function execute(event:CairngormEvent):void
		{
			var eventType:String=GetIconsEvent(event).eventType;
			var page:int=GetIconsEvent(event).page;
			var size:int=GetIconsEvent(event).size;

			var order:int=GetIconsEvent(event).order;
			//获取应用图标
			if (eventType == GetIconsEvent.EVENT_GET_ICONS)
			{
				var themeDelegate:ThemeDelegate=new ThemeDelegate(this);
				var iconTag:String=GetIconsEvent(event).iconTag;
				themeDelegate.GetIconsByTagName(iconTag, order, page, size);
			}
			//获取分类图标
			else if (eventType == GetIconsEvent.EVENT_GET_TYPEICON)
			{
				var themeDelegate:ThemeDelegate=new ThemeDelegate(this);
				var appIconType:int=GetIconsEvent(event).appIconType;
				themeDelegate.GetThemeIconsByCategory(appIconType, order, page, size);
			}
			//获取壁纸图标
			else if (eventType == GetIconsEvent.EVENT_GET_WALPAPERS)
			{
				var picDelegate:PicDelegate=new PicDelegate(this);
				var type:int=GetIconsEvent(event).wallpaperType;
				picDelegate.getWallpapersByType(type, order, page, size);
			}
			//获取搜索壁纸图标
			else if (eventType == GetIconsEvent.EVENT_GET_KEYWORD_WALLPAPER)
			{
				var picDelegate:PicDelegate=new PicDelegate(this);
				var keyword:String=GetIconsEvent(event).wallpaperKeyword;
				var resolution:int=GetIconsEvent(event).resolution;
				picDelegate.getWallpaperByKeyword(keyword, resolution, page, size);
			}
			;
		}

		public function result(event:Object):void
		{
			for (var i:String in event.result['list'])
			{
				event.result['list'][i]=new ObjectProxy(event.result['list'][i]);
			}
			model.curPageList=event.result['list'];
			var totalRecord:int=event.result['count'];
			if ((totalRecord % model.defaultPageSize) > 0)
			{
				model.curPageCount=totalRecord / model.defaultPageSize + 1;
			}
			else
			{
				model.curPageCount=totalRecord / model.defaultPageSize;
			}
		}

		public function fault(event:Object):void
		{
		}
	}

}