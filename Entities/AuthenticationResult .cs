public class AuthenticationResult
{
    public bool Success { get; set; }
    public string Token { get; set; }
    public string ErrorMessage { get; set; }

    public AuthenticationResult(bool success, string token, string errorMessage)
    {
        Success = success;
        Token = token;
        ErrorMessage = errorMessage;
    }
}
