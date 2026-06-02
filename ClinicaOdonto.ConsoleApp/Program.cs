using ClinicaOdonoto.Infra.Repositorios;
using ClinicaOdonto.Dominio.AgregadoConsulta;
using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;
using ClinicaOdonto.Infra.Repositorios;
using System;

Console.WriteLine("=== SISTEMA DE AGENDAMENTO ODONTOLÓGICO (.NET 10) ===");

// 1. Inicialização da infraestrutura e serviços
IConsultaRepositorio consultaRepo = new ConsultaRepositorio();
IDentistaRepositorio dentistaRepo = new DentistaRepositorio();
IPacienteRepositorio pacienteRepo = new PacienteRepositorio();

IMarcarConsultaService domainService = new MarcarConsultaService(consultaRepo);
AgendamentoAppService appService = new AgendamentoAppService(pacienteRepo, dentistaRepo, domainService);

string novoCpf = "11122233344";
DateTime dataDesejada = new DateTime(2026, 06, 01, 14, 00, 00);
Especialidade especialidadeDesejada = Especialidade.Pediatra; // Encontra a Dra. Taylor

// ===================================================================================
// CENÁRIO 1: SUCESSO - CADASTRO DE UM NOVO PACIENTE INÉDITO
// ===================================================================================
try
{
    Console.WriteLine("\n-----------------------------------------------------");
    Console.WriteLine("CENÁRIO 1: Realizando o cadastro de um paciente inédito...");
    Console.WriteLine("-----------------------------------------------------");

    appService.CadastrarNovoPaciente(
        cpf: novoCpf,
        nome: "Thalles Silva",
        dataNascimento: new DateTime(2020, 05, 15),
        numeroWhatsapp: "11977776666"
    );

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Sucesso: Paciente cadastrado com sucesso na memória!");
    Console.ResetColor();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Erro inesperado no cadastro: {ex.Message}");
    Console.ResetColor();
}

// ===================================================================================
// CENÁRIO 2: ERRO DEMONSTRATIVO - TENTAR CADASTRAR O MESMO CPF (DUPLICIDADE)
// ===================================================================================
try
{
    Console.WriteLine("\n-----------------------------------------------------");
    Console.WriteLine("CENÁRIO 2 (ERRO): Tentando cadastrar o mesmo CPF novamente...");
    Console.WriteLine("-----------------------------------------------------");

    // Força a validação do AppService ao tentar reinserir o CPF do Thalles
    appService.CadastrarNovoPaciente(
        cpf: novoCpf,
        nome: "Thalles Repetido",
        dataNascimento: new DateTime(2020, 05, 15),
        numeroWhatsapp: "11977776666"
    );
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Erro demonstrativo capturado: {ex.Message}");
    Console.ResetColor();
}

// ===================================================================================
// CENÁRIO 3: SUCESSO - PRIMEIRO AGENDAMENTO VÁLIDO
// ===================================================================================
try
{
    Console.WriteLine("\n-----------------------------------------------------");
    Console.WriteLine("CENÁRIO 3: Tentando realizar o primeiro Agendamento...");
    Console.WriteLine("-----------------------------------------------------");

    appService.ExecutarFluxoAgendamento(novoCpf, especialidadeDesejada, dataDesejada);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Sucesso: Consulta agendada com sucesso!");
    Console.ResetColor();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Erro inesperado no agendamento: {ex.Message}");
    Console.ResetColor();
}

// ===================================================================================
// CENÁRIO 4: ERRO DEMONSTRATIVO - CONFLITO DE AGENDA (O ERRO ORIGINAL)
// ===================================================================================
try
{
    Console.WriteLine("\n-----------------------------------------------------");
    Console.WriteLine("CENÁRIO 4 (ERRO): Forçando Choque de Horário com o mesmo Dentista...");
    Console.WriteLine("-----------------------------------------------------");

    // Repete exatamente a mesma especialidade e horário do Cenário 3
    appService.ExecutarFluxoAgendamento(novoCpf, especialidadeDesejada, dataDesejada);
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Erro original de conflito capturado com sucesso: {ex.Message}");
    Console.ResetColor();
}

Console.WriteLine("\n-----------------------------------------------------");
Console.WriteLine("Pressione qualquer tecla para sair...");
Console.ReadKey();