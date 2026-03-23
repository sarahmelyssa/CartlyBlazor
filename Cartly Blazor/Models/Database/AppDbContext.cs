using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cartly_Blazor.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<ConfirmacaoPreco> ConfirmacaoPrecos { get; set; }

    public virtual DbSet<Loja> Lojas { get; set; }

    public virtual DbSet<Mensagem> Mensagems { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<RegistoPreco> RegistoPrecos { get; set; }

    public virtual DbSet<Utilizadores> Utilizadores { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("app_role", new[] { "user", "user_manager", "admin" })
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "oauth_authorization_status", new[] { "pending", "approved", "denied", "expired" })
            .HasPostgresEnum("auth", "oauth_client_type", new[] { "public", "confidential" })
            .HasPostgresEnum("auth", "oauth_registration_type", new[] { "dynamic", "manual" })
            .HasPostgresEnum("auth", "oauth_response_type", new[] { "code" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresEnum("storage", "buckettype", new[] { "STANDARD", "ANALYTICS", "VECTOR" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categoria_pkey");

            entity.ToTable("categoria");

            entity.HasIndex(e => e.Nome, "categoria_nome_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(120)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comentario_pkey");

            entity.ToTable("comentario");

            entity.HasIndex(e => e.CriadoEm, "idx_comentario_criado_em");

            entity.HasIndex(e => e.RegistoPrecoId, "idx_comentario_registo_preco_id");

            entity.HasIndex(e => e.UtilizadorId, "idx_comentario_utilizador_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Conteudo).HasColumnName("conteudo");
            entity.Property(e => e.CriadoEm)
                .HasDefaultValueSql("now()")
                .HasColumnName("criado_em");
            entity.Property(e => e.RegistoPrecoId).HasColumnName("registo_preco_id");
            entity.Property(e => e.UtilizadorId).HasColumnName("utilizador_id");

            entity.HasOne(d => d.RegistoPreco).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.RegistoPrecoId)
                .HasConstraintName("comentario_registo_preco_id_fkey");

            entity.HasOne(d => d.Utilizador).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.UtilizadorId)
                .HasConstraintName("comentario_utilizador_id_fkey");
        });

        modelBuilder.Entity<ConfirmacaoPreco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("confirmacao_preco_pkey");

            entity.ToTable("confirmacao_preco");

            entity.HasIndex(e => new { e.RegistoPrecoId, e.UtilizadorId }, "unique_confirmacao").IsUnique();

            entity.HasIndex(e => new { e.RegistoPrecoId, e.UtilizadorId }, "unique_confirmacao_preco").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataConfirmacao)
                .HasDefaultValueSql("now()")
                .HasColumnName("data_confirmacao");
            entity.Property(e => e.Decisao)
                .HasMaxLength(12)
                .HasColumnName("decisao");
            entity.Property(e => e.RegistoPrecoId).HasColumnName("registo_preco_id");
            entity.Property(e => e.UtilizadorId).HasColumnName("utilizador_id");

            entity.HasOne(d => d.RegistoPreco).WithMany(p => p.ConfirmacaoPrecos)
                .HasForeignKey(d => d.RegistoPrecoId)
                .HasConstraintName("confirmacao_preco_registo_preco_id_fkey");

            entity.HasOne(d => d.Utilizador).WithMany(p => p.ConfirmacaoPrecos)
                .HasForeignKey(d => d.UtilizadorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("confirmacao_preco_utilizador_id_fkey");
        });

        modelBuilder.Entity<Loja>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("loja_pkey");

            entity.ToTable("loja");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GoogleMapsLink).HasColumnName("google_maps_link");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Mensagem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mensagem_pkey");

            entity.ToTable("mensagem");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Conteudo).HasColumnName("conteudo");
            entity.Property(e => e.DataEnvio)
                .HasDefaultValueSql("now()")
                .HasColumnName("data_envio");
            entity.Property(e => e.DestinatarioId).HasColumnName("destinatario_id");
            entity.Property(e => e.LidaEm).HasColumnName("lida_em");
            entity.Property(e => e.Tipo)
                .HasDefaultValueSql("'mensagem'::character varying")
                .HasColumnType("character varying")
                .HasColumnName("tipo");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .HasColumnName("titulo");
            entity.Property(e => e.UtilizadorId).HasColumnName("utilizador_id");

            entity.HasOne(d => d.Destinatario).WithMany(p => p.MensagemDestinatarios)
                .HasForeignKey(d => d.DestinatarioId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_msg_destinatario_profiles");

            entity.HasOne(d => d.Utilizador).WithMany(p => p.MensagemUtilizadors)
                .HasForeignKey(d => d.UtilizadorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_msg_remetente_profiles");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("produto_pkey");

            entity.ToTable("produto");

            entity.HasIndex(e => e.CategoriaId, "idx_produto_categoria");

            entity.HasIndex(e => e.CodigoBarras, "unique_codigo_barras").IsUnique();

            entity.HasIndex(e => e.CodigoBarras, "uq_produto_codigo_barras").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.CodigoBarras)
                .HasMaxLength(32)
                .HasColumnName("codigo_barras");
            entity.Property(e => e.CriadoEm)
                .HasDefaultValueSql("now()")
                .HasColumnName("criado_em");
            entity.Property(e => e.Marca)
                .HasMaxLength(120)
                .HasColumnName("marca");
            entity.Property(e => e.Nome)
                .HasMaxLength(180)
                .HasColumnName("nome");
            entity.Property(e => e.Quantidade)
                .HasPrecision(10, 3)
                .HasColumnName("quantidade");
            entity.Property(e => e.Unidade)
                .HasMaxLength(20)
                .HasColumnName("unidade");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("produto_categoria_id_fkey");
        });

        modelBuilder.Entity<RegistoPreco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("registo_preco_pkey");

            entity.ToTable("registo_preco");

            entity.HasIndex(e => new { e.Estado, e.DataRegisto }, "idx_registo_preco_estado_data").IsDescending(false, true);

            entity.HasIndex(e => new { e.ProdutoId, e.LojaId, e.DataRegisto }, "idx_registo_preco_latest").IsDescending(false, false, true);

            entity.HasIndex(e => e.LojaId, "idx_registo_preco_loja");

            entity.HasIndex(e => e.ProdutoId, "idx_registo_preco_produto");

            entity.HasIndex(e => new { e.ProdutoId, e.LojaId }, "idx_registo_produto_loja");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataRegisto)
                .HasDefaultValueSql("now()")
                .HasColumnName("data_registo");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDENTE'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.GrauCredibilidade)
                .HasDefaultValue(0)
                .HasColumnName("grau_credibilidade");
            entity.Property(e => e.LojaId).HasColumnName("loja_id");
            entity.Property(e => e.Moeda)
                .HasMaxLength(3)
                .HasDefaultValueSql("'EUR'::bpchar")
                .IsFixedLength()
                .HasColumnName("moeda");
            entity.Property(e => e.ProdutoId).HasColumnName("produto_id");
            entity.Property(e => e.UtilizadorId).HasColumnName("utilizador_id");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.Loja).WithMany(p => p.RegistoPrecos)
                .HasForeignKey(d => d.LojaId)
                .HasConstraintName("registo_preco_loja_id_fkey");

            entity.HasOne(d => d.Produto).WithMany(p => p.RegistoPrecos)
                .HasForeignKey(d => d.ProdutoId)
                .HasConstraintName("registo_preco_produto_id_fkey");

            entity.HasOne(d => d.Utilizador).WithMany(p => p.RegistoPrecos)
                .HasForeignKey(d => d.UtilizadorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_registo_preco_profiles");
        });

        modelBuilder.Entity<Utilizadores>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("profiles_pkey");

            entity.ToTable("utilizadores");

            entity.HasIndex(e => e.Username, "profiles_username_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(30)
                .HasColumnName("telefone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Role)
                .HasColumnName("role")
                .HasColumnType("app_role")
                .HasDefaultValueSql("'user'::app_role");
        });
        modelBuilder.HasSequence<int>("seq_schema_version", "graphql").IsCyclic();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
