package com.nd.web.iphone.event
{
	import com.adobe.cairngorm.control.CairngormEvent;

	import flash.events.Event;

	public class GenerateThemeEvent extends CairngormEvent
	{
		public static var EVENT_SAVE_THEME:String="saveTheme";

		/**
		 * Constructor.
		 */
		public function GenerateThemeEvent()
		{
			super(EVENT_SAVE_THEME);
		}

		/**
		 * Override the inherited clone() method, but don't return any state.
		 */
		override public function clone():Event
		{
			return new GenerateThemeEvent();
		}
	}

}