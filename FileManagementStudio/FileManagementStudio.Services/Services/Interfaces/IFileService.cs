namespace FileManagementStudio.Services.Services.Interfaces
{
    internal interface IFileService<TEntity> : IGeneralService<TEntity> where TEntity : class
    {
    }
}
