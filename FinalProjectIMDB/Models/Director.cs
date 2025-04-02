using System;
using System.Collections.Generic;

namespace FinalProjectIMDB.Models;

public partial class Director
{
    public string TitleId { get; set; } = null!;

    public string NameId { get; set; } = null!;

    public virtual Title Title { get; set; } = null!;
}
