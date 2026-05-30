using System;
using ClinicaOdonto.Dominio.Comum;
using ClinicaOdonto.Dominio.AgregadoDentista;

namespace ClinicaOdonto.Dominio.AgregadoDentista
{

    // HERANÇA: Dentista estende Pessoa adicionando a propriedade de Especialidade
    public class Dentista : Pessoa
    {
        public Guid Id { get; private set; }
        public Especialidade Especialidade { get; private set; }

        public Dentista(string cpf, string nome, DateTime dataNascimento, string whatsapp, Especialidade especialidade)
            : base(cpf, nome, dataNascimento, whatsapp)
        {
            Id = Guid.NewGuid();
            Especialidade = especialidade;
        }

        // POLIMORFISMO: Sobrescrita (override) do método da classe base 
        // para especializar o comportamento do Dentista.
        public override string ObterDadosResumidos()
        {
            return $"{base.ObterDadosResumidos()} | Especialidade: {Especialidade}";
        }
    }
}