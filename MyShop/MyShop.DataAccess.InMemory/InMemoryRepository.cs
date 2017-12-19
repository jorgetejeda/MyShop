using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Runtime.Caching;
using MyShop.Core.Models;
using MyShop.Core.Contracts;

namespace MyShop.DataAccess.InMemory
{
    //Para no crear por cada entidad las opciones de insertar, editar
    //Actualizar etc... creamos una clase la cual recibe cualquier objeto ( Class Name<T>)
    //Donde el Obejto heradara de la clase BaseEntity
    //Baes entitiy es una clase abstracta para que pueda ser heredada
    //La cual contiene las propiedades genericas que usan todas las clases
    //Tal como Id o FechaCrea
    //public class InMemoryRepository<T> where T : BaseEntity //-------------old
    public class InMemoryRepository<T> : IRepository<T>  where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items; //Creamos nuestra lista que almanecenara el objeto que se envie, ej: List<ProductCategory> productCategories;
        string className; //Una variable nombre para almanecenar el nombre del objeto que venga

        public InMemoryRepository()
        {
            // cuando mande una clase esta remplazara a <T> ejemplo Envian la clase Product entonces seria <Product>
            //Con la funcion typeof(Objeto).Name estamos preguntado por el nombre del objeto
            className = typeof(T).Name; //lo guardamos en nuestra variable string
            // Ahora creamos nuestra cache esta tendra el nombre de la clase que se nos envie
            // Recordando que <T> esta nuestro obejto y con la funcion de arriba conseguimos el nombre del objeto
            // El cual se lo vamos a dar a nuestro cache
            // ej, cache["productCategories"] as List<ProductCategory>;
            items = cache[className] as List<T>; 
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public static void lkl()
        {

        }

        public void Insert(T t)
        {
            items.Add(t);
            Commit();
        }

        public void Update(T t)
        {
            T toUpdate = items.Find(i => i.Id == t.Id);
            if (toUpdate != null)
            {
                toUpdate = t;
                Commit();
            }
            else
            {
                throw new Exception(className + "not found");
            }
        }

        public T Find(string Id)
        {
            T toFind = items.Find(i => i.Id == Id);
            if (toFind != null)
            {
                Commit();
                return toFind;
            }
            else
            {
                throw new Exception(className + "not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T toDelete = items.Find(i => i.Id == Id);
            if (toDelete != null)
            {
                items.Remove(toDelete);
            }
            else
            {
                throw new Exception(className + "not found");
            }
        }

    }
}
