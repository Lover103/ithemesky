package com.nd.web.iphone.view.component
{
	import flash.events.Event;

	import mx.managers.PopUpManager;

	public class LoadingControl
	{
		private static var _loading:Loading;
		private static const EVENT_CLOSE:String="close";
		private static var isShow:Boolean;

		public static function showLoading(displayObject:*, str:String=''):void
		{
			if (_loading == null)
			{
				_loading=new Loading();
				if (str != "")
				{
					_loading.strInfo=str;
				}
				_loading.addEventListener(EVENT_CLOSE, close);
			}
			PopUpManager.addPopUp(_loading, displayObject, true);
			isShow=true;
		}

		public static function removeLoading():void
		{
			isShow=false;
			if (_loading != null)
			{
				PopUpManager.removePopUp(_loading);
			}
			_loading=null;
		}

		public static function close():void
		{
			if (isShow)
			{
				PopUpManager.removePopUp(_loading);
			}
			isShow=false;
		}

		public static function setInfo(str:String):void
		{
			if (_loading != null)
			{
				_loading.strInfo=str;
			}
		}
	}
}