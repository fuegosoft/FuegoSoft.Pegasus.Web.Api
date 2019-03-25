using System;
using System.IO;
using FuegoSoft.Pegasus.Lib.Core.Commands;
using FuegoSoft.Pegasus.Lib.Data.Helper;
using FuegoSoft.Pegasus.Lib.Data.Interface.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class AyudaContext : DbContext
    {

        public AyudaContext()
        {
        }

        public AyudaContext(DbContextOptions<AyudaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Career> Career { get; set; }
        public virtual DbSet<CareerCategory> CareerCategory { get; set; }
        public virtual DbSet<CareerType> CareerType { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<ContractLinkExpiration> ContractLinkExpiration { get; set; }
        public virtual DbSet<ContractVoucher> ContractVoucher { get; set; }
        public virtual DbSet<Contractor> Contractor { get; set; }
        public virtual DbSet<ContractorCareer> ContractorCareer { get; set; }
        public virtual DbSet<ContractorCompany> ContractorCompany { get; set; }
        public virtual DbSet<ContractorProfile> ContractorProfile { get; set; }
        public virtual DbSet<ContractorSkill> ContractorSkill { get; set; }
        public virtual DbSet<ContractorSkillStatistics> ContractorSkillStatistics { get; set; }
        public virtual DbSet<ExceptionLog> ExceptionLog { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<SkillCareer> SkillCareer { get; set; }
        public virtual DbSet<Subscriber> Subscriber { get; set; }
        public virtual DbSet<TokenBlackList> TokenBlackList { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserBanned> UserBanned { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }
        public virtual DbSet<UserCredential> UserCredential { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = new GetJsonDataHelper("ConnectionStrings:DefaultConnection").GetJsonValue();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<UserCredential>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.ToTable("UserCredential");
                entity.Property(e => e.Username).HasMaxLength(50);
                entity.Property(e => e.EmailAddress).HasMaxLength(150);
                entity.Property(e => e.ContactNumber).HasMaxLength(50);
                entity.Property(e => e.LoginKey);
                entity.Property(e => e.UserKey);
                entity.Property(e => e.BirthDate).HasColumnType("datetime");
                entity.Property(e => e.UserType);
            });

            modelBuilder.Entity<Career>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_CareerName");

                entity.Property(e => e.CareerId).HasColumnName("CareerID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CareerCategory>(entity =>
            {
                entity.HasIndex(e => new { e.CareerId, e.Name, e.Description })
                    .HasName("IX_CareerNameDescription");

                entity.Property(e => e.CareerCategoryId).HasColumnName("CareerCategoryID");

                entity.Property(e => e.CareerId).HasColumnName("CareerID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CareerType>(entity =>
            {
                entity.HasIndex(e => new { e.CareerCategoryId, e.Name, e.Description })
                    .HasName("IX_CareerCategoryIDNameDescription");

                entity.Property(e => e.CareerTypeId).HasColumnName("CareerTypeID");

                entity.Property(e => e.CareerCategoryId).HasColumnName("CareerCategoryID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.Address, e.City, e.Province, e.EmailAddress, e.MobileNumber, e.TelephoneNumber })
                    .HasName("IX_NameAddressCityProvinceEmailMobileTelephone");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.BusinessLine)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("('Unknown')");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CompanyKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.MobileNumber).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TelephoneNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.Price, e.Duration })
                    .HasName("IX_NamePriceVoucherCodeDuration");

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.ContractKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Price).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<ContractLinkExpiration>(entity =>
            {
                entity.HasKey(e => e.ContractExpirationId)
                    .HasName("PK__Contract__1831CB273B70DAFD");

                entity.HasIndex(e => e.ExtractKey)
                    .HasName("IX_ContractLinkExpiration");

                entity.Property(e => e.ContractExpirationId).HasColumnName("ContractExpirationID");

                entity.Property(e => e.Base64Link)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.ExtractKey)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.IsUsed).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ContractVoucher>(entity =>
            {
                entity.HasIndex(e => new { e.ContractId, e.VoucherCode, e.CodeDuration })
                    .HasName("IX_VoucherCodes");

                entity.Property(e => e.ContractVoucherId).HasColumnName("ContractVoucherID");

                entity.Property(e => e.CodeDuration).HasColumnType("datetime");

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Duration).HasColumnType("datetime");

                entity.Property(e => e.TotalDiscount).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.VoucherCode)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.VoucherKey).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Contractor>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.SubscriberKey, e.ContractorKey })
                    .HasName("IX_UserSubscriberIDContractorKeyEffectivityDate");

                entity.Property(e => e.ContractorId).HasColumnName("ContractorID");

                entity.Property(e => e.ContractorKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndEffectivityDate).HasColumnType("datetime");

                entity.Property(e => e.StartEffectivityDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SubscriberKey)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ContractorCareer>(entity =>
            {
                entity.HasIndex(e => new { e.ContractorId, e.CareerId, e.Name, e.TotalYearOfExperience })
                    .HasName("IX_ContractorCareer");

                entity.Property(e => e.ContractorCareerId).HasColumnName("ContractorCareerID");

                entity.Property(e => e.CareerId).HasColumnName("CareerID");

                entity.Property(e => e.ContractorId).HasColumnName("ContractorID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.TotalYearOfExperience).HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<ContractorCompany>(entity =>
            {
                entity.HasIndex(e => new { e.ContractorId, e.CompanyId, e.Position, e.YearsOfExperience })
                    .HasName("IX_IDPositionExperience");

                entity.Property(e => e.ContractorCompanyId).HasColumnName("ContractorCompanyID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.ContractorId).HasColumnName("ContractorID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Position).HasMaxLength(100);

                entity.Property(e => e.YearsOfExperience).HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<ContractorProfile>(entity =>
            {
                entity.HasIndex(e => new { e.FirstName, e.LastName, e.Citizenship, e.Age })
                    .HasName("IX_FirstLastNameCitizenshipAge");

                entity.Property(e => e.ContractorProfileId).HasColumnName("ContractorProfileID");

                entity.Property(e => e.Citizenship)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('Filipino')");

                entity.Property(e => e.CivilStatus)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('Single')");

                entity.Property(e => e.ContractorId).HasColumnName("ContractorID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Height).HasMaxLength(25);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.MotherMaidenName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.PlaceOfBirth).HasMaxLength(100);

                entity.Property(e => e.Spouse).HasMaxLength(150);

                entity.Property(e => e.Weight).HasMaxLength(25);
            });

            modelBuilder.Entity<ContractorSkill>(entity =>
            {
                entity.HasIndex(e => new { e.ContractorId, e.SkillId, e.YearsOfExperience })
                    .HasName("IX_SkillContractorIDYearOfExperience");

                entity.Property(e => e.ContractorSkillId).HasColumnName("ContractorSkillID");

                entity.Property(e => e.ContractorId).HasColumnName("ContractorID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.Property(e => e.YearsOfExperience)
                    .HasColumnType("decimal(5, 2)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ContractorSkillStatistics>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.ContractorId, e.SkillId, e.Rating })
                    .HasName("IX_UserContractorSkillRating");

                entity.Property(e => e.ContractorSkillStatisticsId).HasColumnName("ContractorSkillStatisticsID");

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.Property(e => e.ContractorId).HasColumnName("ContractorID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rating).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ExceptionLog>(entity =>
            {
                entity.Property(e => e.ExceptionLogId).HasColumnName("ExceptionLogID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InnerException).IsRequired();

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.Source).IsRequired();

                entity.Property(e => e.StackTrace).IsRequired();
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasIndex(e => new { e.RegionId, e.Psgcode, e.Description, e.Code })
                    .HasName("IX_RegionDescriptionCode");

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Psgcode)
                    .IsRequired()
                    .HasColumnName("PSGCode")
                    .HasMaxLength(255);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasIndex(e => new { e.Description, e.Code })
                    .HasName("IX_Region");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Psgcode)
                    .HasColumnName("PSGCode")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.Description })
                    .HasName("IX_SkillNameDescription");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SkillCareer>(entity =>
            {
                entity.Property(e => e.SkillCareerId).HasColumnName("SkillCareerID");

                entity.Property(e => e.CareerId).HasColumnName("CareerID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");
            });

            modelBuilder.Entity<Subscriber>(entity =>
            {
                entity.HasIndex(e => new { e.ContractId, e.UserId, e.SubscriberKey })
                    .HasName("IX_ContractorContractSubscriberKey");

                entity.Property(e => e.SubscriberId).HasColumnName("SubscriberID");

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.ContractKey)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsAgreedTermsAndCondition).HasDefaultValueSql("((0))");

                entity.Property(e => e.SubscriberKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserKey)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TokenBlackList>(entity =>
            {
                entity.HasIndex(e => e.LoginKey)
                    .HasName("IX_UserLoginIDToken");

                entity.Property(e => e.TokenBlackListId).HasColumnName("TokenBlackListID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.ContactNumber)
                    .HasName("UQ__User__570665C69F8FE242")
                    .IsUnique();

                entity.HasIndex(e => e.EmailAddress)
                    .HasName("UQ__User__49A14740939CF443")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__User__536C85E451E13F5D")
                    .IsUnique();

                entity.HasIndex(e => new { e.Username, e.EmailAddress, e.ContactNumber })
                    .HasName("UIX_UsernameEmailAddressContactNumber")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDisabled).HasDefaultValueSql("((0))");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.UserKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.UserType).HasDefaultValueSql("((1))");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserBanned>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.Reason, e.Description })
                    .HasName("IX_ReasonDescriptionUserID");

                entity.Property(e => e.UserBannedId).HasColumnName("UserBannedID");

                entity.Property(e => e.DateCreate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Duration).HasColumnType("datetime");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.LoginKey })
                    .HasName("IX_TokenLoginKey");

                entity.Property(e => e.UserLoginId).HasColumnName("UserLoginID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.LoginAttempt).HasDefaultValueSql("((0))");

                entity.Property(e => e.LoginKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.LoginTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LogoutTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("UQ__UserProf__1788CCAD4A57DD9B")
                    .IsUnique();

                entity.HasIndex(e => new { e.FirstName, e.LastName, e.UserId })
                    .HasName("IX_FirstLastNameUserID");

                entity.Property(e => e.UserProfileId).HasColumnName("UserProfileID");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(15);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasIndex(e => new { e.UserLoginId, e.UserId })
                    .HasName("IX_UserIDUserLoginID");

                entity.Property(e => e.UserTokenId).HasColumnName("UserTokenID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserLoginId).HasColumnName("UserLoginID");
            });
        }
    }
}
