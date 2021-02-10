// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once
#include "ShellHelpers.h"
#include "RegisterExtension.h"
#include <strsafe.h>
#include <new>  // std::nothrow

void DllAddRef();
void DllRelease();

// use UUDIGEN.EXE to generate unique CLSID values for your objects

class __declspec(uuid("CC19E147-7757-483C-B27F-3D81BCEB38FE")) CExplorerCommandVerb;
class __declspec(uuid("5A777238-07F9-42AB-B074-4004F1ED74A9")) CExplorerCommandStateHandler;

HRESULT CExplorerCommandVerb_CreateInstance(REFIID riid, void **ppv);
HRESULT CExplorerCommandStateHandler_CreateInstance(REFIID riid, void **ppv);
HRESULT CExplorerCommandVerb_RegisterUnRegister(bool fRegister);
HRESULT CExplorerCommandStateHandler_RegisterUnRegister(bool fRegister);
