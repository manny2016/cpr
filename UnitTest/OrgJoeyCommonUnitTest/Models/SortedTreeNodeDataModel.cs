
namespace OrgJoeyCommonUnitTest.Models
{
    using Org.Joey.Common;
    public class SortedTreeNodeDataModel : ISortedTreeNodeKey
    {
        public SortedTreeNodeDataModel(string name)
        {
            this.Name = name;
        }
        public string Name { get; private set; }
    }
}
