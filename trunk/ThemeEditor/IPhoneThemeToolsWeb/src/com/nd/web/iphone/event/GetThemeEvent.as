package com.nd.web.iphone.event
{
	import com.adobe.cairngorm.control.CairngormEvent;

	public class GetThemeEvent extends CairngormEvent
	{
		public static const GET_THEME_EVENT:String="getThemeEvent";

		public var themeId:int;

		public function GetThemeEvent()
		{
			super(GET_THEME_EVENT);
		}

	}
}