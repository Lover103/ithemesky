package com.nd.web.iphone.vo
{
	import flash.display.Bitmap;
	import flash.utils.ByteArray;


	//图标对象
	[Bindable]
	public class IconVo
	{
		public function IconVo()
		{
		}

		//图标标识名
		public var name:String;
		//图标文件名
		public var iconName:String;
		//图标请求id
		public var tagId:String;
		//图标url
		public var url:String;
		//图标byte
		public var sourceBmd:Bitmap;
		//默认图标二进制流
		public var defIconBytes:ByteArray;
		//宽度
		public var width:int;
		//长度
		public var heigh:int;
		//缩略图宽度
		public var thumbWidth:int;
		//缩略图长度
		public var thumbHeight:int
	}
}