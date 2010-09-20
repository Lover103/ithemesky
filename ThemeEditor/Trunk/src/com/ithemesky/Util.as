package com.ithemesky
{	
	
	import flash.display.Bitmap;
	import flash.display.BitmapData;
	import flash.utils.ByteArray;
	
	import mx.core.UIComponent;
	import mx.graphics.codec.PNGEncoder;

	public class Util
	{
		public static function UIComponentToBitmap(target:UIComponent):Bitmap{
			
			var imageWidth:uint = target.width; 
			var imageHeight:uint = target.height; 
			var srcBmp:BitmapData = new BitmapData( imageWidth, imageHeight ); 
			
			srcBmp.draw( target );
			return new Bitmap(srcBmp);
		} 
		
		public static function BitmapDataToByteArray(target:Bitmap):ByteArray{
			var encode:PNGEncoder = new PNGEncoder();
			return encode.encode(target.bitmapData);
		} 
		
	}
}