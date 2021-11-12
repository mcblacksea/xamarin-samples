// onotseike@hotmail.comPaula Aliu
using System;

using Newtonsoft.Json;

namespace WebRTC.Classes
{
    public class IceCandidate
    {
        public IceCandidate(string sdp, string sdpMid, int sdpMLineIndex)
        {
            Sdp = sdp;
            SdpMid = sdpMid;
            SdpMLineIndex = sdpMLineIndex;
        }

        public IceCandidate()
        {

        }

        [JsonProperty("sdp")]
        public string Sdp { get; set; }

        [JsonProperty("sdpMid")]
        public string SdpMid { get; set; }

        [JsonProperty("sdpMLineIndex")]
        public int SdpMLineIndex { get; set; }
    }
}
