<<<<<<< HEAD
﻿namespace Ad_Backend.Domain.Domain
{
    public class Staff
    {

    }
}
=======
namespace Ad_Backend.Domain.Domain;

public class Staff
{
    public long Id { get; set; }

    public string FullName { get; set; }
    public string Email { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public string Position { get; set; } 
}
>>>>>>> main
