package com.ithemesky.event
{
	import flash.events.Event;
	
	public class EditorEvent extends Event
	{
		public static const BACK:String="BACK";
		public static const LOAD_COMPLETE:String = "LOAD_COMPLETE";
		
		public function EditorEvent(eventType:String)
		{
			super(eventType, true);
		}
	}
}