using ClinicaOdonto.Dominio.AgregadoConsulta;
using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;
using ClinicaOdonto.Dominio.Comum;
using System;

public class AgendamentoAppService
{
    private readonly IPacienteRepositorio _pacienteRepo;
    private readonly IDentistaRepositorio _dentistaRepo;
    private readonly IMarcarConsultaService _domainService;

    public AgendamentoAppService(
        IPacienteRepositorio pacienteRepo,
        IDentistaRepositorio dentistaRepo,
        IMarcarConsultaService domainService)
    {
        _pacienteRepo = pacienteRepo;
        _dentistaRepo = dentistaRepo;
        _domainService = domainService;
    }

    public void CadastrarNovoPaciente(string cpf, string nome, DateTime dataNascimento, string numeroWhatsapp)
    {
        // 1. Valida se o paciente já existe para evitar duplicidade
        var pacienteExistente = _pacienteRepo.ObterPorCpf(cpf);
        if (pacienteExistente != null)
            throw new InvalidOperationException("Já existe um paciente cadastrado com este CPF.");

        // 2. Instancia o Value Object e a Entidade (as validações internas do construtor rodam aqui)
        var whatsappVo = new Whatsapp(numeroWhatsapp);
        var novoPaciente = new Paciente(cpf, nome, dataNascimento, whatsappVo);

        // 3. Envia para a infraestrutura persistir no banco
        _pacienteRepo.Inserir(novoPaciente);
    }

    public void ExecutarFluxoAgendamento(string pacienteCpf, Especialidade especialidade, DateTime dataHora)
    {
        // 1. Recupera o paciente pelo CPF na base simulada
        var paciente = _pacienteRepo.ObterPorCpf(pacienteCpf)
            ?? throw new Exception("Paciente não encontrado para o CPF especificado.");

        // 2. CORREÇÃO: Recupera o dentista usando o método que você postou
        var dentista = _dentistaRepo.ObterPorEspecialidade(especialidade)
            ?? throw new Exception($"Nenhum dentista encontrado com a especialidade {especialidade}.");

        // 3. Repassa as entidades higienizadas para as regras de negócio no Domain Service
        _domainService.MarcarConsulta(paciente, dentista, dataHora);
    }
}