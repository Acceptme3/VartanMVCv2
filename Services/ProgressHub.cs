using Microsoft.AspNetCore.SignalR;

namespace VartanMVCv2.Services
{
    public class ProgressHub:Hub
    {
        public async Task SendProgress(int progress)
        {
            await Clients.All.SendAsync("UpdateProgress", progress);
        }
    }
}
