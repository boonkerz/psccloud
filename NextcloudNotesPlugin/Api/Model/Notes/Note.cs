namespace PscCloud.Plugin.Nextcloud.Notes.Api.Model.Notes;

public class Note
{
    public string Id { get; set; }
    
    public string Title { get; set; }

    public string Content { get; set; }
    
    public string etag { get; set; }
}