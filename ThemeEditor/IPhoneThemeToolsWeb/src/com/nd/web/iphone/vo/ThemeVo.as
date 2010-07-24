package com.nd.web.iphone.vo
{
	import com.nd.web.iphone.model.ThemeModelLocator;
	import com.nd.web.iphone.util.ImageUtil;

	import flash.display.Bitmap;


	//主题对象，一个主题包含
	[Bindable]
	public class ThemeVo
	{
		//顶部状态栏
		public var statusBar:IconVo;
		//壁纸
		public var wallpaper:IconVo;
		//底部Dock
		public var dock:IconVo;
		//应用图标AppStore
		public var appStore:IconVo;
		//应用图标Caluculator
		public var calculator:IconVo;
		//应用图标Calendar
		public var calendar:IconVo;
		//应用图标Camera
		public var camera:IconVo;
		//应用图标Clock
		public var clock:IconVo;
		//应用图标Contacts
		public var contacts:IconVo;
		//应用图标ipod
		public var iPod:IconVo;
		//应用图标itunes
		public var iTunes:IconVo;
		//应用图标mail
		public var mail:IconVo;
		//应用图标maps
		public var maps:IconVo;
		//应用图标notes
		public var notes:IconVo;
		//应用图标phone
		public var phone:IconVo;
		//应用图标photos
		public var photos:IconVo;
		//应用图标safari
		public var safari:IconVo;
		//应用图标setting
		public var settings:IconVo;
		//应用图标stocks
		public var stocks:IconVo;
		//应用图标text
		public var text:IconVo;
		//应用图标weather
		public var weather:IconVo;
		//应用图标youTube
		public var youTube:IconVo;

		public function ThemeVo()
		{
			statusBar=new IconVo();
			statusBar.name='statusbar';
			statusBar.iconName='StatusBar';
			statusBar.width=320;
			statusBar.heigh=20;
			statusBar.thumbWidth=95;
			statusBar.thumbHeight=20;

			wallpaper=new IconVo();
			wallpaper.name='wallpaper';
			wallpaper.iconName='Wallpaper';
			wallpaper.width=320;
			wallpaper.heigh=480;
			wallpaper.thumbWidth=93;
			wallpaper.thumbHeight=150;

			dock=new IconVo();
			dock.name='dock';
			dock.iconName='Dock';
			dock.width=320;
			dock.heigh=91;
			dock.thumbWidth=95;
			dock.thumbHeight=50;

			appStore=new IconVo();
			appStore.name='appstore';
			appStore.iconName='App Store';
			appStore.width=60;
			appStore.heigh=60;
			appStore.thumbWidth=60;
			appStore.thumbHeight=60;

			calculator=new IconVo();
			calculator.name='calculator';
			calculator.iconName='Calculator';
			calculator.width=60;
			calculator.heigh=60;
			calculator.thumbWidth=60;
			calculator.thumbHeight=60;

			calendar=new IconVo();
			calendar.name='calendar';
			calendar.iconName='Calendar';
			calendar.width=60;
			calendar.heigh=60;
			calendar.thumbWidth=60;
			calendar.thumbHeight=60;

			camera=new IconVo();
			camera.name='camera';
			camera.iconName='Camera';
			camera.width=60;
			camera.heigh=60;
			camera.thumbWidth=60;
			camera.thumbHeight=60;

			clock=new IconVo();
			clock.name='clock';
			clock.iconName='Clock';
			clock.width=60;
			clock.heigh=60;
			clock.thumbWidth=60;
			clock.thumbHeight=60;

			contacts=new IconVo();
			contacts.name='contacts';
			contacts.iconName='Contacts';
			contacts.width=60;
			contacts.heigh=60;
			contacts.thumbWidth=60;
			contacts.thumbHeight=60;

			iPod=new IconVo();
			iPod.name='ipod';
			iPod.iconName='iPod';
			iPod.width=60;
			iPod.heigh=60;
			iPod.thumbWidth=60;
			iPod.thumbHeight=60;

			iTunes=new IconVo();
			iTunes.name='itunes';
			iTunes.iconName='iTunes';
			iTunes.width=60;
			iTunes.heigh=60;
			iTunes.thumbWidth=60;
			iTunes.thumbHeight=60;

			mail=new IconVo();
			mail.name='mail';
			mail.iconName='Mail';
			mail.width=60;
			mail.heigh=60;
			mail.thumbWidth=60;
			mail.thumbHeight=60;

			maps=new IconVo();
			maps.name='maps';
			maps.iconName='Maps';
			maps.width=60;
			maps.heigh=60;
			maps.thumbWidth=60;
			maps.thumbHeight=60;

			notes=new IconVo();
			notes.name='notes';
			notes.iconName='Notes';
			notes.width=60;
			notes.heigh=60;
			notes.thumbWidth=60;
			notes.thumbHeight=60;

			phone=new IconVo();
			phone.name='phone';
			phone.iconName='Phone';
			phone.width=60;
			phone.heigh=60;
			phone.thumbWidth=60;
			phone.thumbHeight=60;

			photos=new IconVo();
			photos.name='photos';
			photos.iconName='Photos';
			photos.width=60;
			photos.heigh=60;
			photos.thumbWidth=60;
			photos.thumbHeight=60;

			safari=new IconVo();
			safari.name='safari';
			safari.iconName='Safari';
			safari.width=60;
			safari.heigh=60;
			safari.thumbWidth=60;
			safari.thumbHeight=60;

			settings=new IconVo();
			settings.name='settings';
			settings.iconName='Settings';
			settings.width=60;
			settings.heigh=60;
			settings.thumbWidth=60;
			settings.thumbHeight=60;

			stocks=new IconVo();
			stocks.name='stocks';
			stocks.iconName='Stocks';
			stocks.width=60;
			stocks.heigh=60;
			stocks.thumbWidth=60;
			stocks.thumbHeight=60;

			text=new IconVo();
			text.name='text';
			text.iconName='Text';
			text.width=60;
			text.heigh=60;
			text.thumbWidth=60;
			text.thumbHeight=60;

			weather=new IconVo();
			weather.name='weather';
			weather.iconName='Weather';
			weather.width=60;
			weather.heigh=60;
			weather.thumbWidth=60;
			weather.thumbHeight=60;

			youTube=new IconVo();
			youTube.name='youtube';
			youTube.iconName='YouTube';
			youTube.width=60;
			youTube.heigh=60;
			youTube.thumbWidth=60;
			youTube.thumbHeight=60;
		}

		public function fillDefault(model:ThemeModelLocator):void
		{
			statusBar.sourceBmd=new model.assets.statusBarIcon as Bitmap;
			statusBar.defIconBytes=ImageUtil.encodeByteArray(statusBar.sourceBmd.bitmapData);

			wallpaper.sourceBmd=new model.assets.wallpaperIcon as Bitmap;
			wallpaper.defIconBytes=ImageUtil.encodeByteArray(wallpaper.sourceBmd.bitmapData);

			dock.sourceBmd=new model.assets.dockIcon as Bitmap;
			dock.defIconBytes=ImageUtil.encodeByteArray(dock.sourceBmd.bitmapData);

			appStore.sourceBmd=new model.assets.appStoreIcon as Bitmap;
			appStore.defIconBytes=ImageUtil.encodeByteArray(appStore.sourceBmd.bitmapData);

			calculator.sourceBmd=new model.assets.calculatorIcon as Bitmap;
			calculator.defIconBytes=ImageUtil.encodeByteArray(calculator.sourceBmd.bitmapData);

			calendar.sourceBmd=new model.assets.calendarIcon as Bitmap;
			calendar.defIconBytes=ImageUtil.encodeByteArray(calendar.sourceBmd.bitmapData);

			camera.sourceBmd=new model.assets.cameraIcon as Bitmap;
			camera.defIconBytes=ImageUtil.encodeByteArray(camera.sourceBmd.bitmapData);

			clock.sourceBmd=new model.assets.clockIcon as Bitmap;
			clock.defIconBytes=ImageUtil.encodeByteArray(clock.sourceBmd.bitmapData);

			contacts.sourceBmd=new model.assets.contactsIcon as Bitmap;
			contacts.defIconBytes=ImageUtil.encodeByteArray(contacts.sourceBmd.bitmapData);

			iPod.sourceBmd=new model.assets.iPodIcon as Bitmap;
			iPod.defIconBytes=ImageUtil.encodeByteArray(iPod.sourceBmd.bitmapData);

			iTunes.sourceBmd=new model.assets.iTunesIcon as Bitmap;
			iTunes.defIconBytes=ImageUtil.encodeByteArray(iTunes.sourceBmd.bitmapData);

			mail.sourceBmd=new model.assets.mailIcon as Bitmap;
			mail.defIconBytes=ImageUtil.encodeByteArray(mail.sourceBmd.bitmapData);

			maps.sourceBmd=new model.assets.mapsIcon as Bitmap;
			maps.defIconBytes=ImageUtil.encodeByteArray(maps.sourceBmd.bitmapData);

			notes.sourceBmd=new model.assets.notesIcon as Bitmap;
			notes.defIconBytes=ImageUtil.encodeByteArray(notes.sourceBmd.bitmapData);

			phone.sourceBmd=new model.assets.phoneIcon as Bitmap;
			phone.defIconBytes=ImageUtil.encodeByteArray(phone.sourceBmd.bitmapData);

			photos.sourceBmd=new model.assets.photosIcon as Bitmap;
			photos.defIconBytes=ImageUtil.encodeByteArray(photos.sourceBmd.bitmapData);

			safari.sourceBmd=new model.assets.safariIcon as Bitmap;
			safari.defIconBytes=ImageUtil.encodeByteArray(safari.sourceBmd.bitmapData);

			settings.sourceBmd=new model.assets.settingsIcon as Bitmap;
			settings.defIconBytes=ImageUtil.encodeByteArray(settings.sourceBmd.bitmapData);

			stocks.sourceBmd=new model.assets.stocksIcon as Bitmap;
			stocks.defIconBytes=ImageUtil.encodeByteArray(stocks.sourceBmd.bitmapData);

			text.sourceBmd=new model.assets.textIcon as Bitmap;
			text.defIconBytes=ImageUtil.encodeByteArray(text.sourceBmd.bitmapData);

			weather.sourceBmd=new model.assets.weatherIcon as Bitmap;
			weather.defIconBytes=ImageUtil.encodeByteArray(weather.sourceBmd.bitmapData);

			youTube.sourceBmd=new model.assets.youTubeIcon as Bitmap;
			youTube.defIconBytes=ImageUtil.encodeByteArray(youTube.sourceBmd.bitmapData);
		}
	}
}