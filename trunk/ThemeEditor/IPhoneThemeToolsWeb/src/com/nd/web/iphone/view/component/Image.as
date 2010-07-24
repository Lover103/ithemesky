package com.nd.web.iphone.view.component
{
	import com.just.drawing.BitmapDataHelper;
	import com.nd.web.iphone.event.ImageChangeEvent;

	import flash.display.Bitmap;
	import flash.display.BitmapData;
	import flash.display.Sprite;

	/**
	 * 带特定遮照层的图像呈现类
	 */
	public class Image extends Sprite
	{
		private var _scale:Number=1;
		private var _effect:String="";
		private var _imgSource:BitmapData=null;
		private var _maskFront:Sprite=new Sprite();
		private var _maskBackground:Sprite=new Sprite();
		private var _imgBackground:Bitmap=new Bitmap();
		private var _imgFront:Bitmap=new Bitmap();

		public function Image()
		{
			this._maskBackground.doubleClickEnabled=true;
			this._maskFront.doubleClickEnabled=true;
			this.doubleClickEnabled=true;
		}

		/**
		 * 裁入新的图片
		 */
		public function LoadPicture(bmpData:BitmapData):void
		{
			this.clear();
			_imgSource=bmpData;

			var bmdTemp:BitmapData=BitmapDataHelper.ChangeScale(_imgSource.clone(), _scale);
			bmdTemp=ApplyFilter(bmdTemp);
			_imgBackground=new Bitmap(bmdTemp);
			_maskBackground.graphics.clear();
			_maskBackground.graphics.beginFill(0x000000, 0.75);
			_maskBackground.graphics.drawRect(0, 0, bmdTemp.width, bmdTemp.height);

			_imgFront=new Bitmap(bmdTemp);
			_imgFront.mask=_maskFront;

			this.addChild(this._imgBackground);
			this.addChild(this._maskBackground);
			this.addChild(this._imgFront);
			this.addChild(this._maskFront);
			//图片改变重新加载事件
			dispatchEvent(new ImageChangeEvent());
		}

		/**
		 * 按照指定比例缩放图像
		 */
		public function ScalePicture(scale:Number):void
		{
			this._scale=scale;
			this.LoadPicture(this._imgSource);
		}

		/**
		 * 控制遮罩层的显示
		 */
		public function ShowMask(visible:Boolean):void
		{
			_maskBackground.visible=visible;
			_imgFront.visible=visible;
		}

		/**
		 * 设置遮罩层位置
		 */
		public function SetMaskPosition(x:Number, y:Number):void
		{
			this._maskFront.x=x - this.x;
			this._maskFront.y=y - this.y;
		}

		/**
		 * 设置遮罩层大小
		 */
		public function SetMaskSize(w:Number, h:Number):void
		{
			this._maskFront.graphics.clear();
			this._maskFront.graphics.beginFill(0x000000, 1);
			this._maskFront.graphics.drawRect(0, 0, w, h);
			this._maskFront.graphics.endFill();
		}

		/**
		 * 获取图像源BitmapData
		 */
		public function get ImageSource():BitmapData
		{
			return this._imgSource;
		}

		/**
		 * 设置图像的放大缩小级别
		 */
		public function set Scale(scale:Number):void
		{
			this._scale=scale;
		}

		/**
		 * 获取图像的放大缩小级别
		 */
		public function get Scale():Number
		{
			return this._scale;
		}

		/**
		 * 设置图像的当前滤镜
		 */
		public function set Effect(effect:String):void
		{
			this._effect=effect;
		}

		/**
		 * 获取图像的当前滤镜
		 */
		public function get Effect():String
		{
			return this._effect;
		}

		/**
		 * 获取真实的图像宽度
		 */
		public function get RealWidth():Number
		{
			if (_imgSource == null)
				return 0;
			return this._imgSource.width * this._scale;
		}

		/**
		 * 获取真实的图像高度
		 */
		public function get RealHeight():Number
		{
			if (_imgSource == null)
				return 0;
			return this._imgSource.height * this._scale;
		}

		/**
		 * 获取完成后的图像(滤镜加缩放)
		 */
		public function get FinishedImageData():BitmapData
		{
			return this.ApplyFilter(BitmapDataHelper.ChangeScale(this.ImageSource, this._scale));
		}

		/**
		 * 获取缩放后的图像
		 */
		public function get ScaleImageData():BitmapData
		{
			return BitmapDataHelper.ChangeScale(this.ImageSource, this._scale);
		}

		/**
		 * @private 应用滤镜
		 */
		private function ApplyFilter(bmpData:BitmapData):BitmapData
		{
			switch (this._effect)
			{
				case "gray":
					bmpData=BitmapDataHelper.ApplyGrayFilter(bmpData);
					break;
				case "film":
					bmpData=BitmapDataHelper.ApplyFilmFilter(bmpData);
					break;
				case "emboss":
					bmpData=BitmapDataHelper.ApplyEmbossFilter(bmpData);
					break;
				case "blur":
					bmpData=BitmapDataHelper.ApplyBlurFilter(bmpData);
					break;
				case "noise":
					bmpData=BitmapDataHelper.ApplyNoiseFilter(bmpData);
					break;
				case "charcoal":
					bmpData=BitmapDataHelper.ApplyCharcoalFilter(bmpData);
					break;
				case "color":
					bmpData=BitmapDataHelper.ApplyColorFilter(bmpData);
					break;
				case "old":
					bmpData=BitmapDataHelper.ApplyOldPhotoFilter(bmpData);
					break;
				case "mosaic":
					bmpData=BitmapDataHelper.ApplyMosaicFilter(bmpData);
					break;
				default:
					break;
			}
			return bmpData;
		}

		/**
		 * @private
		 */
		private function clear():void
		{
			while (this.numChildren > 0)
			{
				this.removeChildAt(0);
			}
		}

	}
}