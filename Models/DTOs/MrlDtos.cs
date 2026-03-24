using System;
using System.Collections.Generic;

namespace PointScore.Models.DTOs
{
    /// <summary>
    /// DTO que representa una respuesta individual para un SubThread de MRL.
    /// </summary>
    public class MrlResponseDto
    {
        /// <summary>
        /// El ID del SubThread al que pertenece la respuesta.
        /// </summary>
        public int SubThreadId { get; set; }

        /// <summary>
        /// El puntaje asignado a este SubThread.
        /// </summary>
        public int Score { get; set; }
    }

    /// <summary>
    /// Solicitud para guardar múltiples respuestas de MRL para un WSM.
    /// </summary>
    public class SaveMrlResponsesRequest
    {
        /// <summary>
        /// El ID del WSM asociado a estas respuestas.
        /// </summary>
        public Guid WSMId { get; set; }

        /// <summary>
        /// Lista de respuestas individuales para los SubThreads.
        /// </summary>
        public List<MrlResponseDto> Responses { get; set; } = new List<MrlResponseDto>();
    }

    /// <summary>
    /// DTO que contiene el puntaje compuesto resultante del MRL para un WSM.
    /// </summary>
    public class MrlCompositeScoreDto
    {
        /// <summary>
        /// El ID del WSM asociado.
        /// </summary>
        public Guid WSMId { get; set; }

        /// <summary>
        /// El puntaje compuesto final calculado (Promedio).
        /// </summary>
        public decimal? CompositeScore { get; set; }
    }

    /// <summary>
    /// DTO que representa un SubThread de MRL para lectura.
    /// </summary>
    public class MrlSubThreadDto
    {
        public int Id { get; set; }
        public string SubThreadCode { get; set; } = null!;
        public string SubThreadName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DisplayOrder { get; set; }
    }

    /// <summary>
    /// DTO que representa un Thread de MRL agrupado con sus SubThreads.
    /// </summary>
    public class MrlThreadDto
    {
        public string ThreadName { get; set; } = null!;
        public List<MrlSubThreadDto> SubThreads { get; set; } = new List<MrlSubThreadDto>();
    }
}
