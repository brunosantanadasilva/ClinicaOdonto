using System;
using ClinicaOdonto.Dominio.Comum;
using Xunit;

namespace ClinicaOdonto.Dominio.Tests.Comum;

public class WhatsappTests
{
    [Fact]
    public void CriarWhatsapp_ComDadosValidos_DeveInstanciarComSucesso()
    {
        // Arrange
        var numeroValido = "11999991111";

        // Act
        var whatsapp = new Whatsapp(numeroValido);

        // Assert (Auto-verificação)
        Assert.Equal(numeroValido, whatsapp.Numero);
    }

    [Fact]
    public void CriarWhatsapp_ComNumeroInvalido_DeveLancarArgumentException()
    {
        // Arrange
        var numeroCurtoDemais = "123";

        // Act & Assert (Teste Negativo)
        var excecao = Assert.Throws<ArgumentException>(() =>
            new Whatsapp(numeroCurtoDemais)
        );

        Assert.Contains("Formato de WhatsApp inválido", excecao.Message);
    }

    [Fact]
    public void CriarWhatsapp_VazioOuNulo_DeveLancarArgumentException()
    {
        // Act & Assert (Teste Negativo para proteção de nulos)
        var excecao = Assert.Throws<ArgumentException>(() =>
            new Whatsapp(string.Empty)
        );

        Assert.Contains("obrigatório", excecao.Message);
    }
}