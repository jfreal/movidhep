
namespace Brainnom.Core.ObjectSummary
{
    public interface IKeyProvider
    {
        ObjectSummaryKeys FindKeys(object entity);
    }
}