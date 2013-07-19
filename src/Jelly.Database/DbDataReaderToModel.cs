using System.Data.Common;

namespace Jelly.Database
{
    /// <summary>
    /// Represents the method that cast <see cref="DbDataReader"/> to model. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate T DbDataReaderToModel<T>(DbDataReader reader);

}


