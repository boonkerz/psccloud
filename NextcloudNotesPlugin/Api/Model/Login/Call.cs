namespace PscCloud.Plugin.Nextcloud.Notes.Api.Model.Login
{

    public class Poll
    {
        public string token { get; set; }
        
        public string endpoint { get; set; }
    }
    
    public class Call
    {
        public string login { get; set; }
        
        public Poll poll { get; set; }
    }
}