using System;
using ClinicaOdonto.Dominio.AgregadoConsulta;
using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;

namespace ClinicaOdonto.Aplicacao.Servicos;

public class AgendamentoAppService
{
    private readonly IPacienteRepositorio _pacienteRepo;
    private readonly IDentistaRepositorio _dentistaRepo;
    private readonly IMarcarConsultaService _marcarConsultaService;

    public AgendamentoAppService(
        IPacienteRepositorio pacienteRepo,
        IDentistaRepositorio dentistaRepo,
        IMarcarConsultaService marcarConsultaService)
    {
        _pacienteRepo = pacienteRepo;
        _dentistaRepo = dentistaRepo;
        _marcarConsultaService = marcarConsultaService;
    }

    public void ExecutarFluxoAgendamento(string cpfPaciente, Especialidade especialidade, DateTime dataHora)
    {
        // 1. Busca o paciente pelo CPF
        var paciente = _pacienteRepo.ObterPorCpf(cpfPaciente)
            ?? throw new Exception("Paciente não encontrado no sistema.");

        // 2. Busca o Dentista responsável por aquela Especialidade
        var dentista = _dentistaRepo.ObterPorEspecialidade(especialidade)
            ?? throw new Exception("Nenhum dentista disponível para esta especialidade.");

        // 3. Aciona o Serviço de Domínio passando os objetos carregados
        _marcarConsultaService.MarcarConsulta(paciente, dentista, dataHora);
    }
}