package com.ithemesky.event
{
	import flash.display.Bitmap;
	import flash.events.Event;
	
	import mx.core.UIComponent;
	
	public class iPhoneEvent extends Event
	{
		
		public static const ICON_CLICK:String="ICON_CLICK";
		public static const BACKGROUND_CLICK:String = "BACKGROUND_CLICK";
		public static const STATUS_BAR_CLICK:String = "STATUS_BAR_CLICK";
		public static const DOCK_CLICK:String = "DOCK_CLICK";
		
		public var Image:Bitmap;
		public var OffsetX:Number;
		public var OffsetY:Number;
		public var Control:UIComponent;
		public function iPhoneEvent(type:String, image:Bitmap, control:UIComponent, offsetX:Number = 0, offsetY:Number=0)
		{
			Image = image;
			OffsetX = offsetX;
			OffsetY = offsetY;
			Control = control;
			super(type, true);
		}
		
	}
}