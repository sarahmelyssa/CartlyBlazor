using System;
using System.Collections.Generic;

namespace Cartly_Blazor.Models;

public partial class Produto
{
    public long Id { get; set; }

    public string Nome { get; set; } = null!;

    public long CategoriaId { get; set; }

    public DateTime CriadoEm { get; set; }

    public string? Marca { get; set; }

    public string? CodigoBarras { get; set; }

    public string? Unidade { get; set; }

    public decimal? Quantidade { get; set; }

    public virtual Categorium Categoria { get; set; } = null!;

    public virtual ICollection<RegistoPreco> RegistoPrecos { get; set; } = new List<RegistoPreco>();
}
