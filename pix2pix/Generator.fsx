// Copyright 2019 The TensorFlow Authors, adapted by the DiffSharp authors. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#r @"..\bin\Debug\netcoreapp3.1\publish\DiffSharp.Core.dll"
#r @"..\bin\Debug\netcoreapp3.1\publish\DiffSharp.Backends.ShapeChecking.dll"
#r @"..\bin\Debug\netcoreapp3.1\publish\Library.dll"
#load "Layers.fsx"
#load "Discriminator.fsx"

open DiffSharp
open DiffSharp.Model
open Layers

type NetG(inChannels: int, outChannels: int, ngf: int, ?useDropout: bool) =
    inherit Model()
    
    let useDropout = defaultArg useDropout false

    let firstBlock = UNetSkipConnectionInnermost(inChannels=ngf * 8, innerChannels=ngf * 8, outChannels=ngf * 8)
        
    let module1 = UNetSkipConnection(inChannels=ngf * 8, innerChannels=ngf * 8, outChannels=ngf * 8, submodule=firstBlock, useDropOut=useDropout)
    let module2 = UNetSkipConnection(inChannels=ngf * 8, innerChannels=ngf * 8, outChannels=ngf * 8, submodule=module1, useDropOut=useDropout)
    let module3 = UNetSkipConnection(inChannels=ngf * 8, innerChannels=ngf * 8, outChannels=ngf * 8, submodule=module2, useDropOut=useDropout)

    let module4 = UNetSkipConnection(inChannels=ngf * 4, innerChannels=ngf * 8, outChannels=ngf * 4, submodule=module3, useDropOut=useDropout)
    let module5 = UNetSkipConnection(inChannels=ngf * 2, innerChannels=ngf * 4, outChannels=ngf * 2, submodule=module4, useDropOut=useDropout)
    let module6 = UNetSkipConnection(inChannels=ngf, innerChannels=ngf * 2, outChannels=ngf, submodule=module5, useDropOut=useDropout)

    let model = UNetSkipConnectionOutermost(inChannels=inChannels, innerChannels=ngf, outChannels=outChannels, submodule=module6)

    override _.forward(input) =
        model.forward(input)


