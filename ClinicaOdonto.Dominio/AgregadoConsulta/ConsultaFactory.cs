using System;
using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;

namespace ClinicaOdonto.Dominio.AgregadoConsulta;

// ===================================================================================
// PRINCÍPIO SOLID APLICADO: Single Responsibility Principle (SRP)
// OBJETIVO: Esta classe possui o motivo único de existir para gerenciar a fabricação complexa 
// da entidade Consulta. Ela encapsula a criação, desonerando outras camadas dessa carga.
// ===================================================================================
public static class ConsultaFactory
{
    // Comentário Explicativo: O método centraliza a chamada ao construtor interno (internal),
    // garantindo que se a assinatura de criação mudar, apenas esta fábrica sofrerá manutenção.
    public static Consulta CriarConsulta(Paciente paciente, Dentista dentista, DateTime dataHora)
    {
        return new Consulta(paciente, dentista, dataHora);
    }
}