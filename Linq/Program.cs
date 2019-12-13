using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Linq
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //           Pessoa[] pessoas = new Pessoa[] { new Pessoa(1, "Maria"), new Pessoa(2, "Paulo"), new Pessoa(3, "Marcus") };

            //           Veiculo[] veiculos = new Veiculo[] { new Veiculo(1, "Fiat", 1), new Veiculo(2, "Ford", 2), new Veiculo(3, "Ferrari", 1) };

            //           var query = from pessoa in pessoas
            //                       join veiculo in veiculos on pessoa.Codigo
            //equals veiculo.CodigoCliente
            //                       orderby pessoa.Nome ascending
            //                       select new Veiculo { Nome = veiculo.Nome, Codigo = veiculo.Codigo, Pessoa = pessoa };



            //           foreach (var item in query.Reverse())
            //           {
            //               Console.WriteLine($"{item.Nome} {item.Codigo} {item.Pessoa.Nome}");
            //           }

            Context context = new Context();

            var query = from pessoa in context.Pessoa
                        join veiculo in context.Veiculo on pessoa.Codigo
                        equals veiculo.CodigoCliente
                        orderby pessoa.Nome ascending, veiculo.Nome ascending
                        select new { Nome = veiculo.Nome, Codigo = veiculo.Codigo, Pessoa = pessoa };


            List<Veiculo> veiculos = new List<Veiculo>();

            foreach (var valor in query)
            {

                Veiculo veiculo = new Veiculo()
                {
                    Codigo = valor.Codigo,
                    Nome = valor.Nome,
                    Pessoa = valor.Pessoa
                };
                veiculos.Add(veiculo);
               
            }



            foreach (var item in veiculos)
            {
                Console.WriteLine($"{item.Codigo} {item.Nome} {item.Pessoa.Nome}");
            }
        }


        [Table("Pessoas")]
        public class Pessoa
        {
            [Key]
            public int Codigo { get; set; }
            public string Nome { get; set; }

            public Pessoa() { }

            public Pessoa(int codigo, string nome)
            {
                Codigo = codigo;
                Nome = nome;
            }

        }

        [Table("Veiculos")]
        public class Veiculo
        {
            [Key]
            public int Codigo { get; set; }

            public string Nome { get; set; }

            [ForeignKey(nameof(Pessoa))]
            public int CodigoCliente { get; set; }

            public virtual Pessoa Pessoa { get; set; }

            public Veiculo() { }

            public Veiculo(int codigo, string nome, int codigoCliente)
            {
                Codigo = codigo;
                Nome = nome;
                CodigoCliente = codigoCliente;
            }

            public static explicit operator Veiculo(List<object> v)
            {
                throw new NotImplementedException();
            }
        }

        public class Context : DbContext
        {
            public Context() : base("sql")
            {
                Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());
            }

            public DbSet<Pessoa> Pessoa { get; set; }
            public DbSet<Veiculo> Veiculo { get; set; }

        }

        public interface IEmail
        {
            IEmail CreateInstance();
        }
    }
}

