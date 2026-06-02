using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.Comum;
using System;
using Xunit;

namespace ClinicaOdonto.Dominio.Tests.AgregadoDentista;

public class DentistaTests
{
    [Fact]
    public void CriarDentista_ComDataNascimentoFutura_DeveLancarArgumentException()
    {
        var dataFutura = DateTime.Today.AddDays(1);

        var excecao = Assert.Throws<ArgumentException>(() =>
            new Dentista("12345678900", "Dr. Mikael", dataFutura, new Whatsapp("99999"), Especialidade.ClinicoGeral)
        );
        Assert.Contains("Data de nascimento inválida", excecao.Message);
    }

    [Fact]
    public void ObterDadosResumidos_DeDentista_DeveAplicarPolimorfismoComEspecialidade()
    {
        var dentista = new Dentista("11111111111", "Dr. Mikael", new DateTime(1980, 5, 20), new Whatsapp("11988887777"), Especialidade.ClinicoGeral);
        string resumo = dentista.ObterDadosResumidos();
        Assert.Equal("Nome: Dr. Mikael | CPF: 11111111111 | Especialidade: ClinicoGeral", resumo);
    }
}