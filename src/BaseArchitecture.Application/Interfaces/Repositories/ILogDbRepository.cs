namespace BaseArchitecture.Application.Interfaces.Repositories
{
    public  interface ILogDbRepository
    {
        /// <summary>
        /// Eliminar registros antiguos por lotes
        /// </summary>
        Task<int> DeleteOlds(CancellationToken cancellationToken = default);
    }
}
