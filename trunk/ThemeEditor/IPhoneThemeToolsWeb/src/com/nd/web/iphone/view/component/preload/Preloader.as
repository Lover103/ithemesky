package com.nd.web.iphone.view.component.preload
{
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.events.ProgressEvent;
	import flash.text.TextField;
	import flash.text.TextFormat;

	import mx.events.FlexEvent;
	import mx.preloaders.DownloadProgressBar;

	public class Preloader extends DownloadProgressBar
	{
		//显示进度的文字
		private var progressText:TextField;
		//进度条
		public var img:WelcomeScreen;
		//logo页面
		public var logo:WelcomeLogo;

		//private var _timer:Timer;

		public function Preloader()
		{
			super();
			//加入logo
			logo=new WelcomeLogo();
			this.addChild(logo);
			//加入进度条
			img=new WelcomeScreen();
			this.addChild(img);
			//加入进度文字
			var style:TextFormat=new TextFormat(null, '11', 0xffffff, null, null, null, null, null, "center");
			progressText=new TextField();
			progressText.defaultTextFormat=style;
			progressText.x=310;
			progressText.y=170;
			this.addChild(progressText);
		}

		/**
		 * override这个函数，来实现自己Preloader的设置，而不是用其默认的设置
		 */
		override public function set preloader(value:Sprite):void
		{
			value.addEventListener(ProgressEvent.PROGRESS, progHandler);
			value.addEventListener(FlexEvent.INIT_COMPLETE, initCompleteHandler);
			//在这里设置预载界面居中
			//如果在初始化函数中设置，会有stageWidth和最终界面大小不一致的错误，而导致不能居中
			x=(stageWidth / 2) - (300 / 2);
			y=(stageHeight / 2) - (180 / 2);
		}

		private function progHandler(e:ProgressEvent):void
		{
			//计算进度，并且设置文字进度和进度条的进度。
			var prog:Number=e.bytesLoaded / e.bytesTotal * 100;
			progressText.text="已加载 " + String(int(prog)) + "%";
			if (img)
			{
				img.progress=prog;
				img.refresh();
			}
		}

		private function compHandler(e:Event):void
		{

		}

		private function initCompleteHandler(e:FlexEvent):void
		{
			//如果载入完毕，则停止刷新
			img.ready=true;

			//_timer.stop();
			//测试专用。下载完毕后，不马上跳到程序的默认界面。而是停顿10秒后再跳入。
//			var timer:Timer=new Timer(5000, 1);
//			timer.addEventListener(TimerEvent.TIMER, dispatchComplete);
//			timer.start();

			//分发这个事件，来通知程序已经完全下载，可以进入程序的默认界面了
			this.dispatchEvent(new Event(Event.COMPLETE));
		}

		private function initProgHandler(e:FlexEvent):void
		{

		}

		//测试专用,分发这个事件，来通知程序已经完全下载，可以进入程序的默认界面了
//		private function dispatchComplete(event:TimerEvent):void
//		{
//			this.dispatchEvent(new Event(Event.COMPLETE));
//		}
	}
}