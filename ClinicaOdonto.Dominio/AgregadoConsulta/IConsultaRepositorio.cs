using System;

namespace ClinicaOdonto.Dominio.AgregadoConsulta;

public interface IConsultaRepositorio
{
    void Inserir(Consulta consulta);
    void Remover(Consulta consulta);
    void Atualizar(Consulta consulta);
    bool ExisteConsultaNoPeriodo(Guid dentistaId, DateTime dataHora);
}