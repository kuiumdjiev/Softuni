using System.Linq;
using System.Text;
using MusicHub.Data.Models;

namespace MusicHub
{
    using System;

    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context = 
                new MusicHubDbContext();

          // DbInitializer.ResetDatabase(context);
            int duration = int.Parse(Console.ReadLine());

            Console.WriteLine(ExportSongsAboveDuration(context,duration));
            //Test your solutions here
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var allAlbums = context
                .Albums
                .Where(x => x.ProducerId == producerId)
                .Select(x => new
                {
                    AlbumName = x.Name,
                    AlbumDate = x.ReleaseDate,
                    AlbumPrice = x.Price,
                    ProducerName = x.Producer.Name,
                    Songs = x.Songs.Select(s => new
                    {
                        SongName = s.Name,
                        SongPrice = s.Price,
                        SongWriter = s.Writer.Name
                    }).OrderByDescending(o => o.SongName).ThenBy(o => o.SongWriter).ToList()
                })
                .OrderByDescending(x => x.AlbumPrice)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var item in allAlbums)
            {
                sb.AppendLine($"-AlbumName: {item.AlbumName}");
                sb.AppendLine($"-ReleaseDate: {item.AlbumDate.ToString("MM/dd/yyyy")}");
                sb.AppendLine($"-ProducerName: {item.ProducerName}");
                sb.AppendLine($"-Songs:");
                int counter = 1;
                foreach (var song in item.Songs)
                {
                    sb.AppendLine($"---#{counter}");
                    sb.AppendLine($"---SongName: {song.SongName}");
                    sb.AppendLine($"---Price: {song.SongPrice:f2}");
                    sb.AppendLine($"---Writer: {song.SongWriter}");
                    counter++;
                }
                sb.AppendLine($"-AlbumPrice: {item.AlbumPrice:f2}");
            }
            return sb.ToString().Trim();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var result = context.Songs.Where(x => x.Duration > TimeSpan.FromSeconds(duration))
                .Select(x => new
                {
                    SongName = x.Name,
                    SongWriter = x.Writer.Name,
                    SongPerformer = x.SongPerformers.Select(g => g.Performer.FirstName + " " + g.Performer.LastName)
                        .FirstOrDefault(),
                    SongAlbumProducer = x.Album.Producer.Name,
                    SongDuration = x.Duration.ToString("c")
                })
                .OrderBy(x => x.SongName)
                .ThenBy(x => x.SongWriter)
                .ThenBy(x => x.SongPerformer)
                .ToList();
            int count = 1;
            StringBuilder sb = new StringBuilder();
            foreach (var song in result)
            {
                sb.AppendLine($"-Song #{count}");
                sb.AppendLine($"---SongName: {song.SongName}");
                sb.AppendLine($"---Writer: {song.SongWriter}");
                sb.AppendLine($"---Performer: {song.SongPerformer}");
                sb.AppendLine($"---AlbumProducer: {song.SongAlbumProducer}");
                sb.AppendLine($"---Duration: {song.SongDuration}");
                count++;
            }
          return sb.ToString();
        }
    }
}
