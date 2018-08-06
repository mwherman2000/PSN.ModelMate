using Mono.Cecil;
using Mono.Cecil.Cil;
using PSN.ModelMate.EDM;
using PSN.ModelMate.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PSN.ModelMate.Cecil
{
    static public class Cecil2ModelMate
    {
        const string lApplication = "Application";
        const string lApplications = "Applications";
        const string lApplicationComponents = "Application Components";
        const string lApplicationInterfaces = "Application Interfaces";

        const string lTechnology = "Technology";
        const string lArtifacts = "Artifacts";
        const string lTechnologyInterfaces = "Technology Interfaces";
        const string lSystemSoftware = "System Software";

        const string lRelations = "Relations";

        const string lViews = "Views";

        public static model Migrate2ModelMate(string tSourceName, string mSourceName, DirectoryInfo di)
        {
            ModelFactory.TenantName = tSourceName;
            ModelFactory.ModelName = mSourceName;

            var model = ModelFactory.NewModel(mSourceName);

            CecilFactory.Initialize();

            var propdefs = ModelFactory.NewPropertyDefs(CecilFactory.pdc);
            model.propertydefs.Add(propdefs);

            //var element0 = ModelFactory.NewElement(enameTest, ModelConst.ElementType.Artifact);
            //var property0 = ModelFactory.NewProperty(ModelFactory.NewValue(Guid.NewGuid().ToString()), pdUniqueKey);
            //var properties0 = ModelFactory.NewProperties(new property[] { property0 });
            //element0.properties.Add(properties0);
            //elements.element.Add(ModelFactory.NewElement(label, ModelConst.ElementType.ApplicationCollaboration));

            var elements = ModelFactory.NewElements();
            var relationships = ModelFactory.NewRelationships();

            model.elements.Add(elements);
            model.relationships.Add(relationships);

            var fElement = (element)null;
            var aElement = (element)null;
            var modElement = (element)null;
            var arElement = (element)null;
            var tdElement = (element)null;
            var methodElement = (element)null;
            var fieldElement = (element)null;


            var faRelationship = (relationship)null;
            var amodRelationship = (relationship)null;
            var modarRelationship = (relationship)null;
            var modtdRelationship = (relationship)null;
            var tdmethodRelationship = (relationship)null;
            var tdfieldRelationship = (relationship)null;

            FileInfo[] files = di.GetFiles("*.*");
            //FileInfo[] fis = di.GetFiles("Vet*.*");
            foreach (FileInfo fileInfo in files)
            {
                Console.WriteLine("File Name:\t" + fileInfo.Name);
                //Console.WriteLine("File FullName:\t" + fi.FullName);

                if (fileInfo.FullName.EndsWith(".dll") || fileInfo.FullName.EndsWith(".exe"))
                {
                    var fType = ModelConst.ElementType.Artifact;
                    var fTypeName = fType.ToString();
                    var fId = Util.MakeModelElementIdentifier(ModelFactory.ModelName, fileInfo.FullName, fType);
                    var fMatching = from eMatch in elements.element
                                    where eMatch.identifier == fId
                                    select eMatch;
                    if (fMatching.Count() == 0)
                    {
                        fElement = CecilFactory.NewElement(new CecilFactory.PropertyAndValue
                        {
                            pValues = {
                            [(int)CecilConst.PropertyName.APIType] = fileInfo.GetType().Name,
                            [(int)CecilConst.PropertyName.Name] = fileInfo.Name + " [FILE]",
                            [(int)CecilConst.PropertyName.FullName] = fileInfo.FullName,
                            [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                            [(int)CecilConst.PropertyName.ArchiMateElementType] = fTypeName,
                            [(int)CecilConst.PropertyName.EADomain] = ModelConst.EADomainNames[(int)fType],
                            //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            [(int)CecilConst.PropertyName.Namespace] = fileInfo.DirectoryName,
                            [(int)CecilConst.PropertyName.Created] = fileInfo.CreationTimeUtc.ToString(),
                            [(int)CecilConst.PropertyName.LastAccessed] = fileInfo.LastAccessTimeUtc.ToString(),
                            [(int)CecilConst.PropertyName.LastWritten] = fileInfo.LastWriteTimeUtc.ToString(),
                            [(int)CecilConst.PropertyName.Length] = fileInfo.Length.ToString(),
                            [(int)CecilConst.PropertyName.Extension] = fileInfo.Extension,
                            }
                        });
                        if (fElement != null) elements.element.Add(fElement);
                    }
                    else
                    {
                        fElement = fMatching.ElementAt(0);
                        // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                    }

                    //var module = ModuleDefinition.ReadModule(fi.FullName);
                    //AssemblyDefinition assembly = module.Assembly;

                    var assembly = AssemblyDefinition.ReadAssembly(fileInfo.FullName);

                    Console.WriteLine("Assembly Name:\t" + assembly.Name.Name);
                    //Console.WriteLine("Assembly FullName:\t" + assembly.Name.FullName);

                    var aTypeName = DetermineArchiMateSoftwareType(assembly.Name.FullName).ToString();
                    var aType = (ModelConst.ElementType)Enum.Parse(typeof(ModelConst.ElementType), aTypeName);
                    var aId = Util.MakeModelElementIdentifier(ModelFactory.ModelName, assembly.Name.FullName, aType);
                    var aMatching = from eMatch in elements.element
                                where eMatch.identifier == aId
                                select eMatch;
                    if (aMatching.Count() == 0)
                    {
                        aElement = CecilFactory.NewElement(new CecilFactory.PropertyAndValue
                        {
                            pValues = {
                            [(int)CecilConst.PropertyName.APIType] = assembly.GetType().Name,
                            [(int)CecilConst.PropertyName.Name] = assembly.Name.Name + " [ASM]",
                            [(int)CecilConst.PropertyName.FullName] = assembly.Name.FullName,
                            [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                            [(int)CecilConst.PropertyName.ArchiMateElementType] = aTypeName,
                            [(int)CecilConst.PropertyName.EADomain] = ModelConst.EADomainNames[(int)aType],
                            //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            [(int)CecilConst.PropertyName.Version] = assembly.Name.Version.ToString(),
                            //[(int)CecilConst.PropertyName.PublicKeyToken] = (assembly.Name.PublicKeyToken != null && assembly.Name.PublicKeyToken.Length > 0) ? assembly.Name.PublicKeyToken.Select(x => x.ToString("x2")).Aggregate((x, y) => x + y) : "null",
                            //[(int)CecilConst.PropertyName.Culture] = String.IsNullOrEmpty(assembly.Name.Culture) ? "neutral" : assembly.Name.Culture,
                            [(int)CecilConst.PropertyName.ParentFile] = fileInfo.FullName,
                            }
                        });
                        if (aElement != null) elements.element.Add(aElement);
                    }
                    else
                    {
                        aElement = aMatching.ElementAt(0);
                        // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                    }

                    var farelType = ModelConst.RelationshipType.RealisationRelationship;
                    var farelTypeName = farelType.ToString();
                    var farelFullName = farelTypeName + " (" + fileInfo.FullName + " to " + assembly.Name.FullName + ")";
                    var farelId = Util.MakeModelRelationshipIdentifier(ModelFactory.ModelName, farelFullName, farelType, fElement.identifier, aElement.identifier);
                    var farelMatching = from rMatch in relationships.relationship
                                where rMatch.identifier == farelId
                                select rMatch;
                    if (farelMatching.Count() == 0)
                    {
                        faRelationship = CecilFactory.NewRelationship(new CecilFactory.PropertyAndValue
                        {
                            pValues = {
                            [(int)CecilConst.PropertyName.APIType] = Util.MakeBaseObjectTypeName(typeof(relationship)),
                            //[(int)CecilConst.PropertyName.Name] = farelTypeName.Replace("Relationship", "") + " (" + fi.Name + " to " + assembly.Name.Name + ")",
                            [(int)CecilConst.PropertyName.Name] = "Contains [RL]",
                            [(int)CecilConst.PropertyName.FullName] = farelFullName,
                            [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                            [(int)CecilConst.PropertyName.ArchiMateElementType] = farelTypeName,
                            //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            [(int)CecilConst.PropertyName.SourceIdentifier] = fElement.identifier,
                            [(int)CecilConst.PropertyName.TargetIdentifier] = aElement.identifier,
                            }
                        });
                        if (faRelationship != null) relationships.relationship.Add(faRelationship);
                    }
                    else
                    {
                        faRelationship = farelMatching.ElementAt(0);
                        // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                    }

                    ModuleDefinition mainmodule = assembly.MainModule;
                    Console.WriteLine("MainModule Name:\t" + mainmodule.Name);
                    //Console.WriteLine("MainModule FullyQualifiedName:\t" + mainmodule.FullyQualifiedName);
                    //Console.WriteLine("MainModule RuntimeVersion:\t" + mainmodule.RuntimeVersion);

                    var asmModules = assembly.Modules;
                    foreach (var module in asmModules)
                    {
                        Console.WriteLine("Module Name:\t" + module.Name);
                        //Console.WriteLine("Module FullyQualifiedName:\t" + module.FullyQualifiedName);
                        //Console.WriteLine("Module RuntimeVersion:\t" + module.RuntimeVersion);
                    }

                    asmModules = assembly.Modules;
                    foreach (var module in asmModules)
                    {
                        Console.WriteLine("Module Name:\t" + module.Name);
                        //Console.WriteLine("Module FullyQualifiedName:\t" + module.FullyQualifiedName);
                        //Console.WriteLine("Module RuntimeVersion:\t" + module.RuntimeVersion);

                        var modTypeName = DetermineArchiMateFunctionType(module.Name).ToString();
                        var modType = (ModelConst.ElementType)Enum.Parse(typeof(ModelConst.ElementType), modTypeName);
                        var modId = Util.MakeModelElementIdentifier(ModelFactory.ModelName, module.FullyQualifiedName, modType);
                        var modMatching = from eMatch in elements.element
                                        where eMatch.identifier == modId
                                        select eMatch;
                        if (modMatching.Count() == 0)
                        {
                            modElement = CecilFactory.NewElement(new CecilFactory.PropertyAndValue
                            {
                                pValues = {
                                [(int)CecilConst.PropertyName.APIType] = module.GetType().Name,
                                [(int)CecilConst.PropertyName.Name] = module.Name + " [MOD]",
                                [(int)CecilConst.PropertyName.FullName] = module.FullyQualifiedName,
                                [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                [(int)CecilConst.PropertyName.FQName] = module.FullyQualifiedName,
                                [(int)CecilConst.PropertyName.ArchiMateElementType] = modTypeName,
                                [(int)CecilConst.PropertyName.EADomain] = ModelConst.EADomainNames[(int)modType],
                                //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                [(int)CecilConst.PropertyName.Version] = module.RuntimeVersion,
                                [(int)CecilConst.PropertyName.ParentAssembly] = assembly.FullName,
                                }
                            });
                            if (modElement != null) elements.element.Add(modElement);
                        }
                        else
                        {
                            modElement = modMatching.ElementAt(0);
                            // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                        }

                        var amodrelType = ModelConst.RelationshipType.UsedByRelationship;
                        var amodrelTypeName = amodrelType.ToString();
                        var amodrelFullName = amodrelTypeName + " (" + assembly.Name.FullName + " to " + module.FullyQualifiedName + ")";
                        var amodrelId = Util.MakeModelRelationshipIdentifier(ModelFactory.ModelName, amodrelFullName, amodrelType, aElement.identifier, modElement.identifier);
                        var amodrelMatching = from rMatch in relationships.relationship
                                            where rMatch.identifier == amodrelId
                                            select rMatch;
                        if (amodrelMatching.Count() == 0)
                        {
                            amodRelationship = CecilFactory.NewRelationship(new CecilFactory.PropertyAndValue
                            {
                                pValues = {
                                [(int)CecilConst.PropertyName.APIType] = Util.MakeBaseObjectTypeName(typeof(relationship)),
                                //[(int)CecilConst.PropertyName.Name] = amodrelTypeName.Replace("Relationship", "") + " (" + assembly.Name.Name + " to " + module.Name + ")",
                                [(int)CecilConst.PropertyName.Name] = "Contains [UB]",
                                [(int)CecilConst.PropertyName.FullName] = amodrelFullName,
                                [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                [(int)CecilConst.PropertyName.ArchiMateElementType] = amodrelTypeName,
                                //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                [(int)CecilConst.PropertyName.SourceIdentifier] = aElement.identifier,
                                [(int)CecilConst.PropertyName.TargetIdentifier] = modElement.identifier,
                                }
                            });
                            if (amodRelationship != null) relationships.relationship.Add(amodRelationship);
                        }
                        else
                        {
                            amodRelationship = amodrelMatching.ElementAt(0);
                            // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                        }

                        var ars = module.AssemblyReferences;
                        foreach (AssemblyNameReference ar in ars)
                        {
                            Console.WriteLine("AssemblyNameReference Name:\t" + ar.Name);
                            //Console.WriteLine("AssemblyNameReference FullName:\t" + ar.FullName);
                            //Console.WriteLine("AssemblyNameReference Version:\t" + ar.Version.ToString());
                            //Console.WriteLine("AssemblyNameReference IsWindowsRuntime:\t" + ar.IsWindowsRuntime.ToString());

                            var arTypeName = DetermineArchiMateSoftwareType(ar.FullName).ToString();
                            var arType = (ModelConst.ElementType)Enum.Parse(typeof(ModelConst.ElementType), arTypeName);
                            var arId = Util.MakeModelElementIdentifier(ModelFactory.ModelName, ar.FullName, arType);
                            var arMatching = from eMatch in elements.element
                                             where eMatch.identifier == arId
                                             select eMatch;
                            if (arMatching.Count() == 0)
                            {
                                arElement = CecilFactory.NewElement(new CecilFactory.PropertyAndValue
                                {
                                    pValues = {
                                    [(int)CecilConst.PropertyName.APIType] = ar.GetType().Name,
                                    [(int)CecilConst.PropertyName.Name] = ar.Name + " [ARN]",
                                    [(int)CecilConst.PropertyName.FullName] = ar.FullName,
                                    [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                    [(int)CecilConst.PropertyName.ArchiMateElementType] = arTypeName,
                                    [(int)CecilConst.PropertyName.EADomain] = ModelConst.EADomainNames[(int)arType],
                                    //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                    [(int)CecilConst.PropertyName.Version] = ar.Version.ToString(),
                                    //[(int)CecilConst.PropertyName.PublicKeyToken] = (ar.PublicKeyToken != null && ar.PublicKeyToken.Length > 0) ? ar.PublicKeyToken.Select(x => x.ToString("x2")).Aggregate((x, y) => x + y)  : "null",
                                    //[(int)CecilConst.PropertyName.Culture] = String.IsNullOrEmpty(ar.Culture) ? "neutral" : ar.Culture,
                                    [(int)CecilConst.PropertyName.ParentModule] = module.FullyQualifiedName,
                                    }
                                });
                                if (arElement != null) elements.element.Add(arElement);
                            }
                            else
                            {
                                arElement = arMatching.ElementAt(0);
                                // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            }

                            var modarrelType = ModelConst.RelationshipType.AssociationRelationship;
                            var modarrelTypeName = modarrelType.ToString();
                            var modarrelFullName = modarrelTypeName + " (" + module.FullyQualifiedName + " to " + ar.FullName + ")";
                            var modarrelId = Util.MakeModelRelationshipIdentifier(ModelFactory.ModelName, modarrelFullName, modarrelType, modElement.identifier, arElement.identifier);
                            var modarrelMatching = from rMatch in relationships.relationship
                                                  where rMatch.identifier == modarrelId
                                                  select rMatch;
                            if (modarrelMatching.Count() == 0)
                            {
                                modarRelationship = CecilFactory.NewRelationship(new CecilFactory.PropertyAndValue
                                {
                                    pValues = {
                                    [(int)CecilConst.PropertyName.APIType] = Util.MakeBaseObjectTypeName(typeof(relationship)),
                                    //[(int)CecilConst.PropertyName.Name] = modarrelTypeName.Replace("Relationship", "") + " (" + module.Name + " to " + ar.Name + ")",
                                    [(int)CecilConst.PropertyName.Name] = "References [AS]",
                                    [(int)CecilConst.PropertyName.FullName] = modarrelFullName,
                                    [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                    [(int)CecilConst.PropertyName.ArchiMateElementType] = modarrelTypeName,
                                    //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                    [(int)CecilConst.PropertyName.SourceIdentifier] = modElement.identifier,
                                    [(int)CecilConst.PropertyName.TargetIdentifier] = arElement.identifier,
                                    }
                                });
                                if (modarRelationship != null) relationships.relationship.Add(modarRelationship);
                            }
                            else
                            {
                                modarRelationship = modarrelMatching.ElementAt(0);
                                // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            }
                        }

                        var tds = module.Types;
                        foreach (TypeDefinition typedef in tds)
                        {
                            Console.WriteLine("TypeDefinition Name:\t" + typedef.Name);
                            Console.WriteLine("TypeDefinition IsClass:\t" + typedef.IsClass.ToString());
                            Console.WriteLine("TypeDefinition IsEnum:\t" + typedef.IsEnum.ToString());
                            //Console.WriteLine("TypeDefinition FullName:\t" + typedef.FullName);
                            //Console.WriteLine("TypeDefinition IsPublic:\t" + typedef.IsPublic.ToString());
                        }

                        tds = module.Types;
                        foreach (TypeDefinition typedef in tds)
                        {
                            var tdTypeName = DetermineArchiMateServiceType(typedef.FullName).ToString();
                            var tdType = (ModelConst.ElementType)Enum.Parse(typeof(ModelConst.ElementType), tdTypeName);
                            if (typedef.IsPublic || tdType == ModelConst.ElementType.ApplicationService)
                            {
                                Console.WriteLine("TypeDefinition Name:\t" + typedef.Name);
                                //Console.WriteLine("TypeDefinition FullName:\t" + t.FullName);

                                var tdId = Util.MakeModelElementIdentifier(ModelFactory.ModelName, typedef.FullName, tdType);
                                var tdMatching = from eMatch in elements.element
                                            where eMatch.identifier == tdId
                                            select eMatch;
                                if (tdMatching.Count() == 0)
                                {
                                    string suffix = " [TD]";
                                    if (typedef.IsClass) suffix = " [CLASS]";
                                    if (typedef.IsEnum) suffix = " [ENUM]";

                                    tdElement = CecilFactory.NewElement(new CecilFactory.PropertyAndValue
                                    {
                                        pValues = {
                                        [(int)CecilConst.PropertyName.APIType] = typedef.GetType().Name,
                                        [(int)CecilConst.PropertyName.Name] = typedef.Name + suffix,
                                        [(int)CecilConst.PropertyName.FullName] = typedef.FullName,
                                        [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                        [(int)CecilConst.PropertyName.ArchiMateElementType] = tdTypeName,
                                        [(int)CecilConst.PropertyName.EADomain] = ModelConst.EADomainNames[(int)tdType],
                                        //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                        [(int)CecilConst.PropertyName.Namespace] = GetNamespaceFromFQName(typedef.Namespace),
                                        [(int)CecilConst.PropertyName.BaseType] = (typedef.BaseType == null) ? "" : typedef.BaseType.FullName,
                                        [(int)CecilConst.PropertyName.DeclaringType] = (typedef.DeclaringType == null) ? "" : typedef.DeclaringType.FullName,
                                        //[(int)CecilConst.PropertyName.IsPublic] = td.IsPublic.ToString(),
                                        [(int)CecilConst.PropertyName.ParentModule] = module.FullyQualifiedName,
                                        }
                                    });
                                    if (tdElement != null) elements.element.Add(tdElement);
                                }
                                else
                                {
                                    tdElement = tdMatching.ElementAt(0);
                                    // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                }

                                var modtdrelType = ModelConst.RelationshipType.RealisationRelationship;
                                var modtdrelTypeName = modtdrelType.ToString();
                                var modtdrelFullName = modtdrelTypeName + " (" + module.FullyQualifiedName + " to " + typedef.FullName + ")";
                                var modtdrelId = Util.MakeModelRelationshipIdentifier(ModelFactory.ModelName, modtdrelFullName, modtdrelType, modElement.identifier, tdElement.identifier);
                                var modtdrelMatching = from rMatch in relationships.relationship
                                                       where rMatch.identifier == modtdrelId
                                                       select rMatch;
                                if (modtdrelMatching.Count() == 0)
                                {
                                    modtdRelationship = CecilFactory.NewRelationship(new CecilFactory.PropertyAndValue
                                    {
                                        pValues = {
                                    [(int)CecilConst.PropertyName.APIType] = Util.MakeBaseObjectTypeName(typeof(relationship)),
                                    //[(int)CecilConst.PropertyName.Name] = modtdrelTypeName.Replace("Relationship", "") + " (" + module.Name + " to " + typedef.Name + ")",
                                    [(int)CecilConst.PropertyName.Name] = "Defines [RL]",
                                    [(int)CecilConst.PropertyName.FullName] = modtdrelFullName,
                                    [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                    [(int)CecilConst.PropertyName.ArchiMateElementType] = modtdrelTypeName,
                                    //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                    [(int)CecilConst.PropertyName.SourceIdentifier] = modElement.identifier,
                                    [(int)CecilConst.PropertyName.TargetIdentifier] = tdElement.identifier,
                                    }
                                    });
                                    if (modtdRelationship != null) relationships.relationship.Add(modtdRelationship);
                                }
                                else
                                {
                                    modtdRelationship = modtdrelMatching.ElementAt(0);
                                    // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                }

                                var fieldDefs = typedef.Fields;
                                foreach (FieldDefinition fieldDef in fieldDefs)
                                {
                                    //if (fieldDef.Name.Contains("_annotations")) System.Diagnostics.Debugger.Break();

                                    var fieldTypeName = DetermineArchiMateObjectType(fieldDef.FullName).ToString();
                                    var fieldType = (ModelConst.ElementType)Enum.Parse(typeof(ModelConst.ElementType), fieldTypeName);

                                    if (fieldDef.IsPublic || fieldType == ModelConst.ElementType.DataObject)
                                    {
                                        // carry on
                                    }
                                    else
                                    {
                                        continue; // skip
                                    }

                                    //if (fieldDef.Name.Equals(".ctor")) continue;
                                    //if (fieldDef.Name.Equals("Equals")) continue;

                                    Console.WriteLine("    FieldDef Name:\t" + fieldDef.Name);
                                    Console.WriteLine("    FieldDef FullName:\t" + fieldDef.FullName);
                                    //Console.WriteLine("      fieldDef.Module.FullyQualifiedName:\t" + fieldDef.Module.FullyQualifiedName);
                                    //Console.WriteLine("      fieldDef.ReturnType:\t" + fieldDef.ReturnType.ToString());
                                    //Console.WriteLine("      fieldDef.IsPublic:\t" + fieldDef.IsPublic.ToString());

                                    var fieldId = Util.MakeModelElementIdentifier(ModelFactory.ModelName, fieldDef.FullName, fieldType);
                                    var fieldMatching = from eMatch in elements.element
                                                         where eMatch.identifier == fieldId
                                                         select eMatch;
                                    if (fieldMatching.Count() == 0)
                                    {
                                        fieldElement = CecilFactory.NewElement(new CecilFactory.PropertyAndValue
                                        {
                                            pValues = {
                                                [(int)CecilConst.PropertyName.APIType] = fieldDef.GetType().Name,
                                                [(int)CecilConst.PropertyName.Name] = fieldDef.Name + " [FD]",
                                                [(int)CecilConst.PropertyName.FullName] = fieldDef.FullName,
                                                [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                                [(int)CecilConst.PropertyName.ArchiMateElementType] = fieldTypeName,
                                                [(int)CecilConst.PropertyName.EADomain] = ModelConst.EADomainNames[(int)fieldType],
                                                //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                                //[(int)CecilConst.PropertyName.ReturnType] = (field.ReturnType == null) ? "" : field.ReturnType.FullName,
                                                //[(int)CecilConst.PropertyName.ParameterCount] = field.Parameters.Count.ToString(),
                                                //[(int)CecilConst.PropertyName.IsPublic] = field.IsPublic.ToString(),
                                                [(int)CecilConst.PropertyName.ParentModule] = typedef.FullName,
                                                [(int)CecilConst.PropertyName.DeclaringType] = (fieldDef.DeclaringType == null) ? "" : fieldDef.DeclaringType.FullName,
                                                [(int)CecilConst.PropertyName.Namespace] = (fieldDef.FieldType == null) ? "" : fieldDef.FieldType.FullName,
                                            }
                                        });
                                        if (fieldElement != null) elements.element.Add(fieldElement);
                                    }
                                    else
                                    {
                                        fieldElement = fieldMatching.ElementAt(0);
                                        // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                    }

                                    var tdfieldrelType = ModelConst.RelationshipType.AssociationRelationship;
                                    var tdfieldrelTypeName = tdfieldrelType.ToString();
                                    var tdfieldrelFullName = tdfieldrelTypeName + " (" + typedef.FullName + " to " + fieldDef.FullName + ")";
                                    var tdfieldrelId = Util.MakeModelRelationshipIdentifier(ModelFactory.ModelName, tdfieldrelFullName, tdfieldrelType, tdElement.identifier, fieldElement.identifier);
                                    var tdfieldrelMatching = from rMatch in relationships.relationship
                                                              where rMatch.identifier == tdfieldrelId
                                                              select rMatch;
                                    if (tdfieldrelMatching.Count() == 0)
                                    {
                                        tdfieldRelationship = CecilFactory.NewRelationship(new CecilFactory.PropertyAndValue
                                        {
                                            pValues = {
                                            [(int)CecilConst.PropertyName.APIType] = Util.MakeBaseObjectTypeName(typeof(relationship)),
                                            //[(int)CecilConst.PropertyName.Name] = tdfieldrelTypeName.Replace("Relationship", "") + " (" + typedef.Name + " to " + fieldDef.Name + ")",
                                            [(int)CecilConst.PropertyName.Name] = "Defines [AS]",
                                            [(int)CecilConst.PropertyName.FullName] = tdfieldrelFullName,
                                            [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                            [(int)CecilConst.PropertyName.ArchiMateElementType] = tdfieldrelTypeName,
                                            //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                            [(int)CecilConst.PropertyName.SourceIdentifier] = tdElement.identifier,
                                            [(int)CecilConst.PropertyName.TargetIdentifier] = fieldElement.identifier,
                                            }
                                        });
                                        if (tdfieldRelationship != null) relationships.relationship.Add(tdfieldRelationship);
                                    }
                                    else
                                    {
                                        tdfieldRelationship = tdfieldrelMatching.ElementAt(0);
                                        // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                    }
                                }

                                var methodDefs = typedef.Methods;
                                foreach (MethodDefinition methodDef in methodDefs)
                                {
                                    var methodTypeName = DetermineArchiMateInterfaceType(methodDef.FullName).ToString(); 
                                    var methodType = (ModelConst.ElementType)Enum.Parse(typeof(ModelConst.ElementType), methodTypeName);

                                    if (!methodDef.IsPublic && methodType != ModelConst.ElementType.ApplicationInterface) continue;

                                    if (methodDef.Name.Equals(".ctor")) continue;
                                    if (methodDef.Name.Equals("Equals")) continue;
                                    if (methodDef.Name.Equals("ReferenceEquals")) continue;
                                    if (methodDef.Name.Equals("GetHashCode")) continue;
                                    if (methodDef.Name.Equals("GetType")) continue;
                                    if (methodDef.Name.Equals("ToString")) continue;
                                    if (methodDef.Name.Equals("MemberwiseClone")) continue;

                                    Console.WriteLine("    Method Name:\t" + methodDef.Name);
                                    //Console.WriteLine("     Method FullName:\t" + method.FullName);
                                    //Console.WriteLine("      m.Module.FullyQualifiedName:\t" + method.Module.FullyQualifiedName);
                                    //Console.WriteLine("      m.ReturnType:\t" + method.ReturnType.ToString());
                                    //Console.WriteLine("      m.IsPublic:\t" + method.IsPublic.ToString());

                                    var methodId = Util.MakeModelElementIdentifier(ModelFactory.ModelName, methodDef.FullName, methodType);
                                    var methodMatching = from eMatch in elements.element
                                                where eMatch.identifier == methodId
                                                select eMatch;
                                    if (methodMatching.Count() == 0)
                                    {
                                        methodElement = CecilFactory.NewElement(new CecilFactory.PropertyAndValue
                                        {
                                            pValues = {
                                            [(int)CecilConst.PropertyName.APIType] = methodDef.GetType().Name,
                                            [(int)CecilConst.PropertyName.Name] = methodDef.Name + " [MD]",
                                            [(int)CecilConst.PropertyName.FullName] = methodDef.FullName,
                                            [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                            [(int)CecilConst.PropertyName.ArchiMateElementType] = methodTypeName,
                                            [(int)CecilConst.PropertyName.EADomain] = ModelConst.EADomainNames[(int)methodType],
                                            //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                            //[(int)CecilConst.PropertyName.ReturnType] = (method.ReturnType == null) ? "" : method.ReturnType.FullName,
                                            //[(int)CecilConst.PropertyName.ParameterCount] = method.Parameters.Count.ToString(),
                                            //[(int)CecilConst.PropertyName.IsPublic] = method.IsPublic.ToString(),
                                            [(int)CecilConst.PropertyName.ParentModule] = typedef.FullName,
                                            [(int)CecilConst.PropertyName.DeclaringType] = (methodDef.DeclaringType == null) ? "" : methodDef.DeclaringType.FullName,
                                            [(int)CecilConst.PropertyName.Namespace] = (methodDef.DeclaringType == null) ? "" : methodDef.DeclaringType.FullName,
                                            }
                                        });
                                        if (methodElement != null) elements.element.Add(methodElement);
                                    }
                                    else
                                    {
                                        methodElement = methodMatching.ElementAt(0);
                                        // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                    }

                                    var tdmethodrelType = ModelConst.RelationshipType.AssociationRelationship;
                                    var tdmethodrelTypeName = tdmethodrelType.ToString();
                                    var tdmethodrelFullName = tdmethodrelTypeName + " (" + typedef.FullName + " to " + methodDef.FullName + ")";
                                    var tdmethodrelId = Util.MakeModelRelationshipIdentifier(ModelFactory.ModelName, tdmethodrelFullName, tdmethodrelType, tdElement.identifier, methodElement.identifier);
                                    var tdmethodrelMatching = from rMatch in relationships.relationship
                                                          where rMatch.identifier == tdmethodrelId
                                                          select rMatch;
                                    if (tdmethodrelMatching.Count() == 0)
                                    {
                                        tdmethodRelationship = CecilFactory.NewRelationship(new CecilFactory.PropertyAndValue
                                        {
                                            pValues = {
                                            [(int)CecilConst.PropertyName.APIType] = Util.MakeBaseObjectTypeName(typeof(relationship)),
                                            //[(int)CecilConst.PropertyName.Name] = tdmethodrelTypeName.Replace("Relationship", "") + " (" + typedef.Name + " to " + methodDef.Name + ")",
                                            [(int)CecilConst.PropertyName.Name] = "Defines [AS]",
                                            [(int)CecilConst.PropertyName.FullName] = tdmethodrelFullName,
                                            [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                            [(int)CecilConst.PropertyName.ArchiMateElementType] = tdmethodrelTypeName,
                                            //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                            [(int)CecilConst.PropertyName.SourceIdentifier] = tdElement.identifier,
                                            [(int)CecilConst.PropertyName.TargetIdentifier] = methodElement.identifier,
                                            }
                                        });
                                        if (tdmethodRelationship != null) relationships.relationship.Add(tdmethodRelationship);
                                    }
                                    else
                                    {
                                        tdmethodRelationship = tdmethodrelMatching.ElementAt(0);
                                        // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                    }

                                    if (methodDef.IsPublic || methodType == ModelConst.ElementType.ApplicationInterface)
                                    {
                                        ProcessMethodReferences(methodDef, methodElement, elements, relationships);
                                        ProcessFieldReferences(methodDef, methodElement, elements, relationships);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return model;
        }

        public static void ProcessMethodReferences(MethodDefinition method, element methodElement, elements elements, relationships relationships)
        {
            Console.WriteLine("      Methods called by " + method.Name);
            if (method.Body != null && method.Body.Instructions != null)
            {
                foreach (var instruction in method.Body.Instructions)
                {
                    if (instruction.OpCode == OpCodes.Call)
                    {
                        MethodReference methodCall = instruction.Operand as MethodReference;
                        if (methodCall != null)
                        {
                            Console.WriteLine("\tMethodCall Name:\t" + methodCall.Name);
                            Console.WriteLine("\t  methodCall.FullName:\t" + methodCall.FullName);
                            Console.WriteLine("\t  methodCall.Module.FullyQualifiedName:\t" + methodCall.Module.FullyQualifiedName);
                            //Console.WriteLine("\t  methodCall.ReturnType:\t" + methodCall.ReturnType.ToString());
                            //Console.WriteLine("\t  methodCall.DeclaringType:\t" + ((methodCall.DeclaringType == null) ? "" : methodCall.DeclaringType.FullName));
                            //Console.WriteLine("\t  instruction.Offset:\t" + instruction.Offset.ToString());

                            var methodCallElement = (element)null;
                            var methodCallTypeName = DetermineArchiMateInterfaceType(methodCall.FullName).ToString();
                            var methodCallType = (ModelConst.ElementType)Enum.Parse(typeof(ModelConst.ElementType), methodCallTypeName);
                            var methodCallId = Util.MakeModelElementIdentifier(ModelFactory.ModelName, methodCall.FullName, methodCallType);
                            var methodCallMatching = from eMatch in elements.element
                                                     where eMatch.identifier == methodCallId
                                                     select eMatch;
                            if (methodCallMatching.Count() == 0)
                            {
                                methodCallElement = CecilFactory.NewElement(new CecilFactory.PropertyAndValue
                                {
                                    pValues = {
                                            [(int)CecilConst.PropertyName.APIType] = methodCall.GetType().Name,
                                            [(int)CecilConst.PropertyName.Name] = methodCall.Name + " [MC]",
                                            [(int)CecilConst.PropertyName.FullName] = methodCall.FullName,
                                            [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                            [(int)CecilConst.PropertyName.ArchiMateElementType] = methodCallTypeName,
                                            [(int)CecilConst.PropertyName.EADomain] = ModelConst.EADomainNames[(int)methodCallType],
                                            //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                            //[(int)CecilConst.PropertyName.ReturnType] = (methodCall.ReturnType == null) ? "" : methodCall.ReturnType.FullName,
                                            //[(int)CecilConst.PropertyName.ParameterCount] = methodCall.Parameters.Count.ToString(),
                                            //[(int)CecilConst.PropertyName.IsPublic] = methodCall.IsPublic.ToString(),
                                            [(int)CecilConst.PropertyName.DeclaringType] = (methodCall.DeclaringType == null) ? "" : methodCall.DeclaringType.FullName,
                                            [(int)CecilConst.PropertyName.ParentMethod] = method.FullName,
                                            [(int)CecilConst.PropertyName.Namespace] = (methodCall.DeclaringType == null) ? "" : methodCall.DeclaringType.FullName,
                                            }
                                });
                                if (methodCallElement != null) elements.element.Add(methodCallElement);
                            }
                            else
                            {
                                methodCallElement = methodCallMatching.ElementAt(0);
                                // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            }

                            var methmethodCallRelationship = (relationship)null;
                            var methmethodCallrelType = ModelConst.RelationshipType.TriggeringRelationship;
                            var methmethodCallrelTypeName = methmethodCallrelType.ToString();
                            var methmethodCallrelFullName = methmethodCallrelTypeName + " (" + method.FullName + " to " + methodCall.FullName + ")";
                            var methmethodCallrelId = Util.MakeModelRelationshipIdentifier(ModelFactory.ModelName, methmethodCallrelFullName, methmethodCallrelType, methodElement.identifier, methodCallElement.identifier);
                            var methmethodCallrelMatching = from rMatch in relationships.relationship
                                                            where rMatch.identifier == methmethodCallrelId
                                                            select rMatch;
                            if (methmethodCallrelMatching.Count() == 0)
                            {
                                methmethodCallRelationship = CecilFactory.NewRelationship(new CecilFactory.PropertyAndValue
                                {
                                    pValues = {
                                            [(int)CecilConst.PropertyName.APIType] = Util.MakeBaseObjectTypeName(typeof(relationship)),
                                            //[(int)CecilConst.PropertyName.Name] = methmethodCallrelTypeName.Replace("Relationship", "") + " (" + method.Name + " to " + methodCall.Name + ")",
                                            [(int)CecilConst.PropertyName.Name] = "Calls [TR]",
                                            [(int)CecilConst.PropertyName.FullName] = methmethodCallrelFullName,
                                            [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                            [(int)CecilConst.PropertyName.ArchiMateElementType] = methmethodCallrelTypeName,
                                            //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                            [(int)CecilConst.PropertyName.SourceIdentifier] = methodElement.identifier,
                                            [(int)CecilConst.PropertyName.TargetIdentifier] = methodCallElement.identifier,
                                            }
                                });
                                if (methmethodCallRelationship != null) relationships.relationship.Add(methmethodCallRelationship);
                            }
                            else
                            {
                                methmethodCallRelationship = methmethodCallrelMatching.ElementAt(0);
                                // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            }

                            var tdElement = (element)null;
                            var methodCallNamespace = (methodCall.DeclaringType == null) ? "" : methodCall.DeclaringType.FullName;
                            var tdFullName = methodCallNamespace;
                            var tdTypeName = DetermineArchiMateServiceType(tdFullName).ToString();
                            var tdType = (ModelConst.ElementType)Enum.Parse(typeof(ModelConst.ElementType), tdTypeName);
                            var tdId = Util.MakeModelElementIdentifier(ModelFactory.ModelName, tdFullName, tdType);
                            var tdMatching = from eMatch in elements.element
                                             where eMatch.identifier == tdId
                                             select eMatch;
                            if (tdMatching.Count() == 0)
                            {
                                tdElement = CecilFactory.NewElement(new CecilFactory.PropertyAndValue
                                {
                                    pValues = {
                                        [(int)CecilConst.PropertyName.APIType] = typeof(TypeDefinition).GetType().Name,
                                        [(int)CecilConst.PropertyName.Name] = Cecil2ModelMate.GetNameFromFQName(tdFullName)  + " [MCTR]",
                                        [(int)CecilConst.PropertyName.FullName] = tdFullName,
                                        [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.MethodReference.ToString(),
                                        [(int)CecilConst.PropertyName.ArchiMateElementType] = tdTypeName,
                                        [(int)CecilConst.PropertyName.EADomain] = ModelConst.EADomainNames[(int)tdType],
                                        //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                        [(int)CecilConst.PropertyName.Namespace] = GetNamespaceFromFQName(tdFullName),
                                        //[(int)CecilConst.PropertyName.BaseType] = (typedef.BaseType == null) ? "" : typedef.BaseType.FullName,
                                        //[(int)CecilConst.PropertyName.DeclaringType] = (typedef.DeclaringType == null) ? "" : typedef.DeclaringType.FullName,
                                        //[(int)CecilConst.PropertyName.IsPublic] = td.IsPublic.ToString(),
                                        //[(int)CecilConst.PropertyName.ParentModule] = module.FullyQualifiedName,
                                        }
                                });
                                if (tdElement != null) elements.element.Add(tdElement);
                            }
                            else
                            {
                                tdElement = tdMatching.ElementAt(0);
                                // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            }

                            var methodCallTypedefRelationship = (relationship)null;
                            var methodCallTypedefrelType = ModelConst.RelationshipType.AssociationRelationship;
                            var methodCallTypedefrelTypeName = methodCallTypedefrelType.ToString();
                            var methodCallTypedefrelFullName = methodCallTypedefrelTypeName + " (" + tdFullName + " to " + methodCall.FullName + ")";
                            var methodCallTypedefrelId = Util.MakeModelRelationshipIdentifier(ModelFactory.ModelName, methodCallTypedefrelFullName, methodCallTypedefrelType, tdId, methodCallElement.identifier);
                            var methodCallTypedefrelMatching = from rMatch in relationships.relationship
                                                                where rMatch.identifier == methodCallTypedefrelId
                                                                select rMatch;
                            if (methodCallTypedefrelMatching.Count() == 0)
                            {
                                methodCallTypedefRelationship = CecilFactory.NewRelationship(new CecilFactory.PropertyAndValue
                                {
                                    pValues = {
                                            [(int)CecilConst.PropertyName.APIType] = Util.MakeBaseObjectTypeName(typeof(relationship)),
                                            //[(int)CecilConst.PropertyName.Name] = methodCallTypedefrelTypeName.Replace("Relationship", "") + " (" + tdFullName.Substring(tdFullName.LastIndexOf(".")+1) + " to " + methodCall.Name + ")",
                                            [(int)CecilConst.PropertyName.Name] = "Defines [AS]",
                                            [(int)CecilConst.PropertyName.FullName] = methodCallTypedefrelFullName,
                                            [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.MethodReference.ToString(),
                                            [(int)CecilConst.PropertyName.ArchiMateElementType] = methodCallTypedefrelTypeName,
                                            //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                            [(int)CecilConst.PropertyName.SourceIdentifier] = tdId,
                                            [(int)CecilConst.PropertyName.TargetIdentifier] = methodCallElement.identifier,
                                            }
                                });
                                if (methodCallTypedefRelationship != null) relationships.relationship.Add(methodCallTypedefRelationship);
                            }
                            else
                            {
                                methodCallTypedefRelationship = methodCallTypedefrelMatching.ElementAt(0);
                                // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            }
                        }
                    }
                }
            }
        }

        public static string GetNameFromFQName(string objectFQName)
        {
            string s = objectFQName;

            if (string.IsNullOrEmpty(s)) return "";

            if (objectFQName.Contains("PublicKeyToken=")) s = s.Substring(0, s.IndexOf(",")); // for assembly's FQNames

            int leftLessThan = s.IndexOf("<");
            if (leftLessThan <= 0)
            {
                int firstDot = s.LastIndexOf(".");
                if (firstDot > 0) s = s.Substring(firstDot + 1);
            }
            else
            {
                int firstDot = s.Substring(0, leftLessThan).LastIndexOf(".");
                if (firstDot > 0) s = s.Substring(firstDot + 1);
            }

            return s;
        }
        public static string GetNamespaceFromFQName(string objectFQName)
        {
            string s = objectFQName;

            if (string.IsNullOrEmpty(s)) return "";

            if (objectFQName.Contains("PublicKeyToken=")) s = s.Substring(0, s.IndexOf(",")); // for assembly's FQNames

            int leftLessThan = s.IndexOf("<");
            if (leftLessThan <= 0)
            {
                int firstDot = s.LastIndexOf(".");
                if (firstDot > 0) s = s.Substring(0,firstDot);
            }
            else
            {
                int firstDot = s.Substring(0, leftLessThan).LastIndexOf(".");
                if (firstDot > 0) s = s.Substring(0, firstDot);
            }

            return s;
        }

        public static void ProcessFieldReferences(MethodDefinition method, element methodElement, elements elements, relationships relationships)
        {
            Console.WriteLine("      Fields referenced by " + method.Name);
            if (method.Body != null && method.Body.Instructions != null)
            {
                foreach (var instruction in method.Body.Instructions)
                {
                    if (instruction.OpCode == OpCodes.Ldfld)
                    {
                        FieldReference fieldAccess = instruction.Operand as FieldReference;
                        if (fieldAccess != null)
                        {
                            Console.WriteLine("\tFieldAccess Name:\t" + fieldAccess.Name);
                            Console.WriteLine("\t  fieldAccess.FullName:\t" + fieldAccess.FullName);
                            //Console.WriteLine("\t  fieldAccess.FieldType:\t" + fieldAccess.FieldType.ToString());
                            //Console.WriteLine("\t  instruction.Offset:\t" + instruction.Offset.ToString());

                            var fieldAccessElement = (element)null;
                            var fieldAccessTypeName = DetermineArchiMateObjectType(fieldAccess.FullName).ToString();
                            var fieldAccessType = (ModelConst.ElementType)Enum.Parse(typeof(ModelConst.ElementType), fieldAccessTypeName);
                            var fieldAccessId = Util.MakeModelElementIdentifier(ModelFactory.ModelName, fieldAccess.FullName, fieldAccessType);
                            var fieldAccessMatching = from eMatch in elements.element
                                                      where eMatch.identifier == fieldAccessId
                                                      select eMatch;
                            if (fieldAccessMatching.Count() == 0)
                            {
                                fieldAccessElement = CecilFactory.NewElement(new CecilFactory.PropertyAndValue
                                {
                                    pValues = {
                                            [(int)CecilConst.PropertyName.APIType] = fieldAccess.GetType().Name,
                                            [(int)CecilConst.PropertyName.Name] = fieldAccess.Name + " [FA]",
                                            [(int)CecilConst.PropertyName.FullName] = fieldAccess.FullName,
                                            [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                            [(int)CecilConst.PropertyName.ArchiMateElementType] = fieldAccessTypeName,
                                            [(int)CecilConst.PropertyName.EADomain] = ModelConst.EADomainNames[(int)fieldAccessType],
                                            //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                            //[(int)CecilConst.PropertyName.ReturnType] = (fieldAccess.ReturnType == null) ? "" : fieldAccess.ReturnType.FullName,
                                            //[(int)CecilConst.PropertyName.ParameterCount] = fieldAccess.Parameters.Count.ToString(),
                                            //[(int)CecilConst.PropertyName.IsPublic] = fieldAccess.IsPublic.ToString(),
                                            [(int)CecilConst.PropertyName.DeclaringType] = (fieldAccess.DeclaringType == null) ? "" : fieldAccess.DeclaringType.FullName,
                                            [(int)CecilConst.PropertyName.ParentMethod] = method.FullName,
                                            [(int)CecilConst.PropertyName.Namespace] = (fieldAccess.FieldType == null) ? "" : fieldAccess.FieldType.FullName,
                                            }
                                });
                                if (fieldAccessElement != null) elements.element.Add(fieldAccessElement);
                            }
                            else
                            {
                                fieldAccessElement = fieldAccessMatching.ElementAt(0);
                                // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            }

                            var methfieldAccessRelationship = (relationship)null;
                            var methfieldAccessrelType = ModelConst.RelationshipType.AccessRelationship;
                            var methfieldAccessrelTypeName = methfieldAccessrelType.ToString();
                            var methfieldAccessrelFullName = methfieldAccessrelTypeName + " (" + method.FullName + " to " + fieldAccess.FullName + ")";
                            var methfieldAccessrelId = Util.MakeModelRelationshipIdentifier(ModelFactory.ModelName, methfieldAccessrelFullName, methfieldAccessrelType, methodElement.identifier, fieldAccessElement.identifier);
                            var methfieldAccessrelMatching = from rMatch in relationships.relationship
                                                             where rMatch.identifier == methfieldAccessrelId
                                                             select rMatch;
                            if (methfieldAccessrelMatching.Count() == 0)
                            {
                                methfieldAccessRelationship = CecilFactory.NewRelationship(new CecilFactory.PropertyAndValue
                                {
                                    pValues = {
                                            [(int)CecilConst.PropertyName.APIType] = Util.MakeBaseObjectTypeName(typeof(relationship)),
                                            //[(int)CecilConst.PropertyName.Name] = methfieldAccessrelTypeName.Replace("Relationship", "") + " (" + method.Name + " to " + fieldAccess.Name + ")",
                                            [(int)CecilConst.PropertyName.Name] = "Accesses [AC]",
                                            [(int)CecilConst.PropertyName.FullName] = methfieldAccessrelFullName,
                                            [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.Files.ToString(),
                                            [(int)CecilConst.PropertyName.ArchiMateElementType] = methfieldAccessrelTypeName,
                                            //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                            [(int)CecilConst.PropertyName.SourceIdentifier] = methodElement.identifier,
                                            [(int)CecilConst.PropertyName.TargetIdentifier] = fieldAccessElement.identifier,
                                            }
                                });
                                if (methfieldAccessRelationship != null) relationships.relationship.Add(methfieldAccessRelationship);
                            }
                            else
                            {
                                methfieldAccessRelationship = methfieldAccessrelMatching.ElementAt(0);
                                // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            }

                            // to a TypeDefinition element with FullName = fieldAccessNamespace
                            var tdElement = (element)null;
                            var fieldAccessNamespace = (fieldAccess.FieldType == null) ? "" : fieldAccess.FieldType.FullName;
                            var tdFullName = fieldAccessNamespace;
                            var tdTypeName = DetermineArchiMateServiceType(tdFullName).ToString();
                            var tdType = (ModelConst.ElementType)Enum.Parse(typeof(ModelConst.ElementType), tdTypeName);
                            var tdId = Util.MakeModelElementIdentifier(ModelFactory.ModelName, tdFullName, tdType);
                            var tdMatching = from eMatch in elements.element
                                             where eMatch.identifier == tdId
                                             select eMatch;
                            if (tdMatching.Count() == 0)
                            {
                                tdElement = CecilFactory.NewElement(new CecilFactory.PropertyAndValue
                                {
                                    pValues = {
                                        [(int)CecilConst.PropertyName.APIType] = typeof(TypeDefinition).GetType().Name,
                                        [(int)CecilConst.PropertyName.Name] = Cecil2ModelMate.GetNameFromFQName(tdFullName) + " [FATR]",
                                        [(int)CecilConst.PropertyName.FullName] = tdFullName,
                                        [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.FieldReference.ToString(),
                                        [(int)CecilConst.PropertyName.ArchiMateElementType] = tdTypeName,
                                        [(int)CecilConst.PropertyName.EADomain] = ModelConst.EADomainNames[(int)tdType],
                                        //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                        [(int)CecilConst.PropertyName.Namespace] = GetNamespaceFromFQName(tdFullName),
                                        //[(int)CecilConst.PropertyName.BaseType] = (typedef.BaseType == null) ? "" : typedef.BaseType.FullName,
                                        //[(int)CecilConst.PropertyName.DeclaringType] = (typedef.DeclaringType == null) ? "" : typedef.DeclaringType.FullName,
                                        //[(int)CecilConst.PropertyName.IsPublic] = td.IsPublic.ToString(),
                                        //[(int)CecilConst.PropertyName.ParentModule] = module.FullyQualifiedName,
                                        }
                                });
                                if (tdElement != null) elements.element.Add(tdElement);
                            }
                            else
                            {
                                tdElement = tdMatching.ElementAt(0);
                                // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            }

                            var fieldAccessTypedefRelationship = (relationship)null;
                            var fieldAccessTypedefrelType = ModelConst.RelationshipType.AssociationRelationship;
                            var fieldAccessTypedefrelTypeName = fieldAccessTypedefrelType.ToString();
                            var fieldAccessTypedefrelFullName = fieldAccessTypedefrelTypeName + " (" + tdFullName + " to " + fieldAccess.FullName + ")";
                            var fieldAccessTypedefrelId = Util.MakeModelRelationshipIdentifier(ModelFactory.ModelName, fieldAccessTypedefrelFullName, fieldAccessTypedefrelType, tdId, fieldAccessElement.identifier);
                            var fieldAccessTypedefrelMatching = from rMatch in relationships.relationship
                                                     where rMatch.identifier == fieldAccessTypedefrelId
                                                     select rMatch;
                            if (fieldAccessTypedefrelMatching.Count() == 0)
                            {
                                fieldAccessTypedefRelationship = CecilFactory.NewRelationship(new CecilFactory.PropertyAndValue
                                {
                                    pValues = {
                                            [(int)CecilConst.PropertyName.APIType] = Util.MakeBaseObjectTypeName(typeof(relationship)),
                                            //[(int)CecilConst.PropertyName.Name] = fieldAccessTypedefrelTypeName.Replace("Relationship", "") + " (" + tdFullName.Substring(tdFullName.LastIndexOf(".")+1) + " to " + fieldAccess.Name + ")",
                                            [(int)CecilConst.PropertyName.Name] = "Type For [AS]",
                                            [(int)CecilConst.PropertyName.FullName] = fieldAccessTypedefrelFullName,
                                            [(int)CecilConst.PropertyName.TriggerSource] = CecilConst.TriggerSource.FieldReference.ToString(),
                                            [(int)CecilConst.PropertyName.ArchiMateElementType] = fieldAccessTypedefrelTypeName,
                                            //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                                            [(int)CecilConst.PropertyName.SourceIdentifier] = tdId,
                                            [(int)CecilConst.PropertyName.TargetIdentifier] = fieldAccessElement.identifier,
                                            }
                                });
                                if (fieldAccessTypedefRelationship != null) relationships.relationship.Add(fieldAccessTypedefRelationship);
                            }
                            else
                            {
                                fieldAccessTypedefRelationship = fieldAccessTypedefrelMatching.ElementAt(0);
                                // TODO //[(int)CecilConst.PropertyName.AllReferencesCount] = "1",
                            }
                        }
                    }
                }
            }
        }

        public static ModelConst.ElementType DetermineArchiMateObjectType(string nameIn)
        {
            ModelConst.ElementType eType = ModelConst.ElementType.DataObject;

            string name = nameIn;
            if (name.Contains("PublicKeyToken=")) name = name.Substring(0, name.IndexOf(",")); // for assembly's FQNames
            int posSpace = name.IndexOf(" ");
            if (posSpace != -1) name = name.Substring(posSpace + 1);

            string ns = GetNamespaceFromFQName(name);

            if (ns.ToLower().StartsWith("system.") || ns.ToLower().StartsWith("microsoft.") || ns.ToLower().StartsWith("entityframework")
                || ns.ToLower().Equals("system") || ns.ToLower().Equals("microsoft") || ns.ToLower().Equals("mscorlib") 
                || ns.ToLower().Equals("vshost32") || ns.ToLower().Contains("vshost32.exe")
                || ns.Equals("TEntity") || ns.Equals("TResult") || ns.Equals("TValue") || ns.Equals("TEntry") || ns.Equals("T"))
            {
                eType = ModelConst.ElementType.Artifact;
            }

            return eType;
        }
        public static ModelConst.ElementType DetermineArchiMateSoftwareType(string nameIn)
        {
            ModelConst.ElementType eType = ModelConst.ElementType.ApplicationComponent;

            string name = nameIn;
            if (name.Contains("PublicKeyToken=")) name = name.Substring(0, name.IndexOf(",")); // for assembly's FQNames
            int posSpace = name.IndexOf(" ");
            if (posSpace != -1) name = name.Substring(posSpace + 1);

            string ns = GetNamespaceFromFQName(name);

            if (ns.ToLower().StartsWith("system.") || ns.ToLower().StartsWith("microsoft.") || ns.ToLower().StartsWith("entityframework")
                || ns.ToLower().Equals("system") || ns.ToLower().Equals("microsoft") || ns.ToLower().Equals("mscorlib")
                || ns.ToLower().Equals("vshost32") || ns.ToLower().Contains("vshost32.exe")
                || ns.Equals("TEntity") || ns.Equals("TResult") || ns.Equals("TValue") || ns.Equals("TEntry") || ns.Equals("T"))
            {
                eType = ModelConst.ElementType.SystemSoftware;
            }

            return eType;
        }
        public static ModelConst.ElementType DetermineArchiMateFunctionType(string nameIn)
        {
            ModelConst.ElementType eType = ModelConst.ElementType.ApplicationFunction;

            string name = nameIn;
            if (name.Contains("PublicKeyToken=")) name = name.Substring(0, name.IndexOf(",")); // for assembly's FQNames
            int posSpace = name.IndexOf(" ");
            if (posSpace != -1) name = name.Substring(posSpace + 1);

            string ns = GetNamespaceFromFQName(name);

            if (ns.ToLower().StartsWith("system.") || ns.ToLower().StartsWith("microsoft.") || ns.ToLower().StartsWith("entityframework")
                || ns.ToLower().Equals("system") || ns.ToLower().Equals("microsoft") || ns.ToLower().Equals("mscorlib")
                || ns.ToLower().Equals("vshost32") || ns.ToLower().Contains("vshost32.exe")
                || ns.Equals("TEntity") || ns.Equals("TResult") || ns.Equals("TValue") || ns.Equals("TEntry") || ns.Equals("T"))
            {
                eType = ModelConst.ElementType.InfrastructureFunction;
            }

            return eType;
        }
        public static ModelConst.ElementType DetermineArchiMateServiceType(string nameIn)
        {
            ModelConst.ElementType eType = ModelConst.ElementType.ApplicationService;

            string name = nameIn;
            if (name.Contains("PublicKeyToken=")) name = name.Substring(0, name.IndexOf(",")); // for assembly's FQNames
            int posSpace = name.IndexOf(" ");
            if (posSpace != -1) name = name.Substring(posSpace + 1);

            string ns = GetNamespaceFromFQName(name);

            if (ns.ToLower().StartsWith("system.") || ns.ToLower().StartsWith("microsoft.") || ns.ToLower().StartsWith("entityframework")
                || ns.ToLower().Equals("system") || ns.ToLower().Equals("microsoft") || ns.ToLower().Equals("mscorlib")
                || ns.ToLower().Equals("vshost32") || ns.ToLower().Contains("vshost32.exe")
                || ns.Equals("TEntity") || ns.Equals("TResult") || ns.Equals("TValue") || ns.Equals("TEntry") || ns.Equals("T"))
            {
                eType = ModelConst.ElementType.InfrastructureService;
            }

            return eType;
        }
        public static ModelConst.ElementType DetermineArchiMateInterfaceType(string nameIn)
        {
            ModelConst.ElementType eType = ModelConst.ElementType.ApplicationInterface;

            string name = nameIn;
            if (name.Contains("PublicKeyToken=")) name = name.Substring(0, name.IndexOf(",")); // for assembly's FQNames
            int posSpace = name.IndexOf(" ");
            if (posSpace != -1) name = name.Substring(posSpace + 1);

            string ns = GetNamespaceFromFQName(name);

            if (ns.ToLower().StartsWith("system.") || ns.ToLower().StartsWith("microsoft.") || ns.ToLower().StartsWith("entityframework")
                || ns.ToLower().Equals("system") || ns.ToLower().Equals("microsoft") || ns.ToLower().Equals("mscorlib")
                || ns.ToLower().Equals("vshost32") || ns.ToLower().Contains("vshost32.exe")
                || ns.Equals("TEntity") || ns.Equals("TResult") || ns.Equals("TValue") || ns.Equals("TEntry") || ns.Equals("T"))
            {
                eType = ModelConst.ElementType.InfrastructureInterface;
            }

            return eType;
        }
    }
}
