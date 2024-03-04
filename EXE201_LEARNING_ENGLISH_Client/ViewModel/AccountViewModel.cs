namespace EXE201_LEARNING_ENGLISH_Client.ViewModel
{
    public class LoginViewModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class RegisterViewModel : LoginViewModel
    {
        public string Name { get; set; }
        public int Role { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class AccountViewModel : RegisterViewModel
    {

    }
}
