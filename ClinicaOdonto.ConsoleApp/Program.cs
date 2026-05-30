using ClinicaOdonto.Aplicacao.Servicos;
using ClinicaOdonto.Dominio.AgregadoConsulta;
using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;
using ClinicaOdonto.Infra.Repositorios;
using System;

namespace ClinicaOdonto.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE AGENDAMENTO ODONTOLÓGICO (.NET 10) ===");

            // 1. Instanciação e amarração da Injeção de Dependências
            IConsultaRepositorio consultaRepo = new ConsultaRepositorio();
            IDentistaRepositorio dentistaRepo = new DentistaRepositorio();
            IPacienteRepositorio pacienteRepo = new PacienteRepositorio();

            IMarcarConsultaService domainService = new MarcarConsultaService(consultaRepo);
            AgendamentoAppService appService = new AgendamentoAppService(pacienteRepo, dentistaRepo, domainService);

            try
            {
                // Agendamento 1: Horário comercial válido (Segunda-feira, hora cheia)
                DateTime dataDesejada = new DateTime(2026, 06, 01, 14, 00, 00);

                Console.WriteLine("\nTentando realizar Agendamento 1...");
                appService.ExecutarFluxoAgendamento("12345678900", Especialidade.Pediatra, dataDesejada);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Sucesso: Consulta agendada com sucesso!");
                Console.ResetColor();

                // Agendamento 2: Forçando falha por choque de horário na mesma data/especialidade
                Console.WriteLine("\nTentando realizar Agendamento 2 (Mesmo horário e especialidade)...");
                appService.ExecutarFluxoAgendamento("12345678900", Especialidade.Pediatra, dataDesejada);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro capturado: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}