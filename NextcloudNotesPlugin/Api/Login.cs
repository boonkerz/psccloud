using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using Avalonia.Threading;
using PscCloud.Plugin.Nextcloud.Notes.Api.Model.Login;
using RestSharp;

namespace PscCloud.Plugin.Nextcloud.Notes.Api
{
    public class EventPollResponse : EventArgs
    {
        public PollResponse PollResponse { get; set; }
    }
    
    public class Login
    {
        private readonly DispatcherTimer autoSaveTimer = new DispatcherTimer();

        private string pollUrl = "";
        
        private string serverUrl { get; set; }

        private string pollToken = "";
        public event EventHandler<EventPollResponse> LoginFinished;
        public void LoginFlow(string serverUrl)
        {
            this.serverUrl = serverUrl;
            var client = new RestClient(this.serverUrl);
            var request = new RestRequest("index.php/login/v2");
            var response = client.Post<Call>(request);
            var call = response.Data; // Raw content as string

            this.OpenBrowser(call.login);
            this.pollUrl = call.poll.endpoint;
            this.pollToken = call.poll.token;
            
            this.startPoll();

        }
        protected virtual void OnLoginFinished(EventPollResponse e)
        {
            LoginFinished?.Invoke(this, e);
        }
        
        private void startPoll()
        {
            autoSaveTimer.Tick += new EventHandler(reloadPoll);
            autoSaveTimer.Interval = new TimeSpan(0, 0, 30);
            autoSaveTimer.Start();

        }
        
        private void reloadPoll(object sender, EventArgs e)
        {
            var client = new RestClient(this.serverUrl);
            var request = new RestRequest("index.php/login/v2/poll");
            request.AddQueryParameter("token", this.pollToken);
            var response = client.Post<PollResponse>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var ea = new EventPollResponse();
                ea.PollResponse = response.Data;
                OnLoginFinished(ea); //No event data
                autoSaveTimer.Stop();
            }
        }
        
        private void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

    }
}