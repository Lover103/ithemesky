package com.nd.web.iphone.command
{
	import com.adobe.cairngorm.commands.ICommand;
	import com.adobe.cairngorm.control.CairngormEvent;
	import com.nd.web.iphone.business.PicDelegate;
	import com.nd.web.iphone.business.ThemeDelegate;
	import com.nd.web.iphone.event.GetIconTypeEvent;
	import com.nd.web.iphone.model.ThemeModelLocator;

	import mx.rpc.IResponder;
	import mx.utils.ObjectProxy;

	/**
	 * @version	$Revision: $
	 */
	public class GetIconTypeCommand implements ICommand, IResponder
	{

		[Bindable]
		private var model:ThemeModelLocator=ThemeModelLocator.getInstance();

		public function GetIconTypeCommand()
		{
		}

		public function execute(event:CairngormEvent):void
		{
			var eventType:String=GetIconTypeEvent(event).evenType;

			if (eventType == GetIconTypeEvent.EVENT_GET_WALLPAPERS_TYPE)
			{
				var picDelegate:PicDelegate=new PicDelegate(this);
				picDelegate.getWallpaperType();
			}
			else if (eventType == GetIconTypeEvent.EVENT_GET_APPICON_TYPE)
			{
				var themeDelegate:ThemeDelegate=new ThemeDelegate(this);
				themeDelegate.GetAppIconType();
			}
		}

		public function result(event:Object):void
		{
			//请求壁纸分类
			if (model.curIconTypeState == 0)
			{
				for (var i:String in event.result)
				{
					event.result[i]=new ObjectProxy(event.result[i]);
				}
				model.wallpaperTypeList=event.result;
				var allCategorys:Object=new Object();
				allCategorys.c_id=-1;
				allCategorys.c_name='所有分类';
				model.wallpaperTypeList.addItemAt(new ObjectProxy(allCategorys), 0);
				model.iconTypeList=model.wallpaperTypeList;
			}
			else if (model.curIconTypeState == 1)
			{
				for (var i:String in event.result['list'])
				{
					event.result['list'][i]=new ObjectProxy(event.result['list'][i]);
				}
				model.appIconTypeList=event.result['list'];
				model.iconTypeList=model.appIconTypeList;
			}
		}

		public function fault(event:Object):void
		{
		}
	}

}