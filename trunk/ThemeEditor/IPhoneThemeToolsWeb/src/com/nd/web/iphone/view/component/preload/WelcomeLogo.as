package com.nd.web.iphone.view.component.preload
{
	import flash.display.Loader;
	import flash.utils.ByteArray;

	public class WelcomeLogo extends Loader
	{
		//加载界面欢迎图片，大小400*200
		[Embed(source="assets/images/welcome.jpg", mimeType="application/octet-stream")]
		public var WelcomeScreenGraphic:Class;

		public function WelcomeLogo()
		{
			this.loadBytes(new WelcomeScreenGraphic() as ByteArray);
		}
	}
}