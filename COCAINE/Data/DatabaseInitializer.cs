using COCAINE.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace COCAINE.Data
{
    public class DatabaseInitializer
    {
        // URL Сервиса, который обрабатывает запросы на хранение статичных файлов
        const string ImagesUrl = "https://localhost:5001";

        public void SeedDatabase(DatabaseContext context)
        {
            SeedArtists(context);
            SeedAlbums(context);
            SeedTracks(context);
        }

        private void SeedArtists(DatabaseContext context)
        {
            if (context.Artists.Any())
                return;

            context.Artists.Add(new Artist()
            {
                ArtistName = "Aikko",
                ArtistImage = ImagesUrl + "/images/artists/1.png"
            });

            context.Artists.Add(new Artist()
            {
                ArtistName = "pyrokinesis",
                ArtistImage = ImagesUrl + "/images/artists/2.png"
            });

            context.Artists.Add(new Artist()
            {
                ArtistName = "twoxseven",
                ArtistImage = ImagesUrl + "/images/artists/3.png"
            });

            context.Artists.Add(new Artist()
            {
                ArtistName = "INSPACE",
                ArtistImage = ImagesUrl + "/images/artists/4.png"
            });

            context.Artists.Add(new Artist()
            {
                ArtistName = "Katanacss",
                ArtistImage = ImagesUrl + "/images/artists/5.png"
            });

            context.Artists.Add(new Artist()
            {
                ArtistName = "Own Maslou",
                ArtistImage = ImagesUrl + "/images/artists/6.png"
            });

            context.Artists.Add(new Artist()
            {
                ArtistName = "Uxknow",
                ArtistImage = ImagesUrl + "/images/artists/7.png"
            });

            context.SaveChanges();
        }

        private void SeedAlbums(DatabaseContext context)
        {
            var aikko = context.Artists.First(t => t.ArtistName == "Aikko");

            context.Albums.Add(new Album()
            {
                AlbumName = "мои друзья не должны умирать",
                AlbumImage = ImagesUrl + "/images/albums/1.png",
                Artist = aikko,
                ReleaseDate = DateTime.Parse("01-01-2020")
            });

            context.Albums.Add(new Album()
            {
                AlbumName = "Тёмные делишки",
                AlbumImage = ImagesUrl + "/images/albums/2.png",
                Artist = aikko,
                ReleaseDate = DateTime.Parse("01-01-2021")
            });

            context.Albums.Add(new Album()
            {
                AlbumName = "неприязнь",
                AlbumImage = ImagesUrl + "/images/albums/3.png",
                Artist = aikko,
                ReleaseDate = DateTime.Parse("01-01-2021")
            });

            context.Albums.Add(new Album()
            {
                AlbumName = "фикционализм",
                AlbumImage = ImagesUrl + "/images/albums/4.png",
                Artist = aikko,
                ReleaseDate = DateTime.Parse("01-01-2018")
            });

            context.Albums.Add(new Album()
            {
                AlbumName = "не навсегда",
                AlbumImage = ImagesUrl + "/images/albums/5.png",
                Artist = aikko,
                ReleaseDate = DateTime.Parse("01-01-2019")
            });

            context.SaveChanges();
        }

        private void SeedTracks(DatabaseContext context)
        {
            var aikko = context.Artists.First(t => t.ArtistName == "Aikko");
            var pyrokinesis = context.Artists.First(t => t.ArtistName == "pyrokinesis");
            var twoxseven = context.Artists.First(t => t.ArtistName == "twoxseven");
            var inspace = context.Artists.First(t => t.ArtistName == "INSPACE");
            var katanacss = context.Artists.First(t => t.ArtistName == "Katanacss");
            var ownMaslou = context.Artists.First(t => t.ArtistName == "Own Maslou");
            var uxknow = context.Artists.First(t => t.ArtistName == "Uxknow");

            // Альбом "Фикционализм"
            {
                var album = context.Albums
                    .Include(t => t.Tracks)
                    .First(t => t.AlbumName == "фикционализм");

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 1,
                    TrackName = "нечего сказать",
                    Duration = "4:55",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko } 
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 2,
                    TrackName = "костюм",
                    Duration = "2:34",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 3,
                    TrackName = "для других",
                    Duration = "3:25",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 4,
                    TrackName = "запомню тебя такой",
                    Duration = "4:32",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 5,
                    TrackName = "потолок",
                    Duration = "3:19",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 6,
                    TrackName = "черное сердце",
                    Duration = "3:21",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko, pyrokinesis }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 7,
                    TrackName = "фальшивые",
                    Duration = "3:21",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });
            }

            // Альбом "Тёмные делишки"
            {
                var album = context.Albums
                    .Include(t => t.Tracks)
                    .First(t => t.AlbumName == "Тёмные делишки");

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 1,
                    TrackName = "как всё устроено",
                    Duration = "2:07",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 2,
                    TrackName = "глаза-стекляшки",
                    Duration = "3:16",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 3,
                    TrackName = "не возьмём тебя с собой",
                    Duration = "2:08",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 4,
                    TrackName = "в следующий раз",
                    Duration = "2:42",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 5,
                    TrackName = "удобно",
                    Duration = "3:12",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 6,
                    TrackName = "апатия",
                    Duration = "3:18",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 7,
                    TrackName = "забота",
                    Duration = "2:09",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 8,
                    TrackName = "напрокат",
                    Duration = "3:42",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 9,
                    TrackName = "даже когда я буду гореть в аду",
                    Duration = "3:21",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 10,
                    TrackName = "за пазухой у христа",
                    Duration = "2:34",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });
            }

            // Альбом "мои друзья не должны умирать"
            {
                var album = context.Albums
                    .Include(t => t.Tracks)
                    .First(t => t.AlbumName == "мои друзья не должны умирать");

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 1,
                    TrackName = "так неинтересно",
                    Duration = "2:18",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 2,
                    TrackName = "crybaby",
                    Duration = "3:42",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 3,
                    TrackName = "никто не говорит почему",
                    Duration = "3:14",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko, pyrokinesis }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 4,
                    TrackName = "классика",
                    Duration = "3:40",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko, twoxseven }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 5,
                    TrackName = "пустышка",
                    Duration = "4:07",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 6,
                    TrackName = "не хочу ничего знать",
                    Duration = "2:29",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 7,
                    TrackName = "точь-в-точь",
                    Duration = "3:26",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 8,
                    TrackName = "ты не виновата",
                    Duration = "2:28",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 9,
                    TrackName = "в рюкзаки",
                    Duration = "2:52",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 10,
                    TrackName = "10 негритят",
                    Duration = "4:29",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 11,
                    TrackName = "не прокатит",
                    Duration = "4:04",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 12,
                    TrackName = "я не голос поколений",
                    Duration = "3:06",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko, inspace }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 13,
                    TrackName = "место под солнцем",
                    Duration = "3:11",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko, ownMaslou }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 14,
                    TrackName = "женщины и алкоголь",
                    Duration = "3:55",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko, katanacss }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 15,
                    TrackName = "мои друзья не должны умирать",
                    Duration = "2:16",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });
            }

            // Альбом "не навсегда"
            {
                var album = context.Albums
                    .Include(t => t.Tracks)
                    .First(t => t.AlbumName == "не навсегда");

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 1,
                    TrackName = "покой нам только снится",
                    Duration = "5:04",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko, inspace }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 2,
                    TrackName = "идеальной",
                    Duration = "2:28",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 3,
                    TrackName = "наигрались",
                    Duration = "2:50",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 4,
                    TrackName = "люблю этих людей",
                    Duration = "1:58",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 5,
                    TrackName = "ничего не отдашь за меня",
                    Duration = "1:45",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 6,
                    TrackName = "забери меня к себе",
                    Duration = "4:05",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko, uxknow }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 7,
                    TrackName = "не навсегда",
                    Duration = "3:16",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 8,
                    TrackName = "моя любимая композиция",
                    Duration = "3:16",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });
            }

            // Альбом "неприязнь"
            {
                var album = context.Albums
                    .Include(t => t.Tracks)
                    .First(t => t.AlbumName == "неприязнь");

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 1,
                    TrackName = "посчитай ступеньки",
                    Duration = "2:27",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 2,
                    TrackName = "неприязнь",
                    Duration = "1:47",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 3,
                    TrackName = "личное пространство",
                    Duration = "3:05",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });

                album.Tracks.Add(new Track()
                {
                    InAlbumNumber = 4,
                    TrackName = "авантюристка",
                    Duration = "2:30",
                    TrackAlbum = album,
                    TrackArtists = new List<Artist>() { aikko }
                });
            }


            context.SaveChanges();
        }
    }
}
