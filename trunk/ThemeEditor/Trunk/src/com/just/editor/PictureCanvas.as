package com.just.editor
{
	import com.adobe.images.PNGEncoder;
	import com.just.drawing.BitmapDataHelper;

	import flash.display.Bitmap;
	import flash.display.BitmapData;
	import flash.display.Loader;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.events.IEventDispatcher;
	import flash.events.IOErrorEvent;
	import flash.events.MouseEvent;
	import flash.geom.Matrix;
	import flash.geom.Rectangle;
	import flash.net.URLRequest;
	import flash.text.TextField;
	import flash.text.TextFieldAutoSize;
	import flash.text.TextFormat;
	import flash.utils.*;

	import mx.controls.Alert;

	/**
	 *  图片编辑区类
	 */
	public class PictureCanvas extends Sprite implements IEventDispatcher
	{
		private var _originalPhotoData:BitmapData=null;
		private var _currentFilter:String="";

		private var _isImageMove:Boolean=false;
		private var _isSelectorMove:Boolean=false;

		//private var cWater:WaterImageWithTransferTool;
		private var cRulerLine:Sprite;
		private var cSelector:Selector;
		private var cSelectorTip:TextField;
		//private var cRuler:Ruler;
		[Bindable]
		public var cImage:Image;

		private var _canvasWidth:Number;
		private var _canvasHeight:Number;
		private var _selectorWidth:Number=240;
		private var _selectorHeight:Number=320;
		private var _selectorTipTimer:uint=0;

		/**
		 *  使用指定的宽高初始化图片编辑区类
		 */
		public function PictureCanvas()
		{
			//初始化背景图
			this.cImage=new Image();
			this.addChild(cImage);

			//初始化标尺
			//this.cRuler=new Ruler();
			//this.addChild(cRuler);

			//初始化水印图片层
			//this.cWater=new WaterImageWithTransferTool();
			//this.addChild(cWater);

			//初始化标尺校准线
			this.cRulerLine=new Sprite();
			this.addChild(cRulerLine);

			//初始化选框
			this.cSelector=new Selector();
			this.addChild(cSelector);

			this.cSelectorTip=new TextField();
			this.cSelectorTip.text="双击进行剪切操作";
			this.cSelectorTip.visible=false;
			this.cSelectorTip.border=true;
			this.cSelectorTip.background=true;
			this.cSelectorTip.backgroundColor=0xffffff;
			this.cSelectorTip.autoSize=TextFieldAutoSize.LEFT;
			this.cSelectorTip.selectable=false;
			//this.cSelectorTip.height = 18;

			var format:TextFormat=new TextFormat();
			format.color=0x000000;
			format.size=12;
			this.cSelectorTip.setTextFormat(format);

			this.addChild(cSelectorTip);

			//绑定鼠标事件
			this.cSelector.addEventListener(MouseEvent.MOUSE_DOWN, onSelectorMouse_Down);
			this.cSelector.addEventListener(MouseEvent.MOUSE_UP, onSelectorMouse_Up);
			this.cSelector.addEventListener(MouseEvent.MOUSE_MOVE, onSelectorMouse_Move);
			this.cSelector.addEventListener(MouseEvent.MOUSE_OVER, onSelectorMouse_Over);
			this.addEventListener(MouseEvent.DOUBLE_CLICK, onDouble_Click);

			this.cSelector.doubleClickEnabled=true;
			this.doubleClickEnabled=true;
		}

		/**
		 * 重新计算画布大小
		 */
		public function Relayout(w:Number, h:Number):void
		{
			this._canvasWidth=w;
			this._canvasHeight=h;
			//填充背景，as3有背景才能计算宽高...
			this.graphics.clear();
			this.graphics.beginFill(0x000000, 0);
			this.graphics.drawRect(0, 0, w, h);
			this.graphics.endFill();

			if (cImage.ImageSource != null)
			{
				this.SetAproposSize();
			}
		}

		/**
		 * 控制是否选框状态(剪切状态)
		 * @param visible 是否显示
		 */
		public function ShowSelector(visible:Boolean):void
		{
			this.cRulerLine.visible=visible;
			this.cSelector.visible=visible;
			this.cImage.ShowMask(visible);
			if (!visible)
			{
				this.cImage.addEventListener(MouseEvent.MOUSE_DOWN, onImageMouse_Down);
				this.cImage.addEventListener(MouseEvent.MOUSE_UP, onImageMouse_Up);
				this.cImage.addEventListener(MouseEvent.MOUSE_MOVE, onImageMouse_Move);
			}
			else
			{
				this.cImage.removeEventListener(MouseEvent.MOUSE_DOWN, onImageMouse_Down);
				this.cImage.removeEventListener(MouseEvent.MOUSE_UP, onImageMouse_Up);
				this.cImage.removeEventListener(MouseEvent.MOUSE_MOVE, onImageMouse_Move);
			}
		}

		/**
		 * 根据指定URL载入新的图像
		 */
		public function LoadPictureByUrl(url:String):void
		{
			var loader:Loader=new Loader();
			loader.contentLoaderInfo.addEventListener(Event.COMPLETE, function(e:Event):void
				{
					LoadPicture(Bitmap(loader.content).bitmapData);
					//LoadingControl.close();
				});
			loader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, function(e:Event):void
				{
					//LoadingControl.close();
					Alert.show('图片加载失败');
				});
			loader.load(new URLRequest(url));
		}

		/**
		 * 载入新的图像
		 */
		public function LoadPicture(bmd:BitmapData):void
		{
			_originalPhotoData=bmd.clone();
			cImage.Effect="";
			cImage.Scale=1;
			cImage.LoadPicture(bmd);
			SetAproposSize();
		}

		/**
		 * 重置选框大小
		 */
		public function SetSelectorSize(w:Number, h:Number):void
		{
			//设置选框居中
			this._selectorWidth=w;
			this._selectorHeight=h;
			this.cSelector.x=(this._canvasWidth - w) / 2;
			this.cSelector.y=(this._canvasHeight - h) / 2;
			this.cSelector.ShowSelector(w, h);
			//标尺校准线
			this.DrawRulerLine();
			//图片居中
			this.cImage.x=(this._canvasWidth - this.cImage.RealWidth) / 2;
			this.cImage.y=(this._canvasHeight - this.cImage.RealHeight) / 2;

			this.cImage.SetMaskSize(w, h);
			this.cImage.SetMaskPosition(this.cSelector.x, this.cSelector.y);
			//激发事件
			this.OnSelectorPositionChanged(this.cSelector.x, this.cSelector.y);
		}

		/**
		 * 移动选框
		 */
		public function MoveSelector(x:Number, y:Number):void
		{
			this.cSelector.x=x;
			this.cSelector.y=y;
			this.DrawRulerLine();
			this.cImage.SetMaskPosition(this.cSelector.x, this.cSelector.y);
		}

		/**
		 * 设置图片最合适大小
		 */
		public function SetAproposSize(w:int=0, h:int=0):void
		{
			if (w == 0)
				w=this.cImage.ImageSource.width;
			if (h == 0)
				h=this.cImage.ImageSource.height;
			var scale:Number=1;
			if (w > this._canvasWidth)
			{
				scale=this._canvasWidth / w;
			}
			if (h > this._canvasHeight)
			{
				if (this._canvasHeight / h < scale)
				{
					scale=this._canvasHeight / h;
				}
			}
			this.cImage.ScalePicture(scale);
			this.SetSelectorSize(this._selectorWidth, this._selectorHeight);
			this.OnScaleChanged(scale * 100);
		}

		/**
		 * 设置图片实际大小
		 */
		public function SetRealSize():void
		{
			this.cImage.ScalePicture(1);
			this.SetSelectorSize(this._selectorWidth, this._selectorHeight);
			this.OnScaleChanged(100);
		}

		/**
		 * 还原图片
		 */
		public function RestorePicture():void
		{
			this.cImage.Scale=1;
			this.cImage.Effect="";
			this.cImage.LoadPicture(this._originalPhotoData);
			this.SetAproposSize();
		}

		/**
		 * 缩放图片
		 */
		public function ScalePicture(scale:Number):void
		{
			this.cImage.ScalePicture(scale / 100);
			this.SetSelectorSize(this._selectorWidth, this._selectorHeight);
		}

		/**
		 * 剪切图片
		 */
		public function CutPicture():void
		{
			if (this.cSelector.visible)
			{
				var rect:Rectangle=new Rectangle(this.cSelector.x - this.cImage.x, this.cSelector.y - this.cImage.y, this._selectorWidth, this._selectorHeight);
				var bmd:BitmapData=null;
				if (rect.height == 60 && rect.width == 60)
				{
					bmd=BitmapDataHelper.CutRoundRectPhoto(this.cImage.ScaleImageData, rect);
				}
				else
				{
					bmd=BitmapDataHelper.CutPhoto(this.cImage.ScaleImageData, rect);
				}
				this.cImage.Scale=1;
				this.cImage.LoadPicture(bmd);
				this.SetAproposSize(this._selectorWidth, this._selectorHeight);
				this.OnScaleChanged(100);
			}
		}

		/**
		 * 获取当前处理后的图像字节流
		 */
		public function GetPictureBytes():ByteArray
		{
			var bmpData:BitmapData=this.cImage.FinishedImageData;
			//bmpData = this.ApplyFilter(bmpData);
			return PNGEncoder.encode(bmpData);
		}

		/**
		 * 获取当前处理后的Bitmap
		 */
		public function get pictureBitmap():Bitmap
		{
			return new Bitmap(this.cImage.FinishedImageData);
		}

		/**
		 * 控制标尺是否可见
		 */
		public function ToggleRuler():void
		{
			//this.cRuler.visible=!this.cRuler.visible;
		}

		/**
		 * 左转图像
		 */
		public function LeftRotate():void
		{
			cImage.LoadPicture(BitmapDataHelper.LeftRotate(cImage.ImageSource));
			this.SetAproposSize();
		}

		/**
		 * 右转图像
		 */
		public function RightRotate():void
		{
			cImage.LoadPicture(BitmapDataHelper.RightRotate(cImage.ImageSource));
			this.SetAproposSize();
		}

		/**
		 * 水平翻转
		 */
		public function HorizontalRotate():void
		{
			cImage.LoadPicture(BitmapDataHelper.HorizontalRotate(cImage.ImageSource));
		}

		/**
		 * 垂直翻转
		 */
		public function VerticalRotate():void
		{
			cImage.LoadPicture(BitmapDataHelper.VerticalRotate(cImage.ImageSource));
		}

		/**
		 * 设置图片滤镜
		 */
		public function SetEffect(effect:String):void
		{
			this.cImage.Effect=effect;
			this.cImage.LoadPicture(this.cImage.ImageSource);
		}

		/////////////私有方法///////////////
		/**
		 * @private
		 */
		private function onSelectorMouse_Down(e:MouseEvent):void
		{
			this._isSelectorMove=true;
			this.cSelector.startDrag(false, new Rectangle(0, 0, _canvasWidth - this.cSelector.width, _canvasHeight - this.cSelector.height));
		}

		/**
		 * @private
		 */
		private function onSelectorMouse_Move(e:MouseEvent):void
		{
			if (this._isSelectorMove)
			{
				this.cImage.SetMaskPosition(this.cSelector.x, this.cSelector.y);
				this.DrawRulerLine();
				this.OnSelectorPositionChanged(this.cSelector.x, this.cSelector.y);
			}
		}

		/**
		 * @private
		 */
		private function onSelectorMouse_Up(e:MouseEvent):void
		{
			this._isSelectorMove=false;
			this.cSelector.stopDrag();
		}

		/**
		 * @private
		 */
		private function onSelectorMouse_Over(e:MouseEvent):void
		{
			if (this._selectorTipTimer == 0)
			{
				this.cSelectorTip.x=this.cSelector.x + (this.cSelector.width - this.cSelectorTip.width) / 2;
				this.cSelectorTip.y=this.cSelector.y + (this.cSelector.height - this.cSelectorTip.height) / 2;
				this.cSelectorTip.visible=true;
				this._selectorTipTimer=setTimeout(function():void
					{
						cSelectorTip.visible=false;
						clearTimeout(_selectorTipTimer);
						_selectorTipTimer=0;
					}, 1200);
			}
		}

		/**
		 * @private
		 */
		private function onImageMouse_Down(e:MouseEvent):void
		{
			this._isImageMove=true;
			this.cImage.startDrag(false);
		}

		/**
		 * @private
		 */
		private function onImageMouse_Move(e:MouseEvent):void
		{
			if (this._isImageMove)
			{
				if (!e.buttonDown)
				{
					this._isImageMove=false;
					this.cImage.stopDrag();
				}
				this.cImage.SetMaskPosition(this.cSelector.x, this.cSelector.y);
			}
		}

		/**
		 * @private
		 */
		private function onImageMouse_Up(e:MouseEvent):void
		{
			this._isImageMove=false;
			this.cImage.stopDrag();
		}

		/**
		 * @private
		 */
		private function onDouble_Click(e:MouseEvent):void
		{
			this.CutPicture();
		}

		/**
		 * @private 激发缩放级别变更事件
		 */
		private function OnScaleChanged(scale:int):void
		{
			this.dispatchEvent(new ScaleEvent(ScaleEvent.SCALE_CHANGED, scale));
		}

		/**
		 * @private 激发选框坐标改变事件
		 */
		private function OnSelectorPositionChanged(x:Number, y:Number):void
		{
			this.dispatchEvent(new SelectorMouseEvent(SelectorMouseEvent.MOVE, x, y));
		}

		/**
		 * @private 绘制标尺校准线
		 */
		private function DrawRulerLine():void
		{
			with (this.cRulerLine.graphics)
			{
				clear();
				lineStyle(0.5, 0x666666);
				moveTo(0, this.cSelector.y);
				lineTo(this._canvasWidth, this.cSelector.y);
				moveTo(0, this.cSelector.y + this._selectorHeight);
				lineTo(this._canvasWidth, this.cSelector.y + this._selectorHeight);
				moveTo(this.cSelector.x, 0);
				lineTo(this.cSelector.x, this._canvasHeight);
				moveTo(this.cSelector.x + this._selectorWidth, 0);
				lineTo(this.cSelector.x + this._selectorWidth, this._canvasHeight);
			}
		}
	}
}