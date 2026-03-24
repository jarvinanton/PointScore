// CoreDbContext.cs
using Microsoft.EntityFrameworkCore;
using PointScore.Models;

namespace PointScore.Data
{
    public class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options) { }
          // ============================APIScore======================================
        public DbSet<License> Licenses { get; set; }
        public DbSet<LicenseUsage> LicenseUsages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<FeatureScoreResult> FeatureScoresResults { get; set; }
        public DbSet<MbseResult> MbseResults { get; set; }  
          // ============================APIScore======================================
        // DbSets - todos los modelos independientes
          // ============================Users======================================

        public DbSet<User> Users { get; set; }
        public DbSet<UserRoleRequest> UserRoleRequests { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleMap> UserRoleMaps { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        // ============================Users======================================
        public DbSet<WsmRequest> WsmRequests => Set<WsmRequest>();
        public DbSet<OriginatorInfo> OriginatorInfos => Set<OriginatorInfo>();
        public DbSet<WsmOwner> WsmOwners => Set<WsmOwner>();
        public DbSet<WsmWbsStructure> WsmWbsStructures => Set<WsmWbsStructure>();
        public DbSet<ImpactAnalysis> WsmImpactAnalyses => Set<ImpactAnalysis>();
        public DbSet<ChangeDriverType> ChangeDriverTypes => Set<ChangeDriverType>();
        public DbSet<WsmChangeDriver> WsmChangeDrivers => Set<WsmChangeDriver>();
        public DbSet<ModificationType> ModificationTypes => Set<ModificationType>();
        public DbSet<ApprovalWorkflow> ApprovalWorkflows => Set<ApprovalWorkflow>();
        public DbSet<WsmComment> WsmComments => Set<WsmComment>();
        public DbSet<WsmOtherDetail> WsmOtherDetails => Set<WsmOtherDetail>();
        // ============================Deliverable======================================
        public DbSet<WsmDeliverable> WsmDeliverables => Set<WsmDeliverable>();
        public DbSet<Deliverable> Deliverables => Set<Deliverable>();
        public DbSet<WsmDeliverableAssignment> WsmDeliverableAssignments => Set<WsmDeliverableAssignment>();
        // ============================FunctionalArea======================================
        public DbSet<FunctionalArea> FunctionalAreas => Set<FunctionalArea>();
        public DbSet<WsmFunctionalImpact> WsmFunctionalImpacts => Set<WsmFunctionalImpact>();
        // ============================SIA======================================
        public DbSet<WsmSiaScore> WsmSiaScores => Set<WsmSiaScore>();
        // ============================DetailedSiaSection======================================
        public DbSet<DetailedSiaSection> DetailedSiaSections => Set<DetailedSiaSection>();
        // ============================WsmDetailedSiaResponse======================================
        public DbSet<WsmDetailedSiaResponse> WsmDetailedSiaResponses => Set<WsmDetailedSiaResponse>();
        // ============================Block======================================
        public DbSet<Block> Blocks => Set<Block>();
        // ============================RecommendedDesignOwner======================================
        public DbSet<RecommendedDesignOwner> RecommendedDesignOwners { get; set; } = null!;
        // ============================TRL_level======================================
        public DbSet<TRL_level> TRL_levels => Set<TRL_level>();
        // ============================Milestone======================================
        public DbSet<MilestoneCriterion> MilestoneCriteria => Set<MilestoneCriterion>();
        public DbSet<WsmMilestoneResponse> WsmMilestoneResponses => Set<WsmMilestoneResponse>();
        public DbSet<MilestoneWeight> MilestoneWeights => Set<MilestoneWeight>();
        public DbSet<WsmMilestoneScore> WsmMilestoneScores => Set<WsmMilestoneScore>();
        // ============================Interface======================================
        public DbSet<WsmInterfaceDetail> WsmInterfaceDetails => Set<WsmInterfaceDetail>();
        // ============================ORI======================================
        public DbSet<WsmOriAssessment> WsmOriAssessments => Set<WsmOriAssessment>();
        public DbSet<WsmTimeCriticalityAssessment> WsmTimeCriticalityAssessments => Set<WsmTimeCriticalityAssessment>();
        public DbSet<WsmMissionImpactAssessment> WsmMissionImpactAssessments => Set<WsmMissionImpactAssessment>();

        // ============================Cost======================================
        public DbSet<WsmCostAssessment> WsmCostAssessments => Set<WsmCostAssessment>();
        // ============================ScoringFormulaWeights======================================
        public DbSet<ScoringFormulaWeights> ScoringFormulaWeights => Set<ScoringFormulaWeights>();
        public DbSet<WsmWeight> WsmWeights => Set<WsmWeight>();
        // ============================MRL======================================
        public DbSet<MRLSubThread> MRLSubThreads => Set<MRLSubThread>();
        public DbSet<MRLResponse> MRLResponses => Set<MRLResponse>();




      


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // ============================APIScore======================================
             modelBuilder.Entity<Project>()
            .HasOne(p => p.FeatureScoreResult)
            .WithOne(fsr => fsr.Project)
            .HasForeignKey<FeatureScoreResult>(fsr => fsr.ProjectId)
            .IsRequired(false);

            modelBuilder.Entity<WsmRequest>()
                .HasOne(w => w.FeatureScoreResult)
                .WithOne(f => f.WsmRequest)
                .HasForeignKey<FeatureScoreResult>(f => f.WsmRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FeatureScoreResult>(entity =>
            {
                // Configurar precisión para todas las propiedades decimal
                entity.Property(e => e.ENG_SIA).HasPrecision(18, 2);
                entity.Property(e => e.ENG_SIA_WT).HasPrecision(18, 2);
                entity.Property(e => e.FNTL_SIA).HasPrecision(18, 2);
                entity.Property(e => e.FNTL_SIA_WT).HasPrecision(18, 2);
                entity.Property(e => e.COST_IMP).HasPrecision(18, 2);
                entity.Property(e => e.COST_WT).HasPrecision(18, 2);
                entity.Property(e => e.SCH_IMP).HasPrecision(18, 2);
                entity.Property(e => e.SCH_IMP_WT).HasPrecision(18, 2);
                entity.Property(e => e.TECH_IMP).HasPrecision(18, 2);
                entity.Property(e => e.TECH_WT).HasPrecision(18, 2);
                entity.Property(e => e.FNTL_IMP).HasPrecision(18, 2);
                entity.Property(e => e.FNTL_IMP_WT).HasPrecision(18, 2);
                entity.Property(e => e.TRL).HasPrecision(18, 2);
                entity.Property(e => e.TRL_WT).HasPrecision(18, 2);
                entity.Property(e => e.DELIV).HasPrecision(18, 2);
                entity.Property(e => e.DELIV_WT).HasPrecision(18, 2);
                entity.Property(e => e.MRL).HasPrecision(18, 2);
                entity.Property(e => e.MRL_WT).HasPrecision(18, 2);
                entity.Property(e => e.SRR).HasPrecision(18, 2);
                entity.Property(e => e.SRR_WT).HasPrecision(18, 2);
                entity.Property(e => e.PDR).HasPrecision(18, 2);
                entity.Property(e => e.PDR_WT).HasPrecision(18, 2);
                entity.Property(e => e.CDR).HasPrecision(18, 2);
                entity.Property(e => e.CDR_WT).HasPrecision(18, 2);
                entity.Property(e => e.SRR_PDR_CDR_WT).HasPrecision(18, 2);
                entity.Property(e => e.INTERD).HasPrecision(18, 2);
                entity.Property(e => e.INTERD_WT).HasPrecision(18, 2);
                entity.Property(e => e.SELFDEP).HasPrecision(18, 2);
                entity.Property(e => e.SELFDEP_WT).HasPrecision(18, 2);
                entity.Property(e => e.ORI).HasPrecision(18, 2);
                entity.Property(e => e.ORI_WT).HasPrecision(18, 2);
                entity.Property(e => e.USER_IMP).HasPrecision(18, 2);
                entity.Property(e => e.USER_IMP_WT).HasPrecision(18, 2);
                entity.Property(e => e.TIME_CRIT).HasPrecision(18, 2);
                entity.Property(e => e.TIME_CRIT_WT).HasPrecision(18, 2);
                entity.Property(e => e.HICAT_TECH_WT).HasPrecision(18, 2);
                entity.Property(e => e.HICAT_FNTL_WT).HasPrecision(18, 2);
                entity.Property(e => e.HICAT_SCH_WT).HasPrecision(18, 2);
                entity.Property(e => e.HICAT_USER_WT).HasPrecision(18, 2);
                entity.Property(e => e.HICAT_COST_WT).HasPrecision(18, 2);
                entity.Property(e => e.DEV_COST).HasPrecision(18, 2);
                entity.Property(e => e.INSTALL_COST).HasPrecision(18, 2);
                entity.Property(e => e.PROD_COST).HasPrecision(18, 2);
                entity.Property(e => e.PROD_QTY).HasPrecision(18, 2);
                entity.Property(e => e.TOT_PROD_COST).HasPrecision(18, 2);
                entity.Property(e => e.TOTAL_WSM_COST).HasPrecision(18, 2);
                
                // Propiedades de puntuación
                entity.Property(e => e.WSM_COMP_SCORE).HasPrecision(18, 2);
                entity.Property(e => e.HICAT_TECH_SCORE).HasPrecision(18, 2);
                entity.Property(e => e.HICAT_FNTL_SCORE).HasPrecision(18, 2);
                entity.Property(e => e.HICAT_SCH_SCORE).HasPrecision(18, 2);
                entity.Property(e => e.HICAT_USER_SCORE).HasPrecision(18, 2);
                entity.Property(e => e.HICAT_COST_SCORE).HasPrecision(18, 2);
                entity.Property(e => e.COST_CORR_SCORE).HasPrecision(18, 2);
                entity.Property(e => e.RISK_TOL_LIKLI_SCORE).HasPrecision(18, 2);
                entity.Property(e => e.RISK_TOL_CONS_SCORE).HasPrecision(18, 2);
            });

            // Configurar precisión para MbseResult
            modelBuilder.Entity<MbseResult>(entity =>
            {
                entity.Property(e => e.CostScore).HasPrecision(18, 2);
                entity.Property(e => e.Metrics).HasPrecision(18, 2);
            });
            // ============================APIScore======================================
            // ============================Users======================================
            // 🔥 Poblar la tabla Roles con tu enum
             modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "System administrator" },
                new Role { Id = 2, Name = "ConfigurationManager", Description = "Configuration manager" },
                new Role { Id = 3, Name = "ProgramManager", Description = "Program management role" },
                new Role { Id = 4, Name = "WSMOwner", Description = "WSM owner" },
                new Role { Id = 5, Name = "BlockOwner", Description = "Block owner" },
                new Role { Id = 6, Name = "ACM", Description = "ACM role" },
                new Role { Id = 7, Name = "IPTLogistics", Description = "IPT Member - Logistics" },
                new Role { Id = 8, Name = "IPTProductionGFE", Description = "IPT Member - Production/GFE" },
                new Role { Id = 9, Name = "IPTSafety", Description = "IPT Member - Safety" },
                new Role { Id = 10, Name = "IPTQuality", Description = "IPT Member - Quality" },
                new Role { Id = 11, Name = "IPTCyber", Description = "IPT Member - Cyber" },
                new Role { Id = 12, Name = "IPTSoftware", Description = "IPT Member - Software" },
                new Role { Id = 13, Name = "IPTSystemsEngineering", Description = "IPT Member - Systems Engineering" },
                new Role { Id = 14, Name = "IPTTest", Description = "IPT Member - Test" },
                new Role { Id = 15, Name = "IPTAcquisition", Description = "IPT Member - Acquisition (Contracts)" },
                new Role { Id = 16, Name = "IPTFinance", Description = "IPT Member - Finance" },
                new Role { Id = 17, Name = "IPTProgramManagement", Description = "IPT Member - Program Management" },
                new Role { Id = 18, Name = "IPTSecurity", Description = "IPT Member - Security" },
                new Role { Id = 19, Name = "BasicUser", Description = "Basic system user" }
            );

            modelBuilder.Entity<UserRoleRequest>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.RoleRequests)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRoleRequest>()
                .HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
             modelBuilder.Entity<UserRoleMap>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRoleMap>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRoleMap>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Configure RolePermission
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            var user = modelBuilder.Entity<User>();

            // ---- Indexes ----
            user.HasIndex(u => u.CertificateThumbprint)
                .IsUnique()
                .HasFilter("[CertificateThumbprint] IS NOT NULL");

            user.HasIndex(u => u.Email)
                .IsUnique();

            user.HasIndex(u => u.Username)
                .IsUnique();

            // ---- Authentication fields ----
            user.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);

            user.Property(u => u.PasswordSalt)
                .IsRequired()
                .HasMaxLength(128);

            user.Property(u => u.TwoFactorSecret)
                .HasMaxLength(256);
            // ============================Users======================================

            modelBuilder.Entity<RecommendedDesignOwner>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrganizationName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ContactPerson).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ContactEmail).IsRequired().HasMaxLength(100);
                entity.Property(e => e.AssignmentDate).HasDefaultValueSql("GETUTCDATE()");
                
                // Configure the one-to-many relationship
                entity.HasMany(ro => ro.WsmRequests)
                    .WithOne(w => w.RecommendedDesignOwner)
                    .HasForeignKey(w => w.RecommendedDesignOwnerId)
                    .OnDelete(DeleteBehavior.SetNull); // or Restrict/Cascade based on your requirements
            });

            modelBuilder.Entity<FunctionalArea>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Weight).HasColumnType("decimal(5,4)"); // para 0.1000
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<WsmFunctionalImpact>(entity =>
            {
                entity.HasKey(e => new { e.WsmRequestId, e.FunctionalAreaId });

                entity.HasOne(e => e.WsmRequest)
                    .WithMany(w => w.FunctionalImpacts)
                    .HasForeignKey(e => e.WsmRequestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.FunctionalArea)
                    .WithMany(f => f.WsmImpacts)
                    .HasForeignKey(e => e.FunctionalAreaId)
                    .OnDelete(DeleteBehavior.Restrict);

                // NUEVA: Relación con el revisor (WsmOwner)
                entity.HasOne(e => e.Reviewer)
                    .WithMany() // No necesitas navegación inversa si no la usas
                    .HasForeignKey(e => e.ReviewerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            // Seed con los datos oficiales de la hoja "Initial SIA Documentation"
            modelBuilder.Entity<FunctionalArea>().HasData(
                new FunctionalArea { Id = 1, Name = "Logistics", Weight = 0.1000m, IsActive = true, DefaultRoleName = "IPTLogistics" },
                new FunctionalArea { Id = 2, Name = "Production/GFE", Weight = 0.0500m, IsActive = true, DefaultRoleName = "IPTProductionGFE" },
                new FunctionalArea { Id = 3, Name = "Safety", Weight = 0.1000m, IsActive = true, DefaultRoleName = "IPTSafety" },
                new FunctionalArea { Id = 4, Name = "Quality", Weight = 0.0500m, IsActive = true, DefaultRoleName = "IPTQuality" },
                new FunctionalArea { Id = 5, Name = "Cyber", Weight = 0.1000m, IsActive = true, DefaultRoleName = "IPTCyber" },
                new FunctionalArea { Id = 6, Name = "Software", Weight = 0.2000m, IsActive = true, DefaultRoleName = "IPTSoftware" },
                new FunctionalArea { Id = 7, Name = "Systems Engineering", Weight = 0.0700m, IsActive = true, DefaultRoleName = "IPTSystemsEngineering" },
                new FunctionalArea { Id = 8, Name = "Test", Weight = 0.0700m, IsActive = true, DefaultRoleName = "IPTTest" },
                new FunctionalArea { Id = 9, Name = "Acquisition (Contracts)", Weight = 0.1000m, IsActive = true, DefaultRoleName = "IPTAcquisition" },
                new FunctionalArea { Id = 10, Name = "Finance", Weight = 0.0500m, IsActive = true, DefaultRoleName = "IPTFinance" },
                new FunctionalArea { Id = 11, Name = "Program Management", Weight = 0.0400m, IsActive = true, DefaultRoleName = "IPTProgramManagement" },
                new FunctionalArea { Id = 12, Name = "Security", Weight = 0.0700m, IsActive = true, DefaultRoleName = "IPTSecurity" }
            );

            modelBuilder.Entity<WsmOwner>(entity =>
            {
                // Relationship with User (one-to-one)
                entity.HasOne<User>(w => w.User)
                    .WithMany()  // If User doesn't have a navigation property back to WsmOwner
                    .HasForeignKey(w => w.UserId)
                    .IsRequired(false)  // Makes the relationship optional
                    .OnDelete(DeleteBehavior.SetNull);  // Or Restrict if you prefer
            });
            // ========================
            // WsmRequest - Índices y restricciones
            // ========================
            modelBuilder.Entity<DetailedSiaSection>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.FunctionalArea)
                    .WithMany(f => f.Sections)
                    .HasForeignKey(e => e.FunctionalAreaId);
            });
            modelBuilder.Entity<WsmDetailedSiaResponse>(entity =>
            {
                entity.HasKey(e => new { e.WsmRequestId, e.DetailedSiaSectionId });

                entity.HasOne(e => e.WsmRequest)
                    .WithMany(w => w.DetailedSiaResponses)
                    .HasForeignKey(e => e.WsmRequestId);

                entity.HasOne(e => e.Section)
                    .WithMany(s => s.Responses)
                    .HasForeignKey(e => e.DetailedSiaSectionId);
            });
            modelBuilder.Entity<DetailedSiaSection>().HasData(
                // Logistics (Id = 1)
                new DetailedSiaSection { Id = 1, FunctionalAreaId = 1, Title = "Facilities & Infrastructure", Order = 1,IsActive = true },
                new DetailedSiaSection { Id = 2, FunctionalAreaId = 1, Title = "ILS", Order = 2,IsActive = true },
                new DetailedSiaSection { Id = 3, FunctionalAreaId = 1, Title = "Training & Training Support", Order = 3,IsActive = true },
                new DetailedSiaSection { Id = 4, FunctionalAreaId = 1, Title = "Manuals (-10, -23)", Order = 4,IsActive = true },
                new DetailedSiaSection { Id = 5, FunctionalAreaId = 1, Title = "Demilitarization / Disposal", Order = 5,IsActive = true },
                new DetailedSiaSection { Id = 6, FunctionalAreaId = 1, Title = "Item Unique Identification", Order = 6,IsActive = true },
                new DetailedSiaSection { Id = 7, FunctionalAreaId = 1, Title = "Packaging. Handling, Storage, and Transportation (PHS&T)", Order = 7,IsActive = true },
                new DetailedSiaSection { Id = 8, FunctionalAreaId = 1, Title = "Support Equipment", Order = 8,IsActive = true },
                new DetailedSiaSection { Id = 9, FunctionalAreaId = 1, Title = "Product Support Management", Order = 9,IsActive = true },
                new DetailedSiaSection { Id = 10, FunctionalAreaId = 1, Title = "Supply Support", Order = 10,IsActive = true },
                new DetailedSiaSection { Id = 11, FunctionalAreaId = 1, Title = "Maintenance Planning & Mgmt", Order = 11,IsActive = true },
                new DetailedSiaSection { Id = 12, FunctionalAreaId = 1, Title = "Service Life", Order = 12,IsActive = true },
                new DetailedSiaSection { Id = 13, FunctionalAreaId = 1, Title = "Operating Procedures", Order = 13,IsActive = true },
                new DetailedSiaSection { Id = 14, FunctionalAreaId = 1, Title = "Material Release", Order = 14,IsActive = true },

                // Production/GFE (Id = 2)
                new DetailedSiaSection { Id = 15, FunctionalAreaId = 2, Title = "Battery Production", Order = 1,IsActive = true },
                new DetailedSiaSection { Id = 16, FunctionalAreaId = 2, Title = "Schedule / Manpower Impacts", Order = 2,IsActive = true },
                new DetailedSiaSection { Id = 17, FunctionalAreaId = 2, Title = "DVT", Order = 3,IsActive = true },
                new DetailedSiaSection { Id = 18, FunctionalAreaId = 2, Title = "Supply Chain", Order = 4,IsActive = true },
                new DetailedSiaSection { Id = 19, FunctionalAreaId = 2, Title = "Procurement", Order = 5,IsActive = true },

                // Safety (Id = 3)
                new DetailedSiaSection { Id = 20, FunctionalAreaId = 3, Title = "Critical Safety Item", Order = 1,IsActive = true },
                new DetailedSiaSection { Id = 21, FunctionalAreaId = 3, Title = "Environment, Safety, and Occupational Health (ESOH)", Order = 2,IsActive = true },
                new DetailedSiaSection { Id = 22, FunctionalAreaId = 3, Title = "Operational Energy", Order = 3,IsActive = true },
                new DetailedSiaSection { Id = 23, FunctionalAreaId = 3, Title = "Material Release", Order = 4,IsActive = true },
                new DetailedSiaSection { Id = 24, FunctionalAreaId = 3, Title = "Insensitive Munitions", Order = 5,IsActive = true },

                // Quality (Id = 4)
                new DetailedSiaSection { Id = 25, FunctionalAreaId = 4, Title = "First Article Test", Order = 1,IsActive = true },
                new DetailedSiaSection { Id = 26, FunctionalAreaId = 4, Title = "Quality Assurance Survey", Order = 2,IsActive = true },
                new DetailedSiaSection { Id = 27, FunctionalAreaId = 4, Title = "FRB Updates", Order = 3,IsActive = true },
                new DetailedSiaSection { Id = 28, FunctionalAreaId = 4, Title = "Verification Activities", Order = 4,IsActive = true },

                // Cyber (Id = 5)
                new DetailedSiaSection { Id = 29, FunctionalAreaId = 5, Title = "ATO assessment", Order = 1,IsActive = true },
                new DetailedSiaSection { Id = 30, FunctionalAreaId = 5, Title = "ATO Update", Order = 2,IsActive = true },
                new DetailedSiaSection { Id = 31, FunctionalAreaId = 5, Title = "Patching impact", Order = 3,IsActive = true },
                new DetailedSiaSection { Id = 32, FunctionalAreaId = 5, Title = "STIG Impacts", Order = 4,IsActive = true },
                new DetailedSiaSection { Id = 33, FunctionalAreaId = 5, Title = "Ports Protocols and Services (PPS)", Order = 5,IsActive = true },
                new DetailedSiaSection { Id = 34, FunctionalAreaId = 5, Title = "eMASS Updates", Order = 6,IsActive = true },

                // Software (Id = 6)
                new DetailedSiaSection { Id = 35, FunctionalAreaId = 6, Title = "Software Updates", Order = 1,IsActive = true },
                new DetailedSiaSection { Id = 36, FunctionalAreaId = 6, Title = "Software User Manuals", Order = 2,IsActive = true },
                new DetailedSiaSection { Id = 37, FunctionalAreaId = 6, Title = "Interface Design Description", Order = 3,IsActive = true },
                new DetailedSiaSection { Id = 38, FunctionalAreaId = 6, Title = "Navy Schedule Impacts", Order = 4,IsActive = true },

                // Systems Engineering (Id = 7)
                new DetailedSiaSection { Id = 39, FunctionalAreaId = 7, Title = "Performance", Order = 1,IsActive = true },
                new DetailedSiaSection { Id = 40, FunctionalAreaId = 7, Title = "Requirements Specifications", Order = 2,IsActive = true },
                new DetailedSiaSection { Id = 41, FunctionalAreaId = 7, Title = "Interface Control Documents", Order = 3,IsActive = true },
                new DetailedSiaSection { Id = 42, FunctionalAreaId = 7, Title = "CDD", Order = 4,IsActive = true },
                new DetailedSiaSection { Id = 43, FunctionalAreaId = 7, Title = "Configuration Items Specifications", Order = 5,IsActive = true },
                new DetailedSiaSection { Id = 44, FunctionalAreaId = 7, Title = "Accessibility", Order = 6,IsActive = true },
                new DetailedSiaSection { Id = 45, FunctionalAreaId = 7, Title = "Affordability", Order = 7,IsActive = true },
                new DetailedSiaSection { Id = 46, FunctionalAreaId = 7, Title = "Anti-Counterfeiting", Order = 8,IsActive = true },
                new DetailedSiaSection { Id = 47, FunctionalAreaId = 7, Title = "COTS", Order = 9,IsActive = true },
                new DetailedSiaSection { Id = 48, FunctionalAreaId = 7, Title = "Corrosion Prevention / Control", Order = 10,IsActive = true },
                new DetailedSiaSection { Id = 49, FunctionalAreaId = 7, Title = "Human System Integration", Order = 11,IsActive = true },
                new DetailedSiaSection { Id = 50, FunctionalAreaId = 7, Title = "Interoperability & Dependency", Order = 12,IsActive = true },
                new DetailedSiaSection { Id = 51, FunctionalAreaId = 7, Title = "MOSA", Order = 13,IsActive = true },
                new DetailedSiaSection { Id = 52, FunctionalAreaId = 7, Title = "Spectrum Management", Order = 14,IsActive = true },
                new DetailedSiaSection { Id = 53, FunctionalAreaId = 7, Title = "Standardization", Order = 15,IsActive = true },
                new DetailedSiaSection { Id = 54, FunctionalAreaId = 7, Title = "Survivability", Order = 16,IsActive = true },
                new DetailedSiaSection { Id = 55, FunctionalAreaId = 7, Title = "System Security Engineering", Order = 17,IsActive = true },
                new DetailedSiaSection { Id = 56, FunctionalAreaId = 7, Title = "Electromagnetic Interference", Order = 18,IsActive = true },
                new DetailedSiaSection { Id = 57, FunctionalAreaId = 7, Title = "Reliability & Maintainability (R&M)", Order = 19,IsActive = true },
                new DetailedSiaSection { Id = 58, FunctionalAreaId = 7, Title = "Design Interface", Order = 20,IsActive = true },
                new DetailedSiaSection { Id = 59, FunctionalAreaId = 7, Title = "Insensitive Munitions", Order = 21,IsActive = true },
                new DetailedSiaSection { Id = 60, FunctionalAreaId = 7, Title = "Sustaining Engineering", Order = 22,IsActive = true },
                new DetailedSiaSection { Id = 61, FunctionalAreaId = 7, Title = "Technical Data", Order = 23,IsActive = true },

                // Test (Id = 8)
                new DetailedSiaSection { Id = 62, FunctionalAreaId = 8, Title = "Unit Test", Order = 1,IsActive = true },
                new DetailedSiaSection { Id = 63, FunctionalAreaId = 8, Title = "Component Test", Order = 2,IsActive = true },
                new DetailedSiaSection { Id = 64, FunctionalAreaId = 8, Title = "DOT&E required", Order = 3,IsActive = true },
                new DetailedSiaSection { Id = 65, FunctionalAreaId = 8, Title = "Flight Test", Order = 4,IsActive = true },
                new DetailedSiaSection { Id = 66, FunctionalAreaId = 8, Title = "Acceptance Test Procedure", Order = 5,IsActive = true },

                // Acquisition (Contracts) (Id = 9)
                new DetailedSiaSection { Id = 67, FunctionalAreaId = 9, Title = "Contract modification", Order = 1,IsActive = true },
                new DetailedSiaSection { Id = 68, FunctionalAreaId = 9, Title = "CDRL updates", Order = 2,IsActive = true },
                new DetailedSiaSection { Id = 69, FunctionalAreaId = 9, Title = "MIPR", Order = 3,IsActive = true },
                new DetailedSiaSection { Id = 70, FunctionalAreaId = 9, Title = "MOU/MOA Updates", Order = 4,IsActive = true },

                // Finance (Id = 10)
                new DetailedSiaSection { Id = 71, FunctionalAreaId = 10, Title = "Funding Review", Order = 1,IsActive = true },
                new DetailedSiaSection { Id = 72, FunctionalAreaId = 10, Title = "Type of funding", Order = 2,IsActive = true },
                new DetailedSiaSection { Id = 73, FunctionalAreaId = 10, Title = "FY analysis", Order = 3,IsActive = true },

                // Program Management (Id = 11)
                new DetailedSiaSection { Id = 74, FunctionalAreaId = 11, Title = "Schedule", Order = 1,IsActive = true },

                // Security (Id = 12)
                new DetailedSiaSection { Id = 75, FunctionalAreaId = 12, Title = "Intelligence", Order = 1,IsActive = true },
                new DetailedSiaSection { Id = 76, FunctionalAreaId = 12, Title = "Impact to SCG", Order = 2,IsActive = true },
                new DetailedSiaSection { Id = 77, FunctionalAreaId = 12, Title = "Impact to Facility Security", Order = 3,IsActive = true }
            );
            modelBuilder.Entity<WsmRequest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RequestNumber).IsUnique();
                entity.Property(e => e.RequestNumber).HasMaxLength(20).IsRequired();
                entity.Property(e => e.SystemName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Title).HasMaxLength(500).IsRequired();
                entity.Property(e => e.MrlCompositeScore).HasPrecision(18, 4);

                entity.HasOne(w => w.RecommendedDesignOwner)
                  .WithMany(ro => ro.WsmRequests)
                  .HasForeignKey(w => w.RecommendedDesignOwnerId)
                  .OnDelete(DeleteBehavior.SetNull);
                // Relación con OriginatorInfo (1 a muchos)
                entity.HasOne(e => e.Originator)
                      .WithMany(o => o.OriginatedRequests)
                      .HasForeignKey(e => e.OriginatorInfoId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con WsmOwner (Owner principal)
                entity.HasOne(e => e.WsmOwner)
                      .WithMany(o => o.OwnedWsmRequests)
                      .HasForeignKey(e => e.WsmOwnerId)
					  .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Block Owner (opcional)
                entity.HasOne(w => w.Block)
                    .WithMany(b => b.Wsms)
                    .HasForeignKey(w => w.BlockId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(w => w.WsmOwnerUser)
                    .WithMany()
                    .HasForeignKey(w => w.WsmOwnerUserId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Relación con ModificationType
                entity.HasOne(e => e.ModificationType)
                      .WithMany()
                      .HasForeignKey(e => e.ModificationTypeId);

                // 1:1 con ApprovalWorkflow
                entity.HasOne(e => e.ApprovalWorkflow)
                      .WithOne(w => w.WsmRequest)
                      .HasForeignKey<ApprovalWorkflow>(w => w.WsmRequestId);

                // 1:1 con ImpactAnalysis
                entity.HasOne(e => e.ImpactAnalysis)
                      .WithOne(i => i.WsmRequest)
                      .HasForeignKey<ImpactAnalysis>(i => i.WsmRequestId);

                // 1:1 con TimeCriticalityAssessment
                entity.HasOne(e => e.TimeCriticalityAssessment)
                      .WithOne(t => t.WsmRequest)
                      .HasForeignKey<WsmTimeCriticalityAssessment>(t => t.WsmRequestId)
                      .OnDelete(DeleteBehavior.Cascade);

                // 1:1 con WsmWeight
                entity.HasOne(e => e.WsmWeight)
                      .WithOne(w => w.WsmRequest)
                      .HasForeignKey<WsmWeight>(w => w.WsmRequestId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Block>(entity =>
            {
                entity.HasOne(b => b.BlockOwner)
                    .WithMany()
                    .HasForeignKey(b => b.BlockOwnerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<WsmDeliverable>()
                .HasKey(wd => new { wd.WsmRequestId, wd.DeliverableId });

            modelBuilder.Entity<WsmDeliverable>()
                .HasOne(wd => wd.WsmRequest)
                .WithMany(w => w.Deliverables)
                .HasForeignKey(wd => wd.WsmRequestId);

            modelBuilder.Entity<WsmDeliverable>()
                .HasOne(wd => wd.Deliverable)
                .WithMany(d => d.WsmDeliverables)
                .HasForeignKey(wd => wd.DeliverableId);
            

            // ========================
            // WsmWbsStructure - Niveles WBS (muchos por WSM)
            // ========================
            modelBuilder.Entity<WsmWbsStructure>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Level).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Nomenclature).HasMaxLength(200).IsRequired();

                entity.HasOne(e => e.WsmRequest)
                      .WithMany(w => w.WbsStructures)
                      .HasForeignKey(e => e.WsmRequestId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            // modelBuilder.Entity<RolePermission>()
            //   .HasIndex(rp => new { rp.RoleId, rp.PermissionId })
            //   .IsUnique();
            // modelBuilder.Entity<WsmOwner>()
            //     .HasOne(wo => wo.User)
            //     .WithOne()
            //     .HasForeignKey<WsmOwner>(wo => wo.UserId)
            //     .IsRequired(false)  // Hacer la relación opcional
            //     .OnDelete(DeleteBehavior.Restrict);// Or Cascade if you want to delete WsmOwner when User is deleted


            // ========================
            // Change Drivers - Muchos a Muchos
            // ========================
            modelBuilder.Entity<WsmChangeDriver>(entity =>
            {
                entity.HasKey(e => new { e.WsmRequestId, e.ChangeDriverTypeId });

                entity.HasOne(e => e.WsmRequest)
                      .WithMany(w => w.ChangeDrivers)
                      .HasForeignKey(e => e.WsmRequestId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ChangeDriverType)
                      .WithMany()
                      .HasForeignKey(e => e.ChangeDriverTypeId);
            });

            // ========================
            // ChangeDriverType - Catálogo
            // ========================
            modelBuilder.Entity<ChangeDriverType>(entity =>
            {
                entity.HasData(
                    new ChangeDriverType { Id = 1, Name = "Warfighter" },
                    new ChangeDriverType { Id = 2, Name = "Cyber Requirement/Update" },
                    new ChangeDriverType { Id = 3, Name = "Engineering Change Proposal (ECP)" },
                    new ChangeDriverType { Id = 4, Name = "Interoperability Requirement/Update" },
                    new ChangeDriverType { Id = 5, Name = "New Capability Requirement" },
                    new ChangeDriverType { Id = 6, Name = "Obsolescence Update" },
                    new ChangeDriverType { Id = 7, Name = "GFE Update" },
                    new ChangeDriverType { Id = 8, Name = "Reliability and Maintainability (RAM) Requirement" },
                    new ChangeDriverType { Id = 9, Name = "Maintenance Fix" }
                );
            });

            // ========================
            // ModificationType - Catálogo
            // ========================
            modelBuilder.Entity<ModificationType>(entity =>
            {
                entity.HasData(
                    new ModificationType { Id = 1, Name = "Permanent" },
                    new ModificationType { Id = 2, Name = "Temporary" },
                    new ModificationType { Id = 3, Name = "Maintenance" }
                );
            });

            // ========================
            // WsmComment - Historial de comentarios
            // ========================
            modelBuilder.Entity<WsmComment>(entity =>

            {
                entity.HasOne(e => e.WsmRequest)
                      .WithMany(w => w.Comments)
                      .HasForeignKey(e => e.WsmRequestId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Author)
                      .WithMany()
                      .HasForeignKey(e => e.AuthorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ========================
            // WsmDeliverableAssignment - Asignaciones de deliverables a WSM
            // ========================
            modelBuilder.Entity<WsmDeliverableAssignment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.WsmRequest)
                      .WithMany(w => w.DeliverableAssignments)
                      .HasForeignKey(e => e.WsmRequestId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ResponsibleIptMember)
                      .WithMany()
                      .HasForeignKey(e => e.ResponsibleIptMemberId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.UpdatedByUser)
                      .WithMany()
                      .HasForeignKey(e => e.UpdatedByUserId)
                      .OnDelete(DeleteBehavior.NoAction);

                // Índice compuesto para evitar duplicados
                entity.HasIndex(e => new { e.WsmRequestId, e.DeliverableCatalogId })
                      .IsUnique();

                // Índice para búsquedas por deliverable catalog ID
                entity.HasIndex(e => e.DeliverableCatalogId);

                // Índice para búsquedas por estado
                entity.HasIndex(e => e.Status);

                // Índice para búsquedas por responsable
                entity.HasIndex(e => e.ResponsibleIptMemberId);
            });
            
             // ========================
            // TRL Levels
            // ========================
            modelBuilder.Entity<TRL_level>(entity => {
                entity.Property(e => e.Score).HasPrecision(18, 2);
                
                entity.HasOne(e => e.WsmRequest)
                    .WithOne(w => w.TrlLevel)
                    .HasForeignKey<TRL_level>(e => e.WsmRequestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ========================
            // WsmSiaScore Precision
            // ========================
            modelBuilder.Entity<WsmSiaScore>(entity =>
            {
                entity.Property(e => e.Logistics_Score).HasPrecision(18, 2);
                entity.Property(e => e.Logistics_Weight).HasPrecision(18, 2);
                entity.Property(e => e.ProductionGFE_Score).HasPrecision(18, 2);
                entity.Property(e => e.ProductionGFE_Weight).HasPrecision(18, 2);
                entity.Property(e => e.Safety_Score).HasPrecision(18, 2);
                entity.Property(e => e.Safety_Weight).HasPrecision(18, 2);
                entity.Property(e => e.Quality_Score).HasPrecision(18, 2);
                entity.Property(e => e.Quality_Weight).HasPrecision(18, 2);
                entity.Property(e => e.Cyber_Score).HasPrecision(18, 2);
                entity.Property(e => e.Cyber_Weight).HasPrecision(18, 2);
                entity.Property(e => e.Software_Score).HasPrecision(18, 2);
                entity.Property(e => e.Software_Weight).HasPrecision(18, 2);
                entity.Property(e => e.SystemsEngineering_Score).HasPrecision(18, 2);
                entity.Property(e => e.SystemsEngineering_Weight).HasPrecision(18, 2);
                entity.Property(e => e.Test_Score).HasPrecision(18, 2);
                entity.Property(e => e.Test_Weight).HasPrecision(18, 2);
                entity.Property(e => e.AcquisitionContracts_Score).HasPrecision(18, 2);
                entity.Property(e => e.AcquisitionContracts_Weight).HasPrecision(18, 2);
                entity.Property(e => e.Finance_Score).HasPrecision(18, 2);
                entity.Property(e => e.Finance_Weight).HasPrecision(18, 2);
                entity.Property(e => e.ProgramManagement_Score).HasPrecision(18, 2);
                entity.Property(e => e.ProgramManagement_Weight).HasPrecision(18, 2);
                entity.Property(e => e.Security_Score).HasPrecision(18, 2);
                entity.Property(e => e.Security_Weight).HasPrecision(18, 2);
                entity.Property(e => e.EA_Score).HasPrecision(18, 2);
                entity.Property(e => e.OFA_Score).HasPrecision(18, 2);
                entity.Property(e => e.NE_Score).HasPrecision(18, 2);
                entity.Property(e => e.NOFA_Score).HasPrecision(18, 2);
            });
            // ========================
            // Milestone Criteria and Responses
            // ========================
            modelBuilder.Entity<MilestoneCriterion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MilestoneType).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Product).IsRequired().HasMaxLength(200);
                entity.Property(e => e.FunctionalGroup).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Complexity).HasPrecision(18, 2);
                entity.Property(e => e.CreationTime).HasPrecision(18, 2);
            });

            modelBuilder.Entity<WsmMilestoneResponse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.WsmRequest)
                    .WithMany(w => w.MilestoneResponses)
                    .HasForeignKey(e => e.WsmRequestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.MilestoneCriterion)
                    .WithMany(c => c.Responses)
                    .HasForeignKey(e => e.MilestoneCriterionId)
                    .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(e => e.UpdatedByUser)
                    .WithMany()
                    .HasForeignKey(e => e.UpdatedByUserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<MilestoneWeight>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.MilestoneType).IsRequired().HasMaxLength(10);

                entity.Property(e => e.MinWeight).HasPrecision(5, 2);
                entity.Property(e => e.MaxWeight).HasPrecision(5, 2);
            });

            modelBuilder.Entity<WsmMilestoneScore>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MilestoneType).IsRequired().HasMaxLength(10);
                entity.Property(e => e.TotalPotentialScore).HasPrecision(18, 4);
                entity.Property(e => e.TotalApplicableScore).HasPrecision(18, 4);
                entity.Property(e => e.FinalScore).HasPrecision(18, 4);

                // Ensure uniqueness: One WSM can only have one score per MilestoneType
                entity.HasIndex(e => new { e.WsmRequestId, e.MilestoneType }).IsUnique();

                entity.HasOne(e => e.WsmRequest)
                    .WithMany()
                    .HasForeignKey(e => e.WsmRequestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<WsmInterfaceDetail>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.WsmRequest)
                    .WithMany()
                    .HasForeignKey(e => e.WsmRequestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.TargetWsm)
                    .WithMany()
                    .HasForeignKey(e => e.TargetWsmId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                // Unique constraint: Each WSM pair must be unique
                entity.HasIndex(e => new { e.WsmRequestId, e.TargetWsmId, e.IsInternal }).IsUnique();
            });

            // WsmCostAssessment (Task 214 - Detailed SIA Cost Assessment)
            modelBuilder.Entity<WsmCostAssessment>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.DevelopmentCost).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.ProductionCostPerUnit).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.ProductionQuantity).IsRequired();
                entity.Property(e => e.TotalProductionCost).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.InstallationCost).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.CostScore).HasPrecision(18, 2).IsRequired();
                
                entity.HasOne(e => e.WsmRequest)
                    .WithMany()
                    .HasForeignKey(e => e.WsmRequestId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Unique constraint: One WSM can only have one cost assessment
                entity.HasIndex(e => e.WsmRequestId).IsUnique();
            });

            // ScoringFormulaWeights (Task 216 - Scoring Formula Weights Configuration)
            modelBuilder.Entity<ScoringFormulaWeights>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.TechnicalWeight).HasPrecision(5, 2).IsRequired();
                entity.Property(e => e.FunctionalWeight).HasPrecision(5, 2).IsRequired();
                entity.Property(e => e.ScheduleWeight).HasPrecision(5, 2).IsRequired();
                entity.Property(e => e.UserImpactWeight).HasPrecision(5, 2).IsRequired();
                entity.Property(e => e.CostWeight).HasPrecision(5, 2).IsRequired();
                entity.Property(e => e.IsActive).IsRequired();
                
                entity.HasOne(e => e.UpdatedByUser)
                    .WithMany()
                    .HasForeignKey(e => e.UpdatedBy)
                    .OnDelete(DeleteBehavior.SetNull);

                // Seed default weights
                entity.HasData(new ScoringFormulaWeights
                {
                    Id = 1,
                    TechnicalWeight = 15.00m,
                    FunctionalWeight = 15.00m,
                    ScheduleWeight = 20.00m,
                    UserImpactWeight = 50.00m,
                    CostWeight = 75.00m,
                    IsActive = true,
                    UpdatedAt = DateTime.UtcNow,
                    Comments = "Initial default scoring weights"
                });
            });

            // ========================
            // MRL Questionnaire
            // ========================
            modelBuilder.Entity<MRLResponse>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.HasOne(e => e.WsmRequest)
                    .WithMany(w => w.MRLResponses)
                    .HasForeignKey(e => e.WsmRequestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.MRLSubThread)
                    .WithMany()
                    .HasForeignKey(e => e.MRLSubThreadId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.UpdatedByUser)
                    .WithMany()
                    .HasForeignKey(e => e.UpdatedByUserId)
                    .OnDelete(DeleteBehavior.NoAction);

                // Un WSM solo puede tener una respuesta por sub-thread
                entity.HasIndex(e => new { e.WsmRequestId, e.MRLSubThreadId }).IsUnique();
            });

            // Seed de los sub-threads MRL (extraídos de la hoja "MRL Levels")
            modelBuilder.Entity<MRLSubThread>().HasData(
                // A - Technology and Industrial Base
                new MRLSubThread { Id = 1, Thread = "A - Technology and Industrial Base", SubThreadCode = "A.0", SubThreadName = "Technology Maturity", Description = "Should be assessed at TRL 1.", DisplayOrder = 1 },
                new MRLSubThread { Id = 2, Thread = "A - Technology and Industrial Base", SubThreadCode = "A.1", SubThreadName = "Industrial Base", Description = "Global trends in emerging industrial base capabilities identified.", DisplayOrder = 2 },
                new MRLSubThread { Id = 3, Thread = "A - Technology and Industrial Base", SubThreadCode = "A.2", SubThreadName = "Manufacturing Technology Development", Description = "Global trends in manufacturing science and technology identified (i.e., concepts, capabilities).", DisplayOrder = 3 },

                // B - Design
                new MRLSubThread { Id = 4, Thread = "B - Design", SubThreadCode = "B.1", SubThreadName = "Producibility Program", Description = "Hypotheses developed for cause-effect relationships between technology variables and producibility.", DisplayOrder = 4 },
                new MRLSubThread { Id = 5, Thread = "B - Design", SubThreadCode = "B.2", SubThreadName = "Design Maturity", Description = "Current capability deficiencies and gaps identified.", DisplayOrder = 5 },

                // C - Cost & Funding
                new MRLSubThread { Id = 6, Thread = "C - Cost & Funding", SubThreadCode = "C.1", SubThreadName = "Production Cost Knowledge (Cost modeling)", Description = "Hypotheses developed regarding technology impact on affordability.", DisplayOrder = 6 },
                new MRLSubThread { Id = 7, Thread = "C - Cost & Funding", SubThreadCode = "C.2", SubThreadName = "Cost Analysis", Description = "Initial manufacturing and quality costs identified.", DisplayOrder = 7 },
                new MRLSubThread { Id = 8, Thread = "C - Cost & Funding", SubThreadCode = "C.3", SubThreadName = "Manufacturing Investment Budget", Description = "Potential manufacturing investment strategy developed.", DisplayOrder = 8 },

                // D - Materials
                new MRLSubThread { Id = 9, Thread = "D - Materials (Raw Materials, Components, Subassemblies and Subsystems)", SubThreadCode = "D.1", SubThreadName = "Maturity", Description = "New material properties and characteristics surveyed and identified for research (e.g., manufacturability, quality).", DisplayOrder = 9 },
                new MRLSubThread { Id = 10, Thread = "D - Materials (Raw Materials, Components, Subassemblies and Subsystems)", SubThreadCode = "D.2", SubThreadName = "Availability of Materials", Description = "Global trends for material availability, obsolescence, and DMSMS surveyed and identified for research.", DisplayOrder = 10 },
                new MRLSubThread { Id = 11, Thread = "D - Materials (Raw Materials, Components, Subassemblies and Subsystems)", SubThreadCode = "D.3", SubThreadName = "Supply Chain Management", Description = "Global trends for supply chain capability and capacity surveyed.", DisplayOrder = 11 },
                new MRLSubThread { Id = 12, Thread = "D - Materials (Raw Materials, Components, Subassemblies and Subsystems)", SubThreadCode = "D.4", SubThreadName = "Special Handling", Description = "Hazardous materials identified and safety procedures in place.", DisplayOrder = 12 },

                // E - Process Capability & Control
                new MRLSubThread { Id = 13, Thread = "E - Process Capability & Control", SubThreadCode = "E.1", SubThreadName = "Modeling & Simulation (Product & Process)", Description = "Modeling and simulation approaches/tools identified to support manufacturing and quality activities.", DisplayOrder = 13 },
                new MRLSubThread { Id = 14, Thread = "E - Process Capability & Control", SubThreadCode = "E.2", SubThreadName = "Manufacturing Process Maturity", Description = "Hypotheses developed regarding cause-effect relationships between process variables and process stability and repeatability.", DisplayOrder = 14 },
                new MRLSubThread { Id = 15, Thread = "E - Process Capability & Control", SubThreadCode = "E.3", SubThreadName = "Process Yields and Rates", Description = "Hypotheses developed regarding future state manufacturing yields and rates.", DisplayOrder = 15 },

                // F - Quality
                new MRLSubThread { Id = 16, Thread = "F - Quality", SubThreadCode = "F.1", SubThreadName = "Quality Management", Description = "Quality management considerations surveyed and included in early planning activities", DisplayOrder = 16 },
                new MRLSubThread { Id = 17, Thread = "F - Quality", SubThreadCode = "F.2", SubThreadName = "Product Quality", Description = "Quality metrology state of the art surveyed. Hypotheses developed regarding cause-effect relationships between technology variables and quality.", DisplayOrder = 17 },
                new MRLSubThread { Id = 18, Thread = "F - Quality", SubThreadCode = "F.3", SubThreadName = "Supplier Quality/ Management", Description = "Supplier quality and quality management systems state of the art surveyed.", DisplayOrder = 18 },

                // G - Manufacturing Workforce
                new MRLSubThread { Id = 19, Thread = "G - Manufacturing Workforce (Engineering & Production)", SubThreadCode = "G.1", SubThreadName = "Manufacturing Workforce", Description = "Workforce skill sets to support emerging trends in manufacturing and technology surveyed.", DisplayOrder = 19 },

                // H - Facilities
                new MRLSubThread { Id = 20, Thread = "H - Facilities", SubThreadCode = "H.1", SubThreadName = "Tooling/STE/SIE", Description = "State of the art tooling, test and inspection equipment surveyed.", DisplayOrder = 20 },
                new MRLSubThread { Id = 21, Thread = "H - Facilities", SubThreadCode = "H.2", SubThreadName = "Facilities", Description = "Current facility capabilities and capacity surveyed.", DisplayOrder = 21 },

                // I - Manufacturing Management
                new MRLSubThread { Id = 22, Thread = "I - Manufacturing Management", SubThreadCode = "I.1", SubThreadName = "Manufacturing Planning & Scheduling", Description = "Manufacturing management considerations surveyed and included in early planning activities.", DisplayOrder = 22 },
                new MRLSubThread { Id = 23, Thread = "I - Manufacturing Management", SubThreadCode = "I.2", SubThreadName = "Materials Planning", Description = "Materials planning state of the art surveyed.", DisplayOrder = 23 },
                new MRLSubThread { Id = 24, Thread = "I - Manufacturing Management", SubThreadCode = "I.3", SubThreadName = "Manufacturing OT Cybersecurity", Description = "OT cybersecurity requirements for system concepts identified. OT cybersecurity vulnerabilities of potential manufacturing facilities identified.", DisplayOrder = 24 }
            );

        }

    }
}