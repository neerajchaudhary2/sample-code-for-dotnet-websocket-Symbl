using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
namespace websocket
{
	class Program
	{
		static void Main(string[] args)
		{
			//Create my object
			var myData = new
			{
				type = "start_request",
				meetingTitle = "Websockets How-to",
				insightTypes = new[] { "question", "action_item" }, // Will enable insight generation
				config = new
				{
					confidenceThreshold = 0.5,
					languageCode = "en-US",
					speechRecognition = new
					{
						encoding = "LINEAR16",
						sampleRateHertz = 44100,
					}
				},
				speaker = new
				{
					userId = "example@symbl.ai",
					name = "Example Sample",
				}
			};
		string jsonData = JsonConvert.SerializeObject(myData);
			Console.WriteLine(jsonData);
			JObject jsonObject = JObject.Parse(jsonData);
			///define variables 
			string accessToken = "";
			string uniqueMeetingId = "";
			string symblEndpoint = "wss://api.symbl.ai/v1/realtime/insights/"+uniqueMeetingId+"?access_token="+accessToken;
			/// Create a websocket client
			/// 
			WebSocket ws = new WebSocket(symblEndpoint);
			ws.OnOpen += Ws_OnOpen;
			ws.OnMessage += Ws_OnMessage;
			ws.OnError += Ws_OnError;
			ws.OnClose += Ws_OnClose;
			ws.Connect();
			ws.Send(jsonData);
			Console.ReadKey();
		}
		private static void Ws_OnClose(object sender, CloseEventArgs e)
		{
			throw new NotImplementedException();
		}
		private static void Ws_OnError(object sender, ErrorEventArgs e)
		{
			throw new NotImplementedException();
		}
		private static void Ws_OnOpen(object sender, EventArgs e)
		{
			try
			{
				Console.WriteLine(e);
				//Create my object
				var myData = new
				{
					type = "start_request",
					meetingTitle = "Websockets How-to",
					insightTypes = new[] { "question", "action_item" }, // Will enable insight generation
					config = new
					{
						confidenceThreshold = 0.5,
						languageCode = "en-US",
						speechRecognition = new
						{
							encoding = "MULAW",
							sampleRateHertz = 8000,
						}
					},
					speaker = new
					{
						userId = "example@symbl.ai",
						name = "Example Sample",
					}
				};
				//Tranform it to Json object
				string jsonData = JsonConvert.SerializeObject(myData);
				Console.WriteLine("inside Symbl on Open");
			}
			catch
			{
				Console.WriteLine(sender);
				Console.WriteLine(e);
			}
			//throw new NotImplementedException();
		}
		private static void Ws_OnMessage(object sender, MessageEventArgs e)
		{
			Console.WriteLine("inside Symbl on Message");
			Console.WriteLine(e);
			Console.WriteLine(sender);
			Console.WriteLine(JsonConvert.SerializeObject(e));
			//throw new NotImplementedException();
		}
	}
}
