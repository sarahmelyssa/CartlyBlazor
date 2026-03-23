using System;
using System.Collections.Generic;

namespace Cartly_Blazor.Models;

public partial class Comentario
{
    public long Id { get; set; }

    public string Conteudo { get; set; } = null!;

    public DateTime CriadoEm { get; set; }

    public long RegistoPrecoId { get; set; }

    public Guid UtilizadorId { get; set; }

    public virtual RegistoPreco RegistoPreco { get; set; } = null!;

    public virtual Utilizadores Utilizador { get; set; } = null!;
}
