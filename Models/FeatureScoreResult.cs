using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models;

public class FeatureScoreResult
{
    // Propiedad estándar para la clave primaria de la base de datos.
    public int? Id { get; set; }

      // Esta es la corrección clave: la llave foránea y la propiedad de navegación.
    // La llave foránea 'ProjectId' debe ser 'int' y no 'int?' para ser un campo requerido.
    public int? ProjectId { get; set; }
    public Guid? WsmRequestId { get; set; }
    public virtual WsmRequest? WsmRequest { get; set; }

    // Propiedades de ENTRADA


    public decimal? ENG_SIA { get; set; }
    public decimal? ENG_SIA_WT { get; set; }
    public decimal? FNTL_SIA { get; set; }
    public decimal? FNTL_SIA_WT { get; set; }
    public decimal? COST_IMP { get; set; }
    public decimal? COST_WT { get; set; }
    public decimal? SCH_IMP { get; set; }
    public decimal? SCH_IMP_WT { get; set; }
    public decimal? TECH_IMP { get; set; }
    public decimal? TECH_WT { get; set; }
    public decimal? FNTL_IMP { get; set; }
    public decimal? FNTL_IMP_WT { get; set; }
    public decimal? TRL { get; set; }
    public decimal? TRL_WT { get; set; }
    public decimal? DELIV { get; set; }
    public decimal? DELIV_WT { get; set; }
    public decimal? MRL { get; set; }
    public decimal? MRL_WT { get; set; }
    public decimal? SRR { get; set; }
    public decimal? SRR_WT { get; set; }
    public decimal? PDR { get; set; }
    public decimal? PDR_WT { get; set; }
    public decimal? CDR { get; set; }
    public decimal? CDR_WT { get; set; }
    public decimal? SRR_PDR_CDR_WT { get; set; }
    public decimal? INTERD { get; set; }
    public decimal? INTERD_WT { get; set; }
    public decimal? SELFDEP { get; set; }
    public decimal? SELFDEP_WT { get; set; }
    public decimal? ORI { get; set; }
    public decimal? ORI_WT { get; set; }
    public decimal? USER_IMP { get; set; }
    public decimal? USER_IMP_WT { get; set; }
    public decimal? TIME_CRIT { get; set; }
    public decimal? TIME_CRIT_WT { get; set; }
    public decimal? HICAT_TECH_WT { get; set; }
    public decimal? HICAT_FNTL_WT { get; set; }
    public decimal? HICAT_SCH_WT { get; set; }
    public decimal? HICAT_USER_WT { get; set; }
    public decimal? HICAT_COST_WT { get; set; }
    public decimal? DEV_COST { get; set; }
    public decimal? PROD_COST { get; set; }
    public decimal? PROD_QTY { get; set; }
    public decimal? TOT_PROD_COST { get; set; }
    public decimal? INSTALL_COST { get; set; }
   // public string? AI_RESPONSE { get; set; }

    // Propiedades de SALIDA
    public decimal? WSM_COMP_SCORE { get; set; }
    public decimal? HICAT_TECH_SCORE { get; set; }
    public decimal? HICAT_FNTL_SCORE { get; set; }
    public decimal? HICAT_SCH_SCORE { get; set; }
    public decimal? HICAT_USER_SCORE { get; set; }
    public decimal? HICAT_COST_SCORE { get; set; }
    public decimal? RISK_TOL_LIKLI_SCORE { get; set; }
    public decimal? RISK_TOL_CONS_SCORE { get; set; }
    public decimal? TOTAL_WSM_COST { get; set; }
    public decimal? COST_CORR_SCORE { get; set; }
    public decimal? WSM_FINAL_PRIORITY_SCORE { get; set; }

    // Marca de tiempo de la creación del registro.
    public DateTime? CreatedAt { get; set; }

    // Propiedad de navegación que apunta a la entidad Project.
    public Project? Project { get; set; }
}