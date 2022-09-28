using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.Domain;

namespace GymManagementSystem.Infrastructure
{
    public class GymManagementContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Visit> Visits { get; set; }

        public GymManagementContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Activity
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Group)
                .WithMany(g => g.Activities)
                .HasForeignKey(a => a.GroupId);

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Subscription)
                .WithMany(s => s.Activities)
                .HasForeignKey(a => a.SubscriptionId);

            // Client
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Subscription)
                .WithOne(s => s.Client)
                .HasForeignKey<Client>(c => c.SubscriptionId);


            // Coach
            modelBuilder.Entity<Coach>()
                .HasMany(c => c.Groups)
                .WithOne(g => g.Coach);


            // Group
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Coach)
                .WithMany(c => c.Groups)
                .HasForeignKey(g => g.CoachId);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Activities)
                .WithOne(a => a.Group);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Subscriptions)
                .WithOne(s => s.Group);

            // Subscription
            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Subscriptions)
                .HasForeignKey(g => g.GroupId);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Client)
                .WithOne(c => c.Subscription)
                .HasForeignKey<Subscription>(s => s.ClientId);

            modelBuilder.Entity<Subscription>()
                .HasMany(s => s.Activities)
                .WithOne(a => a.Subscription);

            modelBuilder.Entity<Subscription>()
                .HasMany(s => s.Visits)
                .WithOne(v => v.Subscription);

            // Visit
            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Subscription)
                .WithMany(s => s.Visits)
                .HasForeignKey(v => v.SubscriptionId);

            // Data Seeding
            modelBuilder.Entity<Activity>().HasData(
                new Activity()
                {
                    ActivityId = 1,
                    StartTime = new TimeSpan(10, 15, 0),
                    EndTime = new TimeSpan(11, 00, 0),
                    WeekdayId = 1,
                    GroupId = 1
                },
                new Activity()
                {
                    ActivityId = 2,
                    StartTime = new TimeSpan(10, 15, 0),
                    EndTime = new TimeSpan(11, 00, 0),
                    WeekdayId = 2,
                    GroupId = 1
                });

            modelBuilder.Entity<Administrator>().HasData(
                    new Administrator { AdministratorId = 1, AdminName = "Ivan", AdminSurname = "Ivanov" },
                    new Administrator { AdministratorId = 2, AdminName = "Nikolay", AdminSurname = "Ivanov" },
                    new Administrator { AdministratorId = 3, AdminName = "Kirill", AdminSurname = "Ivanov" }
                    );

            modelBuilder.Entity<Client>().HasData(
                new Client() { ClientId = 1, ClientName = "Vasya", ClientSurname = "Petrov", SubscriptionId = 1 }
                );

            modelBuilder.Entity<Coach>().HasData(
                new Coach { CoachId = 1, CoachName = "Petr", Description = "Coach Description" },
                new Coach { CoachId = 2, CoachName = "Roma", Description = "Coach Description" }
                );

            modelBuilder.Entity<Group>().HasData(
                new Group { GroupId = 1, GroupName = "fitness morning", Description = "morning exercises", CoachId = 1 },
                new Group { GroupId = 2, GroupName = "swimming", Description = "swimming exercises", CoachId = 2 }
                );

            modelBuilder.Entity<Subscription>().HasData(
                new Subscription
                {
                    SubscriptionId = 1,
                    BuyDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    DurationInDays = 30,
                    VisitingAmount = 9,
                    IsFreezed = false,
                    FreezeDate = null,
                    FreezeDaysAmount = 3,
                    GroupId = 1,
                    ClientId = 1,
                    Price = 1000
                });

            modelBuilder.Entity<Visit>().HasData(
                new Visit() { VisitId = 1, VisitDateTime = DateTime.Now }
                );
        }
    }
}