package com.ithemesky.event
{
	import flash.events.Event;
	
	public class iPhoneEvent extends Event
	{
		
		public static const ICON_CLICK:String="ICON_CLICK";
		
		public var Image:String;
		public var Type:Number;
		public var OffsetX:Number;
		public var OffsetY:Number;
		public function iPhoneEvent(image:String, type:Number, offsetX:Number = 0, offsetY:Number=0)
		{
			Image = image;
			Type = type;
			OffsetX = offsetX;
			OffsetY = offsetY;
			super(ICON_CLICK, true);
		}
		
	}
}