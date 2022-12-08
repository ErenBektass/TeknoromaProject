using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.GenericRepository.IntRep
{
    public interface IRepository<T> where T:BaseEntity
    {
        //List Commands

        List<T> GetAll(); // bu metot ilgili T neyse o yapıdaki tüm verileri getirecek
        List<T> GetActives(); //bu metot sadece Aktif kullanımda olan verileri getirecek
        List<T> GetPassives();  //bu metot sadece Pasif olan verileri getirecek
        List<T> GetUpdateds(); // bu metot sadece güncellenmiş olan verileri getirecek


        //Modify Commands
        void Add(T item);
        void AddRange(List<T> list);
        void Delete(T item); // bu metot veriyi pasife ceker..
        void DeleteRange(List<T> list);
        void Update(T item);
        void UpdateRange(List<T> list);
        void Destroy(T item); //bu metot veriyi yok eder
        void DestroyRange(List<T> list);

            
        List<T> Where(Expression<Func<T, bool>> exp);

        //Delegate Kullanımı
        bool Any(Expression<Func<T, bool>> exp);
        T FirstOfDefault(Expression<Func<T, bool>> exp);
        object Select(Expression<Func<T, object>> exp);



        //Find Command
        T Find(int id);
        List<T> GetLastDatas(int number);
        List<T> GetFirstDatas(int number);





    }
}
