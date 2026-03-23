using System;
using System.Collections.Generic;

namespace Cartly_Blazor.Models;

public partial class RegistoPreco
{
    public long Id { get; set; }

    public long ProdutoId { get; set; }

    public long LojaId { get; set; }

    public decimal Valor { get; set; }

    public string Moeda { get; set; } = null!;

    public int GrauCredibilidade { get; set; }

    public DateTime DataRegisto { get; set; }

    public string Estado { get; set; } = null!;

    public Guid? UtilizadorId { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<ConfirmacaoPreco> ConfirmacaoPrecos { get; set; } = new List<ConfirmacaoPreco>();

    public virtual Loja Loja { get; set; } = null!;

    public virtual Produto Produto { get; set; } = null!;

    public virtual Utilizadores? Utilizador { get; set; }
}
