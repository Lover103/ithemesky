package com.nd.web.iphone.event
{
	import com.nd.web.iphone.vo.IconVo;

	import flash.events.Event;

	public class AppClickEvent extends Event
	{
		public static const APP_CLICK:String="App_Click";

		public var curIcon:IconVo;

		public function AppClickEvent()
		{
			super(APP_CLICK);
		}

	}
}