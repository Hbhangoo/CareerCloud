using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    class CareerCloudContext: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<ApplicantJobApplicationPoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<ApplicantProfilePoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<ApplicantSkillPoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<ApplicantWorkHistoryPoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<CompanyDescriptionPoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<CompanyJobDescriptionPoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<CompanyJobEducationPoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<CompanyJobPoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<CompanyJobSkillPoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<CompanyLocationPoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<CompanyProfilePoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<SecurityLoginPoco>().Ignore(t => t.TimeStamp);
            modelBuilder.Entity<SecurityLoginsRolePoco>().Ignore(t => t.TimeStamp);










            modelBuilder.Entity<ApplicantProfilePoco>()
            .HasMany(p => p.ApplicantEducations)
            .WithRequired(e => e.ApplicantProfiles)
            .HasForeignKey(e => e.Applicant)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
            .HasMany(p => p.ApplicantJobApplications)
            .WithRequired(j => j.ApplicantProfiles)
            .HasForeignKey(j => j.Applicant)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
            .HasMany(p => p.ApplicantResumes)
            .WithRequired(r => r.ApplicantProfiles)
            .HasForeignKey(r => r.Applicant)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
            .HasMany(p => p.ApplicantSkills)
            .WithRequired(s => s.ApplicantProfiles)
            .HasForeignKey(s => s.Applicant)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
            .HasMany(p => p.ApplicantWorkHistories)
            .WithRequired(w => w.ApplicantProfiles)
            .HasForeignKey(w => w.Applicant)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
            .HasMany(j => j.ApplicantJobApplications)
            .WithRequired(a => a.CompanyJobs)
            .HasForeignKey(a => a.Job)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
            .HasMany(j => j.CompanyJobDescriptions)
            .WithRequired(d => d.CompanyJobs)
            .HasForeignKey(d => d.Job)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
            .HasMany(j => j.CompanyJobSkills)
            .WithRequired(s => s.CompanyJobs)
            .HasForeignKey(s => s.Job)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
            .HasMany(j => j.CompanyJobEducations)
            .WithRequired(e => e.CompanyJobs)
            .HasForeignKey(e => e.Job)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyProfilePoco>()
            .HasMany(p => p.CompanyDescriptions)
            .WithRequired(d => d.CompanyProfiles)
            .HasForeignKey(d => d.Company)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyProfilePoco>()
            .HasMany(p => p.CompanyJobs)
            .WithRequired(j => j.CompanyProfiles)
            .HasForeignKey(j => j.Company)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyProfilePoco>()
            .HasMany(p => p.CompanyLocations)
            .WithRequired(l => l.CompanyProfiles)
            .HasForeignKey(l => l.Company)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityLoginPoco>()
            .HasMany(s => s.ApplicantProfiles)
            .WithRequired(a => a.SecurityLogins)
            .HasForeignKey(a => a.Login)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityLoginPoco>()
            .HasMany(s => s.SecurityLoginsLogs)
            .WithRequired(l => l.SecurityLogins)
            .HasForeignKey(l => l.Login)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityLoginPoco>()
            .HasMany(s => s.SecurityLoginsRoles)
            .WithRequired(r => r.SecurityLogins)
            .HasForeignKey(r => r.Login)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityRolePoco>()
            .HasMany(s => s.SecurityLoginsRoles)
            .WithRequired(l => l.SecurityRoles)
            .HasForeignKey(l => l.Role)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<SystemCountryCodePoco>()
            .HasMany(s => s.ApplicantProfiles)
            .WithRequired(a => a.SystemCountryCodes)
            .HasForeignKey(s => s.Country)
            .WillCascadeOnDelete(true);


            modelBuilder.Entity<SystemCountryCodePoco>()
            .HasMany(s => s.ApplicantWorkHistories)
            .WithRequired(w => w.SystemCountryCodes)
            .HasForeignKey(w => w.CountryCode)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<SystemLanguageCodePoco>()
            .HasMany(s => s.CompanyDescriptions)
            .WithRequired(a => a.SystemLanguageCodes)
            .HasForeignKey(s => s.LanguageId)
            .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
        public CareerCloudContext():base (ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString)
        {

        }

        DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }

        DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }

        DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        DbSet<SecurityLoginsLogPoco> GetSecurityLoginsLogs { get; set; }
        DbSet<SecurityLoginsRolePoco> GetSecurityLoginsRoles { get; set; }
        DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }


















    }
}
