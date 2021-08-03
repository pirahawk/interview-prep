using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace InterviewPrepCode.Sandbox
{
    public class MyTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public MyTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void RunTest()
        {
            List<Category> categories = new List<Category>()
            {
                new Category {Name="Beverages", ID=001},
                new Category {Name="Condiments", ID=002},
                new Category {Name="Vegetables", ID=003},
                new Category {Name="Grains", ID=004},
                new Category {Name="Fruit", ID=005}
            };

            // Specify the second data source.
            List<Product> products = new List<Product>()
            {
                new Product {Name="Cola",  CategoryID=001},
                new Product {Name="Tea",  CategoryID=001},
                new Product {Name="Mustard", CategoryID=002},
                new Product {Name="Pickles", CategoryID=002},
                new Product {Name="Carrots", CategoryID=003},
                new Product {Name="Bok Choy", CategoryID=003},
                new Product {Name="Peaches", CategoryID=005},
                new Product {Name="Melons", CategoryID=005},
            };

            var j = from category in categories
                    join product in products on category.ID equals product.CategoryID into something

                    select something;

            // select new
            // {
            //     category,
            //     //product
            //     something,
            //     AllProducsts = something.ToArray()
            // };

            var j1 = from cat in categories
                     join prod in products on cat.ID equals prod.CategoryID into prodCats
                     from prodCat in prodCats
                     group prodCat by prodCat.CategoryID;

            var j2 = j1.OrderByDescending(pc => pc.Count());


        }

        [Fact]
        void TestConvert()
        {
            var s = "All the Extension methods have all the capabilities of the regular static methods.";

            var a = new[] { 1,2,3};
            var b = new[] {3, 4, 5};

            var count = 1;

            var result = a.Zip(b).ToArray();


            Animal an = new Cat();
            var r = an.Foo();
            var r2 = (an as Cat).Foo();
        }

        class Product
        {
            public string Name { get; set; }
            public int CategoryID { get; set; }
        }

        class Category
        {
            public string Name { get; set; }
            public int ID { get; set; }
        }

        class Animal
        {
            public string Name { get; set; }

            public string Foo()
            {
                return "Animal";
            }
        }

        private class Cat : Animal
        {
            public new string Foo()
            {
                return "Cat";
            }
        }

        private class Dog : Animal
        {
        }

        interface IAnimalMaker<out TAnimal> where TAnimal : Animal
        {
            TAnimal Make();
        }

        private class DogMaker : IAnimalMaker<Dog>
        {
            public Dog Make()
            {
                return new Dog();
            }
        }

        interface IAnimalName<in TAnimal> where TAnimal : Animal
        {
            void NameIt(TAnimal animal);
        }

        private class AnimalName<TAnimal> : IAnimalName<TAnimal> where TAnimal : Animal
        {
            public void NameIt(TAnimal animal)
            {
                animal.Name = "Fido";
            }
        }

        [Fact]
        void TestVariance()
        {
            IAnimalMaker<Dog> a = new DogMaker();

            IAnimalMaker<Animal> ani = a;

            var result = ani.Make();

            IAnimalName<Animal> animalName = new AnimalName<Animal>();

            IAnimalName<Dog> dogger = animalName;

            dogger.NameIt(new Dog());
        }

        [Fact]
        void TestArrs()
        {
            int wha = 0x7FFFFFFF;

            int[] a1 = Enumerable.Range(1, 7).ToArray();
            int[] b = new int[a1.Length-1];

            Array.Copy(a1, 1, 
                b, 0, 
                b.Length);


            var a3 = new Dictionary<int, string>();

            a3[2] = "hello";
            a3[2] = "there";
        }


        [Fact]
        private void HashTest()
        {
            var buckets = new int[1024];
            var hash1 = "Hello".GetHashCode() & int.MaxValue;
            var hash2 = "There".GetHashCode() & int.MaxValue;


            _testOutputHelper.WriteLine($"{hash1}: {hash1 % buckets.Length}");
            _testOutputHelper.WriteLine($"{hash2}: {hash2 % buckets.Length}");
        }
    }
}