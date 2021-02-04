using LojaGama.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        private static List<Boleto> listaBoletos;

        static void Main(string[] args)
           
        {
            listaBoletos = new List<Boleto>();
            while (true)
            {
                Console.WriteLine("================================================"  );
                Console.WriteLine("=============Loja das Meninas Gama======================");
                Console.WriteLine("Selecione uma opção!");
                Console.WriteLine("1-Compra | 2-Pagamneto");

                var opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Comprar();
                        break;
                    case 2:
                        Pagamento();
                        break;
                    default:
                        break;
                }
            }
        }

        public static void Comprar()
        {
            Console.WriteLine("Digite o valor da compra:");
            var valor = Decimal.Parse(Console.ReadLine());

            Console.WriteLine("Digite o cpf do cliente:");
            var cpf = Console.ReadLine();

            Console.WriteLine("Escreva uma descricao se necesario");
            var descricao = Console.ReadLine();

            var boleto = new Boleto(cpf, valor, descricao);
            boleto.GerarBoleto();

            Console.WriteLine($"Boleto gerado com seucesso com o numero {boleto.CodigoBarra} com data de vencimento para {boleto.DataVencimento}");

            listaBoletos.Add(boleto);
        }

        public static void Pagamento()
        {
            Console.WriteLine("Digite o codigo e barras:");
            var numero = Guid.Parse(Console.ReadLine());

            var boleto = listaBoletos
                            .Where(item => item.CodigoBarra == numero)
                            .FirstOrDefault();

            if(boleto is null)
            {
                Console.WriteLine($"Boleto de código {numero} não encontrado!");
                return;
            }

            if(boleto.DataVencimento < DateTime.Now)
            {
                boleto.CalcularJuros();
                Console.WriteLine($"Boleto esta vencido, terá acrescimo de 10% === R$ {boleto.Valor}");
            }

            boleto.Pagar();
            Console.WriteLine($"Boleto de código {numero} pago com sucesso!");
        }
    }

}
