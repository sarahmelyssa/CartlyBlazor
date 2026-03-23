using System;
using System.Collections.Generic;

namespace Cartly_Blazor.Models;

public partial class ConfirmacaoPreco
{
    public long Id { get; set; }

    public long RegistoPrecoId { get; set; }

    public DateTime DataConfirmacao { get; set; }

    public string Decisao { get; set; } = null!;

    public Guid? UtilizadorId { get; set; }

    public virtual RegistoPreco RegistoPreco { get; set; } = null!;

    public virtual Utilizadores? Utilizador { get; set; }
}
