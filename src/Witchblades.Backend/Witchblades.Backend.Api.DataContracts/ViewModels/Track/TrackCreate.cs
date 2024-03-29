﻿using System.ComponentModel.DataAnnotations;

namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    // Used in POST: api/Tracks
    public record TrackCreate : IViewModel
    {
        [Required]
        public string TrackName { get; set; }
        [Required]
        public Guid Album { get; set; }
        [Required]
        public int InAlbumNumber { get; set; }
        public string? Lyrics { get; set; }
        public IEnumerable<Guid>? Collaboration { get; set; }
        public string? Duration { get; set; }
        public string? TrackUrl { get; set; }
    }
}
