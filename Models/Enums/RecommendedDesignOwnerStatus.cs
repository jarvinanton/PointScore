namespace PointScore.Models.Enums
{
    /// <summary>
    /// Estado de la recomendación del Design Owner
    /// </summary>
    public enum RecommendedDesignOwnerStatus
    {
        /// <summary>
        /// Recomendado pero aún no aprobado
        /// </summary>
        Recommended = 0,
        
        /// <summary>
        /// Aprobado oficialmente
        /// </summary>
        Approved = 1,
        
        /// <summary>
        /// Denegado o rechazado
        /// </summary>
        Denied = 2
    }
}
