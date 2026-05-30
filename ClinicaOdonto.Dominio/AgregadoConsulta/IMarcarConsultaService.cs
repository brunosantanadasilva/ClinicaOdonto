using System;
using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;

namespace ClinicaOdonto.Dominio.AgregadoConsulta
{
    // ABSTRAÇÃO: Interface concisa que define o contrato do Caso de Uso do Domínio
    public interface IMarcarConsultaService
    {
        void MarcarConsulta(Paciente paciente, Dentista dentista, DateTime dataHoraInicio);
    }
}