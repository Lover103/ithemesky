package com.nd.web.iphone.business
{
	import com.adobe.cairngorm.business.ServiceLocator;

	import mx.rpc.IResponder;

	/**
	 * @version	$Revision: $
	 */
	public class PicDelegate
	{
		public function PicDelegate(responder:IResponder)
		{
			this.service=ServiceLocator.getInstance().getRemoteObject("picService");
			this.responder=responder;
		}

		/**
		 * 根据分类获到壁纸分布列表
		 */
		public function getWallpapersByType(type:int, order:int, page:int, size:int):void
		{
			var call:Object=service.GetPicList(type, order, page, size);
			call.addResponder(responder);
		}

		public function getWallpaperByKeyword(keyword:String, resolution:int, page:int, size:int):void
		{
			var call:Object=service.GetPicsFromBaidu(keyword, resolution, page, size);
			call.addResponder(responder)
		}

		/**
		 * 获取壁纸项级分类
		 */
		public function getWallpaperType():void
		{
			var call:Object=service.GetTopCategorys();
			call.addResponder(responder);
		}

		private var responder:IResponder;
		private var service:Object;
	}

}