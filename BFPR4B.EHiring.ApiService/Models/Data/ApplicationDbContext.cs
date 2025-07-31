using BFPR4B.EHiring.ApiService.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BFPR4B.EHiring.ApiService.Models.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tblapplication> Tblapplications { get; set; }

    public virtual DbSet<Tblapplicationattachment> Tblapplicationattachments { get; set; }

    public virtual DbSet<Tblapplicationuser> Tblapplicationusers { get; set; }

    public virtual DbSet<Tblotp> Tblotps { get; set; }

    // Add this for your stored procedure result DTO
    public DbSet<ApplicationUserModel> GetApplicationUserDetails { get; set; } // Renamed for clarity

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("name=DefaultSQLConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tblapplication>(entity =>
        {
            entity.HasKey(e => e.Applicationno);

            entity.ToTable("tblapplication");

            entity.Property(e => e.Applicationno).HasColumnName("applicationno");
            entity.Property(e => e.Applicationcode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("applicationcode");
            entity.Property(e => e.Applicationdate)
                .HasDefaultValueSql("(((1)/(1))/(1900))")
                .HasColumnType("smalldatetime")
                .HasColumnName("applicationdate");
            entity.Property(e => e.Jobno).HasColumnName("jobno");
            entity.Property(e => e.Remarks)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("remarks");
            entity.Property(e => e.Statusno).HasColumnName("statusno");
            entity.Property(e => e.Userno).HasColumnName("userno");
        });

        modelBuilder.Entity<Tblapplicationattachment>(entity =>
        {
            entity.HasKey(e => e.Attachmentno);

            entity.ToTable("tblapplicationattachments");

            entity.Property(e => e.Attachmentno).HasColumnName("attachmentno");
            entity.Property(e => e.Applicationno).HasColumnName("applicationno");
            entity.Property(e => e.Attachmentname)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("attachmentname");
            entity.Property(e => e.Attachmenttype)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("attachmenttype");
            entity.Property(e => e.Dateencoded)
                .HasDefaultValue(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("smalldatetime")
                .HasColumnName("dateencoded");
            entity.Property(e => e.Datemodified)
                .HasDefaultValue(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("smalldatetime")
                .HasColumnName("datemodified");
            entity.Property(e => e.Filetype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasComment("PNG, JPEG, PDF, DOCX")
                .HasColumnName("filetype");
            entity.Property(e => e.Jobno).HasColumnName("jobno");
            entity.Property(e => e.Remarks)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("remarks");
            entity.Property(e => e.Statusno).HasColumnName("statusno");
            entity.Property(e => e.Userno).HasColumnName("userno");
        });

        modelBuilder.Entity<Tblapplicationuser>(entity =>
        {
            entity.HasKey(e => e.Userno).HasName("PK_tblapplications");

            entity.ToTable("tblapplicationuser");

            entity.Property(e => e.Userno).HasColumnName("userno");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Barangayno).HasColumnName("barangayno");
            entity.Property(e => e.Birthdate)
                .HasDefaultValueSql("(((1)/(1))/(1900))")
                .HasColumnType("smalldatetime")
                .HasColumnName("birthdate");
            entity.Property(e => e.Bloodtype)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("bloodtype");
            entity.Property(e => e.Cityno).HasColumnName("cityno");
            entity.Property(e => e.Civilstatus).HasColumnName("civilstatus");
            entity.Property(e => e.Contactno)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("contactno");
            entity.Property(e => e.Course).HasColumnName("course");
            entity.Property(e => e.Dateencoded)
                .HasDefaultValueSql("(((1)/(1))/(1900))")
                .HasColumnType("smalldatetime")
                .HasColumnName("dateencoded");
            entity.Property(e => e.Dateupdated)
                .HasDefaultValueSql("(((1)/(1))/(1900))")
                .HasColumnType("smalldatetime")
                .HasColumnName("dateupdated");
            entity.Property(e => e.Emailaddress)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("emailaddress");
            entity.Property(e => e.Firstname)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("firstname");
            entity.Property(e => e.Fullname)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("fullname");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Gsis)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("gsis");
            entity.Property(e => e.Height)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("height");
            entity.Property(e => e.Isverifiedcontact).HasColumnName("isverifiedcontact");
            entity.Property(e => e.Isverifiedemail).HasColumnName("isverifiedemail");
            entity.Property(e => e.Lastname)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("lastname");
            entity.Property(e => e.Miname)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("miname");
            entity.Property(e => e.Nbi)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("nbi");
            entity.Property(e => e.Nbiissueddate)
                .HasDefaultValueSql("(((1)/(1))/(1900))")
                .HasColumnType("smalldatetime")
                .HasColumnName("nbiissueddate");
            entity.Property(e => e.Pagibig)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("pagibig");
            entity.Property(e => e.Passwordsalt)
                .HasDefaultValueSql("(0x)")
                .HasColumnName("passwordsalt");
            entity.Property(e => e.Philhealth)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("philhealth");
            entity.Property(e => e.Presentaddress)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("presentaddress");
            entity.Property(e => e.Presentbarangayno).HasColumnName("presentbarangayno");
            entity.Property(e => e.Presentcityno).HasColumnName("presentcityno");
            entity.Property(e => e.Presentprovinceno).HasColumnName("presentprovinceno");
            entity.Property(e => e.Presentregionno).HasColumnName("presentregionno");
            entity.Property(e => e.Profilepictype)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("profilepictype");
            entity.Property(e => e.Profileurl)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("profileurl");
            entity.Property(e => e.Provinceno).HasColumnName("provinceno");
            entity.Property(e => e.Regionno).HasColumnName("regionno");
            entity.Property(e => e.Religion).HasColumnName("religion");
            entity.Property(e => e.Sss)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("sss");
            entity.Property(e => e.Streetaddress)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("streetaddress");
            entity.Property(e => e.Suffix)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("suffix");
            entity.Property(e => e.Tin)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("tin");
            entity.Property(e => e.Userpass)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("userpass");
            entity.Property(e => e.Weight)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("weight");
        });

        modelBuilder.Entity<Tblotp>(entity =>
        {
            entity.HasKey(e => e.Detno);

            entity.ToTable("tblotp");

            entity.Property(e => e.Detno).HasColumnName("detno");
            entity.Property(e => e.Channel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasComment("SMS, Email,")
                .HasColumnName("channel");
            entity.Property(e => e.Dateencoded)
                .HasDefaultValueSql("(((1)/(1))/(1900))")
                .HasColumnType("smalldatetime")
                .HasColumnName("dateencoded");
            entity.Property(e => e.Expiration)
                .HasDefaultValueSql("(((1)/(1))/(1900))")
                .HasColumnType("smalldatetime")
                .HasColumnName("expiration");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("ipaddress");
            entity.Property(e => e.IsUsed).HasColumnName("is_used");
            entity.Property(e => e.Otpcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("otpcode");
            entity.Property(e => e.Otptype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasComment("'login', 'reset_password'")
                .HasColumnName("otptype");
            entity.Property(e => e.System)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("system");
            entity.Property(e => e.Userno).HasColumnName("userno");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
