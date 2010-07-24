package com.just.drawing
{
	import flash.display.BitmapData;
	import flash.display.IBitmapDrawable;
	import flash.display.Sprite;
	import flash.filters.BitmapFilter;
	import flash.filters.BlurFilter;
	import flash.filters.ColorMatrixFilter;
	import flash.filters.ConvolutionFilter;
	import flash.geom.ColorTransform;
	import flash.geom.Matrix;
	import flash.geom.Point;
	import flash.geom.Rectangle;
	import flash.utils.ByteArray;

	///位图操作静态方法辅助类
	public class BitmapDataHelper
	{

		/**
		 * 剪裁圆角矩形图片
		 */
		public static function CutRoundRectPhoto(bmpData:BitmapData, rect:Rectangle):BitmapData
		{
			var retdata:BitmapData=new BitmapData(rect.width, rect.height, true, 0);
			retdata.copyPixels(bmpData, rect, new Point(0, 0));


			var roundRectSprite:Sprite=new Sprite();
			roundRectSprite.graphics.clear();
			roundRectSprite.graphics.lineStyle(2, 0x0000FF);
			roundRectSprite.graphics.beginFill(0xFFFFFF, 0.2);
			roundRectSprite.graphics.drawRoundRect(1, 1, rect.width - 2, rect.height - 2, 20, 20);
			roundRectSprite.graphics.endFill();
			var roundRectBitmapData:BitmapData=new BitmapData(rect.width, rect.height, true, 0);
			roundRectBitmapData.draw(roundRectSprite);

			for (var pixelY:int=0; pixelY < rect.height; pixelY++)
			{
				for (var pixelX:int=0; pixelX < rect.width; pixelX++)
				{
					if (roundRectBitmapData.getPixel32(pixelX, pixelY) != 0)
					{
						var color:uint=retdata.getPixel32(pixelX, pixelY);
						roundRectBitmapData.setPixel32(pixelX, pixelY, color);
					}

				}
			}
			return roundRectBitmapData;
		}


		//裁剪
		public static function CutPhoto(bmpData:BitmapData, rect:Rectangle):BitmapData
		{
			var retdata:BitmapData=new BitmapData(rect.width, rect.height, true, 0);
			retdata.copyPixels(bmpData, rect, new Point(0, 0));
			return retdata;
		}

		//缩放
		public static function ChangeScale(bmpData:BitmapData, nScale:Number):BitmapData
		{
			var bmd:BitmapData=new BitmapData(bmpData.width * nScale, bmpData.height * nScale, true, 0x00000000);
			var objMatrix:Matrix=new Matrix();
			objMatrix.scale(nScale, nScale);
			bmd.draw(bmpData, objMatrix);
			return bmd;
		}

		//左转
		public static function LeftRotate(bmpData:BitmapData):BitmapData
		{
			var bmd:BitmapData=new BitmapData(bmpData.height, bmpData.width, true, 0x00000000);
			var objMatrix:Matrix=new Matrix();
			objMatrix.rotate(2 * Math.PI * (90 / 360));
			objMatrix.translate(bmpData.height, 0);
			bmd.draw(bmpData, objMatrix);
			return bmd;
		}

		//右转
		public static function RightRotate(bmpData:BitmapData):BitmapData
		{
			var bmd:BitmapData=new BitmapData(bmpData.height, bmpData.width, true, 0x00000000);
			var objMatrix:Matrix=new Matrix();
			objMatrix.rotate(2 * Math.PI * (-90 / 360));
			objMatrix.translate(0, bmpData.width);
			bmd.draw(bmpData, objMatrix);
			return bmd;
		}

		//上下翻转
		public static function VerticalRotate(bmpData:BitmapData):BitmapData
		{
			var bmd:BitmapData=new BitmapData(bmpData.width, bmpData.height, true, 0x00000000);
			for (var x:int=0; x < bmpData.width; x++)
			{
				for (var y:int=0; y < bmpData.height; y++)
				{
					bmd.setPixel32(x, bmpData.height - y - 1, bmpData.getPixel32(x, y));
				}
			}
			return bmd;
		}

		//左右翻转
		public static function HorizontalRotate(bmpData:BitmapData):BitmapData
		{
			var bmd:BitmapData=new BitmapData(bmpData.width, bmpData.height, true, 0x00000000);
			for (var y:int=0; y < bmpData.height; y++)
			{
				for (var x:int=0; x < bmpData.width; x++)
				{
					bmd.setPixel32(bmpData.width - x - 1, y, bmpData.getPixel32(x, y));
				}
			}
			return bmd;
		}

		//灰度转换
		public static function ApplyGrayFilter(bmpData:BitmapData):BitmapData
		{
			return ApplyFilter(bmpData, new ColorMatrixFilter([0.3086, 0.6094, 0.0820, 0, 0, 0.3086, 0.6094, 0.0820, 0, 0, 0.3086, 0.6094, 0.0820, 0, 0, 0, 0, 0, 1, 0]));
		}

		//底片效果
		public static function ApplyFilmFilter(bmpData:BitmapData):BitmapData
		{
			return ApplyFilter(bmpData, new ColorMatrixFilter([-1, 0, 0, 0, 255, 0, -1, 0, 0, 255, 0, 0, -1, 0, 255, 0, 0, 0, 1, 0]));
		}

		//浮雕
		public static function ApplyEmbossFilter(bmpData:BitmapData):BitmapData
		{
			return ApplyFilter(bmpData, new ConvolutionFilter(3, 3, [-2, -1, 0, -1, 1, 1, 0, 1, 2]));
		}

		//模糊
		public static function ApplyBlurFilter(bmpData:BitmapData):BitmapData
		{
			return ApplyFilter(bmpData, new BlurFilter);
		}

		//雪花
		public static function ApplyNoiseFilter(bmpData:BitmapData):BitmapData
		{
			var multiplier:uint=65;
			var bmd:BitmapData=new BitmapData(bmpData.width, bmpData.height, true, 0x00000000);
			bmd.noise(1000, 0, 255, 7, true);
			bmpData.merge(bmd, new Rectangle(0, 0, bmpData.width, bmpData.height), new Point(), multiplier, multiplier, multiplier, multiplier);
			return bmpData;
		}

		//素描
		public static function ApplyCharcoalFilter(bmpData:BitmapData):BitmapData
		{
			bmpData.threshold(bmpData, bmpData.rect, new Point(), ">", 0xff777777, 0xffffffff);
			bmpData.threshold(bmpData, bmpData.rect, new Point(), "<", 0xff777777, 0xff000000);
			return bmpData;
		}

		//着色
		public static function ApplyColorFilter(bmpData:BitmapData, color:uint=0xc926ff):BitmapData
		{
			var colorTransform:ColorTransform=new ColorTransform();
			colorTransform.color=color;
			colorTransform.alphaMultiplier=1;
			colorTransform.redMultiplier=1;
			colorTransform.greenMultiplier=1;
			colorTransform.blueMultiplier=1;
			bmpData.colorTransform(bmpData.rect, colorTransform);
			return bmpData;
		}

		//老昭片
		public static function ApplyOldPhotoFilter(bmpData:BitmapData, color:uint=0xBB6F38):BitmapData
		{
			var colorTransform:ColorTransform=new ColorTransform();
			colorTransform.color=color;
			colorTransform.alphaMultiplier=1;
			colorTransform.redMultiplier=0.7;
			colorTransform.greenMultiplier=0.7;
			colorTransform.blueMultiplier=0.7;
			bmpData.colorTransform(bmpData.rect, colorTransform);
			return bmpData;
		}

		//马赛克
		public static function ApplyMosaicFilter(bmpData:BitmapData, block:Number=5):BitmapData
		{
			var blockRows:int=Math.ceil(bmpData.width / block);
			var blockColumn:int=Math.ceil(bmpData.height / block);
			var fillBlock:BitmapData=new BitmapData(block, block);
			for (var i:int=0; i < blockRows; i++)
			{
				for (var j:int=0; j < blockColumn; j++)
				{
					var fillColor:uint=bmpData.getPixel32(i * block, j * block);
					fillBlock.floodFill(0, 0, fillColor);
					var bytes:ByteArray=fillBlock.getPixels(fillBlock.rect);
					bytes.position=0;
					bmpData.setPixels(new Rectangle(i * block, j * block, block, block), bytes);
				}
			}
			return bmpData;
		}

		public static function ApplyFilter(bmpData:BitmapData, matrixFilter:BitmapFilter):BitmapData
		{
			var bmd:BitmapData=new BitmapData(bmpData.width, bmpData.height, true, 0x00000000);
			var rect:Rectangle=new Rectangle(0, 0, bmpData.width, bmpData.height);
			var pt:Point=new Point(0, 0);
			bmd.applyFilter(bmpData, rect, pt, matrixFilter);
			return bmd;
		}

		//打水印
		public static function WaterMark(bmpDataSource:BitmapData, bmpDataMark:IBitmapDrawable, matrix:Matrix):BitmapData
		{
			bmpDataSource.draw(bmpDataMark, matrix);
			return bmpDataSource;
		}

//        public static function DrawLine(bmpData:BitmapData, color:Uint):void
//        {
//        	return;
//        }
	}
}