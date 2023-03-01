// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DotNet.Interactive.FSharp.Ukrainian

open Microsoft.DotNet.Interactive
open System.Threading.Tasks
open Microsoft.AspNetCore.Html

[<Sealed>]
type FSharpKernelInitializationExtensions() = 
    interface Microsoft.DotNet.Interactive.IKernelExtension with
        member this.OnLoadAsync(kernel: Kernel) = 
            match kernel with
            | :? CompositeKernel as compositeKernel ->
                compositeKernel.Add(new FSharpUaKernel())
                match KernelInvocationContext.Current with
                | null -> ()
                | context -> context.Display(
                    new HtmlString(@"<details><summary>Пиши, компілюй та запускай Ф# код.</summary></details>
<p>Це розширення додає підтримку для <a href=""https://github.com/kant2002/fsharp"">Ф#</a>. Спробуй цей код:</p>
<pre>
    <code>
    нехай хтось = ""Мир""
    printf ""Добридень %s"" хтось
    </code>
</pre>
"),
                    "text/html") |> ignore;
                Task.CompletedTask
            | _ -> Task.CompletedTask

