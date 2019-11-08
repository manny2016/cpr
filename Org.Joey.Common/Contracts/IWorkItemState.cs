

namespace Org.Joey.Common
{
    public interface IWorkItemState
    {
        string Name { get;  }
        void Update();
    }
}
