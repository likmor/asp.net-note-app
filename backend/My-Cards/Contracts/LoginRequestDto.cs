using System.ComponentModel.DataAnnotations;

namespace My_Cards.Contracts
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
