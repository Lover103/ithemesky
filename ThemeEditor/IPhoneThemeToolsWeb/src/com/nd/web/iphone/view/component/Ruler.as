package com.nd.web.iphone.view.component
{
	import flash.display.SpreadMethod;
	import flash.display.Sprite;
	import flash.text.TextField;
	import flash.text.TextFormat;

	/**
	 * 编辑器标尺类
	 */
	public class Ruler extends Sprite
	{
		private var _lineCanvas:Sprite=new Sprite();
		private var _textCanvas:Sprite=new Sprite();

		/**
		 * 初始化编辑器标尺类
		 * @param width:标尺宽
		 * @param height:标尺高
		 */
		public function ShowRuler(width:Number, height:Number):void
		{
			this.addChild(_lineCanvas);
			this.addChild(_textCanvas);

			this._lineCanvas.graphics.clear();
			this._lineCanvas.graphics.lineStyle(0.5, 0x666666, 1);

			this.DrawHorizontalScale(width);
			this.DrawVerticalScale(height);
		}

		/**
		 * @private 画出水平标尺
		 */
		private function DrawHorizontalScale(length:Number):void
		{
			for (var i:int=10; i < length; i+=10)
			{
				_lineCanvas.graphics.moveTo(i, 0);
				if (i == 10)
				{
					_lineCanvas.graphics.lineTo(i, 6);
					AppendLabel("0", 5, 5);
				}
				else if (i % 100 == 0)
				{
					_lineCanvas.graphics.lineTo(i, 10);
					AppendLabel(((i / 100) * 100).toString(), i - 5, 7);
				}
				else if (i % 50 == 0)
				{
					_lineCanvas.graphics.lineTo(i, 7);
				}
				else
				{
					_lineCanvas.graphics.lineTo(i, 4);
				}
			}
		}

		/**
		 * @private 画出垂直标尺
		 */
		private function DrawVerticalScale(length:Number):void
		{
			for (var i:int=10; i < length; i+=10)
			{
				_lineCanvas.graphics.moveTo(0, i);
				if (i == 10)
				{
					_lineCanvas.graphics.lineTo(7, i);
				}
				else if (i % 100 == 0)
				{
					_lineCanvas.graphics.lineTo(10, i);
					AppendLabel(((i / 100) * 100).toString(), 8, i - 5);
				}
				else if (i % 50 == 0)
				{
					_lineCanvas.graphics.lineTo(7, i);
				}
				else
				{
					_lineCanvas.graphics.lineTo(4, i);
				}
			}
		}

		/**
		 * @private 添加标尺数值
		 */
		private function AppendLabel(text:String, x:int, y:int):void
		{
			var lab:TextField=new TextField();
			lab.text=text;
			lab.height=18;
			lab.x=x;
			lab.y=y;
			lab.selectable=false;

			var format:TextFormat=new TextFormat();
			format.font="Verdana";
			format.color=0x666666;
			format.size=9;
			lab.setTextFormat(format);

			this._textCanvas.addChild(lab);
		}

	}
}