package com.nd.web.iphone.util
{
	import com.nd.web.iphone.model.ThemeModelLocator;
	import com.nd.web.iphone.vo.IconVo;

	import flash.display.Bitmap;
	import flash.display.Loader;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.net.URLRequest;
	import flash.utils.ByteArray;


	public class ThemeParseUtil
	{
		private static var model:ThemeModelLocator=ThemeModelLocator.getInstance();

		private static var loader:Loader;

		private static var curThemeObj;

		public function ThemeParseUtil()
		{
		}


		/**
		 * 获取要上传的图标列表
		 */
		public static function getUploadIconArrayList():Object
		{
			var uploadIconObj:Object=new Object();
			packageUploadIcon(uploadIconObj, model.curTheme.wallpaper);
			packageUploadIcon(uploadIconObj, model.curTheme.statusBar);
			packageUploadIcon(uploadIconObj, model.curTheme.dock);
			packageUploadIcon(uploadIconObj, model.curTheme.appStore);
			packageUploadIcon(uploadIconObj, model.curTheme.calculator);
			packageUploadIcon(uploadIconObj, model.curTheme.calendar);
			packageUploadIcon(uploadIconObj, model.curTheme.camera);
			packageUploadIcon(uploadIconObj, model.curTheme.clock);
			packageUploadIcon(uploadIconObj, model.curTheme.contacts);
			packageUploadIcon(uploadIconObj, model.curTheme.iPod);
			packageUploadIcon(uploadIconObj, model.curTheme.iTunes);
			packageUploadIcon(uploadIconObj, model.curTheme.mail);
			packageUploadIcon(uploadIconObj, model.curTheme.maps);
			packageUploadIcon(uploadIconObj, model.curTheme.notes);
			packageUploadIcon(uploadIconObj, model.curTheme.phone);
			packageUploadIcon(uploadIconObj, model.curTheme.photos);
			packageUploadIcon(uploadIconObj, model.curTheme.safari);
			packageUploadIcon(uploadIconObj, model.curTheme.settings);
			packageUploadIcon(uploadIconObj, model.curTheme.stocks);
			packageUploadIcon(uploadIconObj, model.curTheme.text);
			packageUploadIcon(uploadIconObj, model.curTheme.weather);
			packageUploadIcon(uploadIconObj, model.curTheme.youTube);
			return uploadIconObj;
		}

		private static function packageUploadIcon(uploadIconObj:Object, icon:IconVo):void
		{
			var iconBytes:ByteArray=ImageUtil.encodeByteArray(icon.sourceBmd.bitmapData);
			if (icon.defIconBytes.toString() != iconBytes.toString())
			{
				if (icon.name == 'wallpaper' || icon.name == 'statusbar' || icon.name == 'dock')
				{
					uploadIconObj[icon.iconName + ".png"]=iconBytes;
				}
				else
				{
					uploadIconObj["Icons/" + icon.iconName + ".png"]=iconBytes;
				}
			}
		}

		/**
		 * 解析服务器返回的主题，解析到当前编辑主题中
		 */
		public static function parseTheme(themeObj:Object):void
		{
			curThemeObj=themeObj;
			model.curThemeAuthorName=themeObj['theme_author'];
			model.curThemeName=themeObj['theme_name'];
			model.curThemeThemeTypeId=themeObj['theme_cateid'];
			loader=new Loader();
			loader.contentLoaderInfo.addEventListener(Event.COMPLETE, comLoader);
			loader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, ioerrorHandler);
			loadImage();
		}

		private static function ioerrorHandler(event:IOErrorEvent):void
		{
			curThemeObj[curLoadPicName]=null;
			loadImage();
		}

		private static var curLoadPicName:String='';

		private static function loadImage():void
		{
			if (curThemeObj['appstore'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['appstore']));
				curLoadPicName='appstore';
			}
			else if (curThemeObj['calculator'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['calculator']));
				curLoadPicName='calculator';
			}
			else if (curThemeObj['calendar'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['calendar']));
				curLoadPicName='calendar';
			}
			else if (curThemeObj['camera'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['camera']));
				curLoadPicName='camera';
			}
			else if (curThemeObj['clock'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['clock']));
				curLoadPicName='clock';
			}
			else if (curThemeObj['contacts'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['contacts']));
				curLoadPicName='contacts';
			}
			else if (curThemeObj['dock'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['dock']));
				curLoadPicName='dock';
			}
			else if (curThemeObj['ipod'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['ipod']));
				curLoadPicName='ipod';
			}
			else if (curThemeObj['itunes'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['itunes']));
				curLoadPicName='itunes';
			}
			else if (curThemeObj['mail'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['mail']));
				curLoadPicName='mail';
			}
			else if (curThemeObj['maps'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['maps']));
				curLoadPicName='maps';
			}
			else if (curThemeObj['notes'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['notes']));
				curLoadPicName='notes';
			}
			else if (curThemeObj['phone'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['phone']));
				curLoadPicName='phone';
			}
			else if (curThemeObj['photos'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['photos']));
				curLoadPicName='photos';
			}
			else if (curThemeObj['safari'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['safari']));
				curLoadPicName='safari';
			}
			else if (curThemeObj['setting'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['setting']));
				curLoadPicName='setting';
			}
			else if (curThemeObj['statusbar'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['statusbar']));
				curLoadPicName='statusbar';
			}
			else if (curThemeObj['stocks'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['stocks']));
				curLoadPicName='stocks';
			}
			else if (curThemeObj['text'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['text']));
				curLoadPicName='text';
			}
			else if (curThemeObj['wallpaper'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['wallpaper']));
				curLoadPicName='wallpaper';
			}
			else if (curThemeObj['weather'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['weather']));
				curLoadPicName='weather';
			}
			else if (curThemeObj['youtube'] != null)
			{
				loader.load(new URLRequest(model.PIC_URL + curThemeObj['youtube']));
				curLoadPicName='youtube';
			}
			else
			{
				clearLoader();
			}
		}

		private static function comLoader(e:Event):void
		{
			if (e.target.url == (model.PIC_URL + curThemeObj['appstore']))
			{
				model.curTheme.mail.sourceBmd=Bitmap(e.target.content);
				curThemeObj['appstore']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['calculator']))
			{
				model.curTheme.calculator.sourceBmd=Bitmap(e.target.content);
				curThemeObj['calculator']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['calendar']))
			{
				model.curTheme.calendar.sourceBmd=Bitmap(e.target.content);
				curThemeObj['calendar']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['camera']))
			{
				model.curTheme.camera.sourceBmd=Bitmap(e.target.content);
				curThemeObj['camera']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['clock']))
			{
				model.curTheme.clock.sourceBmd=Bitmap(e.target.content);
				curThemeObj['clock']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['contacts']))
			{
				model.curTheme.contacts.sourceBmd=Bitmap(e.target.content);
				curThemeObj['contacts']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['dock']))
			{
				model.curTheme.dock.sourceBmd=Bitmap(e.target.content);
				curThemeObj['dock']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['ipod']))
			{
				model.curTheme.iPod.sourceBmd=Bitmap(e.target.content);
				curThemeObj['ipod']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['itunes']))
			{
				model.curTheme.iTunes.sourceBmd=Bitmap(e.target.content);
				curThemeObj['itunes']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['mail']))
			{
				model.curTheme.mail.sourceBmd=Bitmap(e.target.content);
				curThemeObj['mail']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['maps']))
			{
				model.curTheme.maps.sourceBmd=Bitmap(e.target.content);
				curThemeObj['maps']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['notes']))
			{
				model.curTheme.notes.sourceBmd=Bitmap(e.target.content);
				curThemeObj['notes']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['phone']))
			{
				model.curTheme.phone.sourceBmd=Bitmap(e.target.content);
				curThemeObj['phone']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['photos']))
			{
				model.curTheme.photos.sourceBmd=Bitmap(e.target.content);
				curThemeObj['photos']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['safari']))
			{
				model.curTheme.safari.sourceBmd=Bitmap(e.target.content);
				curThemeObj['safari']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['setting']))
			{
				model.curTheme.settings.sourceBmd=Bitmap(e.target.content);
				curThemeObj['setting']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['statusbar']))
			{
				model.curTheme.statusBar.sourceBmd=Bitmap(e.target.content);
				curThemeObj['statusbar']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['stocks']))
			{
				model.curTheme.stocks.sourceBmd=Bitmap(e.target.content);
				curThemeObj['stocks']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['text']))
			{
				model.curTheme.text.sourceBmd=Bitmap(e.target.content);
				curThemeObj['text']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['wallpaper']))
			{
				model.curTheme.wallpaper.sourceBmd=Bitmap(e.target.content);
				curThemeObj['wallpaper']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['weather']))
			{
				model.curTheme.weather.sourceBmd=Bitmap(e.target.content);
				curThemeObj['weather']=null;
				loadImage();
			}
			else if (e.target.url == (model.PIC_URL + curThemeObj['youtube']))
			{
				model.curTheme.youTube.sourceBmd=Bitmap(e.target.content);
				curThemeObj['youtube']=null;
				loadImage();
			}
		}

		private static function clearLoader():void
		{
			loader.removeEventListener(Event.COMPLETE, comLoader);
		}
	}
}