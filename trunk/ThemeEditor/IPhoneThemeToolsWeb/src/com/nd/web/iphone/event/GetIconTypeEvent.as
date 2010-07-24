package com.nd.web.iphone.event
{
	import com.adobe.cairngorm.control.CairngormEvent;

	import flash.events.Event;

	public class GetIconTypeEvent extends CairngormEvent
	{
		public static var EVENT_GET_WALLPAPERS_TYPE:String="getWallpapersType";

		public static var EVENT_GET_APPICON_TYPE:String="getAppIconType";

		public var page:int;

		public var size:int;

		public var evenType:String;


		/**
		 * Constructor.
		 */
		public function GetIconTypeEvent(type:String)
		{
			evenType=type;
			super(evenType);
		}

		/**
		 * Override the inherited clone() method, but don't return any state.
		 */
		override public function clone():Event
		{
			return new GetIconTypeEvent(evenType);
		}
	}

}