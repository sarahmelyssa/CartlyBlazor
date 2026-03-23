using System;
using System.Collections.Generic;

namespace Cartly_Blazor.Models;

public partial class Loja
{
    public long Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? GoogleMapsLink { get; set; }

    public virtual ICollection<RegistoPreco> RegistoPrecos { get; set; } = new List<RegistoPreco>();
}
