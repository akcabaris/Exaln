using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Exaln.Models; 

namespace Exaln.DBContext
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<IELTSExam> IELTSExams { get; set; } = default!;
        public DbSet<IELTSReadingSection> IELTSReadingSections { get; set; } = default!;
        public DbSet<IELTSReadingSectionPart> IELTSReadingSectionParts { get; set; } = default!;
        public DbSet<IELTSReadingQuestion> IELTSReadingQuestions { get; set; } = default!;


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
        }
    }
}