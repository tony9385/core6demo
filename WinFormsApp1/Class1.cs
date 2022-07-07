using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public  class BaseClass
    {

        public string FactoryId { get; set; }    
        public List<int> Equipments { get; set; }    
    }

    public class ClassA:BaseClass
    {
        public int Id { get; set; } 
    } 
    public class ClassB:BaseClass
    {
        public int Id2 { get; set; } 
    }


    public class Class2<T> where T : BaseClass
    {
        public T ClassInfo { get; set; }   
    }


    public class Class3
    {
        public void Handle()
        {
            var c1 = new Class2<ClassA>();
            var c2 = new Class2<ClassB>();

            //c1.ClassInfo.Equipments.Where()
        }
    }
}
