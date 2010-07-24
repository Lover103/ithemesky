package com.nd.web.iphone.business
{
	import com.adobe.cairngorm.business.ServiceLocator;

	import flash.utils.ByteArray;

	import mx.rpc.IResponder;

	/**
	 * @version	$Revision: $
	 */
	public class ThemeDelegate
	{
		public function ThemeDelegate(responder:IResponder)
		{
			this.service=ServiceLocator.getInstance().getRemoteObject("themeService");
			this.responder=responder;
		}

		/**
		 *	通过tag获取图标分页
		 */
		public function GetIconsByTagName(iconTag:String, order:int, page:int, size:int):void
		{
			this.service.showBusyCursor=true;
			var call:Object=service.GetThemeIconsByTagName(iconTag, order, page, size);
			call.addResponder(responder);
		}

		/**
		 * 通过分类获取图标分布
		 */
		public function GetThemeIconsByCategory(iconTypeId:int, order:int, page:int, size:int):void
		{
			this.service.showBusyCursor=true;
			var call:Object=service.GetThemeIconsByCategory(iconTypeId, order, page, size);
			call.addResponder(responder);
		}

		/**
		 * 获取主题顶级分类
		 */
		public function GetThemeTypes():void
		{
			this.service.showBusyCursor=false;
			var call:Object=service.GetTopCategorys();
			call.addResponder(responder);
		}

		/**
		 * 获取默认主题
		 */
		public function GetDefaultThem():void
		{
			this.service.showBusyCursor=true;
			var call:Object=service.GetDefaultTheme();
			call.addResponder(responder);
		}

		/**
		 *
		 */
		public function GetThemeById(themeId:int):void
		{
			this.service.showBusyCursor=false;
			var call:Object=service.GetThemeInfo(themeId);
			call.addResponder(responder);
		}

		/**
		 * 获取应用图标类型
		 */
		public function GetAppIconType():void
		{
			this.service.showBusyCursor=true;
			var call:Object=service.GetThemeIconCategory();
			call.addResponder(responder);
		}

		/// <summary>
		/// 保存主题
		/// </summary>
		/// <param name="id">小于1表示新增主题</param>
		/// <param name="cid">主题分类</param>
		/// <param name="name">主题名称</param>
		/// <param name="author">作者</param>
		/// <param name="action">0:草稿；1:提交审核</param>
		/// <param name="obj"><remarks>Dictionary<string, byte[]></remarks></param>
		/// <returns>key说明(status:保存状态[0：成功；-1：失败];info:返回说明信息;url:生成主题包的url地址;id:主题ID)</returns>
		public function GenerateTheme(id:int, cid:int, name:String, author:String, action:int, uploadObj:Object, screenShot:ByteArray):void
		{
			this.service.showBusyCursor=true;
			var call:Object=service.SaveTheme(id, cid, name, author, action, uploadObj, screenShot);
			call.addResponder(responder);
		}

		private var responder:IResponder;
		private var service:Object;
	}

}