using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JoySoftware.HomeAssistant.NetDaemon.Common;

public class MotionSnapshot : NetDaemonApp
{
    public string? Alarm { get; set; }
    public string? FilePath { get; set; }
    public IEnumerable<string>? Cameras { get; set; }
    public string? DiscordNotifier { get; set; }
    public string? DiscordChannel { get; set; }
    public override Task InitializeAsync()
    {
        InitializeMotionSnapshots();
        return Task.CompletedTask;
    }

    private void InitializeMotionSnapshots()
    {
        Entity(Alarm).WhenStateChange(to: "triggered").Call(async (entityId, to, from) =>
        {
            var time = DateTime.Now;
            foreach (var camera in this.Cameras)
            {
                var dict = new Dictionary<string, IEnumerable<string>>();
                dict.Add("images", new List<string> { FilePath.Replace("{camera}", camera) });
                await CallService("camera", "snapshot", new
                {
                    entity_id = camera,
                    filename = FilePath.Replace("{camera}", camera)
                });
                await app.CallService("notify", this.DiscordNotifier, new
                {
                    data = dict,
                    message = $"{time} - {camera}",
                    target = this.DiscordChannel
                });

            }
        }).Execute();
    }
}