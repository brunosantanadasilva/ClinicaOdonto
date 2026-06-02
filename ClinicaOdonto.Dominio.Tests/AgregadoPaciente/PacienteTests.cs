using System;
using ClinicaOdonto.Dominio.AgregadoPaciente;
using ClinicaOdonto.Dominio.Comum; // Adicionado para enxergar o Value Object Whatsapp
using Xunit;

namespace ClinicaOdonto.Dominio.Tests.AgregadoPaciente;

public class PacienteTests
{
    [Fact]
    public void CriarPaciente_ComDadosValidos_DeveInstanciarCorretamente()
    {

        var paciente = new Paciente("12345678900", "Bruno Silva", new DateTime(1995, 3, 10), new Whatsapp("11999991111"));

        Assert.NotNull(paciente);
        Assert.Equal("12345678900", paciente.CPF);
    }

    [Theory]
    [InlineData("", "Nome", "11999991111", "CPF é obrigatório.")]
    [InlineData("12345678900", "", "11999991111", "Nome é obrigatório.")]
    [InlineData("12345678900", "Nome", null, "Value cannot be null. (Parameter 'whatsapp')")]
    public void CriarPaciente_ComDadosInvalidos_DeveLancarExcecao(string cpf, string nome, string whatsappNumero, string mensagemEsperada)
    {

        Whatsapp whatsappVo = whatsappNumero != null ? new Whatsapp(whatsappNumero) : null;

        var excecao = Assert.ThrowsAny<ArgumentException>(() =>
            new Paciente(cpf, nome, new DateTime(1995, 3, 10), whatsappVo)
        );

        Assert.Contains(mensagemEsperada, excecao.Message);
    }

    [Fact]
    public void ObterDadosResumidos_DePaciente_DeveRetornarTextoNaFormataoBase()
    {

        var paciente = new Paciente("12345678900", "Bruno Silva", new DateTime(1995, 3, 10), new Whatsapp("11999991111"));

        string resumo = paciente.ObterDadosResumidos();

        Assert.Equal("Nome: Bruno Silva | CPF: 12345678900", resumo);
    }
}