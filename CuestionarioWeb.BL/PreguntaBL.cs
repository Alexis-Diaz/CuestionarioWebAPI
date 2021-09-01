using System;

using CuestionarioWeb.DAL;
using CuestionarioWeb.EN;

namespace CuestionarioWeb.BL
{
    public class PreguntaBL
    {

        private PreguntaDAL _preguntaDAL;
        public PreguntaBL (Contexto context)
        {
            _preguntaDAL = new PreguntaDAL(context);
        }
    }
}
