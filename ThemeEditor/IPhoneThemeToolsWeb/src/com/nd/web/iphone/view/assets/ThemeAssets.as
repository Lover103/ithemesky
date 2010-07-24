package com.nd.web.iphone.view.assets
{
	import com.nd.web.iphone.util.ImageUtil;

	import flash.display.Bitmap;
	import flash.utils.ByteArray;


	public class ThemeAssets
	{
		public function ThemeAssets()
		{
		}

		//iphone外边框
		[Bindable]
		[Embed("/assets/images/iphone_frame.png")]
		public var iphoneIcon:Class;
		//加号图标
		[Bindable]
		[Embed("/assets/images/add.png")]
		public var addIcon:Class;
		//剪切功能图标
		[Bindable]
		[Embed("/assets/images/cut.png")]
		public var cutIcon:Class;
		//拖动功能图标
		[Bindable]
		[Embed("/assets/images/drag.png")]
		public var dragIcon:Class;
		//状态栏图标
		[Bindable]
		[Embed("/assets/images/status.png")]
		public var statusIcon:Class;
		//实际大小功能图标
		[Bindable]
		[Embed("/assets/images/real_size.png")]
		public var realSizeIcon:Class;
		//合适大小功能图标
		[Bindable]
		[Embed("/assets/images/apropos_size.png")]
		public var aproposSizeIcon:Class;
		//还原功能图标
		[Bindable]
		[Embed("/assets/images/restore.png")]
		public var restoreIcon:Class;
		//打开功能图标
		[Bindable]
		[Embed("/assets/images/open_pic.png")]
		public var openPicIcon:Class;
		//应用返回功能图标
		[Bindable]
		[Embed("/assets/images/apply_return.png")]
		public var applyReturnIcon:Class;
		//水平翻转功能图标
		[Bindable]
		[Embed("/assets/images/turn_hor.png")]
		public var turnHorIcon:Class;
		//垂直翻转功能图标
		[Bindable]
		[Embed("/assets/images/turn_ver.png")]
		public var turnVerIcon:Class;
		//向左旋转功能图标
		[Bindable]
		[Embed("/assets/images/turn_left.png")]
		public var turnLeftIcon:Class;
		//右向旋转功能图标
		[Bindable]
		[Embed("/assets/images/turn_right.png")]
		public var turnRightIcon:Class;
		//无特效
		[Bindable]
		[Embed("/assets/images/effect_normal.png")]
		public var effectNormalIcon:Class;
		//模糊
		[Bindable]
		[Embed("/assets/images/effect_blur.png")]
		public var effectBlurIcon:Class;
		//浮雕
		[Bindable]
		[Embed("/assets/images/effect_emboss.png")]
		public var effectEmbossIcon:Class;
		//底片
		[Bindable]
		[Embed("/assets/images/effect_film.png")]
		public var effectFilmIcon:Class;
		//灰度
		[Bindable]
		[Embed("/assets/images/effect_gray.png")]
		public var effectGrayIcon:Class;
		//马赛克
		[Bindable]
		[Embed("/assets/images/effect_mosaic.png")]
		public var effectMosaicIcon:Class;
		//雪花
		[Bindable]
		[Embed("/assets/images/effect_noise.png")]
		public var effectNoiseIcon:Class;
		//老照片
		[Bindable]
		[Embed("/assets/images/effect_oldphoto.png")]
		public var effectOldphotoIcon:Class;
		public var buildIcon:Class;
		[Bindable]
		[Embed(source='/assets/images/iconBack.gif')]
		public var iconBack:Class;
		[Bindable]
		[Embed(source='/assets/images/iconNext.gif')]
		public var iconNext:Class;
		[Bindable]
		[Embed(source='/assets/images/iconEnd.gif')]
		public var iconEnd:Class;
		[Bindable]
		[Embed(source='/assets/images/iconHome.gif')]
		public var iconHome:Class;
		[Bindable]
		[Embed(source='/assets/images/iconBack_dis.gif')]
		public var iconBackDis:Class;
		[Bindable]
		[Embed(source='/assets/images/iconNext_dis.gif')]
		public var iconNextDis:Class;
		[Bindable]
		[Embed(source='/assets/images/iconEnd_dis.gif')]
		public var iconEndDis:Class;
		[Bindable]
		[Embed(source='/assets/images/iconHome_dis.gif')]
		public var iconHomeDis:Class;
		[Bindable]
		[Embed(source='/assets/images/save_edit.png')]
		public var saveEditIcon:Class;
		[Bindable]
		[Embed(source='/assets/images/icon_failure32.png')]
		public var failureIcon:Class;
		[Bindable]
		[Embed(source='/assets/images/icon_success32.png')]
		public var successIcon:Class;
		[Bindable]
		[Embed(source='/assets/images/new_theme.png')]
		public var newThemeIcon:Class;
		[Bindable]
		[Embed(source='/assets/images/download_theme.png')]
		public var downloadThemeIcon:Class;
		[Bindable]
		[Embed(source='/assets/images/arrow_icon.png')]
		public var arrowIcon:Class;
		[Bindable]
		[Embed(source='/assets/images/search.png')]
		public var searchIcon:Class;
		[Bindable]
		[Embed(source='/assets/images/loading.swf')]
		public var loadingIcon:Class;

		/**
		 * 主题资源图标
		 */
		//Dock
		[Bindable]
		[Embed("/assets/theme/Dock.png")]
		public var dockIcon:Class;
		//StatusBar
		[Bindable]
		[Embed("/assets/theme/StatusBar.png")]
		public var statusBarIcon:Class;
		//Wallpaper
		[Bindable]
		[Embed("/assets/theme/Wallpaper.png")]
		public var wallpaperIcon:Class;
		//AppStore
		[Bindable]
		[Embed("/assets/theme/icons/AppStore.png")]
		public var appStoreIcon:Class;
		//Calculator
		[Bindable]
		[Embed("/assets/theme/icons/Calculator.png")]
		public var calculatorIcon:Class;
		//Calendar
		[Bindable]
		[Embed("/assets/theme/icons/Calendar.png")]
		public var calendarIcon:Class;
		//Camera
		[Bindable]
		[Embed("/assets/theme/icons/Camera.png")]
		public var cameraIcon:Class;
		//Clock
		[Bindable]
		[Embed("/assets/theme/icons/Clock.png")]
		public var clockIcon:Class;
		//Contacts
		[Bindable]
		[Embed("/assets/theme/icons/Contacts.png")]
		public var contactsIcon:Class;
		//iPod
		[Bindable]
		[Embed("/assets/theme/icons/iPod.png")]
		public var iPodIcon:Class;
		//iTunes
		[Bindable]
		[Embed("/assets/theme/icons/iTunes.png")]
		public var iTunesIcon:Class;
		//Mail
		[Bindable]
		[Embed("/assets/theme/icons/Mail.png")]
		public var mailIcon:Class;
		//Maps
		[Bindable]
		[Embed("/assets/theme/icons/Maps.png")]
		public var mapsIcon:Class;
		//Notes
		[Bindable]
		[Embed("/assets/theme/icons/Notes.png")]
		public var notesIcon:Class;
		//Phone
		[Bindable]
		[Embed("/assets/theme/icons/Phone.png")]
		public var phoneIcon:Class;
		//Photos
		[Bindable]
		[Embed("/assets/theme/icons/Photos.png")]
		public var photosIcon:Class;
		//Safari
		[Bindable]
		[Embed("/assets/theme/icons/Safari.png")]
		public var safariIcon:Class;
		//Settings
		[Bindable]
		[Embed("/assets/theme/icons/Settings.png")]
		public var settingsIcon:Class;
		//Stocks
		[Bindable]
		[Embed("/assets/theme/icons/Stocks.png")]
		public var stocksIcon:Class;
		//Text
		[Bindable]
		[Embed("/assets/theme/icons/Text.png")]
		public var textIcon:Class;
		//Weather
		[Bindable]
		[Embed("/assets/theme/icons/Weather.png")]
		public var weatherIcon:Class;
		//YouTube
		[Bindable]
		[Embed("/assets/theme/icons/YouTube.png")]
		public var youTubeIcon:Class;
	}
}