using System;

namespace ClinicaOdonto.Dominio.Comum
{
    // ABSTRAÇÃO: Uma classe abstrata não pode ser instanciada diretamente.
    // Ela define um contrato de dados e comportamentos comuns para a hierarquia.
    public abstract class Pessoa
    {
        // ENCAPSULAMENTO: Propriedades com "private set" impedem modificações externas
        // maliciosas ou acidentais, garantindo a integridade dos dados.
        public string CPF { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }

        // VALUE OBJECT
        public Whatsapp Whatsapp { get; private set; }

        // CONSTRUTOR: Garante que nenhuma Pessoa nasça com dados inválidos ou nulos
        protected Pessoa(string cpf, string nome, DateTime dataNascimento, Whatsapp whatsapp)
        {
            if (string.IsNullOrWhiteSpace(cpf)) throw new ArgumentException("CPF é obrigatório.");
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.");
            if (dataNascimento >= DateTime.Today) throw new ArgumentException("Data de nascimento inválida.");

            CPF = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
            Whatsapp = whatsapp ?? throw new ArgumentNullException(nameof(whatsapp));
        }

        // POLIMORFISMO: Método virtual que permite que as subclasses 
        // alterem ou estendam o comportamento de exibição de dados se necessário.
        public virtual string ObterDadosResumidos()
        {
            return $"Nome: {Nome} | CPF: {CPF}";
        }
    }
}