package com.nd.web.iphone.view.component
{
	import flash.display.Sprite;
	import flash.utils.*;

	/**
	 * 虚线动画选择框
	 */
	public class Selector extends Sprite
	{
		private var _padding:int=4;
		private var _interval:int=50;
		private var _timer:uint=0;
		private var _whiteColor:uint=0xffffff;
		private var _blackColor:uint=0x000000;
		private var cavLine:Sprite=new Sprite();

		public function Selector()
		{
			this.addChild(cavLine);
		}

		/**
		 * @public 显示一个指定大小的选择框
		 * <p>调用该方法前需要选将该类的实例作为子对象添加到一个显示对象子节点中</p>
		 * @param w 选择框宽度
		 * @param h 选择框高度
		 */
		public function ShowSelector(w:Number, h:Number):void
		{
			this.graphics.clear();
			this.graphics.beginFill(0x000000, 0);
			this.graphics.drawRect(0, 0, w, h);
			this.graphics.endFill();

			if (_timer > 0)
				clearInterval(_timer);
			var step:int=0;
			_timer=setInterval(function():void
				{
					if (step > 2 * _padding)
						step=0;
					cavLine.graphics.clear();
					cavLine.graphics.lineStyle(1, _blackColor, 1);
					DrawHorizontalLine(step, 0, 0, w);
					DrawVerticalLine(step, w, 0, h);
					DrawVerticalLine(step, 0, 0, h);
					DrawHorizontalLine(step, 0, h, w);
					step++;
				}, _interval);
		}

		/**
		 * @private 从左至右画一条水平虚线
		 */
		private function DrawHorizontalLine(step:int, startX:int, startY:int, endX:int):void
		{
			cavLine.graphics.moveTo(startX, startY);
			if (step > _padding)
			{
				cavLine.graphics.lineStyle(1, _blackColor, 1);
				cavLine.graphics.lineTo(startX + step - _padding, startY);
			}
			cavLine.graphics.lineStyle(1, _whiteColor, 1);
			cavLine.graphics.lineTo(startX + step, startY);

			var index:int=startX + step;
			while (true)
			{
				if (index + 2 * _padding > endX)
				{
					if (index + _padding < endX)
					{
						cavLine.graphics.lineStyle(1, _blackColor, 1);
						cavLine.graphics.lineTo(index + _padding, startY);
						cavLine.graphics.lineStyle(1, _whiteColor, 1);
						cavLine.graphics.lineTo(endX, startY);
					}
					else
					{
						cavLine.graphics.lineStyle(1, _blackColor, 1);
						cavLine.graphics.lineTo(endX, startY);
					}
					break;
				}
				else
				{
					cavLine.graphics.lineStyle(1, _blackColor, 1);
					cavLine.graphics.lineTo(index + _padding, startY);
					cavLine.graphics.lineStyle(1, _whiteColor, 1);
					cavLine.graphics.lineTo(index + 2 * _padding, startY);

				}
				index+=2 * _padding;
			}
		}

		/**
		 * @private 从右至左画一条水平虚线
		 */
		private function DrawHorizontalLine2(step:int, startX:int, startY:int, endX:int):void
		{
			cavLine.graphics.moveTo(startX, startY);
			if (step > _padding)
			{
				cavLine.graphics.lineStyle(1, _blackColor, 1);
				cavLine.graphics.lineTo(startX - step + _padding, startY);
			}
			cavLine.graphics.lineStyle(1, _whiteColor, 1);
			cavLine.graphics.lineTo(startX - step, startY);

			var index:int=startX - step;
			while (true)
			{
				if (index - 2 * _padding < endX)
				{
					if (index - _padding > endX)
					{
						cavLine.graphics.lineStyle(1, _blackColor, 1);
						cavLine.graphics.lineTo(index - _padding, startY);
						cavLine.graphics.lineStyle(1, _whiteColor, 1);
						cavLine.graphics.lineTo(endX, startY);
					}
					else
					{
						cavLine.graphics.lineStyle(1, _blackColor, 1);
						cavLine.graphics.lineTo(endX, startY);
					}
					break;
				}
				else
				{
					cavLine.graphics.lineStyle(1, _blackColor, 1);
					cavLine.graphics.lineTo(index - _padding, startY);
					cavLine.graphics.lineStyle(1, _whiteColor, 1);
					cavLine.graphics.lineTo(index - 2 * _padding, startY);

				}
				index-=2 * _padding;
			}
		}

		/**
		 * @private 从上至下画一条垂直虚线
		 */
		private function DrawVerticalLine(step:int, startX:int, startY:int, endY:int):void
		{
			cavLine.graphics.moveTo(startX, startY);
			if (step > _padding)
			{
				cavLine.graphics.lineStyle(1, _blackColor, 1);
				cavLine.graphics.lineTo(startX, startY + step - _padding);
			}
			cavLine.graphics.lineStyle(1, _whiteColor, 1);
			cavLine.graphics.lineTo(startX, startY + step);

			var index:int=startY + step;
			while (true)
			{
				if (index + 2 * _padding > endY)
				{
					if (index + _padding < endY)
					{
						cavLine.graphics.lineStyle(1, _blackColor, 1);
						cavLine.graphics.lineTo(startX, index + _padding);
						cavLine.graphics.lineStyle(1, _whiteColor, 1);
						cavLine.graphics.lineTo(startX, endY);
					}
					else
					{
						cavLine.graphics.lineStyle(1, _blackColor, 1);
						cavLine.graphics.lineTo(startX, endY);
					}
					break;
				}
				else
				{
					cavLine.graphics.lineStyle(1, _blackColor, 1);
					cavLine.graphics.lineTo(startX, index + _padding);
					cavLine.graphics.lineStyle(1, _whiteColor, 1);
					cavLine.graphics.lineTo(startX, index + 2 * _padding);

				}
				index+=2 * _padding;
			}
		}

		/**
		 * @private 从下至上画一条垂直虚线
		 */
		private function DrawVerticalLine2(step:int, startX:int, startY:int, endY:int):void
		{
			cavLine.graphics.moveTo(startX, startY);
			if (step > _padding)
			{
				cavLine.graphics.lineStyle(1, _blackColor, 1);
				cavLine.graphics.lineTo(startX, startY - step + _padding);
			}
			cavLine.graphics.lineStyle(1, _whiteColor, 1);
			cavLine.graphics.lineTo(startX, startY - step);

			var index:int=startY - step;
			while (true)
			{
				if (index - 2 * _padding < endY)
				{
					if (index - _padding > endY)
					{
						cavLine.graphics.lineStyle(1, _blackColor, 1);
						cavLine.graphics.lineTo(startX, index - _padding);
						cavLine.graphics.lineStyle(1, _whiteColor, 1);
						cavLine.graphics.lineTo(startX, endY);
					}
					else
					{
						cavLine.graphics.lineStyle(1, _blackColor, 1);
						cavLine.graphics.lineTo(startX, endY);
					}
					break;
				}
				else
				{
					cavLine.graphics.lineStyle(1, _blackColor, 1);
					cavLine.graphics.lineTo(startX, index - _padding);
					cavLine.graphics.lineStyle(1, _whiteColor, 1);
					cavLine.graphics.lineTo(startX, index - 2 * _padding);

				}
				index-=2 * _padding;
			}
		}
	}
}