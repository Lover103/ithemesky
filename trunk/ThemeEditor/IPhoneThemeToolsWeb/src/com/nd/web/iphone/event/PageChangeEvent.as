package com.nd.web.iphone.event
{
	import flash.events.Event;

	public class PageChangeEvent extends Event
	{
		public var pageIndex:int=1;
		public var pageSize:int;
		public static const CHANGED:String="PageChange";

		public function PageChangeEvent()
		{
			super(CHANGED);
		}

		/**
		 * Override the inherited clone() method, but don't return any state.
		 */
		override public function clone():Event
		{
			return new PageChangeEvent();
		}
	}
}