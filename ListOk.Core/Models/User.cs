using Microsoft.AspNetCore.Identity;

namespace ListOk.Core.Models
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<Board> Boards { get; set; } = new List<Board>();
    }
}
