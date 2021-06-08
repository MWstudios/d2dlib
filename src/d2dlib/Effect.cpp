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
#include "Effect.h"

//#include "d2d1effects.h"
/*#include <d2d1effecthelpers.h>
#include <fstream>
#include <iterator>
#include <vector>
#include "Context.h"
#define PassText(X) TEXT(#X)
D2DCustomEffect::D2DCustomEffect() { }
void D2DCustomEffect::SetStructPointer(int ptr) { ptrToStruct = ptr; }
HRESULT D2DCustomEffect::CloneSelf(_Outptr_ IUnknown** effect)
{
    *effect = static_cast<ID2D1EffectImpl*>(new D2DCustomEffect());
    return *effect == nullptr ? E_OUTOFMEMORY : S_OK;
}
HRESULT D2DCustomEffect::Register(_In_ ID2D1Factory1* factory)
{
    return factory->RegisterEffectFromString(CLSID_D2DCustomEffect, PassText(< ? xml version = '1.0' ? ><Effect>< / Effect>), NULL, 0, CloneSelf);
}
IFACEMETHODIMP D2DCustomEffect::Initialize(_In_ ID2D1EffectContext* pEffectContext, _In_ ID2D1TransformGraph* pTransformGraph)
{
    std::ifstream input(shaderPath, std::ios::binary);
    std::vector<byte> bytes((std::istreambuf_iterator<char>(input)), (std::istreambuf_iterator<char>()));
    input.close();
    auto shaderFirst = bytes.begin(), shaderLast = bytes.end();
    HRESULT hr = pEffectContext->LoadPixelShader(GUID_D2DCustomShader, &bytes[0], bytes.size());
    if (SUCCEEDED(hr)) hr = pTransformGraph->SetSingleTransformNode(this);
    return hr;
}
IFACEMETHODIMP D2DCustomEffect::PrepareForRender(D2D1_CHANGE_TYPE changeType)
{
    return m_drawInfo->SetPixelShaderConstantBuffer(reinterpret_cast<BYTE*>(ptrToStruct), sizeof(ptrToStruct));
}
IFACEMETHODIMP D2DCustomEffect::SetDrawInfo(_In_ ID2D1DrawInfo* pRenderInfo) { m_drawInfo = pRenderInfo; }*/
/*D2DLIB_API void RegisterEffect(HANDLE ctx,_In_ corewindow window, D2DCustomEffect* effect)
{
    RetrieveContext(ctx);
    effect->Register(context->factory);
    std::shared_ptr<DX::DeviceResources>
}*/
//class D2DCustomEffect : public ID2D1EffectImpl, public ID2D1TransformNode
//{
//};
/*void CreateEffect(HANDLE ctx)
{
	RetrieveContext(ctx);

//	ID2D1Effect perspectiveTransformEffect;
//	m_d2dContext->CreateEffect(CLSID_D2D13DPerspectiveTransform, &perspectiveTransformEffect);
//
//perspectiveTransformEffect->SetInput(0, bitmap);
//
//perspectiveTransformEffect->SetValue(D2D1_3DPERSPECTIVETRANSFORM_PROP_PERSPECTIVE_ORIGIN, D2D1::Vector3F(0.0f, 192.0f, 0.0f));
//perspectiveTransformEffect->SetValue(D2D1_3DPERSPECTIVETRANSFORM_PROP_ROTATION, D2D1::Vector3F(0.0f, 30.0f, 0.0f));
//
//m_d2dContext->BeginDraw();
//m_d2dContext->DrawImage(perspectiveTransformEffect.Get());
//m_d2dContext->EndDraw();

}*/