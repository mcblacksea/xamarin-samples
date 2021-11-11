using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebRTCme.Middleware.Xamarin.Services
{
    class WebRtcIncomingFileStreamFactory : IWebRtcIncomingFileStreamFactory
    {
        public Task<Stream> CreateAsync(string peerUserName, DataParameters dataParameters, Action<string, Guid> onCompleted)
        {
            throw new NotImplementedException();
        }
    }
}
