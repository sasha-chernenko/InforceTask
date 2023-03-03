using Microsoft.EntityFrameworkCore;
using InforceTask.Models;

namespace InforceTask.Context
{
    public class URLContext: DbContext
    {
        public virtual DbSet<URL>? Urls { get; set; }
        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<Role>? Roles { get; set; }
        public virtual DbSet<About>? About { get; set; }
        public URLContext()
        {

        }
        public URLContext(DbContextOptions<URLContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role adminRole = new() { Id = 1, Name = "admin" };
            Role userRole = new() { Id = 2, Name = "ordinary" };
            User adminUser = new() { Id = 1, Login = "admin", Password = "admin", RoleId = adminRole.Id};
            About about = new() { Id = 1, Text = "Для створення скороченого URL, я використовую айді URL-у у базі даних, як унікальний ключ,\nна основі якого і створюється Short URL.\nДля кодування URL використовуються 62 різних символи, з них 26 літер - латиниця нижнього регістру,\n26 - латиниця верхнього, та усі 10 цифр.\nНехай n = айді нашого URL із таблиці БД.\nДалі знаходиться остача від ділення n на 62 і у стрічку результату записується відповідний елемент масиву символів 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789'.\nПісля цього n ділиться на це ж 62, та за умови що n > 0 повертаємось до дії із остачею та внесенням до стрічки результату наступного символу.\nКоли n > 0 перестає бути істиною, стрічка результату розвертається(це потрібно щоб можна було її розкодувати для отримання Id) та записується у базу даних, як укорочений URL.\n" };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            modelBuilder.Entity<About>().HasData(new About[] { about });
            base.OnModelCreating(modelBuilder);
        }

    }
}
