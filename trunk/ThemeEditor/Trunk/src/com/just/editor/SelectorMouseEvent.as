package com.just.editor
{
	import flash.display.InteractiveObject;
	import flash.events.MouseEvent;

	public class SelectorMouseEvent extends MouseEvent
	{
		public static const MOVE:String="MOVE";

		public function SelectorMouseEvent(type:String, localX:Number=0, localY:Number=0)
		{
			super(type, true, false, localX, localY);
		}

	}
}