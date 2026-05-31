using System;
using ClinicaOdonto.Dominio.AgregadoConsulta;
using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;
using Xunit;

namespace ClinicaOdonto.Dominio.Tests.AgregadoConsulta;

public class ConsultaTests
{
    // Método auxiliar (Stub) para fabricar objetos válidos base para os testes
    private (Paciente, Dentista) CriarDadosBase()
    {
        var paciente = new Paciente("12345678900", "Bruno Silva", new DateTime(1995, 3, 10), "99999-1111");
        var dentista = new Dentista("11111111111", "Dr. Mikael", new DateTime(1980, 5, 20), "98888-7777", Especialidade.ClinicoGeral);
        return (paciente, dentista);
    }

    [Fact]
    public void CriarConsulta_ComHorarioE_DiaValido_DeveInstanciarComSucesso()
    {
        // Arrange (Preparação)
        var (paciente, dentista) = CriarDadosBase();
        // Uma Segunda-Feira, às 14:00 (Dia útil e hora cheia válida)
        var dataValida = new DateTime(2026, 6, 1, 14, 0, 0);

        // Act (Ação)
        var consulta = ConsultaFactory.CriarConsulta(paciente, dentista, dataValida);

        // Assert (Verificação - Cenário Positivo)
        Assert.NotNull(consulta);
        Assert.Equal(dataValida, consulta.DataHora);
        Assert.Equal(dataValida.AddMinutes(50), consulta.DataHoraFim); // Invariante do tempo calculada
    }

    [Fact]
    public void CriarConsulta_NoFimDeSemana_DeveLancarDomainException()
    {
        // Arrange
        var (paciente, dentista) = CriarDadosBase();
        // Um Sábado às 10:00 (Dia inválido)
        var sabado = new DateTime(2026, 5, 30, 10, 0, 0);

        // Act & Assert (Cenário Negativo)
        var excecao = Assert.Throws<InvalidOperationException>(() =>
            ConsultaFactory.CriarConsulta(paciente, dentista, sabado)
        );
        Assert.Contains("segunda a sexta-feira", excecao.Message);
    }

    [Fact]
    public void CriarConsulta_ComMinutosFracionados_DeveLancarDomainException()
    {
        // Arrange
        var (paciente, dentista) = CriarDadosBase();
        // Uma terça-feira às 14:35 (Minutos fracionados inválidos)
        var horaFracionada = new DateTime(2026, 6, 2, 14, 35, 0);

        // Act & Assert (Cenário Negativo)
        var excecao = Assert.Throws<InvalidOperationException>(() =>
            ConsultaFactory.CriarConsulta(paciente, dentista, horaFracionada)
        );
        Assert.Contains("horários cheios", excecao.Message);
    }

    [Fact]
    public void CriarConsulta_ForaDaJanelaComercial_DeveLancarDomainException()
    {
        // Arrange
        var (paciente, dentista) = CriarDadosBase();
        // Uma quarta-feira às 19:00 (Fora da janela de início que encerra às 17h)
        var horaInvalida = new DateTime(2026, 6, 3, 19, 0, 0);

        // Act & Assert (Cenário Negativo)
        var excecao = Assert.Throws<InvalidOperationException>(() =>
            ConsultaFactory.CriarConsulta(paciente, dentista, horaInvalida)
        );
        Assert.Contains("entre 08:00 e 17:00", excecao.Message);
    }
    [Fact]
    public void CriarConsulta_AntesDoHorarioPermitido_DeveLancarDomainException()
    {
        // Arrange
        var (paciente, dentista) = CriarDadosBase();
        // Uma segunda-feira às 07:00 (Uma hora antes da abertura)
        var horaCedoDemais = new DateTime(2026, 6, 1, 7, 0, 0);

        // Act & Assert
        var excecao = Assert.Throws<InvalidOperationException>(() =>
            ConsultaFactory.CriarConsulta(paciente, dentista, horaCedoDemais)
        );
        Assert.Contains("entre 08:00 e 17:00", excecao.Message);
    }

    [Fact]
    public void CriarConsulta_PassandoParametrosNulos_DeveLancarArgumentNullException()
    {
        // Arrange
        var (paciente, dentista) = CriarDadosBase();
        var dataValida = new DateTime(2026, 6, 1, 14, 0, 0);

        // Act & Assert (Garante a cobertura das checagens de nulo '?? throw')
        Assert.Throws<ArgumentNullException>(() => ConsultaFactory.CriarConsulta(null!, dentista, dataValida));
        Assert.Throws<ArgumentNullException>(() => ConsultaFactory.CriarConsulta(paciente, null!, dataValida));
    }
}