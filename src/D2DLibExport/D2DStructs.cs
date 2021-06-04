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
using System.Drawing;
using System.Runtime.InteropServices;

using FLOAT = System.Single;

namespace unvell.D2DLib
{
	#region Color
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct D2DColor
	{
		public FLOAT r;
		public FLOAT g;
		public FLOAT b;
		public FLOAT a;

		public D2DColor(FLOAT r, FLOAT g, FLOAT b)
			: this(1, r, g, b)
		{
		}

		public D2DColor(FLOAT a, FLOAT r, FLOAT g, FLOAT b)
		{
			this.a = a;
			this.r = r;
			this.g = g;
			this.b = b;
		}

		public D2DColor(FLOAT alpha, D2DColor color)
		{
			this.a = alpha;
			this.r = color.r;
			this.g = color.g;
			this.b = color.b;
		}

		public static bool operator ==(D2DColor c1, D2DColor c2)
		{ return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b && c1.a == c2.a; }
		public static bool operator !=(D2DColor c1, D2DColor c2)
		{ return c1.r != c2.r || c1.g != c2.g || c1.b != c2.b || c1.a != c2.a; }
		public static D2DColor operator +(D2DColor c, float s)
		{ return new D2DColor(c.a + s, c.r + s, c.g + s, c.b + s); }
		public static D2DColor operator -(D2DColor c, float s)
		{ return new D2DColor(c.a - s, c.r - s, c.g - s, c.b - s); }
		public static D2DColor operator *(D2DColor c, float s)
		{ return new D2DColor(c.a, c.r * s, c.g * s, c.b * s); }
		public static D2DColor operator /(D2DColor c, float s)
		{ return new D2DColor(c.a / s, c.r / s, c.g / s, c.b / s); }
		public static D2DColor operator +(D2DColor c, D2DColor c2)
		{ return new D2DColor(c.a + c2.a, c.r + c2.r, c.g + c2.g, c.b + c2.b); }
		public static D2DColor operator -(D2DColor c, D2DColor c2)
		{ return new D2DColor(c.a - c2.a, c.r - c2.r, c.g - c2.g, c.b - c2.b); }
		public static D2DColor operator *(D2DColor c, D2DColor c2)
		{ return new D2DColor(c.a * c2.a, c.r * c2.r, c.g * c2.g, c.b * c2.b); }
		public static D2DColor operator /(D2DColor c, D2DColor c2)
		{ return new D2DColor(c.a / c2.a, c.r / c2.r, c.g / c2.g, c.b / c2.b); }

		public override bool Equals(object obj)
		{
			if (!(obj is D2DColor)) return false;
			var c2 = (D2DColor)obj;

			return this.r == c2.r && this.g == c2.g && this.b == c2.b && this.a == c2.a;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static D2DColor FromGDIColor(Color gdiColor)
		{
			return new D2DColor(gdiColor.A / 255f, gdiColor.R / 255f,
				gdiColor.G / 255f, gdiColor.B / 255f);
		}

		public static Color ToGDIColor(D2DColor d2color)
		{
			var c = MathFunctions.Clamp(d2color * 255);
			return Color.FromArgb((int)c.a, (int)c.r, (int)c.g, (int)c.b);
		}

		private static readonly Random rand = new Random();

		/// <summary>
		/// Create color by randomly color components.
		/// </summary>
		/// <returns></returns>
		public static D2DColor Randomly()
		{
			return new D2DColor(1, (float)rand.NextDouble(), (float)rand.NextDouble(),
				(float)rand.NextDouble());
		}

		public static readonly D2DColor
			Transparent = FromGDIColor(Color.Transparent),
			Black = FromGDIColor(Color.Black),
			DimGray = FromGDIColor(Color.DimGray),
			Gray = FromGDIColor(Color.Gray),
			DarkGray = FromGDIColor(Color.DarkGray),
			Silver = FromGDIColor(Color.Silver),
			GhostWhite = FromGDIColor(Color.GhostWhite),
			LightGray = FromGDIColor(Color.LightGray),
			White = FromGDIColor(Color.White),
			SlateGray = FromGDIColor(Color.SlateGray),
			DarkSlateGray = FromGDIColor(Color.DarkSlateGray),
			LightSlateGray = FromGDIColor(Color.LightSlateGray),
			WhiteSmoke = FromGDIColor(Color.WhiteSmoke),
			Honeydew = FromGDIColor(Color.Honeydew),
			FloralWhite = FromGDIColor(Color.FloralWhite),
			Ivory = FromGDIColor(Color.Ivory),
			MintCream = FromGDIColor(Color.MintCream),

			Red = FromGDIColor(Color.Red),
			DarkRed = FromGDIColor(Color.DarkRed),
			PaleVioletRed = FromGDIColor(Color.PaleVioletRed),
			OrangeRed = FromGDIColor(Color.OrangeRed),
			IndianRed = FromGDIColor(Color.IndianRed),
			MediumVioletRed = FromGDIColor(Color.MediumVioletRed),
			Coral = FromGDIColor(Color.Coral),
			LightCoral = FromGDIColor(Color.LightCoral),
			Firebrick = FromGDIColor(Color.Firebrick),
			Crimson = FromGDIColor(Color.Crimson),

			Beige = FromGDIColor(Color.Beige),
			Bisque = FromGDIColor(Color.Bisque),
			LightYellow = FromGDIColor(Color.LightYellow),
			Yellow = FromGDIColor(Color.Yellow),
			Gold = FromGDIColor(Color.Gold),
			Goldenrod = FromGDIColor(Color.Goldenrod),
			PaleGoldenrod = FromGDIColor(Color.PaleGoldenrod),
			DarkGoldenrod = FromGDIColor(Color.DarkGoldenrod),
			LightGoldenrodYellow = FromGDIColor(Color.LightGoldenrodYellow),
			Orange = FromGDIColor(Color.Orange),
			DarkOrange = FromGDIColor(Color.DarkOrange),
			BurlyWood = FromGDIColor(Color.BurlyWood),
			Chocolate = FromGDIColor(Color.Chocolate),
			Brown = FromGDIColor(Color.Brown),
			RosyBrown = FromGDIColor(Color.RosyBrown),
			SaddleBrown = FromGDIColor(Color.SaddleBrown),
			SandyBrown = FromGDIColor(Color.SandyBrown),
			Peru = FromGDIColor(Color.Peru),
			Maroon = FromGDIColor(Color.Maroon),
			Sienna = FromGDIColor(Color.Sienna),

			LawnGreen = FromGDIColor(Color.LawnGreen),
			LightGreen = FromGDIColor(Color.LightGreen),
			LightSeaGreen = FromGDIColor(Color.LightSeaGreen),
			MediumSeaGreen = FromGDIColor(Color.MediumSeaGreen),
			DarkSeaGreen = FromGDIColor(Color.DarkSeaGreen),
			Green = FromGDIColor(Color.Green),
			PaleGreen = FromGDIColor(Color.PaleGreen),
			SeaGreen = FromGDIColor(Color.SeaGreen),
			SpringGreen = FromGDIColor(Color.SpringGreen),
			MediumSpringGreen = FromGDIColor(Color.MediumSpringGreen),
			YellowGreen = FromGDIColor(Color.YellowGreen),
			Lime = FromGDIColor(Color.Lime),
			LimeGreen = FromGDIColor(Color.LimeGreen),
			DarkGreen = FromGDIColor(Color.DarkGreen),
			DarkOliveGreen = FromGDIColor(Color.DarkOliveGreen),
			Olive = FromGDIColor(Color.Olive),
			OliveDrab = FromGDIColor(Color.OliveDrab),
			ForestGreen = FromGDIColor(Color.ForestGreen),
			GreenYellow = FromGDIColor(Color.GreenYellow),
			Chartreuse = FromGDIColor(Color.Chartreuse),

			AliceBlue = FromGDIColor(Color.AliceBlue),
			LightBlue = FromGDIColor(Color.LightBlue),
			Blue = FromGDIColor(Color.Blue),
			DarkBlue = FromGDIColor(Color.DarkBlue),
			SkyBlue = FromGDIColor(Color.SkyBlue),
			DeepSkyBlue = FromGDIColor(Color.DeepSkyBlue),
			DodgerBlue = FromGDIColor(Color.DodgerBlue),
			SteelBlue = FromGDIColor(Color.SteelBlue),
			LightSteelBlue = FromGDIColor(Color.LightSteelBlue),
			BlueViolet = FromGDIColor(Color.BlueViolet),
			CadetBlue = FromGDIColor(Color.CadetBlue),
			PowderBlue = FromGDIColor(Color.PowderBlue),
			CornflowerBlue = FromGDIColor(Color.CornflowerBlue),
			RoyalBlue = FromGDIColor(Color.RoyalBlue),
			MidnightBlue = FromGDIColor(Color.MidnightBlue),
			SlateBlue = FromGDIColor(Color.SlateBlue),
			MediumSlateBlue = FromGDIColor(Color.MediumSlateBlue),
			DarkSlateBlue = FromGDIColor(Color.DarkSlateBlue),
			Navy = FromGDIColor(Color.Navy),

			Violet = FromGDIColor(Color.Violet),
			DarkViolet = FromGDIColor(Color.DarkViolet),
			Purple = FromGDIColor(Color.Purple),
			MediumPurple = FromGDIColor(Color.MediumPurple),
			Lavender = FromGDIColor(Color.Lavender),
			LavenderBlush = FromGDIColor(Color.LavenderBlush),
			Plum = FromGDIColor(Color.Plum),
			Fuchsia = FromGDIColor(Color.Fuchsia),
			Magenta = FromGDIColor(Color.Magenta),
			DarkMagenta = FromGDIColor(Color.DarkMagenta),
			Orchid = FromGDIColor(Color.Orchid),
			DarkOrchid = FromGDIColor(Color.DarkOrchid),
			MediumOrchid = FromGDIColor(Color.MediumOrchid),

			Cyan = FromGDIColor(Color.Cyan),
			DarkCyan = FromGDIColor(Color.DarkCyan),
			LightCyan = FromGDIColor(Color.LightCyan),
			Teal = FromGDIColor(Color.Teal),
			Aqua = FromGDIColor(Color.Aqua),
			Aquamarine = FromGDIColor(Color.Aquamarine),
			MediumAquamarine = FromGDIColor(Color.MediumAquamarine),
			Turquoise = FromGDIColor(Color.Turquoise),
			DarkTurquoise = FromGDIColor(Color.DarkTurquoise),
			MediumTurquoise = FromGDIColor(Color.MediumTurquoise),
			PaleTurquoise = FromGDIColor(Color.PaleTurquoise),

			Cornsilk = FromGDIColor(Color.Cornsilk),
			Thistle = FromGDIColor(Color.Thistle),
			Tomato = FromGDIColor(Color.Tomato),

			Pink = FromGDIColor(Color.Pink),
			DeepPink = FromGDIColor(Color.DeepPink),
			HotPink = FromGDIColor(Color.HotPink),
			LightPink = FromGDIColor(Color.LightPink),

			Salmon = FromGDIColor(Color.Salmon),
			LightSalmon = FromGDIColor(Color.LightSalmon),
			DarkSalmon = FromGDIColor(Color.DarkSalmon),
			Moccasin = FromGDIColor(Color.Moccasin),
			NavajoWhite = FromGDIColor(Color.NavajoWhite),
			Tan = FromGDIColor(Color.Tan),
			Wheat = FromGDIColor(Color.Wheat),

			OldLace = FromGDIColor(Color.OldLace),
			MistyRose = FromGDIColor(Color.MistyRose),
			Khaki = FromGDIColor(Color.Khaki),
			DarkKhaki = FromGDIColor(Color.DarkKhaki),
			AntiqueWhite = FromGDIColor(Color.AntiqueWhite),
			PapayaWhip = FromGDIColor(Color.PapayaWhip),
			BlanchedAlmond = FromGDIColor(Color.BlanchedAlmond),
			Linen = FromGDIColor(Color.Linen),
			PeachPuff = FromGDIColor(Color.PeachPuff),
			LemonChiffon = FromGDIColor(Color.LemonChiffon),
			SeaShell = FromGDIColor(Color.SeaShell)
			;
	}
	#endregion

	#region Rect
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct D2DRect
	{
		public FLOAT left;
		public FLOAT top;
		public FLOAT right;
		public FLOAT bottom;

		public D2DRect(float left, float top, float width, float height)
		{
			this.left = left;
			this.top = top;
			this.right = left + width;
			this.bottom = top + height;
		}

		public D2DRect(D2DPoint origin, D2DSize size)
			: this(origin.x - size.width * 0.5f, origin.y - size.height * 0.5f, size.width, size.height)
		{ }

		public D2DPoint Location
		{
			get { return new D2DPoint(left, top); }
			set
			{
				FLOAT width = this.right - this.left;
				FLOAT height = this.bottom - this.top;
				this.left = value.x;
				this.right = value.x + width;
				this.top = value.y;
				this.bottom = value.y + height;
			}
		}

		public FLOAT Width
		{
			get { return this.right - this.left; }
			set { this.right = this.left + value; }
		}

		public FLOAT Height
		{
			get { return this.bottom - this.top; }
			set { this.bottom = this.top + value; }
		}

		public void Offset(FLOAT x, FLOAT y)
		{
			this.left += x;
			this.right += x;
			this.top += y;
			this.bottom += y;
		}

		public FLOAT X
		{
			get { return this.left; }
			set
			{
				FLOAT width = this.right - this.left;
				this.left = value;
				this.right = value + width;
			}
		}

		public FLOAT Y
		{
			get { return this.top; }
			set
			{
				FLOAT height = this.bottom - this.top;
				this.top = value;
				this.bottom = value + height;
			}
		}

		public D2DSize Size
		{
			get
			{
				return new D2DSize(this.Width, this.Height);
			}
			set
			{
				this.Width = value.width;
				this.Height = value.height;
			}
		}

		public static implicit operator D2DRect(Rectangle rect)
		{
			return new D2DRect(rect.X, rect.Y, rect.Width, rect.Height);
		}

		public static implicit operator D2DRect(RectangleF rect)
		{
			return new D2DRect(rect.X, rect.Y, rect.Width, rect.Height);
		}

		public static implicit operator RectangleF(D2DRect rect)
		{
			return new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
		}

		public static explicit operator Rectangle(D2DRect rect)
		{
			return Rectangle.Round(rect);
		}
	}
	#endregion Rect

	#region Rounded Rect

	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct D2DRoundedRect
	{
		public D2DRect rect;
		public FLOAT radiusX;
		public FLOAT radiusY;
	}
	#endregion Rounded Rect

	#region Point
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct D2DPoint
	{
		public FLOAT x;
		public FLOAT y;

		public D2DPoint(FLOAT x, FLOAT y)
		{
			this.x = x;
			this.y = y;
		}

		public void Offset(FLOAT offx, FLOAT offy)
		{
			this.x += offx;
			this.y += offy;
		}

		public static readonly D2DPoint Zero = new D2DPoint(0, 0);
		public static readonly D2DPoint One = new D2DPoint(1, 1);

		public override bool Equals(object obj)
		{
			if (!(obj is D2DPoint)) return false;

			var p2 = (D2DPoint)obj;

			return x == p2.x && y == p2.y;
		}

		public static bool operator==(D2DPoint p1, D2DPoint p2)
		{
			return p1.x == p2.x && p1.y == p2.y;
		}

		public static bool operator !=(D2DPoint p1, D2DPoint p2)
		{
			return p1.x != p2.x || p1.y != p2.y;
		}

		public static implicit operator D2DPoint(Point p)
		{
			return new D2DPoint(p.X, p.Y);
		}

		public static implicit operator D2DPoint(PointF p)
		{
			return new D2DPoint(p.X, p.Y);
		}

		public static implicit operator PointF(D2DPoint p)
		{
			return new PointF(p.x, p.y);
		}

		public static explicit operator Point(D2DPoint p)
		{
			return Point.Round(p);
		}

		public override int GetHashCode()
		{
			return (int)((this.x * 0xff) + this.y);
		}
	}
	#endregion

	#region Size
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct D2DSize
	{
		public FLOAT width;
		public FLOAT height;

		public D2DSize(FLOAT width, FLOAT height)
		{
			this.width = width;
			this.height = height;
		}

		public static readonly D2DSize Empty = new D2DSize(0, 0);

		public static implicit operator D2DSize(Size wsize)
		{
			return new D2DSize(wsize.Width, wsize.Height);
		}

		public static implicit operator D2DSize(SizeF wsize)
		{
			return new D2DSize(wsize.Width, wsize.Height);
		}

		public static implicit operator SizeF(D2DSize s)
		{
			return new SizeF(s.width, s.height);
		}

		public static explicit operator Size(D2DSize s)
		{
			return Size.Round(s);
		}

		public override string ToString()
		{
			return string.Format("D2DSize({0}, {1})", this.width, this.height);
		}
	}
	#endregion Size

	#region Ellipse
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct D2DEllipse
	{
		public D2DPoint origin;
		public FLOAT radiusX;
		public FLOAT radiusY;

		public D2DEllipse(D2DPoint center, FLOAT radiusX, FLOAT radiusY)
		{
			this.origin = center;
			this.radiusX = radiusX;
			this.radiusY = radiusY;
		}


		public D2DEllipse(D2DPoint center, D2DSize radius)
			: this(center, radius.width, radius.height)
		{
		}

		public D2DEllipse(FLOAT x, FLOAT y, FLOAT rx, FLOAT ry)
			: this(new D2DPoint(x, y), rx, ry)
		{
		}

		public FLOAT X { get { return origin.x; } set { origin.x = value; } }
		public FLOAT Y { get { return origin.y; } set { origin.y = value; } }
	}
	#endregion

	#region BezierSegment
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct D2DBezierSegment
	{
		public D2DPoint point1;
		public D2DPoint point2;
		public D2DPoint point3;

		public D2DBezierSegment(D2DPoint point1, D2DPoint point2, D2DPoint point3)
		{
			this.point1 = point1;
			this.point2 = point2;
			this.point3 = point3;
		}

		public D2DBezierSegment(FLOAT x1, FLOAT y1, FLOAT x2, FLOAT y2, FLOAT x3, FLOAT y3)
		{
			this.point1 = new D2DPoint(x1, y1);
			this.point2 = new D2DPoint(x2, y2);
			this.point3 = new D2DPoint(x3, y3);
		}
	}
	#endregion

	#region Matrix
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct D2DMatrix3x2
	{
		public FLOAT a1, b1;
		public FLOAT a2, b2;
		public FLOAT a3, b3;

		public D2DMatrix3x2(float a1, float b1, float a2, float b2, float a3, float b3) {
			this.a1 = a1; this.b1 = b1;
			this.a2 = a2; this.b2 = b2;
			this.a3 = a3; this.b3 = b3;
		}
	}

	#endregion // Matrix
}
