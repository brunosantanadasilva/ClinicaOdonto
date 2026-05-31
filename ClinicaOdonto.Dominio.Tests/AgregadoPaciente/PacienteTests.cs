using System;
using ClinicaOdonto.Dominio.AgregadoPaciente;
using Xunit;

namespace ClinicaOdonto.Dominio.Tests.AgregadoPaciente;

public class PacienteTests
{
    [Fact]
    public void CriarPaciente_ComDadosValidos_DeveInstanciarCorretamente()
    {
        var paciente = new Paciente("12345678900", "Bruno Silva", new DateTime(1995, 3, 10), "99999-1111");
        Assert.NotNull(paciente);
        Assert.NotEqual(Guid.Empty, paciente.Id);
        Assert.Equal("12345678900", paciente.CPF);
    }

    [Theory]
    [InlineData("", "Nome", "99999-1111", "CPF é obrigatório.")]
    [InlineData("12345678900", "", "99999-1111", "Nome é obrigatório.")]
    [InlineData("12345678900", "Nome", "", "Whatsapp é obrigatório.")]
    public void CriarPaciente_ComDadosInvalidos_DeveLancarArgumentException(string cpf, string nome, string whatsapp, string mensagemEsperada)
    {
        var excecao = Assert.Throws<ArgumentException>(() =>
            new Paciente(cpf, nome, new DateTime(1995, 3, 10), whatsapp)
        );
        Assert.Equal(mensagemEsperada, excecao.Message);
    }

    [Fact]
    public void ObterDadosResumidos_DePaciente_DeveRetornarTextoNaFormataoBase()
    {
        var paciente = new Paciente("12345678900", "Bruno Silva", new DateTime(1995, 3, 10), "99999-1111");
        string resumo = paciente.ObterDadosResumidos();
        Assert.Equal("Nome: Bruno Silva | CPF: 12345678900", resumo);
    }
}