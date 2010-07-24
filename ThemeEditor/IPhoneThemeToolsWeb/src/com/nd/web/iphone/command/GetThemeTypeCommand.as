package com.nd.web.iphone.command
{
	import com.adobe.cairngorm.commands.ICommand;
	import com.adobe.cairngorm.control.CairngormEvent;
	import com.nd.web.iphone.business.ThemeDelegate;
	import com.nd.web.iphone.model.ThemeModelLocator;

	import mx.controls.Alert;
	import mx.rpc.IResponder;

	/**
	 * @version	$Revision: $
	 */
	public class GetThemeTypeCommand implements ICommand, IResponder
	{

		[Bindable]
		private var model:ThemeModelLocator=ThemeModelLocator.getInstance();

		public function GetThemeTypeCommand()
		{
		}

		public function execute(event:CairngormEvent):void
		{
			var delegate:ThemeDelegate=new ThemeDelegate(this);
			delegate.GetThemeTypes();
		}

		public function result(event:Object):void
		{
			model.themeTypeList=event.result;
		}

		public function fault(event:Object):void
		{
		}
	}

}