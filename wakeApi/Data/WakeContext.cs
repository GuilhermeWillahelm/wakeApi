using wakeApi.Identity;
using wakeApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace wakeApi.Data
{
    public class WakeContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public WakeContext(DbContextOptions<WakeContext> options) : base(options) { }

        public DbSet<PostVideo> PostVideos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Follower> Followers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role).WithMany(r => r.UserRoles)
                .HasForeignKey(u => u.RoleId).IsRequired();

                userRole.HasOne(ur => ur.User).WithMany(r => r.UserRoles)
                .HasForeignKey(u => u.UserId).IsRequired();
            });

            builder.Entity<PostVideo>(postVideo =>
            {
                postVideo.HasOne(c => c.Channel)
                .WithMany(p => p.PostVideos)
                .HasForeignKey(k => k.ChannelId).OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
