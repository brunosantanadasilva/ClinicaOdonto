using System;
using ClinicaOdonto.Dominio.Comum;

namespace ClinicaOdonto.Dominio.AgregadoPaciente
{
    // HERANÇA: Paciente herda todas as características e propriedades de Pessoa
    public class Paciente : Pessoa
    {
        public Guid Id { get; private set; }

        public Paciente(string cpf, string nome, DateTime dataNascimento, string whatsapp)
            : base(cpf, nome, dataNascimento, whatsapp) // Repassa os dados para o construtor base de Pessoa
        {
            Id = Guid.NewGuid();
        }
    }
}