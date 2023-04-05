open System
open System.Reflection
open VDS.RDF
open VDS.RDF.Writing

let g = new Graph(new Uri("http://example.org/base"))

let asm = Assembly.LoadFrom "./samples/Xwt.dll"

let assemblyNode = g.CreateUriNode(UriFactory.Create("http://example.org/assembly"))
let hasFullName = g.CreateUriNode(UriFactory.Create("http://example.org/hasFullName"))
let referenceAssembly = g.CreateUriNode(UriFactory.Create("http://example.org/referenceAssembly"))
let exportType = g.CreateUriNode(UriFactory.Create("http://example.org/exportType"))

g.Assert(new Triple(assemblyNode, hasFullName, g.CreateLiteralNode(asm.FullName))) |> ignore

printfn "\n# Referenced assemblie:"

let refAsms = asm.GetReferencedAssemblies()
for i in 0 .. refAsms.Length - 1 do
    let refAsm = refAsms[i]
    g.Assert(new Triple(assemblyNode, referenceAssembly, g.CreateLiteralNode(refAsm.FullName))) |> ignore
    printfn "%s" refAsm.FullName

printfn "\n# Exported types:"

let exportedTypes = asm.GetExportedTypes()
for i in 0 .. exportedTypes.Length - 1 do
    let exportedType = exportedTypes[i]
    g.Assert(new Triple(assemblyNode, exportType, g.CreateLiteralNode(exportedType.FullName))) |> ignore
    printfn "%s" exportedType.FullName

let rdfxmlwriter = new RdfXmlWriter()
rdfxmlwriter.Save(g, "assembly.rdf")