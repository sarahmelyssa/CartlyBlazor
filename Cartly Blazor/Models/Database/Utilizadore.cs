using System;
using System.Collections.Generic;

namespace Cartly_Blazor.Models;

public partial class Utilizadores
{
    public Guid Id { get; set; }

    public string? Username { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Nome { get; set; }

    public string? Telefone { get; set; }

    public DateTime? UpdatedAt { get; set; }
    
    public AppRole Role { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<ConfirmacaoPreco> ConfirmacaoPrecos { get; set; } = new List<ConfirmacaoPreco>();

    public virtual ICollection<Mensagem> MensagemDestinatarios { get; set; } = new List<Mensagem>();

    public virtual ICollection<Mensagem> MensagemUtilizadors { get; set; } = new List<Mensagem>();

    public virtual ICollection<RegistoPreco> RegistoPrecos { get; set; } = new List<RegistoPreco>();
}
