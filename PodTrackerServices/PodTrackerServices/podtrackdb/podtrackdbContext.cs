﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PodTrackerServices.podtrackdb
{
    public partial class podtrackdbContext : DbContext
    {
        public podtrackdbContext()
        {
        }

        public podtrackdbContext(DbContextOptions<podtrackdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FollowedPodcast> FollowedPodcast { get; set; }
        public virtual DbSet<PodUser> PodUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseMySQL("server=192.168.0.105;port=3306;user=test;password=test;database=podtrackdb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FollowedPodcast>(entity =>
            {
                entity.ToTable("FollowedPodcast", "podtrackdb");

                entity.HasIndex(e => e.UserId)
                    .HasName("ID_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Rss)
                    .HasColumnName("RSS")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.TimeStep).HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FollowedPodcast)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("ID");
            });

            modelBuilder.Entity<PodUser>(entity =>
            {
                entity.ToTable("PodUser", "podtrackdb");

                entity.HasIndex(e => e.Username)
                    .HasName("Username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Password)
                    .HasMaxLength(265)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}