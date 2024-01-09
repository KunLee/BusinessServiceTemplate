namespace BusinessServiceTemplate.Core.Cache
{
    public interface ICacheManager
    {
        Object this[string category, string key] { get; set; }
    }
}
