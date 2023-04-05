open System.Reflection

let asm = Assembly.LoadFrom "./samples/Xwt.dll"

printfn "\n# Referenced assemblie:"

let refAsms = asm.GetReferencedAssemblies()
for i in 0 .. refAsms.Length - 1 do
    let refAsm = refAsms[i]
    printfn "%s" refAsm.FullName

printfn "\n# Exported types:"

let exportedTypes = asm.GetExportedTypes()
for i in 0 .. exportedTypes.Length - 1 do
    let exportedType = exportedTypes[i]
    printfn "%s" exportedType.FullName
