using System;
using System.Collections.Generic;

namespace FinalProjectIMDB.Models;

public partial class Title
{
    public string TitleId { get; set; } = null!;

    public string? TitleType { get; set; }

    public string? PrimaryTitle { get; set; }

    public string? OriginalTitle { get; set; }

    public bool? IsAdult { get; set; }

    public short? StartYear { get; set; }

    public short? EndYear { get; set; }

    public short? RuntimeMinutes { get; set; }

    public virtual ICollection<Director> Directors { get; set; } = new List<Director>();
}
