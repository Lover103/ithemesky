package com.nd.web.iphone.view.component
{
	import com.senocular.display.TransformTool;

	import flash.display.Bitmap;
	import flash.display.BitmapData;
	import flash.display.Sprite;
	import flash.geom.Matrix;

	/**
	 * 水印图片层
	 */
	public class WaterImageWithTransferTool extends Sprite
	{
		private var _imageSource:BitmapData;
		private var _imgContainer:Sprite=new Sprite();
		private var defaultTool:TransformTool=new TransformTool();

		public function WaterImageWithTransferTool()
		{

		}

		/**
		 * 显示水印图片
		 */
		public function Show(bmd:BitmapData):void
		{
			this.clear();
			_imgContainer=new Sprite();
			_imgContainer.addChild(new Bitmap(bmd));

			this.addChild(defaultTool);
			this.addChild(_imgContainer);
			this.defaultTool.target=_imgContainer;
			this._imageSource=bmd;
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

		/**
		 * 获取水印图
		 */
		public function get ImageSource():BitmapData
		{
			/* trace(_imgContainer.x + "," + _imgContainer.y);
			 return this.defaultTool; */
			defaultTool.apply();
			var img:Sprite=Sprite(defaultTool.target);
			var matrix:Matrix=img.transform.concatenatedMatrix.clone();
			matrix.tx=500;
			matrix.ty=500;
			var bmd:BitmapData=new BitmapData(1000, 1000, true, 0xff0000);
			bmd.draw(img, matrix);
			return bmd;
		}

		public function get RealX():Number
		{
			return this.x + this._imgContainer.x - 500;
		}

		public function get RealY():Number
		{
			return this.y + this._imgContainer.y - 500;
		}
	}
}