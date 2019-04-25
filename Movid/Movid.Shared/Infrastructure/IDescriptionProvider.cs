

namespace Brainnom.Core.ObjectSummary
{
    public interface IDescriptionProvider
    {
        string FindDescription(object entity);

        string[] FindDescriptionPropertyNames(object entity);
    }
}