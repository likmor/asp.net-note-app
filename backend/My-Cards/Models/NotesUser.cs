using Microsoft.AspNetCore.Identity;

namespace My_Cards.Models
{
    public class NotesUser : IdentityUser
    {
        public override string UserName { get; set; }

    }
}
