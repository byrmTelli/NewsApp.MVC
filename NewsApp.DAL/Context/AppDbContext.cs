using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL.Context
{
    public class AppDbContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        #region DbSets
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<AppUserCategory> UserCategories { get; set; }
        public DbSet<UserApproveRecord> UserApproveRecords { get; set; }
        public DbSet<PostApproveRecord> PostApproveRecords { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseSqlite("Data Source=DailyNewsDB.db;");

        //DB Relations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region new&category Relations n-1
            modelBuilder.Entity<Post>()
                .HasOne(news => news.Category)
                .WithMany(category => category.Posts)
                .HasForeignKey(news => news.CategoryId);
            #endregion

            #region category&user 1-N
            modelBuilder.Entity<AppUserCategory>()
           .HasKey(uc => new { uc.UserId, uc.CategoryId });


            modelBuilder.Entity<AppUser>()
                .HasOne<Category>(uc => uc.UserCategory)
                .WithMany(c => c.Users)
                .HasForeignKey(uc => uc.UserCategoryId);
            #endregion

            #region Role%Permission Relations N-N
            modelBuilder.Entity<AppRolePermission>()
                .HasKey(arp => new { arp.AppRoleId, arp.PermissionId });

            modelBuilder.Entity<AppRolePermission>()
                .HasOne(arp => arp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(ar => ar.PermissionId);

            modelBuilder.Entity<AppRolePermission>()
                .HasOne(arp => arp.AppRole)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(r => r.AppRoleId);

            #endregion
        }



    }
}
