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

#include "stdafx.h"
#include "Simple.h"
#include "Pen.h"
#include "Brush.h"

void DrawLine(HANDLE ctx, D2D1_POINT_2F start, D2D1_POINT_2F end, D2D1_COLOR_F color,
	FLOAT width, D2D1_DASH_STYLE dashStyle, D2D1_CAP_STYLE startCap, D2D1_CAP_STYLE endCap,
	D2D1_CAP_STYLE gapCap, D2D1_LINE_JOIN lineJoin, float* customDashes, UINT32 CDcount)
{
	RetrieveContext(ctx);
	ID2D1SolidColorBrush* brush = NULL;
	if (color.a > 0) context->renderTarget->CreateSolidColorBrush(color, &brush);
	if (brush != NULL)
	{
		BrushContext brushCtx;
		brushCtx.brush = brush;
		DrawLineWithBrush(ctx, &brushCtx, start, end, width, dashStyle, startCap, endCap, gapCap, lineJoin, customDashes, CDcount);
	}
	SafeRelease(&brush);
}
void DrawLineWithBrush(HANDLE ctx, HANDLE brushHandle, D2D1_POINT_2F start, D2D1_POINT_2F end,
	FLOAT width, D2D1_DASH_STYLE dashStyle, D2D1_CAP_STYLE startCap, D2D1_CAP_STYLE endCap,
	D2D1_CAP_STYLE gapCap, D2D1_LINE_JOIN lineJoin, float* customDashes, UINT32 CDcount)
{
	RetrieveContext(ctx);
	ID2D1StrokeStyle* strokeStyle = NULL;
	ID2D1Brush* brush = reinterpret_cast<ID2D1Brush*>(reinterpret_cast<BrushContext*>(brushHandle)->brush);
	if (brush != NULL) {
		context->factory->CreateStrokeStyle(D2D1::StrokeStyleProperties(
			startCap, endCap, gapCap, lineJoin, 10.0f, dashStyle, 0.0f), customDashes, CDcount, &strokeStyle);
		context->renderTarget->DrawLine(start, end, brush, width, strokeStyle);
	}

	SafeRelease(&strokeStyle);
}

void DrawArrowLine(HANDLE ctx, D2D1_POINT_2F start, D2D1_POINT_2F end, D2D1_COLOR_F color,
	FLOAT width, D2D1_DASH_STYLE dashStyle, D2D1_CAP_STYLE startCap, D2D1_CAP_STYLE endCap,
	D2D1_CAP_STYLE gapCap, D2D1_LINE_JOIN lineJoin, float* customDashes, UINT32 CDcount)
{
	RetrieveContext(ctx);

	ID2D1SolidColorBrush* brush = NULL;
	(context->renderTarget)->CreateSolidColorBrush(color, &brush);

	ID2D1StrokeStyle* strokeStyle = NULL;

	if (dashStyle != D2D1_DASH_STYLE_SOLID)
	{
		context->factory->CreateStrokeStyle(D2D1::StrokeStyleProperties(
			startCap, endCap, gapCap, lineJoin, 10.0f, dashStyle, 0.0f), customDashes, CDcount, &strokeStyle);
	}

	if (brush != NULL) {
		context->renderTarget->DrawLine(start, end, brush, width, strokeStyle);
	}

	SafeRelease(&strokeStyle);
	SafeRelease(&brush);
}

D2DLIB_API void DrawLineWithPen(HANDLE ctx, D2D1_POINT_2F start, D2D1_POINT_2F end, HANDLE penHandle, FLOAT width)
{
	RetrieveContext(ctx);

	D2DPen* pen = reinterpret_cast<D2DPen*>(penHandle);

	context->renderTarget->DrawLine(start, end, pen->brush, width, pen->strokeStyle);
}

void DrawLines(HANDLE ctx, D2D1_POINT_2F* points, UINT count, D2D1_COLOR_F color,
	FLOAT width, D2D1_DASH_STYLE dashStyle, D2D1_CAP_STYLE startCap, D2D1_CAP_STYLE endCap,
	D2D1_CAP_STYLE gapCap, D2D1_LINE_JOIN lineJoin, float* customDashes, UINT32 CDcount)
{
	if (count <= 1) return;

	RetrieveContext(ctx);

	ID2D1SolidColorBrush* brush = NULL;
	ID2D1StrokeStyle* strokeStyle = NULL;
	
	context->renderTarget->CreateSolidColorBrush(color, &brush);

	if (brush != NULL) {

		if (dashStyle != D2D1_DASH_STYLE_SOLID)
		{
			context->factory->CreateStrokeStyle(D2D1::StrokeStyleProperties(
				startCap, endCap, gapCap, lineJoin, 10.0f, dashStyle, 0.0f), customDashes, CDcount, &strokeStyle);

			ID2D1PathGeometry* pathGeo = NULL;
			ID2D1GeometrySink* sink = NULL;

			context->factory->CreatePathGeometry(&pathGeo);

			if (pathGeo != NULL) {
				pathGeo->Open(&sink);
				sink->BeginFigure(points[0], D2D1_FIGURE_BEGIN::D2D1_FIGURE_BEGIN_FILLED);
				sink->AddLines(points + 1, count - 1);
				//sink->EndFigure(D2D1_FIGURE_END_CLOSED);
				sink->Close();

				context->renderTarget->DrawGeometry(pathGeo, brush, width, strokeStyle);
			}

			SafeRelease(&sink);
			SafeRelease(&pathGeo);
		}
		else
		{
			for (UINT i = 0; i < count - 1; i++)
			{
				context->renderTarget->DrawLine(points[i], points[i + 1], brush, width, strokeStyle);
			}
			//context->renderTarget->DrawLine(points[count - 1], points[0], brush, weight, strokeStyle);
		}
	}

	SafeRelease(&strokeStyle);
	SafeRelease(&brush);
}

void DrawRectangle(HANDLE ctx, D2D1_RECT_F* rect, D2D1_COLOR_F color,
	FLOAT width, D2D1_DASH_STYLE dashStyle, D2D1_CAP_STYLE startCap, D2D1_CAP_STYLE endCap,
	D2D1_CAP_STYLE gapCap, D2D1_LINE_JOIN lineJoin, float* customDashes, UINT32 CDcount)
{
	RetrieveContext(ctx);
	ID2D1SolidColorBrush* brush = NULL;
	if (color.a > 0) context->renderTarget->CreateSolidColorBrush(color, &brush);
	if (brush != NULL)
	{
		BrushContext brushCtx;
		brushCtx.brush = brush;
		DrawRectangleWithBrush(ctx, rect, &brushCtx, width, dashStyle, startCap, endCap, gapCap, lineJoin, customDashes, CDcount);
	}
	SafeRelease(&brush);
}
void DrawRectangleWithBrush(HANDLE ctx, D2D1_RECT_F* rect, HANDLE brushHandle,
	FLOAT width, D2D1_DASH_STYLE dashStyle, D2D1_CAP_STYLE startCap, D2D1_CAP_STYLE endCap,
	D2D1_CAP_STYLE gapCap, D2D1_LINE_JOIN lineJoin, float* customDashes, UINT32 CDcount)
{
	RetrieveContext(ctx);
	ID2D1StrokeStyle* strokeStyle = NULL;
	ID2D1Brush* brush = reinterpret_cast<ID2D1Brush*>(reinterpret_cast<BrushContext*>(brushHandle)->brush);
	if (brush != NULL) {
		context->factory->CreateStrokeStyle(D2D1::StrokeStyleProperties(
			startCap, endCap, gapCap, lineJoin, 10.0f, dashStyle, 0.0f), customDashes, CDcount, &strokeStyle);
		context->renderTarget->DrawRectangle(rect, brush, width, strokeStyle);
	}
	SafeRelease(&strokeStyle);
}

void DrawRectangleWithPen(HANDLE ctx, D2D1_RECT_F* rect, HANDLE strokePen, FLOAT width)
{
	RetrieveContext(ctx);

	D2DPen* pen = reinterpret_cast<D2DPen*>(strokePen);

	if (pen->brush != NULL) {
		context->renderTarget->DrawRectangle(rect, pen->brush, width, pen->strokeStyle);
	}
}

void FillRectangle(HANDLE ctx, D2D1_RECT_F* rect, D2D1_COLOR_F color)
{
	RetrieveContext(ctx);

	ID2D1SolidColorBrush* brush = NULL;
	context->renderTarget->CreateSolidColorBrush(color, &brush);

	if (brush != NULL) {
		context->renderTarget->FillRectangle(rect, brush);
	}

	SafeRelease(&brush);
}

void FillRectangleWithBrush(HANDLE ctx, D2D1_RECT_F* rect, HANDLE brushHandle)
{
	RetrieveContext(ctx);

	BrushContext* brushContext = reinterpret_cast<BrushContext*>(brushHandle);
	ID2D1Brush* brush = reinterpret_cast<ID2D1Brush*>(brushContext->brush);

	if (brush != NULL) {
		context->renderTarget->FillRectangle(rect, brush);
	}
}

D2DLIB_API void DrawRoundedRect(HANDLE ctx, D2D1_ROUNDED_RECT* roundedRect, D2D1_COLOR_F strokeColor,
	D2D1_COLOR_F fillColor, FLOAT strokeWidth, D2D1_DASH_STYLE strokeStyle, D2D1_CAP_STYLE startCap,
	D2D1_CAP_STYLE endCap, D2D1_CAP_STYLE gapCap, D2D1_LINE_JOIN lineJoin, float* customDashes, UINT32 CDcount)
{
	RetrieveContext(ctx);

	ID2D1SolidColorBrush* strokeBrush = NULL;
	ID2D1SolidColorBrush* fillBrush = NULL;
	ID2D1StrokeStyle* strokeStyleObj = NULL;

	if (fillColor.a > 0) {
		context->renderTarget->CreateSolidColorBrush(fillColor, &fillBrush);

		if (fillBrush != NULL) {
			context->renderTarget->FillRoundedRectangle(roundedRect, fillBrush);
		}
	}
	if (strokeColor.a > 0 && strokeWidth > 0) {
		context->renderTarget->CreateSolidColorBrush(strokeColor, &strokeBrush);

		if (strokeBrush != NULL) {

			context->factory->CreateStrokeStyle(D2D1::StrokeStyleProperties(
				startCap, endCap, gapCap, lineJoin, 10.0f, strokeStyle, 0.0f), customDashes, CDcount, &strokeStyleObj);
			context->renderTarget->DrawRoundedRectangle(roundedRect, strokeBrush, strokeWidth, strokeStyleObj);
		}
	}


	SafeRelease(&strokeBrush);
	SafeRelease(&strokeStyleObj);
	SafeRelease(&fillBrush);
}

D2DLIB_API void DrawRoundedRectWithBrush(HANDLE ctx, D2D1_ROUNDED_RECT* roundedRect,
	HANDLE strokePen, HANDLE fillBrush, float strokeWidth) 
{
	RetrieveContext(ctx);

	D2DPen* pen = reinterpret_cast<D2DPen*>(strokePen);
	BrushContext* brushContext = reinterpret_cast<BrushContext*>(fillBrush);
	ID2D1Brush* brush = reinterpret_cast<ID2D1Brush*>(brushContext->brush);

	if (brush != NULL) {
		context->renderTarget->FillRoundedRectangle(roundedRect, brush);
	}
	if (pen != NULL) {
		context->renderTarget->DrawRoundedRectangle(roundedRect, pen->brush, strokeWidth, pen->strokeStyle);
	}

}

D2DLIB_API void DrawRoundedRectWithBrushes(HANDLE ctx, D2D1_ROUNDED_RECT* roundedRect,
	HANDLE strokeBrush, HANDLE fillBrush, float strokeWidth, D2D1_DASH_STYLE dashStyle, D2D1_CAP_STYLE startCap,
	D2D1_CAP_STYLE endCap, D2D1_CAP_STYLE gapCap, D2D1_LINE_JOIN lineJoin, float* customDashes, UINT32 CDcount)
{
	RetrieveContext(ctx);

	ID2D1Brush* brush = reinterpret_cast<ID2D1Brush*>(reinterpret_cast<BrushContext*>(fillBrush)->brush);
	ID2D1Brush* brush2 = reinterpret_cast<ID2D1Brush*>(reinterpret_cast<BrushContext*>(strokeBrush)->brush);

	if (brush != NULL) {
		context->renderTarget->FillRoundedRectangle(roundedRect, brush);
	}
	if (brush2 != NULL) {
		ID2D1StrokeStyle* strokeStyleObj = NULL;
		context->factory->CreateStrokeStyle(D2D1::StrokeStyleProperties(
			startCap, endCap, gapCap, lineJoin, 10.0f, dashStyle, 0.0f), customDashes, CDcount, &strokeStyleObj);
		context->renderTarget->DrawRoundedRectangle(roundedRect, brush2, strokeWidth, strokeStyleObj);
		SafeRelease(&strokeStyleObj);
	}

}

void DrawEllipse(HANDLE ctx, D2D1_ELLIPSE* ellipse, D2D1_COLOR_F color, FLOAT width, D2D1_DASH_STYLE dashStyle,
	D2D1_CAP_STYLE startCap, D2D1_CAP_STYLE endCap, D2D1_CAP_STYLE gapCap, D2D1_LINE_JOIN lineJoin, float* customDashes, UINT32 CDcount)
{
	RetrieveContext(ctx);
	ID2D1SolidColorBrush* brush = NULL;
	if (color.a > 0) context->renderTarget->CreateSolidColorBrush(color, &brush);
	if (brush != NULL)
	{
		BrushContext brushCtx;
		brushCtx.brush = brush;
		DrawEllipseWithBrush(ctx, ellipse, &brushCtx, width, dashStyle, startCap, endCap, gapCap, lineJoin, customDashes, CDcount);
	}
	SafeRelease(&brush);
}
void DrawEllipseWithBrush(HANDLE ctx, D2D1_ELLIPSE* ellipse, HANDLE brushHandle,
	FLOAT width, D2D1_DASH_STYLE dashStyle,
	D2D1_CAP_STYLE startCap, D2D1_CAP_STYLE endCap, D2D1_CAP_STYLE gapCap, D2D1_LINE_JOIN lineJoin, float* customDashes, UINT32 CDcount)
{
	RetrieveContext(ctx);
	ID2D1Brush* brush = reinterpret_cast<ID2D1Brush*>(reinterpret_cast<BrushContext*>(brushHandle)->brush);
	ID2D1StrokeStyle* strokeStyle = NULL;
	if (brush != NULL) {
		if (dashStyle != D2D1_DASH_STYLE_SOLID)
		{
			context->factory->CreateStrokeStyle(D2D1::StrokeStyleProperties(
				startCap, endCap, gapCap, lineJoin, 10.0f, dashStyle, 0.0f), customDashes, CDcount, &strokeStyle);
		}
		context->renderTarget->DrawEllipse(ellipse, brush, width, strokeStyle);
	}
	SafeRelease(&strokeStyle);
}

void FillEllipse(HANDLE handle, D2D1_ELLIPSE* ellipse, D2D1_COLOR_F color)
{
	D2DContext* context = reinterpret_cast<D2DContext*>(handle);

	// Create a black brush.
	ID2D1SolidColorBrush* brush;
	context->renderTarget->CreateSolidColorBrush(color, &brush);

	if (brush != NULL) {
		context->renderTarget->FillEllipse(ellipse, brush);
	}

	SafeRelease(&brush);
}

void FillEllipseWithBrush(HANDLE ctx, D2D1_ELLIPSE* ellipse, HANDLE brushHandle)
{
	RetrieveContext(ctx);

	BrushContext* brushContext = reinterpret_cast<BrushContext*>(brushHandle);
	ID2D1Brush* brush = reinterpret_cast<ID2D1Brush*>(brushContext->brush);

	context->renderTarget->FillEllipse(ellipse, brush);
}