
using System;
using System.Threading.Tasks;

//using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json.Linq;

using WebRTC.DemoApp.Interfaces;
using WebRTC.DemoApp.SignalRClient;
using WebRTC.DemoApp.SignalRClient.Abstraction;

using Xamarin.Essentials;
using Xamarin.Forms;

using ILogger = WebRTC.DemoApp.SignalRClient.Abstraction.ILogger;

namespace WebRTC.DemoApp
{
    public partial class App1 : Application
    {
        #region Properties & Variables

        private ILogger logger;

        public static HubConnection HubConnection { get; set; }
        public static IHubProxy HubProxy { get; set; }
        protected ISdpAnswerRecieved SdpAnswerRecieved { get; set; }
        public static SRTCClient1 Instance { get; set; }

        #endregion

        public App1()
        {
            SdpAnswerRecieved = DependencyService.Get<ISdpAnswerRecieved>();
         
           

            logger = new ConsoleLogger();

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override async void OnStart()
        {
           await InitHub();
             }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


        #region SignalR Hub Connection Methods



        async public static Task InitHub()
        {
            string HUN_NAME = "VideoCallHub";
            Console.WriteLine("Start ConnectAsync");
            HubConnection = new HubConnection("https://teamicu.com");
           // App.HubConnection.Headers.Add(new KeyValuePair<string, string>("Authorization", $"Bearer {Common.Token.access_token}"));
           // App.HubConnection.Headers.Add(new KeyValuePair<string, string>("OrgID", $"{Common.CurrentOrganization.OrganizationID}"));
            HubProxy = HubConnection.CreateHubProxy(HUN_NAME);

            HubProxy.On<string>("OnAnswerSDPRecieved", (_answerSdp) => { App1.Instance.OnWebSocketMessage(_answerSdp); });

            HubProxy.On<string>("OnOfferSDPReceived", (_offerSdp) => { App1.Instance.OnWebSocketMessage(_offerSdp); });

            HubProxy.On<string>("OnLocalICECandidateReceived", (_iceCandidate) => { App1.Instance.OnWebSocketMessage(_iceCandidate); });

            HubProxy.On<string>("OnICECandidatesRemoved", (_candidates) => { App1.Instance.OnWebSocketMessage(_candidates); });
            //HubConnection.Reconnecting += Reconnecting;
            //HubConnection.Reconnected += Reconnected;
            //HubConnection.Closed += Disconnected;
            await App1.HubConnection.Start();
            Console.WriteLine("Start ConnectAsync");
        }
        #endregion
    }
}
