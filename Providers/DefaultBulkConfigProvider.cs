namespace GenericRepository.Core.Providers
{
    public class DefaultBulkConfigProvider : IBulkConfigProvider
    {
        public BulkConfig GetConfig() => new()
        {
            BatchSize = 1000,
            SetOutputIdentity = true,
            PreserveInsertOrder = true,
            TrackingEntities = false
        };
    }
}
