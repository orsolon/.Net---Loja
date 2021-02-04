using System;


namespace LojaGama.Entidades
{
    public class Boleto
    {
        private const int DiasVencimento = 15;
        private const double Juros = 0.10;
       

        //gerar o construtor para inicair o boleto -atalho ctor
        public Boleto(string cpf, 
                      double valor,
                      string descricao)
        {

            //quando for criar uma nova instancia, tem que ser passado esses valores para iniciar
            Cpf = cpf;
            Valor = valor;
            Descricao = descricao;
            DataEmissao = DateTime.Now;
            Confirmacao = false;
        }


        public Guid CodigoBarra { get; set; }
        public double Valor { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }

        public bool Confirmacao { get; set; }
        public string Cpf { get; set; }
        public string Descricao { get; set; }


        public void GerarBoleto()
        {
            CodigoBarra = Guid.NewGuid();
            DataVencimento = DataEmissao.AddDays(DiasVencimento);
        }

        public bool EstaPago()
        {
            return Confirmacao;
        }
        public void CalcularJuros()
        {
            var taxa = Valor * Juros;
            Valor = Valor + taxa;
        }

        public void Pagar ()
        {
            DataPagamento = DateTime.Now;
            Confirmacao = true;
        }

        

    }
}
