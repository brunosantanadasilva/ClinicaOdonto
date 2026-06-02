using ClinicaOdonto.Dominio.AgregadoPaciente;
using ClinicaOdonto.Dominio.Comum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicaOdonoto.Infra.Repositorios
{
    // Simula a tabela de pacientes já populada
    public class PacienteRepositorio : IPacienteRepositorio
    {
        private static readonly List<Paciente> _bancoDeDados =
        [
            new Paciente("12345678900", "Bruno Silva", new DateTime(1995, 3, 10), new Whatsapp("11999991111"))
        ];

        public Paciente? ObterPorId(Guid id)
            => _bancoDeDados.FirstOrDefault(p => p.Id == id);

        public void Inserir(Paciente p) => _bancoDeDados.Add(p);
        public void Remover(Paciente p) => _bancoDeDados.Remove(p);
        public void Atualizar(Paciente p) { }

        public Paciente? ObterPorCpf(string cpf) => _bancoDeDados.FirstOrDefault(p => p.CPF == cpf);
    }
}
