using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSN.ModelMate.Cecil
{
    public static class CecilConst
    {
        public enum TriggerSource
        {
            Files,
            FieldReference,
            MethodReference,
        }

        public enum PropertyName
        {
            UniqueKey,
            Filename,
            FQName,
            Type,
            Name,
            Signature,
            ReturnType,
            Version,
            AllReferencesCount,
            Namespace,
            FQFilename,
            FullName,
            ParentAssembly,
            ParentModule,
            ModuleOffset,
            ParentFile,
            ParentTypeDefinition,
            IsPublic,
            ArchiMateElementType,
            Created,
            LastAccessed,
            LastWritten,
            Length,
            Extension,
            PublicKeyToken,
            Culture,
            APIType,
            BaseType,
            ParameterCount,
            DeclaringType,
            SourceIdentifier,
            TargetIdentifier,
            EADomain,
            ParentMethod,
            TriggerSource,
            NextElement,
            NumberOfItems
        }
    }
}
