﻿/*
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
#pragma warning disable CS1591 // Komentář XML pro veřejně viditelný typ nebo člen se nenašel.

using System;
using System.Runtime.InteropServices;

using FLOAT = System.Single;
using UINT = System.UInt32;
using HWND = System.IntPtr;
using HANDLE = System.IntPtr;

namespace unvell.D2DLib
{
	public class D2DDevice : IDisposable
	{
		internal HANDLE Handle { get; private set; }

		internal D2DDevice(HANDLE deviceHandle)
		{
			this.Handle = deviceHandle;
		}

		public static D2DDevice FromHwnd(HWND hwnd)
		{
			HANDLE contextHandle = D2D.CreateContext(hwnd);
			return new D2DDevice(contextHandle);
		}

		public void Resize()
		{
			if (Handle != HANDLE.Zero) D2D.ResizeContext(this.Handle);
		}

		public D2DPen CreatePen(D2DColor color, D2DDashStyle dashStyle = D2DDashStyle.Solid, 
			float[] customDashes = null, float dashOffset = 0.0f)
		{
			HANDLE handle = D2D.CreatePen(this.Handle, color, dashStyle,
				customDashes, customDashes != null ? (uint)customDashes.Length : 0, dashOffset);

			return handle == HANDLE.Zero ? null : new D2DPen(this, handle, color, dashStyle, customDashes, dashOffset);
		}
	
		internal void DestroyPen(D2DPen pen)
		{
			if (pen == null) throw new ArgumentNullException(nameof(pen));
			D2D.DestroyPen(pen.Handle);
		}

		public D2DSolidColorBrush CreateSolidColorBrush(D2DColor color)
		{
			HANDLE handle = D2D.CreateSolidColorBrush(this.Handle, color);
			return handle == HANDLE.Zero ? null : new D2DSolidColorBrush(handle, color);
		}

		public D2DLinearGradientBrush CreateLinearGradientBrush(float start_x, float start_y, float end_x, float end_y, D2DGradientStop[] gradientStops,
			D2DGradientExtendMode extendMode = D2DGradientExtendMode.None, D2DGradientGamma gamma = D2DGradientGamma.TwoPointTwo)
		{
			return CreateLinearGradientBrush(new D2DPoint(start_x, start_y), new D2DPoint(end_x, end_y), gradientStops, extendMode, gamma);
		}
		public D2DLinearGradientBrush CreateLinearGradientBrush(decimal start_x, decimal start_y, decimal end_x, decimal end_y, D2DGradientStop[] gradientStops,
			D2DGradientExtendMode extendMode = D2DGradientExtendMode.None, D2DGradientGamma gamma = D2DGradientGamma.TwoPointTwo)
		{
			return CreateLinearGradientBrush(new D2DPoint((float)start_x, (float)start_y), new D2DPoint((float)end_x, (float)end_y), gradientStops, extendMode, gamma);
		}
		public D2DLinearGradientBrush CreateLinearGradientBrush(D2DPoint startPoint, D2DPoint endPoint, D2DGradientStop[] gradientStops,
			D2DGradientExtendMode extendMode = D2DGradientExtendMode.None, D2DGradientGamma gamma = D2DGradientGamma.TwoPointTwo)
		{
			HANDLE handle = D2D.CreateLinearGradientBrush(this.Handle, startPoint, endPoint, gradientStops, (uint)gradientStops.Length, extendMode, gamma);
			return new D2DLinearGradientBrush(handle, gradientStops);
		}

		public D2DRadialGradientBrush CreateRadialGradientBrush(D2DPoint origin, D2DPoint offset, FLOAT radiusX, FLOAT radiusY, D2DGradientStop[] gradientStops,
			D2DGradientExtendMode extendMode = D2DGradientExtendMode.None, D2DGradientGamma gamma = D2DGradientGamma.TwoPointTwo)
		{
			HANDLE handle = D2D.CreateRadialGradientBrush(this.Handle, origin, offset, radiusX, radiusY,
				gradientStops, (uint)gradientStops.Length, extendMode, gamma);
			return new D2DRadialGradientBrush(handle, gradientStops);
		}
		/// <summary> Creates a radial (circular) gradient brush, which starts in a center and then blends outwards.</summary>
		/// <param name="center_x">The X coordinate of the circle's center.</param>
		/// <param name="center_y">The Y coordinate of the circle's center.</param>
		/// <param name="offset_x">The X coordinate of the gradient start. Consider this as a smudge tool that shifts the circle's center, yet the outer circle stays at the same position.</param>
		/// <param name="offset_y">The Y coordinate of the gradient start.</param>
		/// <param name="radiusX">Half of the circle's width.</param>
		/// <param name="radiusY">Half of the circle's height.</param>
		/// <param name="gradientStops"></param>
		/// <param name="extendMode">How will the gradient look like beyond its specified boundaries.</param>
		/// <param name="gamma"></param>
		/// <returns></returns>
		public D2DRadialGradientBrush CreateRadialGradientBrush(float center_x, float center_y, float offset_x, float offset_y,
			FLOAT radiusX, FLOAT radiusY, D2DGradientStop[] gradientStops,
			D2DGradientExtendMode extendMode = D2DGradientExtendMode.None, D2DGradientGamma gamma = D2DGradientGamma.TwoPointTwo)
    {
			return CreateRadialGradientBrush(new D2DPoint(center_x, center_y), new D2DPoint(offset_x, offset_y), radiusX, radiusY, gradientStops, extendMode, gamma);
		}
    /// <summary> Creates a radial (circular) gradient brush, which starts in a center and then blends outwards.</summary>
    /// <param name="center_x">The X coordinate of the circle's center.</param>
    /// <param name="center_y">The Y coordinate of the circle's center.</param>
    /// <param name="offset_x">The X coordinate of the gradient start. Consider this as a smudge tool that shifts the circle's center, yet the outer circle stays at the same position.</param>
    /// <param name="offset_y">The Y coordinate of the gradient start.</param>
    /// <param name="radiusX">Half of the circle's width.</param>
    /// <param name="radiusY">Half of the circle's height.</param>
    /// <param name="gradientStops"></param>
    /// <param name="extendMode">How will the gradient look like beyond its specified boundaries.</param>
    /// <param name="gamma"></param>
    /// <returns></returns>
    public D2DRadialGradientBrush CreateRadialGradientBrush(decimal center_x, decimal center_y, decimal offset_x, decimal offset_y,
			decimal radiusX, decimal radiusY, D2DGradientStop[] gradientStops,
			D2DGradientExtendMode extendMode = D2DGradientExtendMode.None, D2DGradientGamma gamma = D2DGradientGamma.TwoPointTwo)
		{
			return CreateRadialGradientBrush(new D2DPoint((float)center_x, (float)center_y),
				new D2DPoint((float)offset_x, (float)offset_y), (float)radiusX, (float)radiusY, gradientStops, extendMode, gamma);
		}

		public D2DRectangleGeometry CreateRectangleGeometry(FLOAT width, FLOAT height)
		{
			var rect = new D2DRect(0, 0, width, height);
			return CreateRectangleGeometry(rect);
		}

		public D2DRectangleGeometry CreateRectangleGeometry(D2DRect rect)
		{
			HANDLE rectGeometryHandle = D2D.CreateRectangleGeometry(this.Handle, ref rect);
			return new D2DRectangleGeometry(this.Handle, rectGeometryHandle);
		}

		public D2DPathGeometry CreatePathGeometry()
		{
			HANDLE geoHandle = D2D.CreatePathGeometry(this.Handle);
			return new D2DPathGeometry(this.Handle, geoHandle);
		}

    public D2DGeometry CreateEllipseGeometry(D2DPoint origin, D2DSize size)
    {
      var ellipse = new D2DEllipse(origin, size);
      return new D2DGeometry(this.Handle, D2D.CreateEllipseGeometry(this.Handle, ref ellipse));
    }

    public D2DGeometry CreatePieGeometry(D2DPoint origin, D2DSize size, float startAngle, float endAngle)
		{
			var path = this.CreatePathGeometry();

			var halfSize = new D2DSize(size.width * 0.5f, size.height * 0.5f);

			var sangle = startAngle * Math.PI / 180f;
			var eangle = endAngle * Math.PI / 180f;
			var angleDiff = endAngle - startAngle;

			var startPoint = new D2DPoint((float)(origin.x + halfSize.width * Math.Cos(sangle)),
				(float)(origin.y + halfSize.height * Math.Sin(sangle)));

			var endPoint = new D2DPoint((float)(origin.x + halfSize.width * Math.Cos(eangle)),
				(float)(origin.y + halfSize.height * Math.Sin(eangle)));

			path.AddLines(new D2DPoint[] { origin, startPoint });

			path.AddArc(endPoint, halfSize, angleDiff,
				angleDiff > 180 ? D2DArcSize.Large : D2DArcSize.Small,
				D2DSweepDirection.Clockwise);

			path.ClosePath();

			return path;
		}

		public D2DBitmap LoadBitmap(byte[] buffer)
		{
			return this.LoadBitmap(buffer, 0, (uint)buffer.Length);
		}

		public D2DBitmap LoadBitmap(byte[] buffer, UINT offset, UINT length)
		{
			var bitmapHandle = D2D.CreateBitmapFromBytes(this.Handle, buffer, offset, length);
			return (bitmapHandle != HWND.Zero) ? new D2DBitmap(bitmapHandle) : null;
		}

		public D2DBitmap LoadBitmap(string filepath)
		{
			return CreateBitmapFromFile(filepath);
		}

		public D2DBitmap CreateBitmapFromFile(string filepath)
		{
			var bitmapHandle = D2D.CreateBitmapFromFile(this.Handle, filepath);
			return (bitmapHandle != HWND.Zero) ? new D2DBitmap(bitmapHandle) : null;
		}

		public D2DBitmap CreateBitmapFromMemory(UINT width, UINT height, UINT stride, IntPtr buffer, UINT offset, UINT length)
		{
			HANDLE d2dbmp = D2D.CreateBitmapFromMemory(this.Handle, width, height, stride, buffer, offset, length);
			return d2dbmp == HANDLE.Zero ? null : new D2DBitmap(d2dbmp);
		}

    public D2DLayer CreateLayer()
    {
      return new D2DLayer(D2D.CreateLayer(this.Handle));
    }

		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr obj);

		public D2DBitmap CreateBitmapFromHBitmap(HWND hbmp, bool useAlphaChannel)
		{
			HANDLE d2dbmp = D2D.CreateBitmapFromHBitmap(this.Handle, hbmp, useAlphaChannel);
			return d2dbmp == HANDLE.Zero ? null : new D2DBitmap(d2dbmp);
		}

		public D2DBitmap CreateBitmapFromGDIBitmap(System.Drawing.Bitmap bmp)
		{
			bool useAlphaChannel =
				(bmp.PixelFormat & System.Drawing.Imaging.PixelFormat.Alpha) == System.Drawing.Imaging.PixelFormat.Alpha;

			return this.CreateBitmapFromGDIBitmap(bmp, useAlphaChannel);
		}

		public D2DBitmap CreateBitmapFromGDIBitmap(System.Drawing.Bitmap bmp, bool useAlphaChannel)
		{
			HANDLE d2dbmp = HANDLE.Zero;
			HANDLE hbitmap = bmp.GetHbitmap();

			if (hbitmap != HANDLE.Zero)
			{
				d2dbmp = D2D.CreateBitmapFromHBitmap(this.Handle, hbitmap, useAlphaChannel);
				DeleteObject(hbitmap);
			}

			return d2dbmp == HANDLE.Zero ? null : new D2DBitmap(d2dbmp);
		}

		public D2DBitmapGraphics CreateBitmapGraphics()
		{
			return CreateBitmapGraphics(D2DSize.Empty);
		}

		public D2DBitmapGraphics CreateBitmapGraphics(float width, float height)
		{
			return CreateBitmapGraphics(new D2DSize(width, height));
		}

		public D2DBitmapGraphics CreateBitmapGraphics(D2DSize size)
		{
			HANDLE bitmapRenderTargetHandle = D2D.CreateBitmapRenderTarget(this.Handle, size);
			return bitmapRenderTargetHandle == HANDLE.Zero ? null
				: new D2DBitmapGraphics(bitmapRenderTargetHandle);
		}

    public void Dispose()
    {
			D2D.DestroyContext(this.Handle);
		}
	}
}
#pragma warning restore CS1591 // Komentář XML pro veřejně viditelný typ nebo člen se nenašel.
