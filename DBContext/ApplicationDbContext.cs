using Exaln.Entities;
using Exaln.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Exaln.DBContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<IELTSExam> IELTSExams { get; set; } = default!;
        public DbSet<IELTSReadingSection> IELTSReadingSections { get; set; } = default!;
        public DbSet<IELTSReadingSectionPart> IELTSReadingSectionParts { get; set; } = default!;
        public DbSet<IELTSReadingQuestion> IELTSReadingQuestions { get; set; } = default!;
        public DbSet<IELTSExamAttempt> IELTSExamAttempts { get; set; } = default!;
        public DbSet<IELTSExamAttemptModule> IELTSExamAttemptModules { get; set; } = default!;
        public DbSet<IELTSExamAttemptReadingAnswer> IELTSExamAttemptReadingAnswers { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IELTSReadingSection>()
                .HasOne(rs => rs.Exam)
                .WithMany(e => e.ReadingSections)
                .HasForeignKey(rs => rs.ExamID)
                .IsRequired(false) 
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<IELTSReadingSectionPart>()
                .HasOne(rsp => rsp.ReadingSection)
                .WithMany(rs => rs.ReadingSectionParts)
                .HasForeignKey(rsp => rsp.ReadingSectionID)
                .IsRequired(false) 
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<IELTSReadingQuestion>()
                .HasOne(rq => rq.ReadingSectionPart)
                .WithMany(rsp => rsp.ReadingQuestions)
                .HasForeignKey(rq => rq.ReadingSectionPartID)
                .IsRequired(false) 
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<IELTSExamAttempt>(e =>
            {
                e.HasKey(x => x.ExamAttemptID);

                e.Property(x => x.UserID)
                 .HasMaxLength(36);

                e.HasMany(x => x.AttemptModules)
                 .WithOne(m => m.ExamAttempt)
                 .HasForeignKey(m => m.ExamAttemptID)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<IELTSExamAttemptModule>(e =>
            {
                e.HasKey(x => x.ExamAttemptModuleID);

                e.Property(x => x.BandScore)
                 .HasPrecision(3, 1);

                e.Property(x => x.ProgressJson)
                 .HasColumnType("jsonb");
            });

            builder.Entity<IELTSExamAttemptReadingAnswer>(e =>
            {
                e.HasKey(x => x.ExamAttemptReadingAnswerID);

                e.Property(x => x.Answer)
                 .IsRequired()
                 .HasDefaultValue("");

                e.HasOne(x => x.ExamAttemptModule)
                 .WithMany(m => m.ReadingAnswers)
                 .HasForeignKey(x => x.ExamAttemptModuleID)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.ReadingQuestion)
                 .WithMany()
                 .HasForeignKey(x => x.ReadingQuestionID)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(x => new { x.ExamAttemptModuleID, x.ReadingQuestionID })
                 .IsUnique();
            });

            builder.Entity<IELTSReadingQuestion>(e =>
            {
                e.HasKey(x => x.ReadingQuestionID);
            });
        }
    }
}