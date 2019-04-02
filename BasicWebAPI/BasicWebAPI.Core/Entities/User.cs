using BasicWebAPI.Core.Interfaces;
using BasicWebAPI.Core.Shared;
using Microsoft.AspNetCore.Identity;

namespace BasicWebAPI.Core.Entities
{
    public class User : IdentityUser, IEntity<string>
    {
        public Status Status { get; set; }
    }
}
