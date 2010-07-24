package com.nd.web.iphone.event
{
	import com.adobe.cairngorm.control.CairngormEvent;

	import flash.events.Event;

	public class GetThemeTypeEvent extends CairngormEvent
	{
		public static var EVENT_GET_THEME_TYPE:String="getThemeType";

		/**
		 * Constructor.
		 */
		public function GetThemeTypeEvent()
		{
			super(EVENT_GET_THEME_TYPE);
		}

		/**
		 * Override the inherited clone() method, but don't return any state.
		 */
		override public function clone():Event
		{
			return new GetThemeTypeEvent();
		}
	}

}