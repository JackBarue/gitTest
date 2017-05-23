using System;
using StRttmy.Model;
using System.Data.Entity;

namespace StRttmy.Repository
{
    public class StRttmyContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Courseware> Coursewares { get; set; }

        public DbSet<CoursewareResource> CoursewareResources { get; set; }

        public DbSet<StType> StTypes { get; set; }

        public DbSet<CoursewareLevel> CoursewareLevels { get; set; }

        public DbSet<StClass> StClasses { get; set; }

        public DbSet<Teaching> Teaching { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<ClassStudent> ClassStudents { get; set; }

        public DbSet<StTypeSupply> StTypeSupplies { get; set; }

        public DbSet<StLevel> StLevels { get; set; }

        public DbSet<TestQuestion> TestQuestions { get; set; }

        public DbSet<TestPaper> TestPapers { get; set; }

        public DbSet<StudentAnswer> StudentsAnswers { get; set; }

        public DbSet<StudentExamPaper> StudentExamPapers { get; set; }

        public DbSet<ClassExamPaper> ClassExamPapers { get; set; }

        public DbSet<ExamPaper> ExamPapers { get; set; }

        public DbSet<TestQuestionResource> TestQuestionResources { get; set; }

        //public DbSet<ZTreeNode> ZTreeNodes { get; set; }

        //public DbSet<CoursewareComment> CoursewareCommentes { get; set; }

        //public DbSet<PlatformZTreeNode> PlatformZTreeNodes { get; set; }

        //public DbSet<MicroCourseware> MicroCoursewares { get; set; }

        //public DbSet<MicroCoursewareResource> MicroCoursewareResources { get; set; }

        public StRttmyContext()
            : base("DefaultConnection")
        {
            //Database.CreateIfNotExists();
        }
    }
}
