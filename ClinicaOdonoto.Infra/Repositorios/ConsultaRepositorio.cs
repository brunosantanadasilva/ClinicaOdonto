using System;
using System.Collections.Generic;
using System.Linq;
using ClinicaOdonto.Dominio.AgregadoConsulta;
using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;

namespace ClinicaOdonto.Infra.Repositorios;

// Simula a tabela de consultas em memória
public class ConsultaRepositorio : IConsultaRepositorio
{
    private static readonly List<Consulta> _bancoDeDados = [];

    public void Inserir(Consulta consulta) => _bancoDeDados.Add(consulta);
    public void Remover(Consulta consulta) => _bancoDeDados.Remove(consulta);
    public void Atualizar(Consulta consulta) { /* Mock */ }

    public bool ExisteConsultaNoPeriodo(Guid dentistaId, DateTime dataHora)
    {
        return _bancoDeDados.Any(c => c.Dentista.Id == dentistaId && c.DataHora == dataHora);
    }
}

// Simula a tabela de dentistas já populada
public class DentistaRepositorio : IDentistaRepositorio
{
    private static readonly List<Dentista> _bancoDeDados =
    [
        new Dentista("111", "Dra. Taylor (OdontoPediatra)", new DateTime(1989,12,13), "999", Especialidade.Pediatra),
        new Dentista("222", "Dr. Mikael (Clínico Geral)", new DateTime(1980,5,20), "888", Especialidade.ClinicoGeral)
    ];

    public void Inserir(Dentista d) => _bancoDeDados.Add(d);
    public void Remover(Dentista d) => _bancoDeDados.Remove(d);
    public void Blacklist(Dentista d) { }

    public Dentista? ObterPorEspecialidade(Especialidade esp) => _bancoDeDados.FirstOrDefault(d => d.Especialidade == esp);
}

// Simula a tabela de pacientes já populada
public class PacienteRepositorio : IPacienteRepositorio
{
    private static readonly List<Paciente> _bancoDeDados =
    [
        new Paciente("12345678900", "Bruno Silva", new DateTime(1995, 3, 10), "99999-1111")
    ];

    public void Inserir(Paciente p) => _bancoDeDados.Add(p);
    public void Remover(Paciente p) => _bancoDeDados.Remove(p);
    public void Atualizar(Paciente p) { }

    public Paciente? ObterPorCpf(string cpf) => _bancoDeDados.FirstOrDefault(p => p.CPF == cpf);
}