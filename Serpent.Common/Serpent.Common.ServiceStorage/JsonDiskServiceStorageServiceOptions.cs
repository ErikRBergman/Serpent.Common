// ReSharper disable StyleCop.SA1402
namespace Serpent.Common.ServiceStorage
{
    // ReSharper disable once UnusedTypeParameter - used to identify it by dependency injection
    public class JsonDiskServiceStorageServiceOptions<T>
    {
        public string Filename { get; set; }
    }

    public class JsonDiskServiceStorageServiceOptions
    {
        public string Filename { get; set; }
    }
}