using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QnA_app.Models
{
    public partial class QnA_DBContext : DbContext
    {
        public QnA_DBContext()
        {
        }

        public QnA_DBContext(DbContextOptions<QnA_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnswersTbl> AnswersTbls { get; set; } = null!;
        public virtual DbSet<CommentsTbl> CommentsTbls { get; set; } = null!;
        public virtual DbSet<QuestionsTbl> QuestionsTbls { get; set; } = null!;
        public virtual DbSet<SignupTbl> SignupTbls { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnswersTbl>(entity =>
            {
                entity.HasKey(e => e.Ansid)
                    .HasName("AnsPK");

                entity.ToTable("AnswersTBL");

                entity.HasIndex(e => e.Answer, "UQ__AnswersT__97F07BE88D48B3BC")
                    .IsUnique();

                entity.Property(e => e.Answer)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AnsweredBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.QidNavigation)
                    .WithMany(p => p.AnswersTbls)
                    .HasForeignKey(d => d.Qid)
                    .HasConstraintName("FK__AnswersTBL__Qid__5165187F");
            });

            modelBuilder.Entity<CommentsTbl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CommentsTBL");

                entity.Property(e => e.Comment)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Ans)
                    .WithMany()
                    .HasForeignKey(d => d.Ansid)
                    .HasConstraintName("FK__CommentsT__Ansid__534D60F1");
            });

            modelBuilder.Entity<QuestionsTbl>(entity =>
            {
                entity.HasKey(e => e.Qid)
                    .HasName("QuesPK");

                entity.ToTable("QuestionsTBL");

                entity.HasIndex(e => e.Question, "UQ__Question__6B387A681C56FF27")
                    .IsUnique();

                entity.Property(e => e.Askedby)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Question)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignupTbl>(entity =>
            {
                entity.ToTable("SignupTBL");

                entity.HasIndex(e => e.Username, "UQ__SignupTB__F3DBC572C34BF2DF")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Confirmpassword)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("confirmpassword");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
