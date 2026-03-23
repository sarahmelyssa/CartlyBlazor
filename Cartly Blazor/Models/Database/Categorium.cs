using System;
using System.Collections.Generic;

namespace Cartly_Blazor.Models;

public partial class Categorium
{
    public long Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
