using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAllProducto(string nombre)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IvBetoExamenBlazorContext context = new DL.IvBetoExamenBlazorContext())
                {
                    //Busqueda por nombre en codigo de LINQ en NET Core
                    var query = (from producto in context.Productos
                                 join cate in context.Categoria on producto.IdCategoria equals cate.IdCategoria
                                 where producto.Nombre.Contains($"{nombre}")
                                 select new
                                 {
                                     IdProducto = producto.IdProducto,
                                     Nombre = producto.Nombre,
                                     Precio = producto.Precio,
                                     Categoria = producto.IdCategoria,
                                     NombreCategoria = cate.NombreCategoria
                                 });

                    if (query != null && query.ToList().Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.Categoria = new ML.Categoria();

                            producto.IdProducto = item.IdProducto;
                            producto.Nombre = item.Nombre;
                            producto.Precio = Convert.ToDouble(item.Precio);
                            producto.Categoria.IdCategoria = (int)item.Categoria;
                            producto.Categoria.NombreCategoria = item.NombreCategoria;

                            result.Objects.Add(producto);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay productos en lista";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IvBetoExamenBlazorContext context = new DL.IvBetoExamenBlazorContext())
                {
                    DL.Producto product = new DL.Producto();
                    product.IdProducto = producto.IdProducto;
                    product.Nombre = producto.Nombre;
                    product.Precio = producto.Precio;
                    product.IdCategoria = producto.Categoria.IdCategoria;

                    context.Productos.Add(product);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IvBetoExamenBlazorContext context = new DL.IvBetoExamenBlazorContext())
                {
                    var query = (from prod in context.Productos
                                 where prod.IdProducto == producto.IdProducto
                                 select prod).SingleOrDefault();
                    if (query != null)
                    {
                        query.IdProducto = producto.IdProducto;
                        query.Nombre = producto.Nombre;
                        query.Precio = producto.Precio;
                        query.IdCategoria = producto.Categoria.IdCategoria;

                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ha ocurrido un error en el proceso, no se pudo actualizar el producto.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Delete(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IvBetoExamenBlazorContext context = new DL.IvBetoExamenBlazorContext())
                {
                    var query = (from prod in context.Productos
                                 where prod.IdProducto == idProducto
                                 select prod).FirstOrDefault();

                    context.Productos.Remove(query);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetById(int idProdcuto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IvBetoExamenBlazorContext context = new DL.IvBetoExamenBlazorContext())
                {
                    var query = (from prod in context.Productos
                                 join cat in context.Categoria on prod.IdCategoria equals cat.IdCategoria
                                 where prod.IdProducto == idProdcuto
                                 select new
                                 {
                                     IdProducto = prod.IdProducto,
                                     Nombre = prod.Nombre,
                                     Precio = prod.Precio,
                                     IdCategoria = cat.IdCategoria,
                                     NombreCategoria = cat.NombreCategoria
                                 });
                    if (query != null && query.ToList().Count > 0)
                    {
                        result.Object = new object();
                        foreach (var item in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.Categoria = new ML.Categoria();

                            producto.IdProducto = item.IdProducto;
                            producto.Nombre = item.Nombre;
                            producto.Precio = Convert.ToDouble(item.Precio);
                            producto.Categoria.IdCategoria = item.IdCategoria;
                            producto.Categoria.NombreCategoria = item.NombreCategoria;

                            result.Object = producto;
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró el producto seleccionado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }

}
