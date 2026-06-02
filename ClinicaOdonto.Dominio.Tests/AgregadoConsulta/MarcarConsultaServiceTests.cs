using ClinicaOdonto.Dominio.AgregadoConsulta;
using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;
using ClinicaOdonto.Dominio.Comum;
using NSubstitute;
using System;
using Xunit;

namespace ClinicaOdonto.Dominio.Tests.AgregadoConsulta;

public class MarcarConsultaServiceTests
{
    private readonly IConsultaRepositorio _repositoryMock;
    private readonly MarcarConsultaService _service;
    private readonly Paciente _pacienteDummy;
    private readonly Dentista _dentistaDummy;

    public MarcarConsultaServiceTests()
    {
        // ISOLAMENTO COM MOCK: NSubstitute cria um dublê da interface de infraestrutura em memória
        _repositoryMock = Substitute.For<IConsultaRepositorio>();

        // Injeta o mock criado no construtor do serviço sob teste
        _service = new MarcarConsultaService(_repositoryMock);

        // Dummies de dados para suporte
        _pacienteDummy = new Paciente("12345678900", "Bruno Silva", new DateTime(1995, 3, 10), new Whatsapp("11999991111"));
        _dentistaDummy = new Dentista("11111111111", "Dr. Mikael", new DateTime(1980, 5, 20), new Whatsapp("11988887777"), Especialidade.ClinicoGeral);
    }

    [Fact]
    public void MarcarConsulta_QuandoNaoHaConflitoDeAgenda_DeveSalvarComSucesso()
    {
        // Arrange
        var horario = new DateTime(2026, 6, 1, 10, 0, 0); // Segunda-feira 10h

        // Configuração do Stub do Mock: Quando perguntado se existe consulta, responde FALSE (Livre)
        _repositoryMock.ExisteConsultaNoPeriodo(_dentistaDummy.Id, horario).Returns(false);

        // Act
        _service.MarcarConsulta(_pacienteDummy, _dentistaDummy, horario);

        // Assert (Auto-verificação de comportamento)
        // Garante que o método 'Inserir' do repositório foi chamado exatamente uma vez com qualquer consulta gerada
        _repositoryMock.Received(1).Inserir(Arg.Any<Consulta>());
    }

    [Fact]
    public void MarcarConsulta_QuandoDentistaJaEstaOcupado_DeveLancarExcecaoEDescartarPersistencia()
    {
        // Arrange
        var horarioConflitante = new DateTime(2026, 6, 1, 10, 0, 0);

        // Configuração do Mock para o Cenário Negativo: Responde TRUE (Ocupado)
        _repositoryMock.ExisteConsultaNoPeriodo(_dentistaDummy.Id, horarioConflitante).Returns(true);

        // Act & Assert
        var excecao = Assert.Throws<InvalidOperationException>(() =>
            _service.MarcarConsulta(_pacienteDummy, _dentistaDummy, horarioConflitante)
        );

        Assert.Contains("Conflito de Agenda", excecao.Message);

        // Garante a integridade: se deu conflito, o repositório NUNCA deve chamar o comando de Inserir no banco
        _repositoryMock.DidNotReceive().Inserir(Arg.Any<Consulta>());
    }
}