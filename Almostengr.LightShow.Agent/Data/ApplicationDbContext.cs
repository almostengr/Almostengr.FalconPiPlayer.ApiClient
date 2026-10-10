using Almostengr.LightShow.Agent.Services.Profiles.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.LightShow.Agent.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public required DbSet<Profile> Profiles { get; set; }
}
