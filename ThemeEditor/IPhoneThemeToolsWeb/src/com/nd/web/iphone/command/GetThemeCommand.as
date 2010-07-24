package com.nd.web.iphone.command
{
	import com.adobe.cairngorm.commands.ICommand;
	import com.adobe.cairngorm.control.CairngormEvent;
	import com.nd.web.iphone.business.ThemeDelegate;
	import com.nd.web.iphone.event.GetThemeEvent;
	import com.nd.web.iphone.model.ThemeModelLocator;
	import com.nd.web.iphone.util.ThemeParseUtil;

	import mx.rpc.IResponder;

	/**
	 * @version	$Revision: $
	 */
	public class GetThemeCommand implements ICommand, IResponder
	{

		[Bindable]
		private var model:ThemeModelLocator=ThemeModelLocator.getInstance();

		public function GetThemeCommand()
		{
		}

		public function execute(event:CairngormEvent):void
		{
			var themeId:int=GetThemeEvent(event).themeId;
			var delegate:ThemeDelegate=new ThemeDelegate(this);
			delegate.GetThemeById(themeId);
		}

		public function result(event:Object):void
		{
			if (event.result['sys_status'] == 0)
			{
				ThemeParseUtil.parseTheme(event.result);
			}
		}

		public function fault(event:Object):void
		{
		}
	}

}