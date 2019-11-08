
using System.Data;

namespace Org.Joey.Common
{
    public interface IDatabase
    {

    }
    public interface IDatabaseFactory
    {
        IDbConnection GenerateDatabase(string connectString = null);
    }
}
