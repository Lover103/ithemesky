package com.nd.web.iphone.control
{
	import com.adobe.cairngorm.control.FrontController;
	import com.nd.web.iphone.command.GenerateThemeCommand;
	import com.nd.web.iphone.command.GetIconTypeCommand;
	import com.nd.web.iphone.command.GetIconsCommand;
	import com.nd.web.iphone.command.GetThemeCommand;
	import com.nd.web.iphone.command.GetThemeTypeCommand;
	import com.nd.web.iphone.event.GenerateThemeEvent;
	import com.nd.web.iphone.event.GetIconTypeEvent;
	import com.nd.web.iphone.event.GetIconsEvent;
	import com.nd.web.iphone.event.GetThemeEvent;
	import com.nd.web.iphone.event.GetThemeTypeEvent;


	public class ThemeController extends FrontController
	{
		public function ThemeController()
		{
			initialiseCommands();
		}

		public function initialiseCommands():void
		{
			addCommand(GetIconsEvent.EVENT_GET_ICONS, GetIconsCommand);
			addCommand(GetIconsEvent.EVENT_GET_WALPAPERS, GetIconsCommand);
			addCommand(GetIconsEvent.EVENT_GET_TYPEICON, GetIconsCommand);
			addCommand(GetIconsEvent.EVENT_GET_KEYWORD_WALLPAPER, GetIconsCommand);
			addCommand(GetIconTypeEvent.EVENT_GET_WALLPAPERS_TYPE, GetIconTypeCommand);
			addCommand(GetIconTypeEvent.EVENT_GET_APPICON_TYPE, GetIconTypeCommand);
			addCommand(GenerateThemeEvent.EVENT_SAVE_THEME, GenerateThemeCommand);
			addCommand(GetThemeTypeEvent.EVENT_GET_THEME_TYPE, GetThemeTypeCommand);
			addCommand(GetThemeEvent.GET_THEME_EVENT, GetThemeCommand);
		}
	}
}