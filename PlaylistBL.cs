using BLL.Abstract;
using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PlaylistBL : IPlaylistBL
    {
        private readonly IPlaylistDataAccess _playlistDataAccess;
        private readonly IMelodieDataAccess _melodieDataAccess;

        public PlaylistBL(IPlaylistDataAccess playlistDataAccess, IMelodieDataAccess melodieDataAccess)
        {
            _playlistDataAccess = playlistDataAccess;
            _melodieDataAccess = melodieDataAccess;
        }

        public IEnumerable<Playlist> GetPlaylist()
        {
            return _playlistDataAccess.GetPlaylist();
        }

        public IEnumerable<Playlist> GetPlaylistBySongNotIn(int id)
        {
            return _playlistDataAccess.GetPlaylistBySongNotIn(id);
        }

        public IEnumerable<Playlist> GetPlaylistBySongIn(int id)
        {
            return _playlistDataAccess.GetPlaylistBySongIn(id);
        }

        public bool AddPlaylist(Playlist playlist)
        {
            return _playlistDataAccess.AddPlaylist(playlist);
        }

        public bool GetMelodiePlaylistByBothIds(int idPlaylist, int idMelodie)
        {
            return _playlistDataAccess.GetMelodiePlaylistByBothIds(idPlaylist, idMelodie);
        }

        public List<Melodie> NotAddedMelody(IEnumerable<Melodie> addedMelody, int playlistId)
        {
            List<Melodie> notAddedMelody = new List<Melodie>();

            IEnumerable<Melodie> allMelody = _melodieDataAccess.GetMelodii();

            Playlist playlist = GetPlaylistById(playlistId);

            foreach (Melodie melodie in allMelody)
            {
                int ok = 0;
                foreach(Melodie melodie1 in addedMelody)
                {
                    if(melodie.Nume == melodie1.Nume && melodie.Id == melodie1.Id)
                    {
                        Console.WriteLine(melodie.Nume);
                        Console.WriteLine(melodie1.Nume);
                        Console.WriteLine();
                        ok = 1;
                        break;
                    }
                    else Console.WriteLine("ocolesc");
                }

                if ( (melodie.Gen == playlist.Gen || playlist.Gen == "Empty") && ok==0)
                {
                    notAddedMelody.Add(melodie);
                }
            }

            return notAddedMelody;
        }

        public bool AddMelodieToPlaylist(int idPlaylist, int idMelodie)
        {
            var melodiePlaylistFromDb = _playlistDataAccess.GetMelodiePlaylistByBothIds(idPlaylist, idMelodie); //nu neaparat necesar, deoarece oricum dropdownul nu
            Playlist playlist = _playlistDataAccess.GetPlaylistById(idPlaylist);
            Melodie melodie = _melodieDataAccess.GetMelodiiById(idMelodie);
            if (melodiePlaylistFromDb == false)
            {
                if (melodie.Gen == playlist.Gen || playlist.Gen=="Empty")
                {
                    if (playlist.NumarPiese < 5)
                    {
                        Console.WriteLine("Mai era loc");
                        _playlistDataAccess.UpdatePlaylistRiseNumarPiese(idPlaylist);
                        return _playlistDataAccess.AddMelodieToPlaylist(idPlaylist, idMelodie); //mai arata playlisturile in care am bagat deja o melodie

                    }
                    Console.WriteLine("E plin");
                    return false;
                }
                Console.WriteLine("Melodia nu are genul Playlistului");
                
            }
            return false; //totusi poate ajuta pe viitor
        }

        public bool CheckDuplicate(int playlistId, Melodie melodie)
        {
            IEnumerable<Melodie> melodiePlaylist = _melodieDataAccess.GetMelodiiFromPlaylist(playlistId);


            if (melodiePlaylist == null)
            {
                Console.WriteLine("Playlist gol");
                
                return AddMelodieToPlaylist(playlistId, melodie.Id);
            }
            else
            {
                Console.WriteLine("Playlist cu ceva in el");
                foreach (Melodie melPls in melodiePlaylist)
                {
                    if(melodie.Id == melPls.Id)
                    {
                        Console.WriteLine("Melodie duplicat");
                        return false;
                    }
                }
                Console.WriteLine("Melodia nu este inca in playlist");
                
                AddMelodieToPlaylist(playlistId, melodie.Id);
                return true;
            }
            //return false;
        }

        public bool AddAllArtistsSongs(int playlistId, int artistId) //FARA DAL
        {
            IEnumerable<Melodie> melodii = _melodieDataAccess.GetMelodiiFromArtist(artistId);
            return PassMelody(playlistId, melodii);
        }

        public bool AddAllAlbumsSongs(int playlistId, int albumId) //FARA DAL
        {
            IEnumerable<Melodie> melodii = _melodieDataAccess.GetMelodiiFromAlbum(albumId);
            return PassMelody(playlistId, melodii);
        }

        public bool AddPlaylistSongsToPlaylist(int idFirstPlaylist, int idSecondPlaylist) //FARA DAL
        {
            IEnumerable<Melodie> melodii = _melodieDataAccess.GetMelodiiFromPlaylist(idSecondPlaylist);
            return PassMelody(idFirstPlaylist, melodii);
        }

        private bool PassMelody(int idPlaylist, IEnumerable<Melodie> melodii)
        {
            if (melodii == null)
            {
                return false;
            }
            else
            {
                foreach (Melodie melodie in melodii)
                {
                    Console.WriteLine("Exista melodii la playlist");

                    if (CheckDuplicate(idPlaylist, melodie))
                    {
                        Console.WriteLine("Melodia nu e duplicat");
                    }
                    else
                    {
                        Console.WriteLine("Melodie duplicat sau alte probleme");
                    }
                }
                return false;
            }
        }

        public bool UpdatePlaylist(Playlist playlist)
        {
            var playlistFromDb = _playlistDataAccess.GetPlaylistById(playlist.Id);
            if (playlistFromDb == null) return false;
            return _playlistDataAccess.UpdatePlaylist(playlist);
        }

        public bool UpdatePlaylistRiseNumarPiese(int playlistId)
        {
            var playlistFromDb = _playlistDataAccess.GetPlaylistById(playlistId);
            if (playlistFromDb != null)
            {
                //if (playlistFromDb.NumarPiese < 3)
                //{
                    //Console.WriteLine("Mai era loc");
                    return _playlistDataAccess.UpdatePlaylistRiseNumarPiese(playlistId);
                //}
                //Console.WriteLine("E plin");
            }
            return false;
        }

        public bool UpdatePlaylistDecreaseNumarPiese(int playlistId)
        {
            var playlistFromDb = _playlistDataAccess.GetPlaylistById(playlistId);
            if (playlistFromDb == null) return false;
            return _playlistDataAccess.UpdatePlaylistDecreaseNumarPiese(playlistId);
        }

        public bool UpdatePlaylistGenre(string gen, int playlistId)
        {
            var playlistFromDb = _playlistDataAccess.GetPlaylistById(playlistId);
            Playlist playlist = _playlistDataAccess.GetPlaylistById(playlistId);
            if (playlistFromDb == null)
            {
                return false;
            }

            if(gen == "Empty")
            {
                Console.WriteLine("Genul playlistului nu necesita stergeri");
            }
            else
            {
                IEnumerable<Melodie> melodii = _melodieDataAccess.GetMelodiiFromPlaylist(playlistId);
                foreach (Melodie melodie in melodii)
                {
                    if (melodie.Gen != gen)
                    {
                        _playlistDataAccess.DeleteMelodieFromPlaylist(playlistId, melodie.Id);
                        Console.WriteLine("Piesa cu gen diferit");
                        Console.WriteLine(melodie.Id);
                        Console.WriteLine();

                        _playlistDataAccess.UpdatePlaylistDecreaseNumarPiese(playlistId);
                    }
                    else
                    {
                        Console.WriteLine("Piesa cu gen potrivit");
                        Console.WriteLine(melodie.Id);
                        Console.WriteLine();
                    }
                }
            }
            
            return _playlistDataAccess.UpdatePlaylistGenre(gen, playlistId);

        }

        public Playlist GetPlaylistById(int id)
        {
            return _playlistDataAccess.GetPlaylistById(id);
        }

        public bool DeletePlaylist(Playlist playlist)
        {
            var playlistFromDb = _playlistDataAccess.GetPlaylistById(playlist.Id);
            if (playlistFromDb == null) return false;
            
            return _playlistDataAccess.DeletePlaylist(playlist);
        }

        public bool DeleteMelodieFromPlaylist(int playlistId, int melodieId)
        {
            _playlistDataAccess.UpdatePlaylistDecreaseNumarPiese(playlistId);
            return _playlistDataAccess.DeleteMelodieFromPlaylist(playlistId, melodieId);
        }
    }
}
