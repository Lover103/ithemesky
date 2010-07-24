package com.nd.web.iphone.view.component
{
	import flash.display.Graphics;
	import flash.display.Sprite;

	import mx.controls.TileList;
	import mx.controls.listClasses.IListItemRenderer;

	public class CustomTileList extends TileList
	{
		override protected function drawSelectionIndicator(indicator:Sprite, x:Number, y:Number, width:Number, height:Number, color:uint, itemRenderer:IListItemRenderer):void
		{
			var g:Graphics=Sprite(indicator).graphics;
			g.clear();
			g.lineStyle(1, 0xFFFF0000);
			g.drawRect(0, 0, width - 5, height - 2);
			g.endFill();
			indicator.x=x;
			indicator.y=y;
		}

		override protected function drawHighlightIndicator(indicator:Sprite, x:Number, y:Number, width:Number, height:Number, color:uint, itemRenderer:IListItemRenderer):void
		{
			var g:Graphics=Sprite(indicator).graphics;
			g.clear();
			g.lineStyle(1, 0xFF00FFFF);
			g.drawRect(0, 0, width - 5, height - 2);
			g.endFill();
			indicator.x=x;
			indicator.y=y;
		}

	}
}