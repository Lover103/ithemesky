package com.nd.web.iphone.view.component
{
	import flash.events.Event;

	public class ScaleEvent extends Event
	{
		public static const SCALE_CHANGED:String="Scale_Changed";

		public function ScaleEvent(type:String, scale:int=100)
		{
			this.CurrentScale=scale;
			super(type);
		}
		public var CurrentScale:int=100;

	}
}