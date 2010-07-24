package com.nd.web.iphone.command
{
	import com.adobe.cairngorm.commands.ICommand;
	import com.adobe.cairngorm.control.CairngormEvent;
	import com.nd.web.iphone.business.ThemeDelegate;
	import com.nd.web.iphone.model.ThemeModelLocator;
	import com.nd.web.iphone.util.ThemeParseUtil;

	import flash.utils.ByteArray;

	import mx.rpc.IResponder;

	/**
	 * @version	$Revision: $
	 */
	public class GenerateThemeCommand implements ICommand, IResponder
	{
		[Bindable]
		private var model:ThemeModelLocator=ThemeModelLocator.getInstance();

		public function GenerateThemeCommand()
		{

		}

		public function execute(event:CairngormEvent):void
		{
			var uploadObj:Object=ThemeParseUtil.getUploadIconArrayList();
			var delegate:ThemeDelegate=new ThemeDelegate(this);
			var id:int=model.curThemeId;
			var cid:int=model.curThemeThemeTypeId;
			var name:String=model.curThemeName;
			var author:String=model.curThemeAuthorName;
			var action:int=model.curThemeIsPublish;
			var screenShot:ByteArray=model.curIphoneScreenShot;
			delegate.GenerateTheme(id, cid, name, author, action, uploadObj, screenShot);
		}

		public function result(event:Object):void
		{
			if (event.result['id'] != null && event.result['id'] != '')
			{
				model.curThemeId=event.result['id'];
			}
			else
			{
				model.curThemeId=-1;
			}
			model.curThemeGenerateStatus=event.result['status'];
			model.curThemeGenerateInfo=event.result['info'];
			model.curThemeDownloadUrl=event.result['url'];
			model.themInfoCavSelectedIndex=0;
			model.isCurThemeInfoVisible=true;
		}

		public function fault(event:Object):void
		{
			model.curThemeGenerateStatus=-1;
			model.curThemeGenerateInfo='服务器内部错误~主题保存失败。';
			model.themInfoCavSelectedIndex=0;
			model.isCurThemeInfoVisible=true;
		}
	}

}