using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OnlineClientRegistration.Common.Security;
using System.Data;

namespace OnlineClientRegistration.DataModels
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Record> Records => Set<Record>();
        public DbSet<TimeTable> TimeTables => Set<TimeTable>();
        public DbSet<CompletedRecord> CompletedRecords => Set<CompletedRecord>();
        public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();
        public DbSet<CustomTime> CustomTimes => Set<CustomTime>();
        public DbSet<CustomDate> CustomDates => Set<CustomDate>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceType>().HasData(
                new ServiceType { Id = 1, Name = "Манікюр (без покриття)", TimeRequired = 300, Price = 200 },
                new ServiceType { Id = 2, Name = "Укріплення гелем", TimeRequired = 300, Price = 100 },
                new ServiceType { Id = 3, Name = "Френч/омбре/додаткова робота", TimeRequired = 300, Price = 50 },
                new ServiceType { Id = 4, Name = "Дизайн (1 ніготь)", TimeRequired = 300, Price = 10 },
                new ServiceType { Id = 5 ,Name = "Ремонт/корекція (1 ніготь)", TimeRequired = 300, Price = 70 },
                new ServiceType { Id = 6, Name = "Зняття гель-лака", TimeRequired = 300, Price = 150 },
                new ServiceType { Id = 7, Name = "Покриття гель-лаком без укріплення (манікюр + покриття)", TimeRequired = 300, Price = 400 },
                new ServiceType { Id = 8, Name = "Нарощування нігтів (нарощування+манікюр+покритя) 1 довжина", TimeRequired = 300, Price = 700 },
                new ServiceType { Id = 9, Name = "Нарощування нігтів (нарощування+манікюр+покритя) 2 довжина", TimeRequired = 300, Price = 800 },
                new ServiceType { Id = 10, Name = "Корекція нігтів (корекція+манікюр+покриття) 1 довжина", TimeRequired = 300, Price = 500 },
                new ServiceType { Id = 11, Name = "Корекція нігтів (корекція+манікюр+покриття) 2 довжина", TimeRequired = 300, Price = 650 },
                new ServiceType { Id = 12, Name = "Зняття нарощених нігтів", TimeRequired = 300, Price = 200 },
                new ServiceType { Id = 13, Name = "Педікюр повний (без покриття)", TimeRequired = 300, Price = 500 },
                new ServiceType { Id = 14, Name = "Педікюр повний (зняття + покриття)", TimeRequired = 300, Price = 600 },
                new ServiceType { Id = 15, Name = "Педікюр пальців (зняття + покриття)", TimeRequired = 300, Price = 400 }
                );

            modelBuilder.Entity<TimeTable>().HasData(
                new TimeTable {Id = 1, NonWorkingDays="0,3,4", StartWorkingTime = TimeSpan.FromHours(10).Ticks, EndWorkingTime = TimeSpan.FromHours(21).Ticks }
                );

            modelBuilder.Entity<CustomDate>().HasData(
                new CustomDate { Date = new DateOnly(2023,12,29), IsWorkingDay = false },
                new CustomDate { Date = new DateOnly(2023,12,30), IsWorkingDay = false },
                new CustomDate { Date = new DateOnly(2023,12,28), IsWorkingDay = true }
                );

            var admin = new Client { Name = "Mykyta", PhoneNumber = "+380971706617"};
            var manager = new Client { Name = "Natasha", PhoneNumber = "+380961311033"};

            modelBuilder.Entity<Client>().HasData(
                admin,
                manager
                ); 

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserPhoneNumber = admin.PhoneNumber,  Role = AccessRoles.Admin },
                new UserRole { UserPhoneNumber = manager.PhoneNumber,Role = AccessRoles.Manager }
            );
        }
    }
}
