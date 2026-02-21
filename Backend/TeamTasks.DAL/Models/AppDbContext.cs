using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.DAL.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Developer> Developers { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskStatus> TaskStatuses { get; set; }

    //Modelos Manuales
    public virtual DbSet<DeveloperWorkloadDto> DeveloperWorkloads { get; set; }
    public virtual DbSet<ProjectStatusSumamryDto> ProjectStatusSumamries { get; set; }
    public virtual DbSet<UpcomingDeadlinesDto> UpcomingDeadlines { get; set; }
    public virtual DbSet<DeveloperDelayRiskPredictionDto> DeveloperDelayRiskPredictions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.DeveloperId).HasName("PK__Develope__DE084CF1213BBF71");

            entity.ToTable("Developers", "Core");

            entity.Property(e => e.DeveloperId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("PK__Priority__D0A3D0BEDA2BC7B3");

            entity.ToTable("Priority", "Core");

            entity.Property(e => e.PriorityId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABEF01852B2C2");

            entity.ToTable("Projects", "Core");

            entity.Property(e => e.ProjectId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ClientName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Statud).WithMany(p => p.Projects)
                .HasForeignKey(d => d.StatudId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Projects__Statud__2F10007B");
        });

        modelBuilder.Entity<ProjectStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__ProjectS__C8EE2063162A6521");

            entity.ToTable("ProjectStatuses", "Core");

            entity.Property(e => e.StatusId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949B10A3D946C");

            entity.ToTable("Tasks", "Core");

            entity.Property(e => e.TaskId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Assigned).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.AssignedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__AssignedI__45F365D3");

            entity.HasOne(d => d.Priority).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__PriorityI__47DBAE45");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__ProjectId__44FF419A");

            entity.HasOne(d => d.Status).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__StatusId__46E78A0C");
        });

        modelBuilder.Entity<TaskStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__TaskStat__C8EE206378C76D6A");

            entity.ToTable("TaskStatuses", "Core");

            entity.Property(e => e.StatusId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        //Registro Modelos Manuales
        modelBuilder.Entity<DeveloperWorkloadDto>(entity =>
        {
            entity.HasNoKey();
            entity.ToView(null);
        });

        modelBuilder.Entity<ProjectStatusSumamryDto>(entity =>
        {
            entity.HasNoKey();
            entity.ToView(null);
        });

        modelBuilder.Entity<UpcomingDeadlinesDto>(entity =>
        {
            entity.HasNoKey();
            entity.ToView(null);
        });

        modelBuilder.Entity<DeveloperDelayRiskPredictionDto>(entity =>
        {
            entity.HasNoKey();
            entity.ToView(null);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
