using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PSN.ModelMate.HTMLScanner
{
    public class AzureTextAnalytics
    {
        /// <summary> 
        /// Azure portal URL. 
        /// </summary> 
        private const string BaseUrl = "https://westus.api.cognitive.microsoft.com/";

        /// <summary> 
        /// Your account key goes here. 
        /// </summary> 
        private const string AccountKey = "4621fc6be17a4367b96831a570145fc9";

        private static string response = "";
        public static string Response
        {
            get
            {
                return response;
            }

            set
            {
                response = value;
            }
        }

        private static string location = "";
        public static string Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }


        static public string FormatRequestJSON(List<String> values)
        {
            int idCounter = 1;
            StringBuilder reqJSON = new StringBuilder();
            reqJSON.Append("{\"documents\":[");
            foreach (string item in values)
            {
                string item2 = item.Replace("\"", ""); // mwh
                reqJSON.Append("{\"id\":\"" + idCounter + "\",\"text\":\"" + item2 + "\"},"); // mwh
                idCounter++;
            }
            reqJSON.Append("]}");
            //tbRequest2.Text = reqJSON.ToString(); // mwh
            return reqJSON.ToString();
        }

        public static async Task MakeSentimentRequest(string request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                // Request headers. 
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AccountKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Request body. Insert your text data here in JSON format. 
                byte[] byteData = Encoding.UTF8.GetBytes(request);
                // Detect sentiment: 
                var uri = "text/analytics/v2.0/sentiment";
                response = await PostToEndpoint(client, uri, byteData);
                //tbResults2.Text = response; // mwh
            }
        }

        // Location = {https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/operations/0bbde8ca3b9c4d8ea1642a9be5d2e9e7}
        public static async Task MakeTopicsRequest(string request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                // Request headers. 
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AccountKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Request body. Insert your text data here in JSON format. 
                byte[] byteData = Encoding.UTF8.GetBytes(request);
                var uri = "text/analytics/v2.0/topics";
                response = await PostToEndpoint(client, uri, byteData);
                //tbResults2.Text = response; // mwh
            }
        }

        public static async Task GetTopicsResults(string operationId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                // Request headers. 
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AccountKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));  
                var uri = "text/analytics/v2.0/operations/" + operationId;
                response = await GetFromEndpoint(client, uri);
                //tbResults2.Text = response; // mwh
            }
        }

        public static async Task MakeKeyPhrasesRequest(string request)
        {
            response = "NULL";

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                // Request headers. 
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AccountKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Request body. Insert your text data here in JSON format. 
                byte[] byteData = Encoding.UTF8.GetBytes(request);
                var uri = "/text/analytics/v2.0/keyPhrases";
                response = await PostToEndpoint(client, uri, byteData);
                System.Threading.Thread.Sleep(1000); // KLUDGE

                //tbResults2.Text = response; // mwh
                response = response.Replace("{\"documents\":[{\"keyPhrases\":[", "").Replace("],\"id\":\"1\"}],\"errors\":[]}", "").Replace("\"", "").ToLower();

                string requestLowercase = request.ToLower();

                if (requestLowercase.Contains("outcomes")) response = response + ",outcomes**";
                if (requestLowercase.Contains("responsible party")) response = response + ",responsible party**";
                if (requestLowercase.Contains("thinkers")) response = response + ",thinkers**";
                if (requestLowercase.Contains("department")) response = response + ",department**";
                if (requestLowercase.Contains("type")) response = response + ",type**";
                if (requestLowercase.Contains("believable people")) response = response + ",believable people**";
                if (requestLowercase.Contains("principles")) response = response + ",principles**";
                if (requestLowercase.Contains("personal responsibility")) response = response + ",personal responsibility**";
                if (requestLowercase.Contains("step")) response = response + ",step**";
                if (requestLowercase.Contains("lack")) response = response + ",lack**";
                if (requestLowercase.Contains("brained thinkers")) response = response + ",brained thinkers**";
                if (requestLowercase.Contains("assign responsibilities")) response = response + ",assign responsibilities**";
                if (requestLowercase.Contains("failures")) response = response + ",failures**";
                if (requestLowercase.Contains("dots")) response = response + ",dots**";
                if (requestLowercase.Contains("disagreements")) response = response + ",disagreements**";
                if (requestLowercase.Contains("capacity")) response = response + ",capacity**";
                if (requestLowercase.Contains("performance")) response = response + ",performance**";
                if (requestLowercase.Contains("design")) response = response + ",design**";
                if (requestLowercase.Contains("opinions")) response = response + ",opinions**";
                if (requestLowercase.Contains("track records")) response = response + ",track records**";
                if (requestLowercase.Contains("relevant people")) response = response + ",relevant people**";
                if (requestLowercase.Contains("improvement")) response = response + ",improvement**";
                if (requestLowercase.Contains("level")) response = response + ",level**";
                if (requestLowercase.Contains("pictures")) response = response + ",pictures**";
                if (requestLowercase.Contains("feedback")) response = response + ",feedback**";
                if (requestLowercase.Contains("resources")) response = response + ",resources**";
                if (requestLowercase.Contains("minded people")) response = response + ",minded people**";
                if (requestLowercase.Contains("ratio")) response = response + ",ratio**";
                if (requestLowercase.Contains("assessments")) response = response + ",assessments**";
                if (requestLowercase.Contains("success")) response = response + ",success**";
                if (requestLowercase.Contains("specific people")) response = response + ",specific people**";
                if (requestLowercase.Contains("believable person")) response = response + ",believable person**";
                if (requestLowercase.Contains("track record")) response = response + ",track record**";
                if (requestLowercase.Contains("behavior")) response = response + ",behavior**";
                if (requestLowercase.Contains("produce outcomes")) response = response + ",produce outcomes**";
                if (requestLowercase.Contains("bad outcomes")) response = response + ",bad outcomes**";
                if (requestLowercase.Contains("probability")) response = response + ",probability**";
                if (requestLowercase.Contains("purpose")) response = response + ",purpose**";
                if (requestLowercase.Contains("line")) response = response + ",line**";
                if (requestLowercase.Contains("evolutionary process")) response = response + ",evolutionary process**";
                if (requestLowercase.Contains("hold people")) response = response + ",hold people**";
                if (requestLowercase.Contains("issue")) response = response + ",issue**";
                if (requestLowercase.Contains("hand")) response = response + ",hand**";
                if (requestLowercase.Contains("transparency")) response = response + ",transparency**";
                if (requestLowercase.Contains("left-brained thinkers")) response = response + ",left-brained thinkers**";
                if (requestLowercase.Contains("tool")) response = response + ",tool**";
                if (requestLowercase.Contains("marginal gains")) response = response + ",marginal gains**";
                if (requestLowercase.Contains("inadequate training")) response = response + ",inadequate training**";
                if (requestLowercase.Contains("issues")) response = response + ",issues**";
                if (requestLowercase.Contains("requirements")) response = response + ",requirements**";
                if (requestLowercase.Contains("accuracy")) response = response + ",accuracy**";
                if (requestLowercase.Contains("facilities")) response = response + ",facilities**";
                if (requestLowercase.Contains("head")) response = response + ",head**";
                if (requestLowercase.Contains("departments")) response = response + ",departments**";
                if (requestLowercase.Contains("instructions")) response = response + ",instructions**";
                if (requestLowercase.Contains("assessing people")) response = response + ",assessing people**";
                if (requestLowercase.Contains("ideas")) response = response + ",ideas**";
                if (requestLowercase.Contains("sets")) response = response + ",sets**";
                if (requestLowercase.Contains("inevitable responsible party")) response = response + ",inevitable responsible party**";
                if (requestLowercase.Contains("assigning personal responsibilities")) response = response + ",assigning personal responsibilities**";
                if (requestLowercase.Contains("individuals")) response = response + ",individuals**";
                if (requestLowercase.Contains("believable responsible parties")) response = response + ",believable responsible parties**";
                if (requestLowercase.Contains("experiences")) response = response + ",experiences**";
                if (requestLowercase.Contains("interviewers")) response = response + ",interviewers**";
                if (requestLowercase.Contains("personal evolution")) response = response + ",personal evolution**";
                if (requestLowercase.Contains("assessment")) response = response + ",assessment**";
                if (requestLowercase.Contains("hear people")) response = response + ",hear people**";
                if (requestLowercase.Contains("pattern")) response = response + ",pattern**";
                if (requestLowercase.Contains("harry")) response = response + ",harry**";
                if (requestLowercase.Contains("views of people")) response = response + ",views of people**";
                if (requestLowercase.Contains("relevant parties")) response = response + ",relevant parties**";
                if (requestLowercase.Contains("questioning")) response = response + ",questioning**";
                if (requestLowercase.Contains("rapid improvements")) response = response + ",rapid improvements**";
                if (requestLowercase.Contains("learning process")) response = response + ",learning process**";
                if (requestLowercase.Contains("lack of ability")) response = response + ",lack of ability**";
                if (requestLowercase.Contains("records")) response = response + ",records**";
                if (requestLowercase.Contains("issues log")) response = response + ",issues log**";
                if (requestLowercase.Contains("tools")) response = response + ",tools**";
                if (requestLowercase.Contains("comment")) response = response + ",comment**";
                if (requestLowercase.Contains("sample size")) response = response + ",sample size**";
                if (requestLowercase.Contains("boiling water")) response = response + ",boiling water**";
                if (requestLowercase.Contains("service clients")) response = response + ",service clients**";
                if (requestLowercase.Contains("tech resources")) response = response + ",tech resources**";
                if (requestLowercase.Contains("log")) response = response + ",log**";
                if (requestLowercase.Contains("closed-minded people")) response = response + ",closed-minded people**";
                if (requestLowercase.Contains("capabilities")) response = response + ",capabilities**";
                if (requestLowercase.Contains("support")) response = response + ",support**";
                if (requestLowercase.Contains("specifics")) response = response + ",specifics**";
                if (requestLowercase.Contains("decision-maker")) response = response + ",decision-maker**";
                if (requestLowercase.Contains("context")) response = response + ",context**";
                if (requestLowercase.Contains("vision")) response = response + ",vision**";
                if (requestLowercase.Contains("reporting")) response = response + ",reporting**";
                if (requestLowercase.Contains("event")) response = response + ",event**";
                if (requestLowercase.Contains("crisis")) response = response + ",crisis**";
                if (requestLowercase.Contains("lots")) response = response + ",lots**";
                if (requestLowercase.Contains("procedures manual")) response = response + ",procedures manual**";
                if (requestLowercase.Contains("smart people")) response = response + ",smart people**";
                if (requestLowercase.Contains("cohesion")) response = response + ",cohesion**";
                if (requestLowercase.Contains("rely on learning")) response = response + ",rely on learning**";
                if (requestLowercase.Contains("memory bank")) response = response + ",memory bank**";
                if (requestLowercase.Contains("learners")) response = response + ",learners**";
                if (requestLowercase.Contains("daily updates")) response = response + ",daily updates**";
                if (requestLowercase.Contains("surveys")) response = response + ",surveys**";
                if (requestLowercase.Contains("professor sperry")) response = response + ",professor sperry**";
                if (requestLowercase.Contains("benefits")) response = response + ",benefits**";
                if (requestLowercase.Contains("hire people")) response = response + ",hire people**";
                if (requestLowercase.Contains("idea of people")) response = response + ",idea of people**";
                if (requestLowercase.Contains("requires for people")) response = response + ",requires for people**";
                if (requestLowercase.Contains("assess people")) response = response + ",assess people**";
                if (requestLowercase.Contains("setting goals")) response = response + ",setting goals**";
                if (requestLowercase.Contains("delegating responsibilities")) response = response + ",delegating responsibilities**";
                if (requestLowercase.Contains("responsibility of determining")) response = response + ",responsibility of determining**";
                if (requestLowercase.Contains("determine success in school")) response = response + ",determine success in school**";
                if (requestLowercase.Contains("school performance")) response = response + ",school performance**";
                if (requestLowercase.Contains("assigned tasks")) response = response + ",assigned tasks**";
                if (requestLowercase.Contains("responsibility for achieving goals")) response = response + ",responsibility for achieving goals**";
                if (requestLowercase.Contains("achieving individual tasks")) response = response + ",achieving individual tasks**";
                if (requestLowercase.Contains("plan design")) response = response + ",plan design**";
                if (requestLowercase.Contains("believable interviewers")) response = response + ",believable interviewers**";
                if (requestLowercase.Contains("believable person with experience")) response = response + ",believable person with experience**";
                if (requestLowercase.Contains("confident opinion")) response = response + ",confident opinion**";
                if (requestLowercase.Contains("step failure")) response = response + ",step failure**";
                if (requestLowercase.Contains("identifying failure")) response = response + ",identifying failure**";
                if (requestLowercase.Contains("Identify root")) response = response + ",Identify root**";
                if (requestLowercase.Contains("Step Process")) response = response + ",Step Process**";
                if (requestLowercase.Contains("process of personal evolution")) response = response + ",process of personal evolution**";
                if (requestLowercase.Contains("quality discussion")) response = response + ",quality discussion**";
                if (requestLowercase.Contains("hear discussions")) response = response + ",hear discussions**";
                if (requestLowercase.Contains("pattern of behavior")) response = response + ",pattern of behavior**";
                if (requestLowercase.Contains("baseball cards")) response = response + ",baseball cards**";
                if (requestLowercase.Contains("individual")) response = response + ",individual**";
                if (requestLowercase.Contains("reasons managers")) response = response + ",reasons managers**";
                if (requestLowercase.Contains("type of outcome")) response = response + ",type of outcome**";
                if (requestLowercase.Contains("understand Harry")) response = response + ",understand Harry**";
                if (requestLowercase.Contains("synthesized dots")) response = response + ",synthesized dots**";
                if (requestLowercase.Contains("playing probabilities")) response = response + ",playing probabilities**";
                if (requestLowercase.Contains("answered questions")) response = response + ",answered questions**";
                if (requestLowercase.Contains("school learning")) response = response + ",school learning**";
                if (requestLowercase.Contains("purpose of understanding")) response = response + ",purpose of understanding**";
                if (requestLowercase.Contains("responsible parties")) response = response + ",responsible parties**";
                if (requestLowercase.Contains("job design")) response = response + ",job design**";
                if (requestLowercase.Contains("encourage disagreement")) response = response + ",encourage disagreement**";
                if (requestLowercase.Contains("differences in people")) response = response + ",differences in people**";
                if (requestLowercase.Contains("line discussion")) response = response + ",line discussion**";
                if (requestLowercase.Contains("train people")) response = response + ",train people**";
                if (requestLowercase.Contains("principles for handling")) response = response + ",principles for handling**";
                if (requestLowercase.Contains("lack of training")) response = response + ",lack of training**";
                if (requestLowercase.Contains("type of issue")) response = response + ",type of issue**";
                if (requestLowercase.Contains("hand level")) response = response + ",hand level**";
                if (requestLowercase.Contains("machine level")) response = response + ",machine level**";
                if (requestLowercase.Contains("enhance transparency")) response = response + ",enhance transparency**";
                if (requestLowercase.Contains("picture thinkers")) response = response + ",picture thinkers**";
                if (requestLowercase.Contains("type people")) response = response + ",type people**";
                if (requestLowercase.Contains("valuable comments")) response = response + ",valuable comments**";
                if (requestLowercase.Contains("valuable tool")) response = response + ",valuable tool**";
                if (requestLowercase.Contains("quality person")) response = response + ",quality person**";
                if (requestLowercase.Contains("learned tools")) response = response + ",learned tools**";
                if (requestLowercase.Contains("discussion of differences")) response = response + ",discussion of differences**";
                if (requestLowercase.Contains("constant feedback")) response = response + ",constant feedback**";
                if (requestLowercase.Contains("inadequate ability")) response = response + ",inadequate ability**";
                if (requestLowercase.Contains("inadequate learning")) response = response + ",inadequate learning**";
                if (requestLowercase.Contains("building departments")) response = response + ",building departments**";
                if (requestLowercase.Contains("separate department")) response = response + ",separate department**";
                if (requestLowercase.Contains("technology department")) response = response + ",technology department**";
                if (requestLowercase.Contains("client service department")) response = response + ",client service department**";
                if (requestLowercase.Contains("lack of capacity")) response = response + ",lack of capacity**";
                if (requestLowercase.Contains("building technology")) response = response + ",building technology**";
                if (requestLowercase.Contains("tech people")) response = response + ",tech people**";
                if (requestLowercase.Contains("issues logs")) response = response + ",issues logs**";
                if (requestLowercase.Contains("specific mistakes")) response = response + ",specific mistakes**";
                if (requestLowercase.Contains("conceptual people")) response = response + ",conceptual people**";
                if (requestLowercase.Contains("conceptual decision")) response = response + ",conceptual decision**";
                if (requestLowercase.Contains("capability issue")) response = response + ",capability issue**";
                if (requestLowercase.Contains("job requirement")) response = response + ",job requirement**";
                if (requestLowercase.Contains("capacity issue")) response = response + ",capacity issue**";
                if (requestLowercase.Contains("ratio of managers")) response = response + ",ratio of managers**";
                if (requestLowercase.Contains("accuracy of people")) response = response + ",accuracy of people**";
                if (requestLowercase.Contains("technology people")) response = response + ",technology people**";
                if (requestLowercase.Contains("facilities people")) response = response + ",facilities people**";
                if (requestLowercase.Contains("head of technology")) response = response + ",head of technology**";
                if (requestLowercase.Contains("technology manager")) response = response + ",technology manager**";
                if (requestLowercase.Contains("support department")) response = response + ",support department**";
                if (requestLowercase.Contains("broader context")) response = response + ",broader context**";
                if (requestLowercase.Contains("broader vision")) response = response + ",broader vision**";
                if (requestLowercase.Contains("reporting lines")) response = response + ",reporting lines**";
                if (requestLowercase.Contains("department lines")) response = response + ",department lines**";
                if (requestLowercase.Contains("lot of time")) response = response + ",lot of time**";
                if (requestLowercase.Contains("event of crises")) response = response + ",event of crises**";
                if (requestLowercase.Contains("organizational cohesion")) response = response + ",organizational cohesion**";
                if (requestLowercase.Contains("rely on memory-based learning")) response = response + ",rely on memory-based learning**";
                if (requestLowercase.Contains("memory-based learners")) response = response + ",memory-based learners**";
                if (requestLowercase.Contains("reasoning-based thinkers")) response = response + ",reasoning-based thinkers**";
                if (requestLowercase.Contains("rely on reasoning")) response = response + ",rely on reasoning**";
                if (requestLowercase.Contains("stored instructions")) response = response + ",stored instructions**";
                if (requestLowercase.Contains("performance surveys")) response = response + ",performance surveys**";
                if (requestLowercase.Contains("expense of time")) response = response + ",expense of time**";
                if (requestLowercase.Contains("incremental benefits")) response = response + ",incremental benefits**";

                if (requestLowercase.Contains("bridgewater") && !response.Contains("bridgewater")) response = response + ",bridgewater***";

                if (requestLowercase.Contains("radically") && !response.Contains("radically")) response = response + ",radically*";
                else if (requestLowercase.Contains("radical") && !response.Contains("radical")) response = response + ",radical*";
                if (requestLowercase.Contains("machine") && !response.Contains("machine")) response = response + ",machine*";
                if (requestLowercase.Contains("economic") && !response.Contains("economic")) response = response + ",economic*";
                if (requestLowercase.Contains("people") && !response.Contains("people")) response = response + ",people*";
                if (requestLowercase.Contains("mistake") && !response.Contains("mistake")) response = response + ",mistake*";
                if (requestLowercase.Contains("transparency") && !response.Contains("transparency")) response = response + ",transparency*";
                if (requestLowercase.Contains("transparent") && !response.Contains("transparent")) response = response + ",transparent*";
                if (requestLowercase.Contains("principles") && !response.Contains("principles")) response = response + ",principles*";
                else if (requestLowercase.Contains("principle") && !response.Contains("principle")) response = response + ",principle*";
                if (requestLowercase.Contains("truthful") && !response.Contains("truthful")) response = response + ",truthful*";
                else if (requestLowercase.Contains("truth") && !response.Contains("truth")) response = response + ",truth*";
                if (requestLowercase.Contains("trust") && !response.Contains("trust")) response = response + ",trust*";
                if (requestLowercase.Contains("fear") && !response.Contains("fear")) response = response + ",fear*";
                if (requestLowercase.Contains("integrity") && !response.Contains("integrity")) response = response + ",integrity*";
                if (requestLowercase.Contains("reality") && !response.Contains("reality")) response = response + ",reality*";
                if (requestLowercase.Contains("success") && !response.Contains("success")) response = response + ",success*";
                if (requestLowercase.Contains("values") && !response.Contains("values")) response = response + ",values*";
                else if (requestLowercase.Contains("value") && !response.Contains("value")) response = response + ",value*";
                if (requestLowercase.Contains("standards") && !response.Contains("standards")) response = response + ",standards*";
                else if (requestLowercase.Contains("standard") && !response.Contains("standard")) response = response + ",standard*";
                if (requestLowercase.Contains("decide") && !response.Contains("decide")) response = response + ",decide*";
                if (requestLowercase.Contains("deciding") && !response.Contains("deciding")) response = response + ",deciding*";
                if (requestLowercase.Contains("decisions") && !response.Contains("decisions")) response = response + ",decisions*";
                else if (requestLowercase.Contains("decision") && !response.Contains("decision")) response = response + ",decision*";
                if (requestLowercase.Contains("understanding") && !response.Contains("understanding")) response = response + ",understanding*";
                else if (requestLowercase.Contains("understand") && !response.Contains("understand")) response = response + ",understand*";
                if (requestLowercase.Contains("understood") && !response.Contains("understood")) response = response + ",understood*";
                if (requestLowercase.Contains("visualize") && !response.Contains("visualize")) response = response + ",visualize*";
                if (requestLowercase.Contains("assess") && !response.Contains("assess")) response = response + ",assess*";
                if (requestLowercase.Contains("risk") && !response.Contains("risk")) response = response + ",risk*";
                if (requestLowercase.Contains("reward") && !response.Contains("reward")) response = response + ",reward*";
                if (requestLowercase.Contains("connected") && !response.Contains("connected")) response = response + ",connected*";
                if (requestLowercase.Contains("fundamental") && !response.Contains("fundamental")) response = response + ",fundamental*";
                if (requestLowercase.Contains("believe") && !response.Contains("believe")) response = response + ",believe*";
                if (requestLowercase.Contains("beliefs") && !response.Contains("beliefs")) response = response + ",beliefs*";
                else if (requestLowercase.Contains("belief") && !response.Contains("belief")) response = response + ",belief*";
                if (requestLowercase.Contains("management principles") && !response.Contains("management principles")) response = response + ",management principles*";
                else if (requestLowercase.Contains("management principle") && !response.Contains("management principle")) response = response + ",management principle*";
                else if (requestLowercase.Contains("management") && !response.Contains("management")) response = response + ",management*";
                if (requestLowercase.Contains("honestly") && !response.Contains("honestly")) response = response + ",honestly*";
                if (requestLowercase.Contains("honesty") && !response.Contains("honesty")) response = response + ",honesty*";
                if (requestLowercase.Contains("greatest") && !response.Contains("greatest")) response = response + ",greatest*";
                else if (requestLowercase.Contains("great") && !response.Contains("great")) response = response + ",great*";
                if (requestLowercase.Contains("honesty") && !response.Contains("honesty")) response = response + ",honesty*";
                if (requestLowercase.Contains("impediments") && !response.Contains("impediments")) response = response + ",impediments*";
                else if (requestLowercase.Contains("impediment") && !response.Contains("impediment")) response = response + ",impediment*";
                if (requestLowercase.Contains("game") && !response.Contains("game")) response = response + ",game*";
                else if (requestLowercase.Contains("gaming") && !response.Contains("gaming")) response = response + ",gaming*";
                if (requestLowercase.Contains("winning") && !response.Contains("winning")) response = response + ",winning*";
                else if (requestLowercase.Contains("win") && !response.Contains("win")) response = response + ",win*";
                if (requestLowercase.Contains("laws") && !response.Contains("laws")) response = response + ",laws*";
                if (requestLowercase.Contains("over-arching") && !response.Contains("over-arching")) response = response + ",over-arching*";
                if (requestLowercase.Contains("overarching") && !response.Contains("overarching")) response = response + ",overarching*";
                if (requestLowercase.Contains("talent") && !response.Contains("talent")) response = response + ",talent*";
                if (requestLowercase.Contains("problems") && !response.Contains("problems")) response = response + ",problems*";
                else if (requestLowercase.Contains("problem") && !response.Contains("problem")) response = response + ",problem*";
                if (requestLowercase.Contains("important") && !response.Contains("important")) response = response + ",important*";
                if (requestLowercase.Contains("money") && !response.Contains("money")) response = response + ",money*";
                if (requestLowercase.Contains("lucky") && !response.Contains("lucky")) response = response + ",lucky*";
                else if (requestLowercase.Contains("luck") && !response.Contains("luck")) response = response + ",luck*";
                if (requestLowercase.Contains("stress") && !response.Contains("stress")) response = response + ",stress*";
                if (requestLowercase.Contains("pressure") && !response.Contains("pressure")) response = response + ",pressure*";
                if (requestLowercase.Contains("condident") && !response.Contains("confident")) response = response + ",confident*";
                if (requestLowercase.Contains("confidence") && !response.Contains("confidence")) response = response + ",confidence*";
                if (requestLowercase.Contains("opinions") && !response.Contains("opinions")) response = response + ",opinions*";
                else if (requestLowercase.Contains("opinion") && !response.Contains("opinion")) response = response + ",opinion*";
                if (requestLowercase.Contains("independence") && !response.Contains("independence")) response = response + ",independence*";
                if (requestLowercase.Contains("independent") && !response.Contains("independent")) response = response + ",independent*";
                if (requestLowercase.Contains("better") && !response.Contains("better")) response = response + ",better*";
                if (requestLowercase.Contains("improvements") && !response.Contains("improvements")) response = response + ",improvements*";
                else if (requestLowercase.Contains("improvement") && !response.Contains("improvement")) response = response + ",improvement*";
                else if (requestLowercase.Contains("improve") && !response.Contains("improve")) response = response + ",improve*";
                if (requestLowercase.Contains("best") && !response.Contains("best")) response = response + ",best*";
                if (requestLowercase.Contains("conflict") && !response.Contains("conflict")) response = response + ",conflict*";
                if (requestLowercase.Contains("progress") && !response.Contains("progress")) response = response + ",progress*";
                if (requestLowercase.Contains("confusion") && !response.Contains("confusion")) response = response + ",confusion*";
                if (requestLowercase.Contains("judge") && !response.Contains("judge")) response = response + ",judge*";
                if (requestLowercase.Contains("judging") && !response.Contains("judging")) response = response + ",judging*";
                if (requestLowercase.Contains("feedback") && !response.Contains("feedback")) response = response + ",feedback*";
                if (requestLowercase.Contains("tests") && !response.Contains("tests")) response = response + ",tests*";
                if (requestLowercase.Contains("quiz") && !response.Contains("quiz")) response = response + ",quiz*";
                if (requestLowercase.Contains("learning") && !response.Contains("learning")) response = response + ",learning*";
                else if (requestLowercase.Contains("learn") && !response.Contains("learn")) response = response + ",learn*";
                if (requestLowercase.Contains("ability") && !response.Contains("ability")) response = response + ",ability*";
                if (requestLowercase.Contains("abilities") && !response.Contains("abilities")) response = response + ",abilities*";
                if (requestLowercase.Contains("root cause") && !response.Contains("root cause")) response = response + ",root cause*";
                if (requestLowercase.Contains("generalizations") && !response.Contains("generalizations")) response = response + ",generalizations*";
                else if (requestLowercase.Contains("generalization") && !response.Contains("generalization")) response = response + ",generalization*";
                if (requestLowercase.Contains("unacceptable") && !response.Contains("unacceptable")) response = response + ",unacceptable*";
                else if (requestLowercase.Contains("acceptable") && !response.Contains("acceptable")) response = response + ",acceptable*";
                if (requestLowercase.Contains("behavior") && !response.Contains("behavior")) response = response + ",behavior*";
                if (requestLowercase.Contains("skills") && !response.Contains("skills")) response = response + ",skills*";
                else if (requestLowercase.Contains("skill") && !response.Contains("skill")) response = response + ",skill*";

                if (requestLowercase.Contains("accuracy") && !response.Contains("accuracy")) response = response + ",accuracy*";
                if (requestLowercase.Contains("accuracy of people") && !response.Contains("accuracy of people")) response = response + ",accuracy of people*";
                if (requestLowercase.Contains("accurate criticisms") && !response.Contains("accurate criticisms")) response = response + ",accurate criticisms*";
                if (requestLowercase.Contains("achieving clarity") && !response.Contains("achieving clarity")) response = response + ",achieving clarity*";
                if (requestLowercase.Contains("adding people") && !response.Contains("adding people")) response = response + ",adding people*";
                if (requestLowercase.Contains("agreed tasks") && !response.Contains("agreed tasks")) response = response + ",agreed tasks*";
                else if (requestLowercase.Contains("agree") && !response.Contains("agree")) response = response + ",agree*";
                if (requestLowercase.Contains("agreements") && !response.Contains("agreements")) response = response + ",agreements*";
                else if (requestLowercase.Contains("agreement") && !response.Contains("agreement")) response = response + ",agreement*";
                if (requestLowercase.Contains("answered questions") && !response.Contains("answered questions")) response = response + ",answered questions*";
                if (requestLowercase.Contains("assess people") && !response.Contains("assess people")) response = response + ",assess people*";
                if (requestLowercase.Contains("assign responsibilities") && !response.Contains("assign responsibilities")) response = response + ",assign responsibilities*";
                if (requestLowercase.Contains("assigning personal responsibilities") && !response.Contains("assigning personal responsibilities")) response = response + ",assigning personal responsibilities*";
                if (requestLowercase.Contains("bad outcomes") && !response.Contains("bad outcomes")) response = response + ",bad outcomes*";
                if (requestLowercase.Contains("behaviors") && !response.Contains("behaviors")) response = response + ",behaviors*";
                else if (requestLowercase.Contains("behavior") && !response.Contains("behavior")) response = response + ",behavior*";
                if (requestLowercase.Contains("believable interviewers") && !response.Contains("believable interviewers")) response = response + ",believable interviewers*";
                if (requestLowercase.Contains("believable people") && !response.Contains("believable people")) response = response + ",believable people*";
                if (requestLowercase.Contains("boiling water") && !response.Contains("boiling water")) response = response + ",boiling water*";
                else if (requestLowercase.Contains("boil") && !response.Contains("boil")) response = response + ",boil*";
                if (requestLowercase.Contains("brained abilities") && !response.Contains("brained abilities")) response = response + ",brained abilities*";
                if (requestLowercase.Contains("brained thinkers") && !response.Contains("brained thinkers")) response = response + ",brained thinkers*";
                if (requestLowercase.Contains("building departments") && !response.Contains("building departments")) response = response + ",building departments*";
                if (requestLowercase.Contains("building technology") && !response.Contains("building technology")) response = response + ",building technology*";
                if (requestLowercase.Contains("capacity") && !response.Contains("capacity")) response = response + ",capacity*";
                if (requestLowercase.Contains("capacity issue") && !response.Contains("capacity issue")) response = response + ",capacity issue*";
                if (requestLowercase.Contains("career path") && !response.Contains("career path")) response = response + ",career path*";
                if (requestLowercase.Contains("categorize outcomes") && !response.Contains("categorize outcomes")) response = response + ",categorize outcomes*";
                if (requestLowercase.Contains("clarity") && !response.Contains("clarity")) response = response + ",clarity*";
                if (requestLowercase.Contains("client service department") && !response.Contains("client service department")) response = response + ",client service department*";
                if (requestLowercase.Contains("common for people") && !response.Contains("common for people")) response = response + ",common for people*";
                if (requestLowercase.Contains("common mistake") && !response.Contains("common mistake")) response = response + ",common mistake*";
                if (requestLowercase.Contains("common root") && !response.Contains("common root")) response = response + ",common root*";
                if (requestLowercase.Contains("conceptual decision-maker") && !response.Contains("conceptual decision-maker")) response = response + ",conceptual decision-maker*";
                if (requestLowercase.Contains("connecting specific mistakes") && !response.Contains("connecting specific mistakes")) response = response + ",connecting specific mistakes*";
                if (requestLowercase.Contains("critical opinion") && !response.Contains("critical opinion")) response = response + ",critical opinion*";
                if (requestLowercase.Contains("criticisms") && !response.Contains("criticisms")) response = response + ",criticisms*";
                else if (requestLowercase.Contains("criticism") && !response.Contains("criticism")) response = response + ",criticism*";
                if (requestLowercase.Contains("decision-maker") && !response.Contains("decision-maker")) response = response + ",decision-maker*";
                if (requestLowercase.Contains("delegating responsibilities") && !response.Contains("delegating responsibilities")) response = response + ",delegating responsibilities*";
                if (requestLowercase.Contains("departments") && !response.Contains("departments")) response = response + ",departments*";
                else if (requestLowercase.Contains("department") && !response.Contains("department")) response = response + ",department*";
                if (requestLowercase.Contains("department lines") && !response.Contains("department lines")) response = response + ",department lines*";
                if (requestLowercase.Contains("desired behavior") && !response.Contains("desired behavior")) response = response + ",desired behavior*";
                if (requestLowercase.Contains("differences") && !response.Contains("differences")) response = response + ",differences*";
                else if (requestLowercase.Contains("difference") && !response.Contains("difference")) response = response + ",differences*";
                if (requestLowercase.Contains("differences in innate abilities") && !response.Contains("differences in innate abilities")) response = response + ",differences in innate abilities*";
                if (requestLowercase.Contains("difficult for people") && !response.Contains("difficult for people")) response = response + ",difficult for people*";
                if (requestLowercase.Contains("disagreements") && !response.Contains("disagreements")) response = response + ",disagreements*";
                else if (requestLowercase.Contains("disagreement") && !response.Contains("disagreement")) response = response + ",disagreement*";
                if (requestLowercase.Contains("discussions") && !response.Contains("discussions")) response = response + ",discussions*";
                else if (requestLowercase.Contains("discussion") && !response.Contains("discussion")) response = response + ",discussion*";
                if (requestLowercase.Contains("discussion of differences") && !response.Contains("discussion of differences")) response = response + ",discussion of differences*";
                if (requestLowercase.Contains("distinct goals") && !response.Contains("distinct goals")) response = response + ",distinct goals*";
                if (requestLowercase.Contains("encourage disagreement") && !response.Contains("encourage disagreement")) response = response + ",encourage disagreement*";
                if (requestLowercase.Contains("expense of time") && !response.Contains("expense of time")) response = response + ",expense of time*";
                if (requestLowercase.Contains("experiences") && !response.Contains("experiences")) response = response + ",experiences*";
                else if (requestLowercase.Contains("experience") && !response.Contains("experience")) response = response + ",experience*";
                if (requestLowercase.Contains("experiences of training") && !response.Contains("experiences of training")) response = response + ",experiences of training*";
                if (requestLowercase.Contains("facilities") && !response.Contains("facilities")) response = response + ",facilities*";
                if (requestLowercase.Contains("facilities people") && !response.Contains("facilities people")) response = response + ",facilities people*";
                if (requestLowercase.Contains("future") && !response.Contains("future")) response = response + ",future*";
                if (requestLowercase.Contains("future design") && !response.Contains("future design")) response = response + ",future design*";
                if (requestLowercase.Contains("hand") && !response.Contains("hand")) response = response + ",hand*";
                if (requestLowercase.Contains("hand level") && !response.Contains("hand level")) response = response + ",hand level*";
                if (requestLowercase.Contains("harry") && !response.Contains("harry")) response = response + ",harry*";
                if (requestLowercase.Contains("head of technology") && !response.Contains("head of technology")) response = response + ",head of technology*";
                if (requestLowercase.Contains("heads") && !response.Contains("heads")) response = response + ",heads*";
                if (requestLowercase.Contains("hire managers") && !response.Contains("hire managers")) response = response + ",hire managers*";
                if (requestLowercase.Contains("hire people") && !response.Contains("hire people")) response = response + ",hire people*";
                if (requestLowercase.Contains("hiring") && !response.Contains("hiring")) response = response + ",hiring*";
                if (requestLowercase.Contains("hold people") && !response.Contains("hold people")) response = response + ",hold people*";
                if (requestLowercase.Contains("identify root") && !response.Contains("identify root")) response = response + ",identify root*";
                if (requestLowercase.Contains("identifying failure") && !response.Contains("identifying failure")) response = response + ",identifying failure*";
                if (requestLowercase.Contains("improves with experience") && !response.Contains("improves with experience")) response = response + ",improves with experience*";
                if (requestLowercase.Contains("inadequate ability") && !response.Contains("inadequate ability")) response = response + ",inadequate ability*";
                if (requestLowercase.Contains("inadequate learning") && !response.Contains("inadequate learning")) response = response + ",inadequate learning*";
                if (requestLowercase.Contains("inadequate training") && !response.Contains("inadequate training")) response = response + ",inadequate training*";
                if (requestLowercase.Contains("incremental gains") && !response.Contains("incremental gains")) response = response + ",incremental gains*";
                if (requestLowercase.Contains("inevitable responsible party") && !response.Contains("inevitable responsible party")) response = response + ",inevitable responsible party*";
                if (requestLowercase.Contains("intelligent people") && !response.Contains("intelligent people")) response = response + ",intelligent people*";
                if (requestLowercase.Contains("interviewers") && !response.Contains("interviewers")) response = response + ",interviewers*";
                if (requestLowercase.Contains("issues logs") && !response.Contains("issues logs")) response = response + ",issues logs*";
                else if (requestLowercase.Contains("issues log") && !response.Contains("issues log")) response = response + ",issues log*";
                if (requestLowercase.Contains("iterative process") && !response.Contains("iterative process")) response = response + ",iterative process*";
                if (requestLowercase.Contains("job slip") && !response.Contains("job slip")) response = response + ",job slip*";
                if (requestLowercase.Contains("lack") && !response.Contains("lack")) response = response + ",lack*";
                if (requestLowercase.Contains("lack of ability") && !response.Contains("lack of ability")) response = response + ",lack of ability*";
                if (requestLowercase.Contains("lack of capacity") && !response.Contains("lack of capacity")) response = response + ",lack of capacity*";
                if (requestLowercase.Contains("learners") && !response.Contains("learners")) response = response + ",learners*";
                if (requestLowercase.Contains("left-brained thinkers") && !response.Contains("left-brained thinkers")) response = response + ",left-brained thinkers*";
                if (requestLowercase.Contains("levels") && !response.Contains("levels")) response = response + ",levels*";
                else if (requestLowercase.Contains("level") && !response.Contains("level")) response = response + ",level*";
                if (requestLowercase.Contains("level discussion") && !response.Contains("level discussion")) response = response + ",level discussion*";
                if (requestLowercase.Contains("levels of discussion") && !response.Contains("levels of discussion")) response = response + ",levels of discussion*";
                if (requestLowercase.Contains("levels of understanding") && !response.Contains("levels of understanding")) response = response + ",levels of understanding*";
                if (requestLowercase.Contains("line") && !response.Contains("line")) response = response + ",line*";
                if (requestLowercase.Contains("line discussion") && !response.Contains("line discussion")) response = response + ",line discussion*";
                if (requestLowercase.Contains("log") && !response.Contains("log")) response = response + ",log*";
                if (requestLowercase.Contains("machine-level discussion") && !response.Contains("machine-level discussion")) response = response + ",machine-level discussion*";
                if (requestLowercase.Contains("manifestations of root") && !response.Contains("manifestations of root")) response = response + ",manifestations of root*";
                else if (requestLowercase.Contains("manifestations") && !response.Contains("manifestations")) response = response + ",manifestations*";
                if (requestLowercase.Contains("marginal gains") && !response.Contains("marginal gains")) response = response + ",marginal gains*";
                if (requestLowercase.Contains("memory-based learners") && !response.Contains("memory-based learners")) response = response + ",memory-based learners*";
                if (requestLowercase.Contains("minded people") && !response.Contains("minded people")) response = response + ",minded people*";
                if (requestLowercase.Contains("minor issues") && !response.Contains("minor issues")) response = response + ",minor issues*";
                if (requestLowercase.Contains("mutual understanding") && !response.Contains("mutual understanding")) response = response + ",mutual understanding*";
                if (requestLowercase.Contains("opinion") && !response.Contains("opinion")) response = response + ",opinion*";
                if (requestLowercase.Contains("outcome") && !response.Contains("outcome")) response = response + ",outcome*";
                if (requestLowercase.Contains("people in jobs") && !response.Contains("people in jobs")) response = response + ",people in jobs*";
                if (requestLowercase.Contains("people with responsibilities") && !response.Contains("people with responsibilities")) response = response + ",people with responsibilities*";
                if (requestLowercase.Contains("personal evolution") && !response.Contains("personal evolution")) response = response + ",personal evolution*";
                if (requestLowercase.Contains("personal responsibility") && !response.Contains("personal responsibility")) response = response + ",personal responsibility*";
                if (requestLowercase.Contains("picture thinkers") && !response.Contains("picture thinkers")) response = response + ",picture thinkers*";
                if (requestLowercase.Contains("pictures") && !response.Contains("pictures")) response = response + ",pictures*";
                if (requestLowercase.Contains("plan design") && !response.Contains("plan design")) response = response + ",plan design*";
                else if (requestLowercase.Contains("planned solution") && !response.Contains("planned solution")) response = response + ",planned solution*";
                else if (requestLowercase.Contains("plan") && !response.Contains("plan")) response = response + ",plan*";
                if (requestLowercase.Contains("principles for handling") && !response.Contains("principles for handling")) response = response + ",principles for handling*";
                if (requestLowercase.Contains("process of personal evolution") && !response.Contains("process of personal evolution")) response = response + ",process of personal evolution*";
                if (requestLowercase.Contains("produce outcomes") && !response.Contains("produce outcomes")) response = response + ",produce outcomes*";
                if (requestLowercase.Contains("produce progress") && !response.Contains("produce progress")) response = response + ",produce progress*";
                if (requestLowercase.Contains("professor sperry") && !response.Contains("professor sperry")) response = response + ",professor sperry*";
                if (requestLowercase.Contains("progress") && !response.Contains("progress")) response = response + ",progress*";
                if (requestLowercase.Contains("proper discussion") && !response.Contains("proper discussion")) response = response + ",proper discussion*";
                if (requestLowercase.Contains("quality communication") && !response.Contains("quality communication")) response = response + ",quality communication*";
                if (requestLowercase.Contains("questioning") && !response.Contains("questioning")) response = response + ",questioning*";
                else if (requestLowercase.Contains("question") && !response.Contains("question")) response = response + ",question*";
                if (requestLowercase.Contains("ratio of managers") && !response.Contains("ratio of managers")) response = response + ",ratio of managers*";
                else if (requestLowercase.Contains("ratio") && !response.Contains("ratio")) response = response + ",ratio*";
                if (requestLowercase.Contains("reach agreements") && !response.Contains("reach agreements")) response = response + ",reach agreements*";
                if (requestLowercase.Contains("reaching agreement") && !response.Contains("reaching agreement")) response = response + ",reaching agreement*";
                if (requestLowercase.Contains("reasoning-based thinkers") && !response.Contains("reasoning-based thinkers")) response = response + ",reasoning-based thinkers*";
                else if (requestLowercase.Contains("reasoning") && !response.Contains("reasoning")) response = response + ",reasoning*";
                if (requestLowercase.Contains("reasons managers") && !response.Contains("reasons managers")) response = response + ",reasons managers*";
                if (requestLowercase.Contains("records") && !response.Contains("records")) response = response + ",records*";
                if (requestLowercase.Contains("rely on learning") && !response.Contains("rely on learning")) response = response + ",rely on learning*";
                if (requestLowercase.Contains("rely on memory-based learning") && !response.Contains("rely on memory-based learning")) response = response + ",rely on memory-based learning*";
                if (requestLowercase.Contains("rely on reasoning") && !response.Contains("rely on reasoning")) response = response + ",rely on reasoning*";
                if (requestLowercase.Contains("reporting levels") && !response.Contains("reporting levels")) response = response + ",reporting levels*";
                else if (requestLowercase.Contains("reporting lines") && !response.Contains("reporting lines")) response = response + ",reporting lines*";
                else if (requestLowercase.Contains("reporting ratios") && !response.Contains("reporting ratios")) response = response + ",reporting ratios*";
                else if (requestLowercase.Contains("reporting") && !response.Contains("reporting")) response = response + ",reporting*";
                if (requestLowercase.Contains("reports") && !response.Contains("reports")) response = response + ",reports*";
                if (requestLowercase.Contains("requires for people") && !response.Contains("requires for people")) response = response + ",requires for people*";
                if (requestLowercase.Contains("responsibility for achieving goals") && !response.Contains("responsibility for achieving goals")) response = response + ",responsibility for achieving goals*";
                if (requestLowercase.Contains("responsibility of determining") && !response.Contains("responsibility of determining")) response = response + ",responsibility of determining*";
                if (requestLowercase.Contains("responsible parties") && !response.Contains("responsible parties")) response = response + ",responsible parties*";
                if (requestLowercase.Contains("responsible party") && !response.Contains("responsible party")) response = response + ",responsible party*";
                if (requestLowercase.Contains("rewarding process") && !response.Contains("rewarding process")) response = response + ",rewarding process*";
                if (requestLowercase.Contains("root") && !response.Contains("root")) response = response + ",root*";
                if (requestLowercase.Contains("sample size") && !response.Contains("sample size")) response = response + ",sample size*";
                if (requestLowercase.Contains("school learning") && !response.Contains("school learning")) response = response + ",school learning*";
                else if (requestLowercase.Contains("school performance") && !response.Contains("school performance")) response = response + ",school performance*";
                else if (requestLowercase.Contains("school") && !response.Contains("school")) response = response + ",school*";
                if (requestLowercase.Contains("separate department") && !response.Contains("separate department")) response = response + ",separate department*";
                if (requestLowercase.Contains("service clients") && !response.Contains("service clients")) response = response + ",service clients*";
                if (requestLowercase.Contains("shared mission") && !response.Contains("shared mission")) response = response + ",shared mission*";
                if (requestLowercase.Contains("ski instructor") && !response.Contains("ski instructor")) response = response + ",ski instructor*";
                else if (requestLowercase.Contains("ski") && !response.Contains("ski")) response = response + ",ski*";
                if (requestLowercase.Contains("slip") && !response.Contains("slip")) response = response + ",slip*";
                if (requestLowercase.Contains("smart people") && !response.Contains("smart people")) response = response + ",smart people*";
                if (requestLowercase.Contains("solution") && !response.Contains("solution")) response = response + ",solution*";
                if (requestLowercase.Contains("specific people") && !response.Contains("specific people")) response = response + ",specific people*";
                if (requestLowercase.Contains("step failure") && !response.Contains("step failure")) response = response + ",step failure*";
                if (requestLowercase.Contains("successful people") && !response.Contains("successful people")) response = response + ",successful people*";
                if (requestLowercase.Contains("support department") && !response.Contains("support department")) response = response + ",support department*";
                else if (requestLowercase.Contains("support") && !response.Contains("support")) response = response + ",support*";
                if (requestLowercase.Contains("symptomatic of root") && !response.Contains("symptomatic of root")) response = response + ",symptomatic of root*";
                if (requestLowercase.Contains("synch with people") && !response.Contains("synch with people")) response = response + ",synch with people*";
                else if (requestLowercase.Contains("synch") && !response.Contains("synch")) response = response + ",synch*";
                if (requestLowercase.Contains("task-level discussion") && !response.Contains("task-level discussion")) response = response + ",task-level discussion*";
                if (requestLowercase.Contains("technology department") && !response.Contains("technology department")) response = response + ",technology department*";
                if (requestLowercase.Contains("technology manager") && !response.Contains("technology manager")) response = response + ",technology manager*";
                if (requestLowercase.Contains("technology people") && !response.Contains("technology people")) response = response + ",technology people*";
                if (requestLowercase.Contains("thinkers") && !response.Contains("thinkers")) response = response + ",thinkers*";
                if (requestLowercase.Contains("track records") && !response.Contains("track records")) response = response + ",track records*";
                if (requestLowercase.Contains("training") && !response.Contains("training")) response = response + ",training*";
                if (requestLowercase.Contains("type of outcome") && !response.Contains("type of outcome")) response = response + ",type of outcome*";
                else if (requestLowercase.Contains("type people") && !response.Contains("type people")) response = response + ",type people*";
                else if (requestLowercase.Contains("type") && !response.Contains("type")) response = response + ",type*";
                if (requestLowercase.Contains("understand harry") && !response.Contains("understand harry")) response = response + ",understand harry*";
                if (requestLowercase.Contains("valuable comments") && !response.Contains("valuable comments")) response = response + ",valuable comments*";
                if (requestLowercase.Contains("views of people") && !response.Contains("views of people")) response = response + ",views of people*";
                else if (requestLowercase.Contains("views") && !response.Contains("views")) response = response + ",views*";
                else if (requestLowercase.Contains("view") && !response.Contains("view")) response = response + ",view*";
                if (requestLowercase.Contains("water") && !response.Contains("water")) response = response + ",water*";

                if (requestLowercase.Contains("going forward") && !response.Contains("going forward")) response = response + ",going forward*";
                if (requestLowercase.Contains("removed") && !response.Contains("removed")) response = response + ",removed*";
                if (requestLowercase.Contains("private") && !response.Contains("private")) response = response + ",private*";
                if (requestLowercase.Contains("distant") && !response.Contains("distant")) response = response + ",distant*";
                if (requestLowercase.Contains("openness") && !response.Contains("openness")) response = response + ",openness*";
                else if (requestLowercase.Contains("open") && !response.Contains("open")) response = response + ",open*";
                if (requestLowercase.Contains("dangerous") && !response.Contains("dangerous")) response = response + ",dangerous*";
                else if (requestLowercase.Contains("danger") && !response.Contains("danger")) response = response + ",danger*";
                if (requestLowercase.Contains("debating") && !response.Contains("debating")) response = response + ",debating*";
                if (requestLowercase.Contains("debate") && !response.Contains("debate")) response = response + ",debate*";
                if (requestLowercase.Contains("agreeing") && !response.Contains("agreeing")) response = response + ",agreeing*";
                if (requestLowercase.Contains("worry") && !response.Contains("worry")) response = response + ",worry*";
                if (requestLowercase.Contains("effectively") && !response.Contains("effectively")) response = response + ",effectively*";
                if (requestLowercase.Contains("please everyone") && !response.Contains("please everyone")) response = response + ",please everyone*";
                if (requestLowercase.Contains("more that we don't know") && !response.Contains("more that we don't know")) response = response + ",more that we don't know*";

                if (requestLowercase.Contains("hugging a loved one") && !response.Contains("hugging a loved one")) response = response + ",hugging a loved one*";
                if (requestLowercase.Contains("do you what you set out to do") && !response.Contains("do you what you set out to do")) response = response + ",do you what you set out to do*";
                if (requestLowercase.Contains("looking like this") && !response.Contains("looking like this")) response = response + ",looking like this*";
                if (requestLowercase.Contains("synthesize") && !response.Contains("synthesize")) response = response + ",synthesize*";

                if (requestLowercase.Contains("woman") && !response.Contains("woman")) response = response + ",woman*";

                if (requestLowercase.Contains("women") && !response.Contains("women")) response = response + ",women*";

                if (requestLowercase.Contains("female") && !response.Contains("female")) response = response + ",female*";
                else if (requestLowercase.Contains("male") && !response.Contains("male")) response = response + ",male*";

                if (requestLowercase.Contains("micromanaging") && !response.Contains("micromanaging")) response = response + ",micromanaging*";
                else if (requestLowercase.Contains("managing") && !response.Contains("managing")) response = response + ",managing*";

                response = response.Replace("bridgewater", "Bridgewater");
                response = response.Replace("harry", "Harry");
                response = response.Replace("professor sperry", "Professor Sperry");
                if (response.StartsWith(",")) response = response.Substring(1);

                if (String.IsNullOrEmpty(response)) response = "NULL";
            }
        }

        private static async Task<String> PostToEndpoint(HttpClient client, string uri, byte[] byteData)
        {
            using (var content = new ByteArrayContent(byteData))
            {
                location = "NULL";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseMessage = await client.PostAsync(uri, content);
                if (responseMessage.Headers.Location != null)
                {
                    location = responseMessage.Headers.Location.ToString();
                }
                return await responseMessage.Content.ReadAsStringAsync();
            }
        }

        private static async Task<String> GetFromEndpoint(HttpClient client, string uri)
        {
            var response = await client.GetAsync(uri);
            return await response.Content.ReadAsStringAsync();
        }

        //private void AnalyzeSentiment(string value)
        //{
        //    double total = 0;
        //    RootObject obj = JsonConvert.DeserializeObject<RootObject>(value);
        //    foreach (var item in obj.documents)
        //    {
        //        total += (Double.Parse(item.score) * 100);
        //    }

        //    double average = total / obj.documents.Count;
        //    Windows.UI.Xaml.Media.SolidColorBrush myBrush = new Windows.UI.Xaml.Media.SolidColorBrush();
        //    if (average > 0 && average <= 40)
        //    {
        //        myBrush.Color = Colors.Red;
        //        rectSentiment.Fill = myBrush;
        //    }
        //    else if (average > 40 && average <= 65)
        //    {
        //        myBrush.Color = Colors.Yellow;
        //        rectSentiment.Fill = myBrush;
        //    }
        //    else
        //    {
        //        myBrush.Color = Colors.Green;
        //        rectSentiment.Fill = myBrush;
        //    }
        //}

        //private void btnGetSentiment_Click(object sender, RoutedEventArgs e)
        //{
        //    AuthorizeTwitter();
        //    List<String> response = SearchTwitter();
        //    string request = FormatRequestJSON(response);
        //    MakeSentimentRequests(request);
        //}

        //private void btnGetKeyPhases_Click(object sender, RoutedEventArgs e)
        //{
        //    string text = "Recognize that performance in school, while of some value in making assessments, doesn’t tell you much about whether the person has the values and abilities you are looking for. Memory and processing speed tend to be the abilities that determine success in school(largely because they’re easier to measure and grade) and are most valued, so school performance is an excellent gauge of these. School performance is also a good gauge for measuring willingness and ability to follow directions, as well as determination.However, school is of limited value for teaching and testing common sense, vision, creativity, or decision - making.63 Since those traits all outweigh memory, processing speed, and the ability to follow directions in most jobs, you must look beyond school to ascertain whether the applicant has the qualities you’re looking for.";

        //    List<String> response = new List<string>();
        //    response.Add(text);
        //    string request = FormatRequestJSON(response);
        //    MakeKeyPhrasesRequests(request);
        //}

        //private void btnAnalyze_Click(object sender, RoutedEventArgs e)
        //{
        //    AnalyzeSentiment(tbResults2.Text); // mwh
        //}
    }
}
