using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Categoria
    {
        public static ML.Result GetAllCategoria()
        {
            ML.Result result = new ML.Result();
            using(DL.IvBetoExamenBlazorContext context = new DL.IvBetoExamenBlazorContext())
            {
                var query = (from categoria in context.Categoria
                             select new
                             {
                                 IdCategoria = categoria.IdCategoria,
                                 NombreCategoria = categoria.NombreCategoria
                             });
                if (query != null && query.ToList().Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach(var item in query)
                    {
                        ML.Categoria aux = new ML.Categoria();
                        aux.IdCategoria = item.IdCategoria;
                        aux.NombreCategoria = item.NombreCategoria;

                        result.Objects.Add(aux);
                        result.Correct = true;
                    }
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontraron categorias disponibles";
                }
            }
            return result;
        }
    }
}
