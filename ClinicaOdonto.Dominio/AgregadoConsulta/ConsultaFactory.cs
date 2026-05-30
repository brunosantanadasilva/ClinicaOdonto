using System;
using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;

namespace ClinicaOdonto.Dominio.AgregadoConsulta;

// ===================================================================================
// PRINCÍPIO SOLID APLICADO: Single Responsibility Principle (SRP)
// OBJETIVO: Garantir que esta classe mude por um único motivo: a lógica de fabricação.
// ===================================================================================
public static class ConsultaFactory
{
    // O trecho abaixo foi isolado para retirar a carga de instanciação complexa de outras classes
    public static Consulta CriarConsulta(Paciente paciente, Dentista dentista, DateTime dataHora)
    {
        return new Consulta(paciente, dentista, dataHora);
    }
}