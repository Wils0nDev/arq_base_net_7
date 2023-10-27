using System;
using System.Collections.Generic;
using System.Text;

namespace BaseArchitecture.Domain
{

    /// <summary>
    /// Genera una nueva entidad copiando sus Keys ID 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenerateEntity<TEntity>
    {

        /// <summary>
        /// Genera una nueva instancia del objeto  TEntity con su clave.
        /// </summary>
        /// <returns></returns>
        TEntity RecoverKey();

    }
}
