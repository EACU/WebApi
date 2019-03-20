using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EACA_API.Models.Account;
using EACA_API.Models.AccountEntities.Tokens;
using EACA_API.Models.Institute;
using System;

namespace EACA_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApiUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options) {}

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<SubjectAssignment> SubjectAssignments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //InitializeDataBase(builder);

            builder.Entity<RefreshToken>()
                .HasAlternateKey(c => c.UserId)
                .HasName("refreshToken_UserId");

            builder.Entity<RefreshToken>()
                .HasAlternateKey(c => c.Token)
                .HasName("refreshToken_Token");

            builder.Entity<SubjectAssignment>()
                .HasKey(x => new { x.SubjectId, x.InstructorId });

            builder.Entity<StudentGroup>()
                .HasOne(x => x.Student)
                .WithMany(x => x.StudentGroups)
                .HasForeignKey(x => x.StudentId);

            builder.Entity<StudentGroup>()
                .HasOne(x => x.Group)
                .WithMany(x => x.StudentGroups)
                .HasForeignKey(x => x.GroupId);

            builder.Entity<StudentGroup>()
                .HasKey(x => new { x.StudentId, x.GroupId });

            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Тестовые данные 
        /// </summary>
        //private void InitializeDataBase(ModelBuilder builder)
        //{
        //    Faculty faculty1 = new Faculty { Id = GetGuid(), Name = "Творческих индустрий" };
        //    builder.Entity<Faculty>().HasData(faculty1);

        //    Department department1 = new Department { Id = GetGuid(), FacultyId = faculty1.Id, Name = "Искусства и гуманитарные науки", Code = "50.03.01" };
        //    Department department2 = new Department { Id = GetGuid(), FacultyId = faculty1.Id, Name = "Прикладная информатика", Code = "09.03.03" };
        //    builder.Entity<Department>().HasData(department1, department2);

        //    PlacesInfo placesInfo1 = new PlacesInfo { Id = GetGuid(), MainContestPlaces = 10, SpecialQuotaPlaces = 2, TargetPlaces = 0, NotBudetPlaces = 6 };
        //    PlacesInfo placesInfo2 = new PlacesInfo { Id = GetGuid(), MainContestPlaces = 10, SpecialQuotaPlaces = 2, TargetPlaces = 0, NotBudetPlaces = 6 };
        //    PlacesInfo placesInfo3 = new PlacesInfo { Id = GetGuid(), MainContestPlaces = 10, SpecialQuotaPlaces = 2, TargetPlaces = 2, NotBudetPlaces = 8 };
        //    PlacesInfo placesInfo4 = new PlacesInfo { Id = GetGuid(), MainContestPlaces = 9, SpecialQuotaPlaces = 1, TargetPlaces = 8, NotBudetPlaces = 10 };
        //    PlacesInfo placesInfo5 = new PlacesInfo { Id = GetGuid(), MainContestPlaces = 9, SpecialQuotaPlaces = 1, TargetPlaces = 0, NotBudetPlaces = 5 };
        //    PlacesInfo placesInfo6 = new PlacesInfo { Id = GetGuid(), MainContestPlaces = 9, SpecialQuotaPlaces = 1, TargetPlaces = 0, NotBudetPlaces = 5 };
        //    PlacesInfo placesInfo7 = new PlacesInfo { Id = GetGuid(), MainContestPlaces = 9, SpecialQuotaPlaces = 1, TargetPlaces = 0, NotBudetPlaces = 20 };
        //    PlacesInfo placesInfo8 = new PlacesInfo { Id = GetGuid(), MainContestPlaces = 9, SpecialQuotaPlaces = 1, TargetPlaces = 0, NotBudetPlaces = 20 };
        //    builder.Entity<PlacesInfo>().HasData(placesInfo1, placesInfo2, placesInfo3, placesInfo4, placesInfo5, placesInfo6, placesInfo7, placesInfo8);

        //    Course courseVC1 = new Course { Id = GetGuid(), Name = "Визуальные коммуникации", Year = 2018, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo1.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };
        //    Course courseVC2 = new Course { Id = GetGuid(), Name = "Визуальные коммуникации", Year = 2017, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo1.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };
        //    Course courseVC3 = new Course { Id = GetGuid(), Name = "Визуальные коммуникации", Year = 2016, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo1.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };
        //    Course courseVC4 = new Course { Id = GetGuid(), Name = "Визуальные коммуникации", Year = 2015, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo1.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };

        //    Course courseDM1 = new Course { Id = GetGuid(), Name = "Танец и современная пластическая культура", Year = 2018, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo2.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };
        //    Course courseDM2 = new Course { Id = GetGuid(), Name = "Танец и современная пластическая культура", Year = 2017, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo2.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };
        //    Course courseDM3 = new Course { Id = GetGuid(), Name = "Танец и современная пластическая культура", Year = 2016, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo2.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };
        //    Course courseDM4 = new Course { Id = GetGuid(), Name = "Танец и современная пластическая культура", Year = 2015, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo2.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };

        //    Course courseCM1 = new Course { Id = GetGuid(), Name = "Технологии управления в сфере культуры", Year = 2018, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo3.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };
        //    Course courseCM2 = new Course { Id = GetGuid(), Name = "Технологии управления в сфере культуры", Year = 2017, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo3.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };
        //    Course courseCM3 = new Course { Id = GetGuid(), Name = "Технологии управления в сфере культуры", Year = 2016, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo3.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };
        //    Course courseCM4 = new Course { Id = GetGuid(), Name = "Технологии управления в сфере культуры", Year = 2015, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo3.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };

        //    Course courseCMZ1 = new Course { Id = GetGuid(), Name = "Технологии управления в сфере культуры", Year = 2018, PeriodOfStudy = "5 лет", PlacesInfoId = placesInfo4.Id, DepartmentId = department1.Id, FormEducation = FormEducation.PartTime };
        //    Course courseCMZ2 = new Course { Id = GetGuid(), Name = "Технологии управления в сфере культуры", Year = 2017, PeriodOfStudy = "5 лет", PlacesInfoId = placesInfo4.Id, DepartmentId = department1.Id, FormEducation = FormEducation.PartTime };
        //    Course courseCMZ3 = new Course { Id = GetGuid(), Name = "Технологии управления в сфере культуры", Year = 2016, PeriodOfStudy = "5 лет", PlacesInfoId = placesInfo4.Id, DepartmentId = department1.Id, FormEducation = FormEducation.PartTime };
        //    Course courseCMZ4 = new Course { Id = GetGuid(), Name = "Технологии управления в сфере культуры", Year = 2015, PeriodOfStudy = "5 лет", PlacesInfoId = placesInfo4.Id, DepartmentId = department1.Id, FormEducation = FormEducation.PartTime };
        //    Course courseCMZ5 = new Course { Id = GetGuid(), Name = "Технологии управления в сфере культуры", Year = 2014, PeriodOfStudy = "5 лет", PlacesInfoId = placesInfo4.Id, DepartmentId = department1.Id, FormEducation = FormEducation.PartTime };

        //    Course courseJC1 = new Course { Id = GetGuid(), Name = "Журналистика в области культуры", Year = 2018, PeriodOfStudy = "5 лет", PlacesInfoId = placesInfo5.Id, DepartmentId = department1.Id, FormEducation = FormEducation.PartTime };
        //    Course courseJC2 = new Course { Id = GetGuid(), Name = "Журналистика в области культуры", Year = 2016, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo5.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };
        //    Course courseJC3 = new Course { Id = GetGuid(), Name = "Журналистика в области культуры", Year = 2015, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo5.Id, DepartmentId = department1.Id, FormEducation = FormEducation.FullTime };

        //    Course courseAS1 = new Course { Id = GetGuid(), Name = "Арт и спорт маркетинг", Year = 2018, PeriodOfStudy = "5 лет", PlacesInfoId = placesInfo6.Id, DepartmentId = department1.Id, FormEducation = FormEducation.PartTime };

        //    Course courseCA1 = new Course { Id = GetGuid(), Name = "Цифровое искусство", Year = 2018, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo7.Id, DepartmentId = department2.Id, FormEducation = FormEducation.FullTime };

        //    Course coursePI1 = new Course { Id = GetGuid(), Name = "Прикладная информатика в социально-культурной сфере", Year = 2017, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo8.Id, DepartmentId = department2.Id, FormEducation = FormEducation.FullTime };
        //    Course coursePI2 = new Course { Id = GetGuid(), Name = "Прикладная информатика в социально-культурной сфере", Year = 2016, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo8.Id, DepartmentId = department2.Id, FormEducation = FormEducation.FullTime };
        //    Course coursePI3 = new Course { Id = GetGuid(), Name = "Прикладная информатика в социально-культурной сфере", Year = 2015, PeriodOfStudy = "4 года", PlacesInfoId = placesInfo8.Id, DepartmentId = department2.Id, FormEducation = FormEducation.FullTime };

        //    builder.Entity<Course>()
        //        .HasData(
        //            courseVC1, courseVC2, courseVC3, courseVC4,
        //            courseDM1, courseDM2, courseDM3, courseDM4,
        //            courseCM1, courseCM2, courseCM3, courseCM4,
        //            courseCMZ1, courseCMZ2, courseCMZ3, courseCMZ4, courseCMZ5,
        //            courseJC1, courseJC2, courseJC3,
        //            courseAS1,
        //            courseCA1,
        //            coursePI1, coursePI2, coursePI3
        //        );

        //    Group groupVC1 = new Group { Id = GetGuid(), CourseId = courseVC1.Id, Active = true, Number = 123 };
        //    Group groupVC2 = new Group { Id = GetGuid(), CourseId = courseVC2.Id, Active = true, Number = 223 };
        //    Group groupVC3 = new Group { Id = GetGuid(), CourseId = courseVC3.Id, Active = true, Number = 323 };
        //    Group groupVC4 = new Group { Id = GetGuid(), CourseId = courseVC4.Id, Active = true, Number = 423 };

        //    Group groupDM1 = new Group { Id = GetGuid(), CourseId = courseDM1.Id, Active = true, Number = 124 };
        //    Group groupDM2 = new Group { Id = GetGuid(), CourseId = courseDM2.Id, Active = true, Number = 224 };
        //    Group groupDM3 = new Group { Id = GetGuid(), CourseId = courseDM3.Id, Active = true, Number = 324 };
        //    Group groupDM4 = new Group { Id = GetGuid(), CourseId = courseDM4.Id, Active = true, Number = 424 };

        //    Group groupCM1 = new Group { Id = GetGuid(), CourseId = courseCM1.Id, Active = true, Number = 121 };
        //    Group groupCM2 = new Group { Id = GetGuid(), CourseId = courseCM2.Id, Active = true, Number = 221 };
        //    Group groupCM3 = new Group { Id = GetGuid(), CourseId = courseCM3.Id, Active = true, Number = 321 };
        //    Group groupCM4 = new Group { Id = GetGuid(), CourseId = courseCM4.Id, Active = true, Number = 421 };

        //    Group groupCMZ1 = new Group { Id = GetGuid(), CourseId = courseCMZ1.Id, Active = true, Number = 131 };
        //    Group groupCMZ12 = new Group { Id = GetGuid(), CourseId = courseCMZ1.Id, Active = true, Number = 132 }; // тест двойной группы
        //    Group groupCMZ2 = new Group { Id = GetGuid(), CourseId = courseCMZ2.Id, Active = true, Number = 231 };
        //    Group groupCMZ3 = new Group { Id = GetGuid(), CourseId = courseCMZ3.Id, Active = true, Number = 331 };
        //    Group groupCMZ4 = new Group { Id = GetGuid(), CourseId = courseCMZ4.Id, Active = true, Number = 431 };
        //    Group groupCMZ5 = new Group { Id = GetGuid(), CourseId = courseCMZ5.Id, Active = true, Number = 531 };

        //    Group groupJC1 = new Group { Id = GetGuid(), CourseId = courseJC1.Id, Active = true, Number = 134 };
        //    Group groupJC2 = new Group { Id = GetGuid(), CourseId = courseJC2.Id, Active = true, Number = 322 };
        //    Group groupJC3 = new Group { Id = GetGuid(), CourseId = courseJC3.Id, Active = true, Number = 422 };

        //    Group groupAS1 = new Group { Id = GetGuid(), CourseId = courseAS1.Id, Active = true, Number = 133 };

        //    Group groupCA1 = new Group { Id = GetGuid(), CourseId = courseCA1.Id, Active = true, Number = 126 };

        //    Group groupPI1 = new Group { Id = GetGuid(), CourseId = coursePI1.Id, Active = true, Number = 225 };
        //    Group groupPI2 = new Group { Id = GetGuid(), CourseId = coursePI2.Id, Active = true, Number = 325 };
        //    Group groupPI3 = new Group { Id = GetGuid(), CourseId = coursePI3.Id, Active = true, Number = 425 };

        //    builder.Entity<Group>()
        //        .HasData(
        //            groupVC1, groupVC2, groupVC3, groupVC4,
        //            groupDM1, groupDM2, groupDM3, groupDM4,
        //            groupCM1, groupCM2, groupCM3, groupCM4,
        //            groupCMZ1, groupCMZ2, groupCMZ3, groupCMZ4, groupCMZ5,
        //            groupJC1, groupJC2, groupJC3,
        //            groupAS1,
        //            groupCA1,
        //            groupPI1, groupPI2, groupPI3
        //        );
        //}

        private static string GetGuid() => Guid.NewGuid().ToString();
    }
}
