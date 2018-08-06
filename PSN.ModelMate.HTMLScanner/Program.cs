using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace PSN.ModelMate.HTMLScanner
{
    class Program
    {
        // ... Target page.
        const string page = "https://www.principles.com";
        static string pagecontents = "";

        enum ContentTypes
        {
            BaseEntity_Principles_RayDalio_ModelMate,
            CommentaryText_Principles_RayDalio_ModelMate,
            QuestionText_Principles_RayDalio_ModelMate,
            TopicText_Principles_RayDalio_ModelMate,
            BulletText_Principles_RayDalio_ModelMate,
            NumberText_Principles_RayDalio_ModelMate,
            LetterText_Principles_RayDalio_ModelMate,
            ImageUrl_Principles_RayDalio_ModelMate,
            FigureText_Principles_RayDalio_ModelMate,
            KeyPhrase_Principles_RayDalio_ModelMate,
        }

        enum EntityType
        {
            Publication_Principles_RayDalio_ModelMate,
            Section_Principles_RayDalio_ModelMate,
            Topic_Principles_RayDalio_ModelMate,
            Principle_Principles_RayDalio_ModelMate,
            Subprinciple_Principles_RayDalio_ModelMate,
            Unknown_Principles_RayDalio_ModelMate,
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //string ss = "Donï¿½tfoovar??"; // "fooï¿½bar"; // "â€™";
            //string tt = FixEncoding(ss);
            //Console.WriteLine("[" + tt + "]");

            Console.WriteLine("Creating and starting task...");
            //Task t = new Task(DownloadPageAsync);
            //t.Start();
            //Console.WriteLine("Waiting...");
            //t.Wait();

            Console.WriteLine("Creating, starting and waiting...");
            DownloadPageAsync().Wait();

            //Console.WriteLine(pagecontents);

            // Create a new parser front-end (can be re-used)
            var parser = new HtmlParser();

            //Just get the DOM representation
            var document = parser.Parse(pagecontents);

            //var mainSections = document.QuerySelectorAll("section");
            //foreach(var s in mainSections)
            //{
            //    Console.WriteLine("NodeName:\t'" + s.NodeName);
            //    Console.WriteLine("TagName:\t'" + s.TagName);
            //    foreach (var attr in s.Attributes)
            //    {
            //        Console.WriteLine("Attribute:\t'" + attr.Name + "=" + attr.Value);
            //    }
            //}

            //Console.WriteLine("Principle Sections:");
            //foreach (var s in mainSections)
            //{
            //    if (String.IsNullOrEmpty(s.GetAttribute("id")) ? false : s.GetAttribute("id").StartsWith("Principle"))
            //    { 
            //        Console.WriteLine("NodeName:\t'" + s.NodeName);
            //        Console.WriteLine("TagName:\t'" + s.TagName);
            //        Console.WriteLine("ClassName:\t'" + s.ClassName);
            //        foreach (var attr in s.Attributes)
            //        {
            //            Console.WriteLine("Attribute:\t'" + attr.Name + "=" + attr.Value);
            //        }
            //    }
            //}

            bool doMakeKeywordPhrases = true;

            bool haveTopicTitle = false;
            bool isAfterFirstPrincipleNumber = false;
            bool isAfterFirstPrincipleTitle = false;
            string childPrincipleNumber = "0"; // to get the initial Publication number correct (and not SNULL)
            string parentPrincipleNumber = "0";
            string topicNumber = "NULL";
            string principleNumber = "NULL";
            string subprincipleNumber = "NULL";
            EntityType entityType = EntityType.Unknown_Principles_RayDalio_ModelMate;
            string topicText = "NULL";
            string sectionPrincipleNumber = "S0"; // format Snnn
            string sectionTitle = "NULL";
            string principleTitle = "NULL";
            string principleText = "NULL";
            string principleCommentaryText = "NULL";
            string principleQuestionText = "NULL";
            string principleLetterText = "NULL";
            string principleBulletText = "NULL";
            string principleNumberText = "NULL";
            string principleImageUrl = "NULL";
            string principleFigureText = "NULL";
            int entityOrder = 0;

            var principleSections = document.All.Where(m =>
                    (m.LocalName == "div" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("section__title")) ||
                    (m.LocalName == "section" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName == "topic") ||
                        (m.LocalName == "div" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("topic__number")) ||
                        //(m.LocalName == "div" && !String.IsNullOrEmpty(m.ClassName)&& m.ClassName.Contains("topic__title")) ||
                        (m.LocalName == "div" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("topic__text")) ||
                    (m.LocalName == "div" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName == "principle") ||
                        (m.LocalName == "span" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("principle__number")) ||
                        (m.LocalName == "h2" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("principle__title")) ||
                    (m.LocalName == "div" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName == "subprinciple") ||
                        (m.LocalName == "span" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("subprinciple__number")) ||
                        (m.LocalName == "h3" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("subprinciple__title")) ||
                    (m.LocalName == "p" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("commentary")) ||
                    (m.LocalName == "p" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("question")) ||
                    (m.LocalName == "p" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("bullet")) ||
                    (m.LocalName == "p" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("number")) ||
                    (m.LocalName == "p" && !String.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("letter")) ||
                    (m.LocalName == "figure" && !String.IsNullOrEmpty(m.ClassName) && !m.ClassName.Contains("image")) ||
                    (m.LocalName == "img")
            );

            List<String> corpus = new List<string>();
            // 0            1                       2                        3           4                5            6        7 
            // entityorder, child principle number, parent principle number, entitytype, principle title, contenttype, content, keyphases
            string csvformat = "\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"";
            using (System.IO.StreamWriter _File = new System.IO.StreamWriter(@"HTMLScanner-RayDalioPrinciples.csv",false,Encoding.UTF8))
            {
                _File.WriteLine(csvformat, 
                    "EntityOrder", "ChildPrincipleNumber", "ParentPrincipleNumber", "EntityType", "PrincipleTitle", "ContentType", "Content", "KeyPhrases");
                _File.WriteLine(csvformat,
                    "0", "0", "-1", "Publication_Principles_RayDalio_ModelMate", "PRINCIPLES BY RAY DALIO", "BaseEntity_Principles_RayDalio_ModelMate", "0. PRINCIPLES BY RAY DALIO", "principles,Ray Dalio");

                Console.WriteLine("Principle Sections 2:");
                foreach (var docPart in principleSections)
                {
                    //if (String.IsNullOrEmpty(s.GetAttribute("id")) ? false : s.GetAttribute("id").StartsWith("Principle"))
                    //{
                    Console.WriteLine("LocalName:\t'" + docPart.LocalName + "'");
                    Console.WriteLine("NodeName:\t'" + docPart.NodeName + "'");
                    Console.WriteLine("TagName:\t'" + docPart.TagName + "'");
                    Console.WriteLine("ClassName:\t'" + docPart.ClassName + "'");
                    foreach (var attr in docPart.Attributes)
                    {
                        Console.WriteLine("Attribute:\t'" + attr.Name + "=" + attr.Value + "'");
                    }
                    //}

                    //if (s.LocalName == "figure") System.Diagnostics.Debugger.Break(); // â€™  
     

                    if (docPart.ClassName.Contains("section__title"))
                    {
                        parentPrincipleNumber = "0";
                        childPrincipleNumber = "S" + childPrincipleNumber.ToString();
                        sectionPrincipleNumber = childPrincipleNumber;

                        sectionTitle = Regex.Replace(FixEncoding(docPart.TextContent), @"\s+", " ").Replace("“", "&quote;").Replace("”", "&quote;").Replace("\"", "&quote;").Trim();
                        entityType = MapEnitityType("section");
                        corpus.Add(sectionTitle);

                        Console.WriteLine("principleNumber:\t'" + childPrincipleNumber + "' parent:" + parentPrincipleNumber + " entityType: " + entityType.ToString() + " insideSectionTitle: " + haveTopicTitle.ToString());
                        Console.WriteLine("sectionTitle:\t'" + sectionTitle + "'");

                        if (doMakeKeywordPhrases && !String.IsNullOrEmpty(sectionTitle))
                        {
                            List<String> response = new List<string>();
                            response.Add(sectionTitle);
                            string request = AzureTextAnalytics.FormatRequestJSON(response);
                            AzureTextAnalytics.MakeKeyPhrasesRequest(request).Wait();
                        }
                        else
                        {
                            AzureTextAnalytics.Response = "NULL";
                        }
                        string keyphases = AzureTextAnalytics.Response;

                        if (String.IsNullOrEmpty(sectionTitle)) sectionTitle = "NULL";

                        _File.WriteLine(csvformat,
                            ++entityOrder, childPrincipleNumber, parentPrincipleNumber, entityType.ToString(), sectionTitle, ContentTypes.BaseEntity_Principles_RayDalio_ModelMate.ToString(), childPrincipleNumber + ". " + sectionTitle, keyphases);

                        parentPrincipleNumber = childPrincipleNumber;
                    }
                    else if (docPart.ClassName.Contains("topic__number"))
                    {
                        childPrincipleNumber = Regex.Replace(FixEncoding(docPart.TextContent), @"\s+", " ").Replace("“", "&quote;").Replace("”", "&quote;").Replace("\"", "&quote;").Trim();
                        isAfterFirstPrincipleNumber = true;

                        parentPrincipleNumber = sectionPrincipleNumber;
                        topicNumber = childPrincipleNumber;
                        entityType = MapEnitityType(docPart.ClassName);
                        corpus.Add(sectionTitle);

                        Console.WriteLine("principleNumber:\t'" + childPrincipleNumber + "' parent:" + parentPrincipleNumber + " entityType: " + entityType.ToString() + " insideSectionTitle: " + haveTopicTitle.ToString());
                        Console.WriteLine("childPrincipleNumber:\t'" + childPrincipleNumber + "'");
                    }
                    else if (docPart.ClassName.Contains("topic__text"))
                    {
                        topicText = Regex.Replace(FixEncoding(docPart.TextContent), @"\s+", " ").Replace("“", "&quote;").Replace("”", "&quote;").Replace("\"", "&quote;").Trim();
                        isAfterFirstPrincipleTitle = true;
                        corpus.Add(topicText);

                        Console.WriteLine("principleNumber:\t'" + childPrincipleNumber + "' parent:" + parentPrincipleNumber + " entityType: " + entityType.ToString() + " insideSectionTitle: " + haveTopicTitle.ToString());
                        Console.WriteLine("topicText:\t'" + topicText + "'");

                        if (doMakeKeywordPhrases && !String.IsNullOrEmpty(topicText))
                        {
                            List<String> response = new List<string>();
                            response.Add(topicText);
                            string request = AzureTextAnalytics.FormatRequestJSON(response);
                            AzureTextAnalytics.MakeKeyPhrasesRequest(request).Wait();
                        }
                        else
                        {
                            AzureTextAnalytics.Response = "NULL";
                        }
                        string keyphases = AzureTextAnalytics.Response;

                        if (String.IsNullOrEmpty(topicText)) topicText = "NULL";

                        _File.WriteLine(csvformat,
                            ++entityOrder, childPrincipleNumber, parentPrincipleNumber, entityType.ToString(), topicText, ContentTypes.BaseEntity_Principles_RayDalio_ModelMate.ToString(), childPrincipleNumber + ". " + topicText, keyphases);

                        parentPrincipleNumber = childPrincipleNumber;

                        _File.WriteLine(csvformat,
                            ++entityOrder, childPrincipleNumber, parentPrincipleNumber, entityType.ToString(), topicText, ContentTypes.TopicText_Principles_RayDalio_ModelMate.ToString(), topicText, keyphases); // link TopicText to BaseEntity

                        parentPrincipleNumber = childPrincipleNumber;
                    }
                    else if (docPart.ClassName.Contains("_number"))
                    {
                        childPrincipleNumber = Regex.Replace(FixEncoding(docPart.TextContent), @"\s+", " ").Replace("“", "&quote;").Replace("”", "&quote;").Replace("\"", "&quote;").Trim();
                        isAfterFirstPrincipleNumber = true;

                        switch (docPart.ClassName)
                        {
                            case "topic__number":
                                {
                                    throw new ArgumentOutOfRangeException("topic__number"); // processed above
                                    break;
                                }
                            case "principle__number":
                                {
                                    parentPrincipleNumber = topicNumber; // set above
                                    principleNumber = childPrincipleNumber;
                                    entityType = MapEnitityType(docPart.ClassName);
                                    break;
                                }
                            case "subprinciple__number":
                                {
                                    parentPrincipleNumber = principleNumber;
                                    subprincipleNumber = childPrincipleNumber;
                                    entityType = MapEnitityType(docPart.ClassName);
                                    break;
                                }
                            default:
                                {
                                    throw new NotImplementedException("s.ClassName: " + docPart.ClassName);
                                }
                        }

                        Console.WriteLine("principleNumber:\t'" + childPrincipleNumber + "' parent:" + parentPrincipleNumber + " entityType: " + entityType.ToString() + " insideSectionTitle: " + haveTopicTitle.ToString());
                        Console.WriteLine("childPrincipleNumber:\t'" + childPrincipleNumber + "'");
                    }
                    else if (docPart.ClassName.Contains("_title"))
                    {
                        principleTitle = Regex.Replace(FixEncoding(docPart.TextContent), @"\s+", " ").Replace("“", "&quote;").Replace("”", "&quote;").Replace("\"", "&quote;").Trim();
                        isAfterFirstPrincipleTitle = true;
                        corpus.Add(principleTitle);

                        Console.WriteLine("principleNumber:\t'" + childPrincipleNumber + "' parent:" + parentPrincipleNumber + " entityType: " + entityType.ToString() + " insideSectionTitle: " + haveTopicTitle.ToString());
                        Console.WriteLine("principleTitle:\t'" + principleTitle + "'");

                        if (doMakeKeywordPhrases && !String.IsNullOrEmpty(principleTitle))
                        {
                            List<String> response = new List<string>();
                            response.Add(principleTitle);
                            string request = AzureTextAnalytics.FormatRequestJSON(response);
                            AzureTextAnalytics.MakeKeyPhrasesRequest(request).Wait();
                        }
                        else
                        {
                            AzureTextAnalytics.Response = "NULL";
                        }
                        string keyphases = AzureTextAnalytics.Response;

                        if (String.IsNullOrEmpty(principleTitle)) principleTitle = "NULL";

                        _File.WriteLine(csvformat,
                            ++entityOrder, childPrincipleNumber, parentPrincipleNumber, entityType.ToString(), principleTitle, ContentTypes.BaseEntity_Principles_RayDalio_ModelMate.ToString(), childPrincipleNumber + ". " + principleTitle, keyphases);

                        parentPrincipleNumber = childPrincipleNumber;
                    }
                    else if (docPart.ClassName == "commentary" && isAfterFirstPrincipleNumber && isAfterFirstPrincipleTitle)
                    {
                        principleCommentaryText = Regex.Replace(FixEncoding(docPart.TextContent), @"\s+", " ").Replace("“", "&quote;").Replace("”", "&quote;").Replace("\"", "&quote;").Trim();
                        corpus.Add(principleCommentaryText);

                        Console.WriteLine("principleNumber:\t'" + childPrincipleNumber + "': '" + principleTitle + "' parent:" + parentPrincipleNumber + " entityType: " + entityType.ToString());
                        Console.WriteLine("principleCommentaryText:\t'" + principleCommentaryText + "'");

                        if (doMakeKeywordPhrases && !String.IsNullOrEmpty(principleCommentaryText))
                        {
                            List<String> response = new List<string>();
                            response.Add(principleCommentaryText);
                            string request = AzureTextAnalytics.FormatRequestJSON(response);
                            AzureTextAnalytics.MakeKeyPhrasesRequest(request).Wait();
                        }
                        else
                        {
                            AzureTextAnalytics.Response = "NULL";
                        }
                        string keyphases = AzureTextAnalytics.Response;

                        if (String.IsNullOrEmpty(principleCommentaryText)) principleCommentaryText = "NULL";

                        _File.WriteLine(csvformat,
                            ++entityOrder, childPrincipleNumber, parentPrincipleNumber, entityType.ToString(), principleTitle, ContentTypes.CommentaryText_Principles_RayDalio_ModelMate.ToString(), principleCommentaryText, keyphases);
                    }
                    else if (docPart.ClassName == "question" && isAfterFirstPrincipleNumber && isAfterFirstPrincipleTitle)
                    {
                        principleQuestionText = Regex.Replace(FixEncoding(docPart.TextContent), @"\s+", " ").Replace("“", "&quote;").Replace("”", "&quote;").Replace("\"", "&quote;").Trim();
                        corpus.Add(principleQuestionText);

                        Console.WriteLine("principleNumber:\t'" + childPrincipleNumber + "': '" + principleTitle + "' parent:" + parentPrincipleNumber + " entityType: " + entityType.ToString());
                        Console.WriteLine("principleQuestion:\t'" + principleQuestionText + "'");

                        if (doMakeKeywordPhrases && !String.IsNullOrEmpty(principleQuestionText))
                        {
                            List<String> response = new List<string>();
                            response.Add(principleQuestionText);
                            string request = AzureTextAnalytics.FormatRequestJSON(response);
                            AzureTextAnalytics.MakeKeyPhrasesRequest(request).Wait();
                        }
                        else
                        {
                            AzureTextAnalytics.Response = "NULL";
                        }
                        string keyphases = AzureTextAnalytics.Response;

                        if (String.IsNullOrEmpty(principleQuestionText)) principleQuestionText = "NULL";

                        _File.WriteLine(csvformat,
                            ++entityOrder, childPrincipleNumber, parentPrincipleNumber, entityType.ToString(), principleTitle, ContentTypes.QuestionText_Principles_RayDalio_ModelMate.ToString(), principleQuestionText, keyphases);
                    }
                    else if (docPart.ClassName == "letter" && isAfterFirstPrincipleNumber && isAfterFirstPrincipleTitle)
                    {
                        principleLetterText = Regex.Replace(FixEncoding(docPart.TextContent), @"\s+", " ").Replace("“", "&quote;").Replace("”", "&quote;").Replace("\"", "&quote;").Trim();
                        corpus.Add(principleLetterText);

                        Console.WriteLine("principleNumber:\t'" + childPrincipleNumber + "': '" + principleTitle + "' parent:" + parentPrincipleNumber + " entityType: " + entityType.ToString());
                        Console.WriteLine("principleLetterText:\t'" + principleLetterText + "'");

                        if (doMakeKeywordPhrases && !String.IsNullOrEmpty(principleLetterText))
                        {
                            List<String> response = new List<string>();
                            response.Add(principleLetterText);
                            string request = AzureTextAnalytics.FormatRequestJSON(response);
                            AzureTextAnalytics.MakeKeyPhrasesRequest(request).Wait();
                        }
                        else
                        {
                            AzureTextAnalytics.Response = "NULL";
                        }
                        string keyphases = AzureTextAnalytics.Response;

                        if (String.IsNullOrEmpty(principleLetterText)) principleLetterText = "NULL";

                        _File.WriteLine(csvformat,
                            ++entityOrder, childPrincipleNumber, parentPrincipleNumber, entityType.ToString(), principleTitle, ContentTypes.LetterText_Principles_RayDalio_ModelMate.ToString(), principleLetterText, keyphases);
                    }
                    else if (docPart.ClassName == "bullet" && isAfterFirstPrincipleNumber && isAfterFirstPrincipleTitle)
                    {
                        principleBulletText = Regex.Replace(FixEncoding(docPart.TextContent), @"\s+", " ").Replace("“", "&quote;").Replace("”", "&quote;").Replace("\"", "&quote;").Trim();
                        corpus.Add(principleBulletText);

                        Console.WriteLine("principleNumber:\t'" + childPrincipleNumber + "': '" + principleTitle + "' parent:" + parentPrincipleNumber + " entityType: " + entityType.ToString());
                        Console.WriteLine("principleBulletText:\t'" + principleBulletText + "'");

                        if (doMakeKeywordPhrases && !String.IsNullOrEmpty(principleBulletText))
                        {
                            List<String> response = new List<string>();
                            response.Add(principleBulletText);
                            string request = AzureTextAnalytics.FormatRequestJSON(response);
                            AzureTextAnalytics.MakeKeyPhrasesRequest(request).Wait();
                        }
                        else
                        {
                            AzureTextAnalytics.Response = "NULL";
                        }
                        string keyphases = AzureTextAnalytics.Response;

                        if (String.IsNullOrEmpty(principleBulletText)) principleBulletText = "NULL";

                        _File.WriteLine(csvformat,
                            ++entityOrder, childPrincipleNumber, parentPrincipleNumber, entityType.ToString(), principleTitle, ContentTypes.BulletText_Principles_RayDalio_ModelMate.ToString(), principleBulletText, keyphases);
                    }
                    else if (docPart.ClassName == "number" && isAfterFirstPrincipleNumber && isAfterFirstPrincipleTitle)
                    {
                        principleNumberText = Regex.Replace(FixEncoding(docPart.TextContent), @"\s+", " ").Replace("“", "&quote;").Replace("”", "&quote;").Replace("\"", "&quote;").Trim();
                        corpus.Add(principleNumberText);

                        Console.WriteLine("principleNumber:\t'" + childPrincipleNumber + "': '" + principleTitle + "' parent:" + parentPrincipleNumber + " entityType: " + entityType.ToString());
                        Console.WriteLine("principleNumberText:\t'" + principleNumberText + "'");

                        if (doMakeKeywordPhrases && !String.IsNullOrEmpty(principleNumberText))
                        {
                            List<String> response = new List<string>();
                            response.Add(principleNumberText);
                            string request = AzureTextAnalytics.FormatRequestJSON(response);
                            AzureTextAnalytics.MakeKeyPhrasesRequest(request).Wait();
                        }
                        else
                        {
                            AzureTextAnalytics.Response = "NULL";
                        }
                        string keyphases = AzureTextAnalytics.Response;

                        if (String.IsNullOrEmpty(principleNumberText)) principleNumberText = "NULL";

                        _File.WriteLine(csvformat,
                            ++entityOrder, childPrincipleNumber, parentPrincipleNumber, entityType.ToString(), principleTitle, ContentTypes.NumberText_Principles_RayDalio_ModelMate.ToString(), principleNumberText, keyphases);
                    }
                    else if (docPart.LocalName == "img" && isAfterFirstPrincipleNumber && isAfterFirstPrincipleTitle)
                    {
                        principleImageUrl = docPart.GetAttribute("src");
                        //corpus.Add(principleImageUrl);

                        Console.WriteLine("principleNumber:\t'" + childPrincipleNumber + "': '" + principleTitle + "' parent:" + parentPrincipleNumber + " entityType: " + entityType.ToString());
                        Console.WriteLine("principleImageUrl:\t'" + principleImageUrl + "'");

                        if (doMakeKeywordPhrases && !String.IsNullOrEmpty(principleImageUrl))
                        {
                            List<String> response = new List<string>();
                            response.Add(principleImageUrl);
                            string request = AzureTextAnalytics.FormatRequestJSON(response);
                            AzureTextAnalytics.MakeKeyPhrasesRequest(request).Wait();
                        }
                        else
                        {
                            AzureTextAnalytics.Response = "NULL";
                        }
                        string keyphases = AzureTextAnalytics.Response;

                        if (String.IsNullOrEmpty(principleImageUrl)) principleImageUrl = "NULL";

                        _File.WriteLine(csvformat,
                            ++entityOrder, childPrincipleNumber, parentPrincipleNumber, entityType.ToString(), principleTitle, ContentTypes.ImageUrl_Principles_RayDalio_ModelMate.ToString(), principleImageUrl, keyphases);
                    }
                    else if (docPart.LocalName == "figure" && isAfterFirstPrincipleNumber && isAfterFirstPrincipleTitle && !docPart.ClassName.Contains("image"))
                    {
                        principleFigureText = Regex.Replace(FixEncoding(docPart.TextContent), @"\s+", " ").Replace("“", "&quote;").Replace("”", "&quote;").Replace("\"", "&quote;").Trim();
                        corpus.Add(principleFigureText);

                        Console.WriteLine("principleNumber:\t'" + childPrincipleNumber + "': '" + principleTitle + "' parent:" + parentPrincipleNumber + " entityType: " + entityType.ToString());
                        Console.WriteLine("principleFigureText:\t'" + principleFigureText + "'");

                        if (doMakeKeywordPhrases && !String.IsNullOrEmpty(principleFigureText))
                        {
                            List<String> response = new List<string>();
                            response.Add(principleFigureText);
                            string request = AzureTextAnalytics.FormatRequestJSON(response);
                            AzureTextAnalytics.MakeKeyPhrasesRequest(request).Wait();
                        }
                        else
                        {
                            AzureTextAnalytics.Response = "NULL";
                        }
                        string keyphases = AzureTextAnalytics.Response;

                        if (String.IsNullOrEmpty(principleFigureText)) principleFigureText = "NULL";

                        _File.WriteLine(csvformat,
                            ++entityOrder, childPrincipleNumber, parentPrincipleNumber, entityType.ToString(), principleTitle, ContentTypes.FigureText_Principles_RayDalio_ModelMate.ToString(), principleFigureText, keyphases);
                    }

                    _File.Flush();
                }
            }

            string request2 = AzureTextAnalytics.FormatRequestJSON(corpus);
            AzureTextAnalytics.MakeTopicsRequest(request2).Wait();
            string response2 = AzureTextAnalytics.Response;
            string location2 = AzureTextAnalytics.Location;
            string[] parts = location2.Split('/');
            string operationId = parts[parts.Length - 1];
            string response3 = "\"Running\"";
            while (response3.Contains("\"Running\""))
            {
                Console.WriteLine("Waiting...");
                System.Threading.Thread.Sleep(60 * 1000);
                AzureTextAnalytics.GetTopicsResults(operationId).Wait();
                response3 = AzureTextAnalytics.Response;
                Console.WriteLine(response3);
            }

            using (System.IO.StreamWriter _File = new System.IO.StreamWriter(@"HTMLScanner-RayDalioPrinciples.json"))
            {
                _File.WriteLine(response3);
            }

            ////Serialize it back to the console
            //Console.WriteLine(document.DocumentElement.OuterHtml);

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }

        static async Task DownloadPageAsync()
        {
            // ... Use HttpClient.
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page, HttpCompletionOption.ResponseContentRead))
            using (HttpContent content = response.Content)
            {
                // ... Read the string.
                pagecontents = await content.ReadAsStringAsync();

                // ... Display the result.
                if (pagecontents != null && pagecontents.Length >= 1)
                {
                    Console.WriteLine(pagecontents.Length);
                    //Console.WriteLine("Display result...");
                    //Console.ReadLine();
                    //Console.WriteLine(result);
                    //Console.WriteLine("End of result...");
                    //Console.ReadLine();
                }
            }
        }

        // http://stackoverflow.com/questions/10888040/how-to-convert-%C3%A2%E2%82%AC-to-apostrophe-in-c e.g. â€™ as in Donâ€™t (Don't)
        static private string FixEncoding(string s)
        {
            //if (s.Contains("Don")) System.Diagnostics.Debugger.Break();

            return s; // fix was to change the file Encoding to UTF8

            //string text = "";

            //string hexString1 = "";
            //string hexString2 = "";

            //if (String.IsNullOrEmpty(s))
            //{
            //    text = "";
            //}
            //else
            //{
            //    var bytes = Encoding.Default.GetBytes(s);
            //    hexString1 = BitConverter.ToString(bytes);
            //    text = Encoding.UTF8.GetString(bytes);
            //    hexString2 = BitConverter.ToString(Encoding.Default.GetBytes(text));
            //}

            //if (s.Contains("Don")) System.Diagnostics.Debugger.Break();
            //if (s.Length != text.Length) System.Diagnostics.Debugger.Break();

            //return text;
        }

        static private EntityType MapEnitityType(string s)
        {
            EntityType entityType = EntityType.Unknown_Principles_RayDalio_ModelMate;

            s = s.Replace("__number", "");

            switch (s)
            {
                case "publication":
                    {
                        entityType = EntityType.Publication_Principles_RayDalio_ModelMate;
                        break;
                    }
                case "section":
                    {
                        entityType = EntityType.Section_Principles_RayDalio_ModelMate;
                        break;
                    }
                case "topic":
                    {
                        entityType = EntityType.Topic_Principles_RayDalio_ModelMate;
                        break;
                    }
                case "principle":
                    {
                        entityType = EntityType.Principle_Principles_RayDalio_ModelMate;
                        break;
                    }
                case "subprinciple":
                    {
                        entityType = EntityType.Subprinciple_Principles_RayDalio_ModelMate;
                        break;
                    }
                default:
                    {
                        entityType = EntityType.Unknown_Principles_RayDalio_ModelMate;
                        break;
                    }
            }

            return entityType;
        }
    }
}
