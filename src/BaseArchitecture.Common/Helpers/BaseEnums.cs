using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Common.Helpers
{
    public class BaseEnums
    {

        /// <summary>
        /// Estados de entidad para ejecutar un eliminar o crear registro
        /// </summary>
        public enum StatusFile
        {
            /// <summary>
            /// Nuevo registro
            /// </summary>
            New = 1,

            /// <summary>
            /// Solo lectura 
            /// </summary>
            NoTouch = 2,

            /// <summary>
            /// Eliminar registro
            /// </summary>
            Delete = 3
        }


        public enum EntityState
        {
            /// <summary>
            /// Sin cambios en la entidad
            /// </summary>
            Unchanged = 0,

            /// <summary>
            /// Marcado para eliminar
            /// </summary>
            Deleted = 1,

            /// <summary>
            /// Marcado para modificar.
            /// </summary>
            Modified = 2,

            /// <summary>
            /// Marcado para agregar.
            /// </summary>
            Added = 3
        }
    }

}
