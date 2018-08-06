using PSN.ModelMate.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PSN.ModelMate.Lib
{
    public static class Util
    {
        /// <summary>
        /// Calculates the eta.
        /// </summary>
        /// <param name="processStarted">When the process started</param>
        /// <param name="totalElements">How many items are being processed</param>
        /// <param name="processedElements">How many items are done</param>
        /// <returns>A string representing the time left</returns>
        public static string PredictCompletion(DateTime processStarted, int totalElements, int processedElements)
        {
            int totalMilliseconds = (int)(DateTime.Now - processStarted).Milliseconds;
            if (totalMilliseconds < 1) totalMilliseconds = 1;
            double itemsPertotalMilliseconds = processedElements / totalMilliseconds;
            int secondsRemaining = (int)((totalElements - processedElements) / itemsPertotalMilliseconds) / 1000;

            return new TimeSpan(0, 0, secondsRemaining).ToString(@"hh\:mm\:ss");
        }

        //public static string MakeIdentifierNewGuid(string prefix) // deprecated
        //{
        //    string id = "";
        //    id = prefix + ":" + Guid.NewGuid().ToString();

        //    return id;
        //}

        public static string MakeIdentifierTimestamped(string prefix)
        {
            return MakeIdentifierTimestamped(prefix, DateTime.Now);
        }
        public static string MakeIdentifierTimestamped(string prefix, DateTime dt)
        {
            string id = Guid.NewGuid().ToString();
            id = id.Replace("{", "").Replace("}", "").ToUpper();

            string timestamp = dt.ToString(ModelConst.ID_DATETIMEFORMAT);
            timestamp = timestamp.Replace(':', '-').Replace('.', '-'); //redundant

            id = "id-" + prefix + "." + timestamp + "-" + id;

            return id;
        }
        public static string MakeIdentifierRootIdentifier(string type, string suffix)
        {
            string id = "";
            id = "id-" + type.Substring(0, 1) + MakeSafeIdentifier(suffix);
            return id;
        }
        public static string MakeIdentifierRootIdentifier(string type, string subtype, string suffix)
        {
            string id = "";
            id = "id-" + type.Substring(0, 1) + MakeSafeIdentifier(subtype) + "-" + MakeSafeIdentifier(suffix);
            return id;
        }
        public static string MakeIdentifierFromParentIdentifier(string parentIdentifier, string type, string suffix)
        {
            string id = "";
            id = parentIdentifier + "." + type.Substring(0, 1) + MakeSafeIdentifier(suffix);
            return id;
        }
        public static string MakeIdentifierFromParentIdentifier(string parentIdentifier, string type, string subtype, string suffix)
        {
            string id = "";
            id = parentIdentifier + "." + type.Substring(0, 1) + MakeSafeIdentifier(subtype) + "-" + MakeSafeIdentifier(suffix);
            return id;
        }

        public static string MakeModelElementIdentifier(string modelNameIn, string labelIn, ModelConst.ElementType typeIn)
        {
            string id = "";

            id = Util.MakeIdentifierFromParentIdentifier(
                                Util.MakeIdentifierRootIdentifier(typeof(model).Name, Util.TrimWhitespace(modelNameIn)),
                                MakeBaseObjectTypeName(typeof(element)), typeIn.ToString(), Util.TrimWhitespace(labelIn));
            return id;
        }
        public static string MakeModelRelationshipIdentifier(string modelNameIn, string labelIn, ModelConst.RelationshipType typeIn)
        {
            string id = "";

            id = Util.MakeIdentifierFromParentIdentifier(
                      Util.MakeIdentifierRootIdentifier(MakeBaseObjectTypeName(typeof(model)), Util.TrimWhitespace(modelNameIn)),
                      MakeBaseObjectTypeName(typeof(relationship)), 
                      typeIn.ToString(), 
                      Util.TrimWhitespace(labelIn));
            return id;
        }
        public static string MakeModelRelationshipIdentifier(string modelNameIn,
                                                                ModelConst.RelationshipType typeIn,
                                                                string eSourceIdentifier, string eTargetIdentifier)
        {
            string id = "";

            id = Util.MakeIdentifierFromParentIdentifier(
                      Util.MakeIdentifierRootIdentifier(MakeBaseObjectTypeName(typeof(model)), Util.TrimWhitespace(modelNameIn)),
                      MakeBaseObjectTypeName(typeof(relationship)),
                      typeIn.ToString(),
                      "NoLabel" + "-" + eSourceIdentifier + "-" + eTargetIdentifier);
            return id;
        }
        public static string MakeModelRelationshipIdentifier(string modelNameIn, string labelIn,
                                                       ModelConst.RelationshipType typeIn,
                                                       string eSourceIdentifier, string eTargetIdentifier)
        {
            string id = "";

            id = Util.MakeIdentifierFromParentIdentifier(
                      Util.MakeIdentifierRootIdentifier(MakeBaseObjectTypeName(typeof(model)), Util.TrimWhitespace(modelNameIn)),
                      MakeBaseObjectTypeName(typeof(relationship)),
                      typeIn.ToString(),
                      Util.TrimWhitespace(labelIn) + "-" + eSourceIdentifier + "-" + eTargetIdentifier);
            return id;
        }

        public static string MakeBaseObjectTypeName(Type t)
        {
            string objTypeName = t.GetType().Name;
            int underPos = objTypeName.IndexOf("_");
            if (underPos > 0) objTypeName = objTypeName.Substring(0, underPos);
            return objTypeName;
        }
        public static string MakeBaseObjectTypeName(object o)
        { 
            string objTypeName = o.GetType().Name;
            int underPos = objTypeName.IndexOf("_");
            if (underPos > 0) objTypeName = objTypeName.Substring(0, underPos);
            return objTypeName;
        }

        private static Int32 _lastId = 0;
        private static DateTime _dtModelMateBaseline = new DateTime(2016, 1, 1);
        private static DateTime _dtModelMateNow = DateTime.Now;
        private static Int64 Id64 = (Int64)(_dtModelMateNow - _dtModelMateBaseline).TotalMilliseconds; // Seed
        private static Int32 Id = (Int32)Id64; // TODO - not multi-user friendly
        public static Int32 MakeIdInt32()
        {
            Id++;

            return Id;
        }

        public static string MakeSafeIdentifier(string s)
        {
            string safe = s;
            safe = Regex.Replace(safe, "[aeiou]", "");
            safe = Regex.Replace(safe, "[^a-zA-Z0-9]", "");
            safe = TrimWhitespace(safe);

            return safe;
        }

        public static string TrimWhitespace(string s)
        {
            char[] whitespace = new char[] { ' ', '\t', '\r', '\n' };
            return s.TrimStart(whitespace).TrimEnd(whitespace);
        }
    }
}
