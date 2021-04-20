/*
* MIT License
*
* Copyright (c) 2009-2021 Jingwood, unvell.com. All right reserved.
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace unvell.D2DLib.WinForm
{
	public static class D2DWinFormExtensions
	{
		public static void DrawBitmap(this D2DGraphics g, Bitmap bitmap, float x, float y, float opacity = 1,
			bool alpha = false, D2DBitmapInterpolationMode interpolationMode = D2DBitmapInterpolationMode.Linear)
		{
			g.DrawBitmap(bitmap, new D2DRect(x, y, bitmap.Width, bitmap.Height), opacity, alpha, interpolationMode);
		}

		public static void DrawBitmap(this D2DGraphics g, Bitmap bitmap, D2DRect destRect, float opacity = 1,
			bool alpha = false, D2DBitmapInterpolationMode interpolationMode = D2DBitmapInterpolationMode.Linear)
		{
			IntPtr hbitmap = bitmap.GetHbitmap();

			if (hbitmap != IntPtr.Zero)
			{
				var srcRect = new D2DRect(0, 0, bitmap.Width, bitmap.Height);

				g.DrawGDIBitmap(hbitmap, destRect, srcRect, opacity, alpha, interpolationMode);
				Win32.DeleteObject(hbitmap);
			}
		}
    #region Brush-related text drawing
    public static void DrawText(this D2DGraphics g, string text, D2DBrush brush, float x, float y,
			DWriteTextAlignment halign = DWriteTextAlignment.Leading, DWriteParagraphAlignment valign = DWriteParagraphAlignment.Near,
			DWriteFontWeight fontWeight = DWriteFontWeight.Normal, DWriteFontStyle fontStyle = DWriteFontStyle.Normal,
			DWriteFontStretch fontStretch = DWriteFontStretch.Normal, DWriteWordWrapping textWrap = DWriteWordWrapping.None,
			DWriteTextOptions textOptions = DWriteTextOptions.None)
		{
			using (var font = new Font("Arial", 8.25f))
			{
				g.DrawText(text, brush, font, x, y, halign, valign, fontWeight, fontStyle, fontStretch, textWrap, textOptions);
			}
		}

		public static void DrawText(this D2DGraphics g, string text, D2DBrush brush, Font font, float x, float y,
			DWriteTextAlignment halign = DWriteTextAlignment.Leading, DWriteParagraphAlignment valign = DWriteParagraphAlignment.Near,
			DWriteFontWeight fontWeight = DWriteFontWeight.Normal, DWriteFontStyle fontStyle = DWriteFontStyle.Normal,
			DWriteFontStretch fontStretch = DWriteFontStretch.Normal, DWriteWordWrapping textWrap = DWriteWordWrapping.None,
			DWriteTextOptions textOptions = DWriteTextOptions.None)
		{
			var rect = new D2DRect(x, y, int.MaxValue, int.MaxValue);
			g.DrawText(text, brush, font.Name, font.Size * 96f / 72f, rect, halign, valign, fontWeight, fontStyle, fontStretch, textWrap, textOptions);
		}

		public static void DrawText(this D2DGraphics g, string text, D2DBrush brush, Font font, Point location,
			DWriteTextAlignment halign=DWriteTextAlignment.Leading, DWriteParagraphAlignment valign=DWriteParagraphAlignment.Near,
			DWriteFontWeight fontWeight = DWriteFontWeight.Normal, DWriteFontStyle fontStyle = DWriteFontStyle.Normal,
			DWriteFontStretch fontStretch = DWriteFontStretch.Normal, DWriteWordWrapping textWrap = DWriteWordWrapping.None,
			DWriteTextOptions textOptions = DWriteTextOptions.None)
		{
			var rect = new D2DRect(location.X, location.Y, int.MaxValue, int.MaxValue);
			g.DrawText(text, brush, font.Name, font.Size * 96f / 72f, rect, halign, valign, fontWeight, fontStyle, fontStretch, textWrap, textOptions);
		}
		public static void DrawText(this D2DGraphics g, string text, D2DBrush brush, Font font, D2DRect rect,
			DWriteTextAlignment halign = DWriteTextAlignment.Leading, DWriteParagraphAlignment valign = DWriteParagraphAlignment.Near,
			DWriteFontWeight fontWeight = DWriteFontWeight.Normal, DWriteFontStyle fontStyle = DWriteFontStyle.Normal,
			DWriteFontStretch fontStretch = DWriteFontStretch.Normal, DWriteWordWrapping textWrap = DWriteWordWrapping.None,
			DWriteTextOptions textOptions = DWriteTextOptions.None)
		{
			g.DrawText(text, brush, font.Name, font.Size * 96f / 72f, rect, halign, valign, fontWeight, fontStyle, fontStretch, textWrap, textOptions);
		}
		public static void DrawText(this D2DGraphics g, string text, D2DBrush brush, Font font, PointF location,
			DWriteFontWeight fontWeight = DWriteFontWeight.Normal, DWriteFontStyle fontStyle = DWriteFontStyle.Normal,
			DWriteFontStretch fontStretch = DWriteFontStretch.Normal, DWriteWordWrapping textWrap = DWriteWordWrapping.None,
			DWriteTextOptions textOptions = DWriteTextOptions.None)
		{
			var rect = new D2DRect(location.X, location.Y, int.MaxValue, int.MaxValue);
			g.DrawText(text, brush, font.Name, font.Size * 96f / 72f, rect,
				fontWeight: fontWeight, fontStyle: fontStyle, fontStretch: fontStretch, textWrap: textWrap, textOptions: textOptions);
		}
    #endregion
    #region Color-related text drawing
    public static void DrawText(this D2DGraphics g, string text, D2DColor color, float x, float y,
			DWriteTextAlignment halign = DWriteTextAlignment.Leading, DWriteParagraphAlignment valign = DWriteParagraphAlignment.Near,
			DWriteFontWeight fontWeight = DWriteFontWeight.Normal, DWriteFontStyle fontStyle = DWriteFontStyle.Normal,
			DWriteFontStretch fontStretch = DWriteFontStretch.Normal, DWriteWordWrapping textWrap = DWriteWordWrapping.None,
			DWriteTextOptions textOptions = DWriteTextOptions.None)
		{
			using (var font = new Font("Arial", 8.25f))
			{
				g.DrawText(text, color, font, x, y, halign, valign, fontWeight, fontStyle, fontStretch, textWrap, textOptions);
			}
		}
		public static void DrawText(this D2DGraphics g, string text, D2DColor color, Font font, float x, float y,
			DWriteTextAlignment halign = DWriteTextAlignment.Leading, DWriteParagraphAlignment valign = DWriteParagraphAlignment.Near,
			DWriteFontWeight fontWeight = DWriteFontWeight.Normal, DWriteFontStyle fontStyle = DWriteFontStyle.Normal,
			DWriteFontStretch fontStretch = DWriteFontStretch.Normal, DWriteWordWrapping textWrap = DWriteWordWrapping.None,
			DWriteTextOptions textOptions = DWriteTextOptions.None)
		{
			var rect = new D2DRect(x, y, int.MaxValue, int.MaxValue);
			g.DrawText(text, color, font.Name, font.Size * 96f / 72f, rect, halign, valign, fontWeight, fontStyle, fontStretch, textWrap, textOptions);
		}

		public static void DrawText(this D2DGraphics g, string text, D2DColor color, Font font, Point location,
			DWriteTextAlignment halign = DWriteTextAlignment.Leading, DWriteParagraphAlignment valign = DWriteParagraphAlignment.Near,
			DWriteFontWeight fontWeight = DWriteFontWeight.Normal, DWriteFontStyle fontStyle = DWriteFontStyle.Normal,
			DWriteFontStretch fontStretch = DWriteFontStretch.Normal, DWriteWordWrapping textWrap = DWriteWordWrapping.None,
			DWriteTextOptions textOptions = DWriteTextOptions.None)
		{
			var rect = new D2DRect(location.X, location.Y, int.MaxValue, int.MaxValue);
			g.DrawText(text, color, font.Name, font.Size * 96f / 72f, rect, halign, valign, fontWeight, fontStyle, fontStretch, textWrap, textOptions);
		}
		public static void DrawText(this D2DGraphics g, string text, D2DColor color, Font font, D2DRect rect,
			DWriteTextAlignment halign = DWriteTextAlignment.Leading, DWriteParagraphAlignment valign = DWriteParagraphAlignment.Near,
			DWriteFontWeight fontWeight = DWriteFontWeight.Normal, DWriteFontStyle fontStyle = DWriteFontStyle.Normal,
			DWriteFontStretch fontStretch = DWriteFontStretch.Normal, DWriteWordWrapping textWrap = DWriteWordWrapping.None,
			DWriteTextOptions textOptions = DWriteTextOptions.None)
		{
			g.DrawText(text, color, font.Name, font.Size * 96f / 72f, rect, halign, valign, fontWeight, fontStyle, fontStretch, textWrap, textOptions);
		}
		public static void DrawText(this D2DGraphics g, string text, D2DColor color, Font font, PointF location,
			DWriteFontWeight fontWeight = DWriteFontWeight.Normal, DWriteFontStyle fontStyle = DWriteFontStyle.Normal,
			DWriteFontStretch fontStretch = DWriteFontStretch.Normal, DWriteWordWrapping textWrap = DWriteWordWrapping.None,
			DWriteTextOptions textOptions = DWriteTextOptions.None)
		{
			var rect = new D2DRect(location.X, location.Y, int.MaxValue, int.MaxValue);
			g.DrawText(text, color, font.Name, font.Size * 96f / 72f, rect,
				fontWeight: fontWeight, fontStyle: fontStyle, fontStretch: fontStretch, textWrap: textWrap, textOptions: textOptions);
		}
    #endregion
  }
}

namespace unvell.D2DLib
{
	public static class D2DStructExtensions
	{
	
	}
}
