package com.nd.web.iphone.util
{

	import flash.display.BitmapData;
	import flash.utils.ByteArray;

	import mx.graphics.codec.PNGEncoder;


	public class ImageUtil
	{
		public function ImageUtil()
		{
		}

		/**
		 * 从BitmapData中获取ByteArray
		 */
		public static function encodeByteArray(bitmapData:BitmapData):ByteArray
		{
			var pngEnconder:PNGEncoder=new PNGEncoder();
			var bytes:ByteArray=pngEnconder.encode(bitmapData);
			return bytes;
		}

		public static function checkFileType(bytes:ByteArray):String
		{
//			if (!bytes || bytes.length == 0)
//				return "unknown";
//			bytes.position=0;
//
//			var len:uint=Math.floor(bytes.length / 2);
//			var tmpArray:Array=FileHeaders.slice(0);
//			for (var i:uint=0; i < len; i+=2)
//			{
//				var byte:uint;
//				try
//				{
//					byte=bytes.readByte();
//				}
//				catch (e:Error)
//				{
//					break;
//				}
//				var bstr:String=byte.toString(16);
//				bstr=bstr.slice(bstr.length - 2).toUpperCase();
//				bstr=bstr.length < 2 ? "0" + bstr : bstr;
//				tmpArray=filterLetters(bstr, tmpArray, i);
//				if (tmpArray.length == 1 && tmpArray[0].header.length == i + 2)
//				{
//					return tmpArray[0].ext;
//				}
//				if (tmpArray.length == 0)
//				{
//					break;
//				}
//			}
			//
			return "unknown";
		}
	}
}