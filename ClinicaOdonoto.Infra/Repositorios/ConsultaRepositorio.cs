using ClinicaOdonto.Dominio.AgregadoConsulta;
using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;
using ClinicaOdonto.Dominio.Comum;
using System;
using System.Collections.Generic;
using System.Linq;

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

