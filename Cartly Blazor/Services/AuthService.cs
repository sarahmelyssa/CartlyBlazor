using Supabase;
using Supabase.Gotrue;
using Client = Supabase.Client;

namespace Cartly_Blazor.Services;

public class AuthService
{
    private readonly Client _supabase;

    public AuthService(Client supabase)
    {
        _supabase = supabase;
    }

    public async Task<(bool Success, string Message)> RegisterAsync(
        string nome,
        string username,
        string email,
        string password,
        string? telefone = null)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return (false, "Email e palavra-passe são obrigatórios.");

            var options = new SignUpOptions
            {
                Data = new Dictionary<string, object>
                {
                    { "nome", nome ?? "" },
                    { "username", username ?? "" },
                    { "telefone", telefone ?? "" }
                }
            };

            var session = await _supabase.Auth.SignUp(email.Trim(), password, options);

            if (session?.User == null)
                return (false, "Erro ao criar conta.");

            return (true, "Conta criada com sucesso! Verifica o email.");
        }
        catch (Exception ex)
        {
            return (false, $"Erro: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> LoginAsync(string email, string password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return (false, "Email e palavra-passe são obrigatórios.");

            var session = await _supabase.Auth.SignIn(email.Trim(), password);

            if (session?.User == null)
                return (false, "Email ou palavra-passe inválidos.");

            return (true, "Login efetuado com sucesso!");
        }
        catch (Exception ex)
        {
            return (false, $"Erro: {ex.Message}");
        }
    }

    public async Task LogoutAsync()
    {
        await _supabase.Auth.SignOut();
    }

    public User? GetCurrentUser()
    {
        return _supabase.Auth.CurrentUser;
    }

    public async Task<(bool Success, string Message)> RecoverPassword(string email)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email))
                return (false, "O email é obrigatório.");

            var options = new ResetPasswordForEmailOptions(email.Trim());

            await _supabase.Auth.ResetPasswordForEmail(options);

            return (true, "Email de recuperação enviado!");
        }
        catch (Exception ex)
        {
            var msg = ex.Message ?? "";

            if (msg.Contains("over_email_send_rate_limit", StringComparison.OrdinalIgnoreCase) ||
                msg.Contains("rate limit exceeded", StringComparison.OrdinalIgnoreCase))
            {
                return (false, "Tentaste demasiadas vezes. Espera um pouco e tenta novamente.");
            }

            return (false, $"Erro: {msg}");
        }
    }
}