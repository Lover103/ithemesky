package com.ithemesky.event
{
	import flash.events.Event;
	
	public class EditorEvent extends Event
	{
		public static const BACK:String="BACK";
		public static const ADD_ICON:String="ADD_ICON";
		public static const LOAD_COMPLETE:String = "LOAD_COMPLETE";
		public static const COMPIRE:String = "COMPIRE";
		
		public function EditorEvent(eventType:String)
		{
			super(eventType, true);
		}
	}
}