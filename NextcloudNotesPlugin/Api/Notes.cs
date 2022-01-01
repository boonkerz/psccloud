using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using Avalonia.Threading;
using PscCloud.Plugin.Nextcloud.Notes.Api.Model.Login;
using PscCloud.Plugin.Nextcloud.Notes.Api.Model.Notes;
using PscCloud.Plugin.Nextcloud.Notes.Model;
using RestSharp;
using RestSharp.Authenticators;

namespace PscCloud.Plugin.Nextcloud.Notes.Api
{
    public class Notes
    {
        
        async public Task<List<Note>> GetNotes(Settings settings)
        {
            var notes = new List<Note>();
            
            var client = new RestClient(settings.Server);
            var request = new RestRequest("index.php/apps/notes/api/v1/notes");
            client.Authenticator = new HttpBasicAuthenticator(settings.LoginName, settings.AppPassword);

            var response = await client.ExecuteGetAsync<List<Note>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                notes = response.Data;
            }

            return notes;
        }

        async public Task<bool> SaveNote(Settings settings, Note note)
        {
            var client = new RestClient(settings.Server);
            client.Authenticator = new HttpBasicAuthenticator(settings.LoginName, settings.AppPassword);
            var request = new RestRequest("index.php/apps/notes/api/v1/notes/" + note.Id, Method.PUT);
            request.AddHeader("If-Match", "\"" + note.etag + "\"");
            request.AddJsonBody(new { title = note.Title, content = note.Content});
            var response = await client.ExecuteAsync<Note>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}