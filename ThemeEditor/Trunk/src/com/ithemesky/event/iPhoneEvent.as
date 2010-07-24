package com.ithemesky.event
{
	import flash.events.Event;
	
	public class iPhoneEvent extends Event
	{
		
		public static const ICON_CLICK:String="ICON_CLICK";
		public static const BACKGROUND_CLICK:String = "BACKGROUND_CLICK";
		
		public var Image:String;
		public var OffsetX:Number;
		public var OffsetY:Number;
		public function iPhoneEvent(type:String, image:String, offsetX:Number = 0, offsetY:Number=0)
		{
			Image = image;
			OffsetX = offsetX;
			OffsetY = offsetY;
			super(type, true);
		}
		
	}
}