using PointScore.Models.DTOs;

namespace PointScore.Services
{
    public interface IDeliverableCatalogService
    {
        /// <summary>
        /// Obtiene todos los deliverables del catálogo
        /// </summary>
        IEnumerable<DeliverableCatalogDto> GetAllDeliverables(IEnumerable<FunctionalAreaDto>? functionalAreas = null);

        /// <summary>
        /// Obtiene deliverables filtrados según criterios
        /// </summary>
        IEnumerable<DeliverableCatalogDto> GetFilteredDeliverables(DeliverableCatalogFilterDto filter, IEnumerable<FunctionalAreaDto>? functionalAreas = null);

        /// <summary>
        /// Obtiene un deliverable específico por ID
        /// </summary>
        DeliverableCatalogDto? GetDeliverableById(int id, IEnumerable<FunctionalAreaDto>? functionalAreas = null);

        /// <summary>
        /// Obtiene todas las áreas funcionales únicas
        /// </summary>
        IEnumerable<string> GetFunctionalAreas();

        /// <summary>
        /// Obtiene deliverables por área funcional
        /// </summary>
        IEnumerable<DeliverableCatalogDto> GetDeliverablesByFunctionalArea(string functionalArea, IEnumerable<FunctionalAreaDto>? functionalAreas = null);

        /// <summary>
        /// Obtiene solo deliverables aplicables a WSM (con X en la columna)
        /// </summary>
        IEnumerable<DeliverableCatalogDto> GetApplicableWSMDeliverables(IEnumerable<FunctionalAreaDto>? functionalAreas = null);

        /// <summary>
        /// Obtiene estadísticas del catálogo
        /// </summary>
        object GetCatalogStatistics(IEnumerable<FunctionalAreaDto>? functionalAreas = null);

        /// <summary>
        /// Obtiene deliverables filtrados por rol (basado en DefaultRoleName de áreas funcionales)
        /// </summary>
        IEnumerable<DeliverableCatalogDto> GetDeliverablesByRole(string roleName, IEnumerable<FunctionalAreaDto>? functionalAreas = null);

        /// <summary>
        /// Obtiene deliverables filtrados por múltiples roles (basado en DefaultRoleName de áreas funcionales)
        /// Incluye caso especial para "CM" que filtra por FunctionalArea directamente
        /// </summary>
        IEnumerable<DeliverableCatalogDto> GetDeliverablesByRoles(IEnumerable<string> roleNames, IEnumerable<FunctionalAreaDto>? functionalAreas = null);
    }
}
