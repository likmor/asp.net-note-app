using System.ComponentModel.DataAnnotations;

namespace My_Cards.Contracts
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
