using System;
using System.Text.RegularExpressions;

namespace ClinicaOdonto.Dominio.Comum;

// Utilizar 'record' garante imutabilidade e comparação por valor nativamente no C#
public record Whatsapp
{
    public string Numero { get; init; }

    public Whatsapp(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException("O número de WhatsApp é obrigatório.");

        // Regra de negócio/validação encapsulada dentro do próprio Value Object
        if (!ValidarFormato(numero))
            throw new ArgumentException("Formato de WhatsApp inválido. Use o padrão (XX) 9XXXX-XXXX ou apenas números.");

        Numero = numero;
    }

    private static bool ValidarFormato(string numero)
    {
        // Limpa caracteres especiais para validar apenas os números essenciais
        string apenasNumeros = Regex.Replace(numero, @"[^\d]", "");
        return apenasNumeros.Length >= 10 && apenasNumeros.Length <= 11;
    }
}