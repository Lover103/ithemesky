package com.just.editor
{
	import flash.events.Event;

	public class ImageChangeEvent extends Event
	{

		public static const IMAGE_CHANGE:String="Image_Change";

		public function ImageChangeEvent()
		{
			super(IMAGE_CHANGE, true);
		}

	}
}