using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PointScore.Migrations
{
    /// <inheritdoc />
    public partial class AddSubWeightsAndCostTechImp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChangeDriverTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeDriverTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deliverables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsCdrl = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(5,4)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DefaultRoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MbseResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostScore = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Metrics = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MbseResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MilestoneCriteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MilestoneType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Product = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FunctionalGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Criteria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredOptionalTailored = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IprOqe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complexity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreationTime = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsBlockLevel = table.Column<bool>(type: "bit", nullable: false),
                    IsExitCriteria = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceSection = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExpectedOqeLevel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilestoneCriteria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MilestoneWeights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MilestoneType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MinWeight = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    MaxWeight = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilestoneWeights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MRLSubThreads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thread = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubThreadCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubThreadName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MRLSubThreads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OriginatorInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OriginatorInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resource = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecommendedDesignOwners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusComments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedDesignOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetailedSiaSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FunctionalAreaId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MaxCostWeight = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    MaxScheduleWeight = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    MaxPerformanceWeight = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedSiaSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailedSiaSections_FunctionalAreas_FunctionalAreaId",
                        column: x => x.FunctionalAreaId,
                        principalTable: "FunctionalAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Organization = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CertificateThumbprint = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DesignOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(256)", maxLength: 256, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(128)", maxLength: 128, nullable: false),
                    TwoFactorSecret = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_RecommendedDesignOwners_DesignOwnerId",
                        column: x => x.DesignOwnerId,
                        principalTable: "RecommendedDesignOwners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlockOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    MaxPointCapacity = table.Column<int>(type: "int", nullable: true),
                    MaxCost = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blocks_Users_BlockOwnerId",
                        column: x => x.BlockOwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentMonthRequests = table.Column<int>(type: "int", nullable: false),
                    MonthKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScoringFormulaWeights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnicalWeight = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    FunctionalWeight = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    ScheduleWeight = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    UserImpactWeight = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    CostWeight = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Tech_ENG_SIA_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Tech_TECH_IMP_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Tech_TRL_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Tech_DELIV_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Tech_SRR_PDR_CDR_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Tech_INTERD_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Tech_SELFDEP_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    FntL_SIA_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    FntL_IMP_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    User_ORI_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    User_USER_IMP_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    User_TIME_CRIT_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Sch_SCH_IMP_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Sch_MRL_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Cost_SIA_TECH_ECP_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Cost_TRL_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Cost_TECH_IMP_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Cost_DELIV_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Cost_MRL_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Cost_SRR_PDR_CDR_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Cost_INTERD_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Cost_SELFDEP_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Cost_TIME_CRIT_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Cost_SIA_COST_ECP_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Milestone_SRR_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Milestone_PDR_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    Milestone_CDR_WT = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoringFormulaWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoringFormulaWeights_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleMaps",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleMaps", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoleMaps_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleMaps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RequestedRole = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoleRequests_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmOwners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmOwners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "WsmRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ProposedPriority = table.Column<float>(type: "real", nullable: false),
                    DesiredNeedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequiredNeedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImpactsBenefits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentsRecommendations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImplementationFollowOnComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommendedDesignOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedDesignOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImplementationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginatorInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmOwnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModificationTypeId = table.Column<int>(type: "int", nullable: false),
                    BlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MrlCompositeScore = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmRequests_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WsmRequests_ModificationTypes_ModificationTypeId",
                        column: x => x.ModificationTypeId,
                        principalTable: "ModificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WsmRequests_OriginatorInfos_OriginatorInfoId",
                        column: x => x.OriginatorInfoId,
                        principalTable: "OriginatorInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WsmRequests_RecommendedDesignOwners_ApprovedDesignOwnerId",
                        column: x => x.ApprovedDesignOwnerId,
                        principalTable: "RecommendedDesignOwners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WsmRequests_RecommendedDesignOwners_RecommendedDesignOwnerId",
                        column: x => x.RecommendedDesignOwnerId,
                        principalTable: "RecommendedDesignOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WsmRequests_Users_WsmOwnerUserId",
                        column: x => x.WsmOwnerUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LicenseUsages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseUsages_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalWorkflows",
                columns: table => new
                {
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkflowStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DevCCB_Approved = table.Column<bool>(type: "bit", nullable: false),
                    DevCCB_ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DevCCB_Disapproved = table.Column<bool>(type: "bit", nullable: false),
                    DevCCB_DisapprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProdCCB_Approved = table.Column<bool>(type: "bit", nullable: false),
                    ProdCCB_ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProdCCB_Disapproved = table.Column<bool>(type: "bit", nullable: false),
                    ProdCCB_DisapprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FieldingCCB_Approved = table.Column<bool>(type: "bit", nullable: false),
                    FieldingCCB_ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImplementationNumberAssigned = table.Column<bool>(type: "bit", nullable: false),
                    ImplementationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImplementationCompleted = table.Column<bool>(type: "bit", nullable: false),
                    ImplementationCompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CommentsFollowOn = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GovtApprovalSubmitted = table.Column<bool>(type: "bit", nullable: false),
                    GovtApprovalSubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GovtApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightTestRequired = table.Column<bool>(type: "bit", nullable: true),
                    FlightTestDeterminationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlightTestComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialReleaseRequired = table.Column<bool>(type: "bit", nullable: true),
                    MaterialReleaseDeterminationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaterialReleaseComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialReleaseDeterminedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImpactThreshold = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImpactThresholdDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImpactThresholdComments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalWorkflows", x => x.WsmRequestId);
                    table.ForeignKey(
                        name: "FK_ApprovalWorkflows_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureScoreResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ENG_SIA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ENG_SIA_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FNTL_SIA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FNTL_SIA_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    COST_IMP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    COST_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SCH_IMP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SCH_IMP_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TECH_IMP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TECH_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FNTL_IMP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FNTL_IMP_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TRL = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TRL_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DELIV = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DELIV_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MRL = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MRL_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SRR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SRR_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PDR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PDR_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CDR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CDR_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SRR_PDR_CDR_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    INTERD = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    INTERD_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SELFDEP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SELFDEP_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ORI = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ORI_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    USER_IMP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    USER_IMP_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TIME_CRIT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TIME_CRIT_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HICAT_TECH_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HICAT_FNTL_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HICAT_SCH_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HICAT_USER_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HICAT_COST_WT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DEV_COST = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PROD_COST = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PROD_QTY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TOT_PROD_COST = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    INSTALL_COST = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WSM_COMP_SCORE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HICAT_TECH_SCORE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HICAT_FNTL_SCORE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HICAT_SCH_SCORE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HICAT_USER_SCORE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HICAT_COST_SCORE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RISK_TOL_LIKLI_SCORE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RISK_TOL_CONS_SCORE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TOTAL_WSM_COST = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    COST_CORR_SCORE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureScoreResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureScoreResult_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeatureScoreResult_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MRLResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MRLSubThreadId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MRLResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MRLResponses_MRLSubThreads_MRLSubThreadId",
                        column: x => x.MRLSubThreadId,
                        principalTable: "MRLSubThreads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MRLResponses_Users_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MRLResponses_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TRL_levels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRL_levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TRL_levels_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmChangeDrivers",
                columns: table => new
                {
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeDriverTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmChangeDrivers", x => new { x.WsmRequestId, x.ChangeDriverTypeId });
                    table.ForeignKey(
                        name: "FK_WsmChangeDrivers_ChangeDriverTypes_ChangeDriverTypeId",
                        column: x => x.ChangeDriverTypeId,
                        principalTable: "ChangeDriverTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WsmChangeDrivers_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmComments_WsmOwners_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "WsmOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WsmComments_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmCostAssessments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DevelopmentCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductionCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductionQuantity = table.Column<int>(type: "int", nullable: false),
                    TotalProductionCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InstallationCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CostScore = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalWsmCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostCorrelatedScore = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmCostAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmCostAssessments_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmDeliverables",
                columns: table => new
                {
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmDeliverables", x => new { x.WsmRequestId, x.DeliverableId });
                    table.ForeignKey(
                        name: "FK_WsmDeliverables_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WsmDeliverables_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmDesignOwnerAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusComments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmDesignOwnerAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmDesignOwnerAssignments_RecommendedDesignOwners_DesignOwnerId",
                        column: x => x.DesignOwnerId,
                        principalTable: "RecommendedDesignOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WsmDesignOwnerAssignments_Users_AssignedByUserId",
                        column: x => x.AssignedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WsmDesignOwnerAssignments_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmDetailedSiaResponses",
                columns: table => new
                {
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DetailedSiaSectionId = table.Column<int>(type: "int", nullable: false),
                    IsImpacted = table.Column<bool>(type: "bit", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmDetailedSiaResponses", x => new { x.WsmRequestId, x.DetailedSiaSectionId });
                    table.ForeignKey(
                        name: "FK_WsmDetailedSiaResponses_DetailedSiaSections_DetailedSiaSectionId",
                        column: x => x.DetailedSiaSectionId,
                        principalTable: "DetailedSiaSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WsmDetailedSiaResponses_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmFunctionalImpacts",
                columns: table => new
                {
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FunctionalAreaId = table.Column<int>(type: "int", nullable: false),
                    IsImpacted = table.Column<bool>(type: "bit", nullable: false),
                    ReviewerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmFunctionalImpacts", x => new { x.WsmRequestId, x.FunctionalAreaId });
                    table.ForeignKey(
                        name: "FK_WsmFunctionalImpacts_FunctionalAreas_FunctionalAreaId",
                        column: x => x.FunctionalAreaId,
                        principalTable: "FunctionalAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WsmFunctionalImpacts_Users_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WsmFunctionalImpacts_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmImpactAnalyses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Software = table.Column<bool>(type: "bit", nullable: false),
                    Cyber = table.Column<bool>(type: "bit", nullable: false),
                    Training = table.Column<bool>(type: "bit", nullable: false),
                    Safety = table.Column<bool>(type: "bit", nullable: false),
                    Requirements = table.Column<bool>(type: "bit", nullable: false),
                    ProductionLine = table.Column<bool>(type: "bit", nullable: false),
                    DevelopmentSIL = table.Column<bool>(type: "bit", nullable: false),
                    FieldTacticalSIL = table.Column<bool>(type: "bit", nullable: false),
                    TestEvent = table.Column<bool>(type: "bit", nullable: false),
                    DepotRepair = table.Column<bool>(type: "bit", nullable: false),
                    OtherImpact = table.Column<bool>(type: "bit", nullable: false),
                    SoftwareApplicability = table.Column<bool>(type: "bit", nullable: false),
                    Test = table.Column<bool>(type: "bit", nullable: false),
                    Quality = table.Column<bool>(type: "bit", nullable: false),
                    Logistics = table.Column<bool>(type: "bit", nullable: false),
                    SystemsEngineering = table.Column<bool>(type: "bit", nullable: false),
                    AcquisitionContracts = table.Column<bool>(type: "bit", nullable: false),
                    Finance = table.Column<bool>(type: "bit", nullable: false),
                    ProgramManagement = table.Column<bool>(type: "bit", nullable: false),
                    Security = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmImpactAnalyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmImpactAnalyses_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmInterfaceDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetWsmId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HasPhysical = table.Column<bool>(type: "bit", nullable: false),
                    PhysicalCount = table.Column<int>(type: "int", nullable: true),
                    HasEnergy = table.Column<bool>(type: "bit", nullable: false),
                    EnergyCount = table.Column<int>(type: "int", nullable: true),
                    HasMass = table.Column<bool>(type: "bit", nullable: false),
                    MassCount = table.Column<int>(type: "int", nullable: true),
                    HasInfo = table.Column<bool>(type: "bit", nullable: false),
                    InfoCount = table.Column<int>(type: "int", nullable: true),
                    IsInternal = table.Column<bool>(type: "bit", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmInterfaceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmInterfaceDetails_WsmRequests_TargetWsmId",
                        column: x => x.TargetWsmId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WsmInterfaceDetails_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmMilestoneResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MilestoneCriterionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OqeEvidence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApplicableScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmMilestoneResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmMilestoneResponses_MilestoneCriteria_MilestoneCriterionId",
                        column: x => x.MilestoneCriterionId,
                        principalTable: "MilestoneCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WsmMilestoneResponses_Users_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WsmMilestoneResponses_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmMilestoneScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MilestoneType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TotalPotentialScore = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    TotalApplicableScore = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    FinalScore = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmMilestoneScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmMilestoneScores_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmMissionImpactAssessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequirementLevel = table.Column<int>(type: "int", nullable: false),
                    BenefitToMission = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmMissionImpactAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmMissionImpactAssessments_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmOriAssessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Likelihood = table.Column<int>(type: "int", nullable: false),
                    Consequence = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmOriAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmOriAssessments_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmOtherDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmOtherDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmOtherDetails_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmSiaScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logistics_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Logistics_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductionGFE_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductionGFE_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Safety_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Safety_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quality_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quality_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cyber_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cyber_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Software_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Software_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SystemsEngineering_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SystemsEngineering_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Test_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Test_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AcquisitionContracts_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AcquisitionContracts_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Finance_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Finance_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProgramManagement_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProgramManagement_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Security_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Security_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EA_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OFA_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NE_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NOFA_Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmSiaScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmSiaScores_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmTimeCriticalityAssessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeginWorkNeedTime = table.Column<int>(type: "int", nullable: false),
                    CompleteWorkNeedTime = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmTimeCriticalityAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmTimeCriticalityAssessments_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmWbsStructures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nomenclature = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Applicability = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmWbsStructures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmWbsStructures_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WsmWeights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WsmRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TechnicalWeight = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    FunctionalWeight = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ScheduleWeight = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    UserImpactWeight = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CostWeight = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsmWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WsmWeights_WsmRequests_WsmRequestId",
                        column: x => x.WsmRequestId,
                        principalTable: "WsmRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChangeDriverTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Warfighter" },
                    { 2, "Cyber Requirement/Update" },
                    { 3, "Engineering Change Proposal (ECP)" },
                    { 4, "Interoperability Requirement/Update" },
                    { 5, "New Capability Requirement" },
                    { 6, "Obsolescence Update" },
                    { 7, "GFE Update" },
                    { 8, "Reliability and Maintainability (RAM) Requirement" },
                    { 9, "Maintenance Fix" }
                });

            migrationBuilder.InsertData(
                table: "FunctionalAreas",
                columns: new[] { "Id", "DefaultRoleName", "IsActive", "Name", "Weight" },
                values: new object[,]
                {
                    { 1, "IPTLogistics", true, "Logistics", 0.1000m },
                    { 2, "IPTProductionGFE", true, "Production/GFE", 0.0500m },
                    { 3, "IPTSafety", true, "Safety", 0.1000m },
                    { 4, "IPTQuality", true, "Quality", 0.0500m },
                    { 5, "IPTCyber", true, "Cyber", 0.1000m },
                    { 6, "IPTSoftware", true, "Software", 0.2000m },
                    { 7, "IPTSystemsEngineering", true, "Systems Engineering", 0.0700m },
                    { 8, "IPTTest", true, "Test", 0.0700m },
                    { 9, "IPTAcquisition", true, "Acquisition (Contracts)", 0.1000m },
                    { 10, "IPTFinance", true, "Finance", 0.0500m },
                    { 11, "IPTProgramManagement", true, "Program Management", 0.0400m },
                    { 12, "IPTSecurity", true, "Security", 0.0700m }
                });

            migrationBuilder.InsertData(
                table: "MRLSubThreads",
                columns: new[] { "Id", "Description", "DisplayOrder", "SubThreadCode", "SubThreadName", "Thread" },
                values: new object[,]
                {
                    { 1, "Should be assessed at TRL 1.", 1, "A.0", "Technology Maturity", "A - Technology and Industrial Base" },
                    { 2, "Global trends in emerging industrial base capabilities identified.", 2, "A.1", "Industrial Base", "A - Technology and Industrial Base" },
                    { 3, "Global trends in manufacturing science and technology identified (i.e., concepts, capabilities).", 3, "A.2", "Manufacturing Technology Development", "A - Technology and Industrial Base" },
                    { 4, "Hypotheses developed for cause-effect relationships between technology variables and producibility.", 4, "B.1", "Producibility Program", "B - Design" },
                    { 5, "Current capability deficiencies and gaps identified.", 5, "B.2", "Design Maturity", "B - Design" },
                    { 6, "Hypotheses developed regarding technology impact on affordability.", 6, "C.1", "Production Cost Knowledge (Cost modeling)", "C - Cost & Funding" },
                    { 7, "Initial manufacturing and quality costs identified.", 7, "C.2", "Cost Analysis", "C - Cost & Funding" },
                    { 8, "Potential manufacturing investment strategy developed.", 8, "C.3", "Manufacturing Investment Budget", "C - Cost & Funding" },
                    { 9, "New material properties and characteristics surveyed and identified for research (e.g., manufacturability, quality).", 9, "D.1", "Maturity", "D - Materials (Raw Materials, Components, Subassemblies and Subsystems)" },
                    { 10, "Global trends for material availability, obsolescence, and DMSMS surveyed and identified for research.", 10, "D.2", "Availability of Materials", "D - Materials (Raw Materials, Components, Subassemblies and Subsystems)" },
                    { 11, "Global trends for supply chain capability and capacity surveyed.", 11, "D.3", "Supply Chain Management", "D - Materials (Raw Materials, Components, Subassemblies and Subsystems)" },
                    { 12, "Hazardous materials identified and safety procedures in place.", 12, "D.4", "Special Handling", "D - Materials (Raw Materials, Components, Subassemblies and Subsystems)" },
                    { 13, "Modeling and simulation approaches/tools identified to support manufacturing and quality activities.", 13, "E.1", "Modeling & Simulation (Product & Process)", "E - Process Capability & Control" },
                    { 14, "Hypotheses developed regarding cause-effect relationships between process variables and process stability and repeatability.", 14, "E.2", "Manufacturing Process Maturity", "E - Process Capability & Control" },
                    { 15, "Hypotheses developed regarding future state manufacturing yields and rates.", 15, "E.3", "Process Yields and Rates", "E - Process Capability & Control" },
                    { 16, "Quality management considerations surveyed and included in early planning activities", 16, "F.1", "Quality Management", "F - Quality" },
                    { 17, "Quality metrology state of the art surveyed. Hypotheses developed regarding cause-effect relationships between technology variables and quality.", 17, "F.2", "Product Quality", "F - Quality" },
                    { 18, "Supplier quality and quality management systems state of the art surveyed.", 18, "F.3", "Supplier Quality/ Management", "F - Quality" },
                    { 19, "Workforce skill sets to support emerging trends in manufacturing and technology surveyed.", 19, "G.1", "Manufacturing Workforce", "G - Manufacturing Workforce (Engineering & Production)" },
                    { 20, "State of the art tooling, test and inspection equipment surveyed.", 20, "H.1", "Tooling/STE/SIE", "H - Facilities" },
                    { 21, "Current facility capabilities and capacity surveyed.", 21, "H.2", "Facilities", "H - Facilities" },
                    { 22, "Manufacturing management considerations surveyed and included in early planning activities.", 22, "I.1", "Manufacturing Planning & Scheduling", "I - Manufacturing Management" },
                    { 23, "Materials planning state of the art surveyed.", 23, "I.2", "Materials Planning", "I - Manufacturing Management" },
                    { 24, "OT cybersecurity requirements for system concepts identified. OT cybersecurity vulnerabilities of potential manufacturing facilities identified.", 24, "I.3", "Manufacturing OT Cybersecurity", "I - Manufacturing Management" }
                });

            migrationBuilder.InsertData(
                table: "ModificationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Permanent" },
                    { 2, "Temporary" },
                    { 3, "Maintenance" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "System administrator", "Admin" },
                    { 2, "Configuration manager", "ConfigurationManager" },
                    { 3, "Program management role", "ProgramManager" },
                    { 4, "WSM owner", "WSMOwner" },
                    { 5, "Block owner", "BlockOwner" },
                    { 6, "ACM role", "ACM" },
                    { 7, "IPT Member - Logistics", "IPTLogistics" },
                    { 8, "IPT Member - Production/GFE", "IPTProductionGFE" },
                    { 9, "IPT Member - Safety", "IPTSafety" },
                    { 10, "IPT Member - Quality", "IPTQuality" },
                    { 11, "IPT Member - Cyber", "IPTCyber" },
                    { 12, "IPT Member - Software", "IPTSoftware" },
                    { 13, "IPT Member - Systems Engineering", "IPTSystemsEngineering" },
                    { 14, "IPT Member - Test", "IPTTest" },
                    { 15, "IPT Member - Acquisition (Contracts)", "IPTAcquisition" },
                    { 16, "IPT Member - Finance", "IPTFinance" },
                    { 17, "IPT Member - Program Management", "IPTProgramManagement" },
                    { 18, "IPT Member - Security", "IPTSecurity" },
                    { 19, "Basic system user", "BasicUser" }
                });

            migrationBuilder.InsertData(
                table: "ScoringFormulaWeights",
                columns: new[] { "Id", "Comments", "CostWeight", "Cost_DELIV_WT", "Cost_INTERD_WT", "Cost_MRL_WT", "Cost_SELFDEP_WT", "Cost_SIA_COST_ECP_WT", "Cost_SIA_TECH_ECP_WT", "Cost_SRR_PDR_CDR_WT", "Cost_TECH_IMP_WT", "Cost_TIME_CRIT_WT", "Cost_TRL_WT", "FntL_IMP_WT", "FntL_SIA_WT", "FunctionalWeight", "IsActive", "Milestone_CDR_WT", "Milestone_PDR_WT", "Milestone_SRR_WT", "Sch_MRL_WT", "Sch_SCH_IMP_WT", "ScheduleWeight", "Tech_DELIV_WT", "Tech_ENG_SIA_WT", "Tech_INTERD_WT", "Tech_SELFDEP_WT", "Tech_SRR_PDR_CDR_WT", "Tech_TECH_IMP_WT", "Tech_TRL_WT", "TechnicalWeight", "UpdatedAt", "UpdatedBy", "UserImpactWeight", "User_ORI_WT", "User_TIME_CRIT_WT", "User_USER_IMP_WT" },
                values: new object[] { 1, "Client-verified default scoring weights", 75.00m, 15.00m, 10.00m, 5.00m, 5.00m, 30.00m, 5.00m, 15.00m, 5.00m, 10.00m, 5.00m, 70.00m, 30.00m, 30.00m, true, 40.00m, 35.00m, 25.00m, 40.00m, 60.00m, 30.00m, 10.00m, 5.00m, 10.00m, 25.00m, 10.00m, 15.00m, 25.00m, 40.00m, new DateTime(2026, 8, 17, 17, 45, 21, 954, DateTimeKind.Utc).AddTicks(2255), null, 100.00m, 25.00m, 35.00m, 40.00m });

            migrationBuilder.InsertData(
                table: "DetailedSiaSections",
                columns: new[] { "Id", "FunctionalAreaId", "IsActive", "MaxCostWeight", "MaxPerformanceWeight", "MaxScheduleWeight", "Order", "Title" },
                values: new object[,]
                {
                    { 1, 1, true, 0m, 0m, 0m, 1, "Facilities & Infrastructure" },
                    { 2, 1, true, 0m, 0m, 0m, 2, "ILS" },
                    { 3, 1, true, 0m, 0m, 0m, 3, "Training & Training Support" },
                    { 4, 1, true, 0m, 0m, 0m, 4, "Manuals (-10, -23)" },
                    { 5, 1, true, 0m, 0m, 0m, 5, "Demilitarization / Disposal" },
                    { 6, 1, true, 0m, 0m, 0m, 6, "Item Unique Identification" },
                    { 7, 1, true, 0m, 0m, 0m, 7, "Packaging. Handling, Storage, and Transportation (PHS&T)" },
                    { 8, 1, true, 0m, 0m, 0m, 8, "Support Equipment" },
                    { 9, 1, true, 0m, 0m, 0m, 9, "Product Support Management" },
                    { 10, 1, true, 0m, 0m, 0m, 10, "Supply Support" },
                    { 11, 1, true, 0m, 0m, 0m, 11, "Maintenance Planning & Mgmt" },
                    { 12, 1, true, 0m, 0m, 0m, 12, "Service Life" },
                    { 13, 1, true, 0m, 0m, 0m, 13, "Operating Procedures" },
                    { 14, 1, true, 0m, 0m, 0m, 14, "Material Release" },
                    { 15, 2, true, 0m, 0m, 0m, 1, "Battery Production" },
                    { 16, 2, true, 0m, 0m, 0m, 2, "Schedule / Manpower Impacts" },
                    { 17, 2, true, 0m, 0m, 0m, 3, "DVT" },
                    { 18, 2, true, 0m, 0m, 0m, 4, "Supply Chain" },
                    { 19, 2, true, 0m, 0m, 0m, 5, "Procurement" },
                    { 20, 3, true, 0m, 0m, 0m, 1, "Critical Safety Item" },
                    { 21, 3, true, 0m, 0m, 0m, 2, "Environment, Safety, and Occupational Health (ESOH)" },
                    { 22, 3, true, 0m, 0m, 0m, 3, "Operational Energy" },
                    { 23, 3, true, 0m, 0m, 0m, 4, "Material Release" },
                    { 24, 3, true, 0m, 0m, 0m, 5, "Insensitive Munitions" },
                    { 25, 4, true, 0m, 0m, 0m, 1, "First Article Test" },
                    { 26, 4, true, 0m, 0m, 0m, 2, "Quality Assurance Survey" },
                    { 27, 4, true, 0m, 0m, 0m, 3, "FRB Updates" },
                    { 28, 4, true, 0m, 0m, 0m, 4, "Verification Activities" },
                    { 29, 5, true, 0m, 0m, 0m, 1, "ATO assessment" },
                    { 30, 5, true, 0m, 0m, 0m, 2, "ATO Update" },
                    { 31, 5, true, 0m, 0m, 0m, 3, "Patching impact" },
                    { 32, 5, true, 0m, 0m, 0m, 4, "STIG Impacts" },
                    { 33, 5, true, 0m, 0m, 0m, 5, "Ports Protocols and Services (PPS)" },
                    { 34, 5, true, 0m, 0m, 0m, 6, "eMASS Updates" },
                    { 35, 6, true, 0m, 0m, 0m, 1, "Software Updates" },
                    { 36, 6, true, 0m, 0m, 0m, 2, "Software User Manuals" },
                    { 37, 6, true, 0m, 0m, 0m, 3, "Interface Design Description" },
                    { 38, 6, true, 0m, 0m, 0m, 4, "Navy Schedule Impacts" },
                    { 39, 7, true, 0m, 0m, 0m, 1, "Performance" },
                    { 40, 7, true, 0m, 0m, 0m, 2, "Requirements Specifications" },
                    { 41, 7, true, 0m, 0m, 0m, 3, "Interface Control Documents" },
                    { 42, 7, true, 0m, 0m, 0m, 4, "CDD" },
                    { 43, 7, true, 0m, 0m, 0m, 5, "Configuration Items Specifications" },
                    { 44, 7, true, 0m, 0m, 0m, 6, "Accessibility" },
                    { 45, 7, true, 0m, 0m, 0m, 7, "Affordability" },
                    { 46, 7, true, 0m, 0m, 0m, 8, "Anti-Counterfeiting" },
                    { 47, 7, true, 0m, 0m, 0m, 9, "COTS" },
                    { 48, 7, true, 0m, 0m, 0m, 10, "Corrosion Prevention / Control" },
                    { 49, 7, true, 0m, 0m, 0m, 11, "Human System Integration" },
                    { 50, 7, true, 0m, 0m, 0m, 12, "Interoperability & Dependency" },
                    { 51, 7, true, 0m, 0m, 0m, 13, "MOSA" },
                    { 52, 7, true, 0m, 0m, 0m, 14, "Spectrum Management" },
                    { 53, 7, true, 0m, 0m, 0m, 15, "Standardization" },
                    { 54, 7, true, 0m, 0m, 0m, 16, "Survivability" },
                    { 55, 7, true, 0m, 0m, 0m, 17, "System Security Engineering" },
                    { 56, 7, true, 0m, 0m, 0m, 18, "Electromagnetic Interference" },
                    { 57, 7, true, 0m, 0m, 0m, 19, "Reliability & Maintainability (R&M)" },
                    { 58, 7, true, 0m, 0m, 0m, 20, "Design Interface" },
                    { 59, 7, true, 0m, 0m, 0m, 21, "Insensitive Munitions" },
                    { 60, 7, true, 0m, 0m, 0m, 22, "Sustaining Engineering" },
                    { 61, 7, true, 0m, 0m, 0m, 23, "Technical Data" },
                    { 62, 8, true, 0m, 0m, 0m, 1, "Unit Test" },
                    { 63, 8, true, 0m, 0m, 0m, 2, "Component Test" },
                    { 64, 8, true, 0m, 0m, 0m, 3, "DOT&E required" },
                    { 65, 8, true, 0m, 0m, 0m, 4, "Flight Test" },
                    { 66, 8, true, 0m, 0m, 0m, 5, "Acceptance Test Procedure" },
                    { 67, 9, true, 0m, 0m, 0m, 1, "Contract modification" },
                    { 68, 9, true, 0m, 0m, 0m, 2, "CDRL updates" },
                    { 69, 9, true, 0m, 0m, 0m, 3, "MIPR" },
                    { 70, 9, true, 0m, 0m, 0m, 4, "MOU/MOA Updates" },
                    { 71, 10, true, 0m, 0m, 0m, 1, "Funding Review" },
                    { 72, 10, true, 0m, 0m, 0m, 2, "Type of funding" },
                    { 73, 10, true, 0m, 0m, 0m, 3, "FY analysis" },
                    { 74, 11, true, 0m, 0m, 0m, 1, "Schedule" },
                    { 75, 12, true, 0m, 0m, 0m, 1, "Intelligence" },
                    { 76, 12, true, 0m, 0m, 0m, 2, "Impact to SCG" },
                    { 77, 12, true, 0m, 0m, 0m, 3, "Impact to Facility Security" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_BlockOwnerId",
                table: "Blocks",
                column: "BlockOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailedSiaSections_FunctionalAreaId",
                table: "DetailedSiaSections",
                column: "FunctionalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureScoreResult_ProjectId",
                table: "FeatureScoreResult",
                column: "ProjectId",
                unique: true,
                filter: "[ProjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureScoreResult_WsmRequestId",
                table: "FeatureScoreResult",
                column: "WsmRequestId",
                unique: true,
                filter: "[WsmRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalAreas_Name",
                table: "FunctionalAreas",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_UserId",
                table: "Licenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseUsages_LicenseId",
                table: "LicenseUsages",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_MRLResponses_MRLSubThreadId",
                table: "MRLResponses",
                column: "MRLSubThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_MRLResponses_UpdatedByUserId",
                table: "MRLResponses",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MRLResponses_WsmRequestId_MRLSubThreadId",
                table: "MRLResponses",
                columns: new[] { "WsmRequestId", "MRLSubThreadId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringFormulaWeights_UpdatedBy",
                table: "ScoringFormulaWeights",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TRL_levels_WsmRequestId",
                table: "TRL_levels",
                column: "WsmRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMaps_RoleId",
                table: "UserRoleMaps",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleRequests_RoleId",
                table: "UserRoleRequests",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleRequests_UserId",
                table: "UserRoleRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CertificateThumbprint",
                table: "Users",
                column: "CertificateThumbprint",
                unique: true,
                filter: "[CertificateThumbprint] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DesignOwnerId",
                table: "Users",
                column: "DesignOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WsmChangeDrivers_ChangeDriverTypeId",
                table: "WsmChangeDrivers",
                column: "ChangeDriverTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmComments_AuthorId",
                table: "WsmComments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmComments_WsmRequestId",
                table: "WsmComments",
                column: "WsmRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmCostAssessments_WsmRequestId",
                table: "WsmCostAssessments",
                column: "WsmRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WsmDeliverables_DeliverableId",
                table: "WsmDeliverables",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmDesignOwnerAssignments_AssignedByUserId",
                table: "WsmDesignOwnerAssignments",
                column: "AssignedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmDesignOwnerAssignments_DesignOwnerId",
                table: "WsmDesignOwnerAssignments",
                column: "DesignOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmDesignOwnerAssignments_WsmRequestId",
                table: "WsmDesignOwnerAssignments",
                column: "WsmRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmDetailedSiaResponses_DetailedSiaSectionId",
                table: "WsmDetailedSiaResponses",
                column: "DetailedSiaSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmFunctionalImpacts_FunctionalAreaId",
                table: "WsmFunctionalImpacts",
                column: "FunctionalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmFunctionalImpacts_ReviewerId",
                table: "WsmFunctionalImpacts",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmImpactAnalyses_WsmRequestId",
                table: "WsmImpactAnalyses",
                column: "WsmRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WsmInterfaceDetails_TargetWsmId",
                table: "WsmInterfaceDetails",
                column: "TargetWsmId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmInterfaceDetails_WsmRequestId_TargetWsmId_IsInternal",
                table: "WsmInterfaceDetails",
                columns: new[] { "WsmRequestId", "TargetWsmId", "IsInternal" },
                unique: true,
                filter: "[TargetWsmId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WsmMilestoneResponses_MilestoneCriterionId",
                table: "WsmMilestoneResponses",
                column: "MilestoneCriterionId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmMilestoneResponses_UpdatedByUserId",
                table: "WsmMilestoneResponses",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmMilestoneResponses_WsmRequestId",
                table: "WsmMilestoneResponses",
                column: "WsmRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmMilestoneScores_WsmRequestId_MilestoneType",
                table: "WsmMilestoneScores",
                columns: new[] { "WsmRequestId", "MilestoneType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WsmMissionImpactAssessments_WsmRequestId",
                table: "WsmMissionImpactAssessments",
                column: "WsmRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmOriAssessments_WsmRequestId",
                table: "WsmOriAssessments",
                column: "WsmRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WsmOtherDetails_WsmRequestId",
                table: "WsmOtherDetails",
                column: "WsmRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmOwners_UserId",
                table: "WsmOwners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmRequests_ApprovedDesignOwnerId",
                table: "WsmRequests",
                column: "ApprovedDesignOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmRequests_BlockId",
                table: "WsmRequests",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmRequests_ModificationTypeId",
                table: "WsmRequests",
                column: "ModificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmRequests_OriginatorInfoId",
                table: "WsmRequests",
                column: "OriginatorInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmRequests_RecommendedDesignOwnerId",
                table: "WsmRequests",
                column: "RecommendedDesignOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmRequests_RequestNumber",
                table: "WsmRequests",
                column: "RequestNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WsmRequests_WsmOwnerUserId",
                table: "WsmRequests",
                column: "WsmOwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmSiaScores_WsmRequestId",
                table: "WsmSiaScores",
                column: "WsmRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WsmTimeCriticalityAssessments_WsmRequestId",
                table: "WsmTimeCriticalityAssessments",
                column: "WsmRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WsmWbsStructures_WsmRequestId",
                table: "WsmWbsStructures",
                column: "WsmRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WsmWeights_WsmRequestId",
                table: "WsmWeights",
                column: "WsmRequestId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalWorkflows");

            migrationBuilder.DropTable(
                name: "FeatureScoreResult");

            migrationBuilder.DropTable(
                name: "LicenseUsages");

            migrationBuilder.DropTable(
                name: "MbseResults");

            migrationBuilder.DropTable(
                name: "MilestoneWeights");

            migrationBuilder.DropTable(
                name: "MRLResponses");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "ScoringFormulaWeights");

            migrationBuilder.DropTable(
                name: "TRL_levels");

            migrationBuilder.DropTable(
                name: "UserRoleMaps");

            migrationBuilder.DropTable(
                name: "UserRoleRequests");

            migrationBuilder.DropTable(
                name: "WsmChangeDrivers");

            migrationBuilder.DropTable(
                name: "WsmComments");

            migrationBuilder.DropTable(
                name: "WsmCostAssessments");

            migrationBuilder.DropTable(
                name: "WsmDeliverables");

            migrationBuilder.DropTable(
                name: "WsmDesignOwnerAssignments");

            migrationBuilder.DropTable(
                name: "WsmDetailedSiaResponses");

            migrationBuilder.DropTable(
                name: "WsmFunctionalImpacts");

            migrationBuilder.DropTable(
                name: "WsmImpactAnalyses");

            migrationBuilder.DropTable(
                name: "WsmInterfaceDetails");

            migrationBuilder.DropTable(
                name: "WsmMilestoneResponses");

            migrationBuilder.DropTable(
                name: "WsmMilestoneScores");

            migrationBuilder.DropTable(
                name: "WsmMissionImpactAssessments");

            migrationBuilder.DropTable(
                name: "WsmOriAssessments");

            migrationBuilder.DropTable(
                name: "WsmOtherDetails");

            migrationBuilder.DropTable(
                name: "WsmSiaScores");

            migrationBuilder.DropTable(
                name: "WsmTimeCriticalityAssessments");

            migrationBuilder.DropTable(
                name: "WsmWbsStructures");

            migrationBuilder.DropTable(
                name: "WsmWeights");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "MRLSubThreads");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ChangeDriverTypes");

            migrationBuilder.DropTable(
                name: "WsmOwners");

            migrationBuilder.DropTable(
                name: "Deliverables");

            migrationBuilder.DropTable(
                name: "DetailedSiaSections");

            migrationBuilder.DropTable(
                name: "MilestoneCriteria");

            migrationBuilder.DropTable(
                name: "WsmRequests");

            migrationBuilder.DropTable(
                name: "FunctionalAreas");

            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.DropTable(
                name: "ModificationTypes");

            migrationBuilder.DropTable(
                name: "OriginatorInfos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RecommendedDesignOwners");
        }
    }
}
