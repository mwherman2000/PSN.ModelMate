using PSN.ModelMate.EDM;
using PSN.ModelMate.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSN.ModelMate.Cecil
{
    public static class CecilFactory
    {
        public class PropertyAndValue
        {
            public string[] pValues = new string[(int)CecilConst.PropertyName.NumberOfItems];
        }

        //static PropertyAndValue pavc2 = new PropertyAndValue
        //{
        //    pValue = {
        //        [0] = "Hi",
        //        [(int)CecilConst.PropertyName.ArchiMateElementType] = "Bye",
        //    }
        //};

        public static propertydef[] pdc = new propertydef[(int)CecilConst.PropertyName.NumberOfItems];

        public static void Initialize()
        {
            if (pdc[(int)CecilConst.PropertyName.UniqueKey] == null)
            {
                ModelConst.PropertyDataType pdtString = ModelConst.PropertyDataType.stringType;
                propertydef pdUniqueKey = ModelFactory.NewPropertyDef(CecilConst.PropertyName.UniqueKey.ToString(), pdtString);
                propertydef pdFilename = ModelFactory.NewPropertyDef(CecilConst.PropertyName.Filename.ToString(), pdtString);
                propertydef pdFQName = ModelFactory.NewPropertyDef(CecilConst.PropertyName.FQName.ToString(), pdtString);
                propertydef pdType = ModelFactory.NewPropertyDef(CecilConst.PropertyName.Type.ToString(), pdtString);
                propertydef pdName = ModelFactory.NewPropertyDef(CecilConst.PropertyName.Name.ToString(), pdtString);
                propertydef pdSignature = ModelFactory.NewPropertyDef(CecilConst.PropertyName.Signature.ToString(), pdtString);
                propertydef pdReturnType = ModelFactory.NewPropertyDef(CecilConst.PropertyName.ReturnType.ToString(), pdtString);
                propertydef pdVersion = ModelFactory.NewPropertyDef(CecilConst.PropertyName.Version.ToString(), pdtString);
                propertydef pdAllReferencesCount = ModelFactory.NewPropertyDef(CecilConst.PropertyName.AllReferencesCount.ToString(), ModelConst.PropertyDataType.numberType);
                propertydef pdNamespace = ModelFactory.NewPropertyDef(CecilConst.PropertyName.Namespace.ToString(), pdtString);
                propertydef pdFQFilename = ModelFactory.NewPropertyDef(CecilConst.PropertyName.FQFilename.ToString(), pdtString);
                propertydef pdFullName = ModelFactory.NewPropertyDef(CecilConst.PropertyName.FullName.ToString(), pdtString);
                propertydef pdParentAssembly = ModelFactory.NewPropertyDef(CecilConst.PropertyName.ParentAssembly.ToString(), pdtString);
                propertydef pdParentModule = ModelFactory.NewPropertyDef(CecilConst.PropertyName.ParentModule.ToString(), pdtString);
                propertydef pdModuleOffset = ModelFactory.NewPropertyDef(CecilConst.PropertyName.ModuleOffset.ToString(), pdtString);
                propertydef pdParentFile = ModelFactory.NewPropertyDef(CecilConst.PropertyName.ParentFile.ToString(), pdtString);
                propertydef pdParentTypeDefinition = ModelFactory.NewPropertyDef(CecilConst.PropertyName.ParentTypeDefinition.ToString(), pdtString);
                propertydef pdIsPublic = ModelFactory.NewPropertyDef(CecilConst.PropertyName.IsPublic.ToString(), ModelConst.PropertyDataType.booleanType);
                propertydef pdArchiMateElementType = ModelFactory.NewPropertyDef(CecilConst.PropertyName.ArchiMateElementType.ToString(), pdtString);
                propertydef pdCreated = ModelFactory.NewPropertyDef(CecilConst.PropertyName.Created.ToString(), ModelConst.PropertyDataType.dateType);
                propertydef pdLastAccessed = ModelFactory.NewPropertyDef(CecilConst.PropertyName.LastAccessed.ToString(), ModelConst.PropertyDataType.dateType);
                propertydef pdLastWritten = ModelFactory.NewPropertyDef(CecilConst.PropertyName.LastWritten.ToString(), ModelConst.PropertyDataType.dateType);
                propertydef pdLength = ModelFactory.NewPropertyDef(CecilConst.PropertyName.Length.ToString(), ModelConst.PropertyDataType.numberType);
                propertydef pdExtension = ModelFactory.NewPropertyDef(CecilConst.PropertyName.Extension.ToString(), pdtString);
                propertydef pdPublicKeyToken = ModelFactory.NewPropertyDef(CecilConst.PropertyName.PublicKeyToken.ToString(), pdtString);
                propertydef pdCulture = ModelFactory.NewPropertyDef(CecilConst.PropertyName.Culture.ToString(), pdtString);
                propertydef pdAPIType = ModelFactory.NewPropertyDef(CecilConst.PropertyName.APIType.ToString(), pdtString);
                propertydef pdBaseType = ModelFactory.NewPropertyDef(CecilConst.PropertyName.BaseType.ToString(), pdtString);
                propertydef pdParameterCount = ModelFactory.NewPropertyDef(CecilConst.PropertyName.ParameterCount.ToString(), ModelConst.PropertyDataType.numberType);
                propertydef pdDeclaringType = ModelFactory.NewPropertyDef(CecilConst.PropertyName.DeclaringType.ToString(), pdtString);
                propertydef pdSourceIdentifier = ModelFactory.NewPropertyDef(CecilConst.PropertyName.SourceIdentifier.ToString(), pdtString);
                propertydef pdTargetIdentifier = ModelFactory.NewPropertyDef(CecilConst.PropertyName.TargetIdentifier.ToString(), pdtString);
                propertydef pdEADomain = ModelFactory.NewPropertyDef(CecilConst.PropertyName.EADomain.ToString(), pdtString);
                propertydef pdParentMethod = ModelFactory.NewPropertyDef(CecilConst.PropertyName.ParentMethod.ToString(), pdtString);
                propertydef pdTriggerSource = ModelFactory.NewPropertyDef(CecilConst.PropertyName.TriggerSource.ToString(), pdtString);
                propertydef pdNextElement = ModelFactory.NewPropertyDef(CecilConst.PropertyName.NextElement.ToString(), pdtString);

                pdc[(int)CecilConst.PropertyName.UniqueKey] = pdUniqueKey;
                pdc[(int)CecilConst.PropertyName.Filename] = pdFilename;
                pdc[(int)CecilConst.PropertyName.FQName] = pdFQName;
                pdc[(int)CecilConst.PropertyName.Type] = pdType;
                pdc[(int)CecilConst.PropertyName.Name] = pdName;
                pdc[(int)CecilConst.PropertyName.Signature] = pdSignature;
                pdc[(int)CecilConst.PropertyName.ReturnType] = pdReturnType;
                pdc[(int)CecilConst.PropertyName.Version] = pdVersion;
                pdc[(int)CecilConst.PropertyName.AllReferencesCount] = pdAllReferencesCount;
                pdc[(int)CecilConst.PropertyName.Namespace] = pdNamespace;
                pdc[(int)CecilConst.PropertyName.FQFilename] = pdFQFilename;
                pdc[(int)CecilConst.PropertyName.FullName] = pdFullName;
                pdc[(int)CecilConst.PropertyName.ParentAssembly] = pdParentAssembly;
                pdc[(int)CecilConst.PropertyName.ParentModule] = pdParentModule;
                pdc[(int)CecilConst.PropertyName.ModuleOffset] = pdModuleOffset;
                pdc[(int)CecilConst.PropertyName.ParentFile] = pdParentFile;
                pdc[(int)CecilConst.PropertyName.ParentTypeDefinition] = pdParentTypeDefinition;
                pdc[(int)CecilConst.PropertyName.IsPublic] = pdIsPublic;
                pdc[(int)CecilConst.PropertyName.ArchiMateElementType] = pdArchiMateElementType;
                pdc[(int)CecilConst.PropertyName.Created] = pdCreated;
                pdc[(int)CecilConst.PropertyName.LastAccessed] = pdLastAccessed;
                pdc[(int)CecilConst.PropertyName.LastWritten] = pdLastWritten;
                pdc[(int)CecilConst.PropertyName.Length] = pdLength;
                pdc[(int)CecilConst.PropertyName.Extension] = pdExtension;
                pdc[(int)CecilConst.PropertyName.PublicKeyToken] = pdPublicKeyToken;
                pdc[(int)CecilConst.PropertyName.Culture] = pdCulture;
                pdc[(int)CecilConst.PropertyName.APIType] = pdAPIType;
                pdc[(int)CecilConst.PropertyName.BaseType] = pdBaseType;
                pdc[(int)CecilConst.PropertyName.ParameterCount] = pdParameterCount;
                pdc[(int)CecilConst.PropertyName.DeclaringType] = pdDeclaringType;
                pdc[(int)CecilConst.PropertyName.SourceIdentifier] = pdSourceIdentifier;
                pdc[(int)CecilConst.PropertyName.TargetIdentifier] = pdTargetIdentifier;
                pdc[(int)CecilConst.PropertyName.EADomain] = pdEADomain;
                pdc[(int)CecilConst.PropertyName.ParentMethod] = pdParentMethod;
                pdc[(int)CecilConst.PropertyName.TriggerSource] = pdTriggerSource;
                pdc[(int)CecilConst.PropertyName.NextElement] = pdNextElement;
            }
        }

        public static element NewElement(PropertyAndValue pavcIn)
        {
            element e = null;

            if (pavcIn.pValues[(int) CecilConst.PropertyName.Name] == null) throw new ArgumentNullException(CecilConst.PropertyName.Name.ToString());
            if (pavcIn.pValues[(int)CecilConst.PropertyName.FullName] == null) throw new ArgumentNullException(CecilConst.PropertyName.FQName.ToString());
            if (pavcIn.pValues[(int)CecilConst.PropertyName.ArchiMateElementType] == null) throw new ArgumentNullException(CecilConst.PropertyName.ArchiMateElementType.ToString());
            if (pavcIn.pValues[(int)CecilConst.PropertyName.APIType] == null) throw new ArgumentNullException(CecilConst.PropertyName.APIType.ToString());

            string label = pavcIn.pValues[(int)CecilConst.PropertyName.Name];
            string fullname = pavcIn.pValues[(int)CecilConst.PropertyName.FullName];
            string type = pavcIn.pValues[(int)CecilConst.PropertyName.ArchiMateElementType];

            //if (fullname.Contains("_annotations")) System.Diagnostics.Debugger.Break();

            properties ps = ModelFactory.NewProperties();
            foreach (CecilConst.PropertyName pn in Enum.GetValues(typeof(CecilConst.PropertyName)))
            {
                if (pn == CecilConst.PropertyName.NumberOfItems) break;

                int ipn = (int)pn;
                if (pavcIn.pValues[ipn] != null)
                {
                    property p = ModelFactory.NewProperty(ModelFactory.NewValue(pavcIn.pValues[ipn]), pdc[ipn]);
                    ps.property.Add(p);
                }
            }

            ModelConst.ElementType eType = (ModelConst.ElementType)Enum.Parse(typeof(ModelConst.ElementType), type);
            e = ModelFactory.NewElement(fullname, eType, ps);

            e.label.ElementAt(0).label_text = label; // short name for display; fullname to compute unique identifier

            return e;
        }

        public static relationship NewRelationship(PropertyAndValue pavcIn)
        {
            relationship r = null;

            if (pavcIn.pValues[(int)CecilConst.PropertyName.Name] == null) throw new ArgumentNullException(CecilConst.PropertyName.Name.ToString());
            if (pavcIn.pValues[(int)CecilConst.PropertyName.FullName] == null) throw new ArgumentNullException(CecilConst.PropertyName.FQName.ToString());
            if (pavcIn.pValues[(int)CecilConst.PropertyName.ArchiMateElementType] == null) throw new ArgumentNullException(CecilConst.PropertyName.ArchiMateElementType.ToString());
            if (pavcIn.pValues[(int)CecilConst.PropertyName.APIType] == null) throw new ArgumentNullException(CecilConst.PropertyName.APIType.ToString());
            if (pavcIn.pValues[(int)CecilConst.PropertyName.SourceIdentifier] == null) throw new ArgumentNullException(CecilConst.PropertyName.SourceIdentifier.ToString());
            if (pavcIn.pValues[(int)CecilConst.PropertyName.TargetIdentifier] == null) throw new ArgumentNullException(CecilConst.PropertyName.TargetIdentifier.ToString());

            string label = pavcIn.pValues[(int)CecilConst.PropertyName.Name];
            string fullname = pavcIn.pValues[(int)CecilConst.PropertyName.FullName];
            string type = pavcIn.pValues[(int)CecilConst.PropertyName.ArchiMateElementType];
            string eSourceIdentifier = pavcIn.pValues[(int)CecilConst.PropertyName.SourceIdentifier];
            string eTargetIdentifier = pavcIn.pValues[(int)CecilConst.PropertyName.TargetIdentifier];

            properties ps = ModelFactory.NewProperties();
            foreach (CecilConst.PropertyName pn in Enum.GetValues(typeof(CecilConst.PropertyName)))
            {
                if (pn == CecilConst.PropertyName.NumberOfItems) break;

                int ipn = (int)pn;
                if (pavcIn.pValues[ipn] != null)
                {
                    property p = ModelFactory.NewProperty(ModelFactory.NewValue(pavcIn.pValues[ipn]), pdc[ipn]);
                    ps.property.Add(p);
                }
            }

            ModelConst.RelationshipType eType = (ModelConst.RelationshipType)Enum.Parse(typeof(ModelConst.RelationshipType), type);
            r = ModelFactory.NewRelationship(fullname, eType, eSourceIdentifier, eTargetIdentifier, ps);

            r.label.ElementAt(0).label_text = label; // short name for display; fullname to compute unique identifier

            return r;
        }
    }
}
