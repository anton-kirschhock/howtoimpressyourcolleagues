namespace Kirschhock.HTIYC.Common.Abstractions
{
    public interface INamedResource : IResource
    {
        string Name { get; }
    }
}
