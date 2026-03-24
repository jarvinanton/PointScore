using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models;

/// <summary>
/// Representa un proyecto en la base de datos.
/// Esta entidad tiene una relación de uno a uno con FeatureScoreResult.
/// </summary>
public class Project
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    // Propiedad de navegación para la relación de uno a uno con FeatureScoreResult.
    // Esta propiedad es opcional, ya que un proyecto puede existir sin una puntuación de característica.
    public FeatureScoreResult? FeatureScoreResult { get; set; }
}