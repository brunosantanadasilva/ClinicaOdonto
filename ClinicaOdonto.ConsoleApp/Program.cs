using ClinicaOdonoto.Infra.Repositorios;
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

            // 1. Inicialização da infraestrutura e serviços
            IConsultaRepositorio consultaRepo = new ConsultaRepositorio();
            IDentistaRepositorio dentistaRepo = new DentistaRepositorio();
            IPacienteRepositorio pacienteRepo = new PacienteRepositorio();

            IMarcarConsultaService domainService = new MarcarConsultaService(consultaRepo);
            AgendamentoAppService appService = new AgendamentoAppService(pacienteRepo, dentistaRepo, domainService);

            try
            {
                // Cenário 1: Horário comercial válido (Segunda-feira às 14:00h)
                DateTime dataDesejada = new DateTime(2026, 06, 01, 14, 00, 00);

                Console.WriteLine("\nTentando realizar Agendamento 1...");

                string pacienteCpf = "12345678900"; // Existe no seu PacienteRepositorio
                Especialidade especialidadeDesejada = Especialidade.Pediatra; // Vai encontrar a Dra. Taylor

                // Executa o fluxo passando a chave de negócio (CPF) e a Especialidade
                appService.ExecutarFluxoAgendamento(pacienteCpf, especialidadeDesejada, dataDesejada);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Sucesso: Consulta agendada com sucesso!");
                Console.ResetColor();

                // Cenário 2: Forçando falha (Mesmo horário para a mesma especialidade/dentista)
                Console.WriteLine("\nTentando realizar Agendamento 2 (Mesmo horário e especialidade)...");
                appService.ExecutarFluxoAgendamento(pacienteCpf, especialidadeDesejada, dataDesejada);
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