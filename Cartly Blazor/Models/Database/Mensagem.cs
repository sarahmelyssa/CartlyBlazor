using System;
using System.Collections.Generic;

namespace Cartly_Blazor.Models;

public partial class Mensagem
{
    public long Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Conteudo { get; set; } = null!;

    public DateTime DataEnvio { get; set; }

    public DateTime? LidaEm { get; set; }

    public Guid? UtilizadorId { get; set; }

    public Guid? DestinatarioId { get; set; }

    public string? Tipo { get; set; }

    public virtual Utilizadores? Destinatario { get; set; }

    public virtual Utilizadores? Utilizador { get; set; }
}
