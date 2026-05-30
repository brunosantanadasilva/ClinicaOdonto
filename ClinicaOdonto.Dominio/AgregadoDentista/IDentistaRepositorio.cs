using System;

namespace ClinicaOdonto.Dominio.AgregadoDentista;

public interface IDentistaRepositorio
{
    void Inserir(Dentista dentista);
    void Remover(Dentista dentista);
    void Blacklist(Dentista dentista); // Equivalente ao seu Atualizar/Gerenciar
    Dentista? ObterPorEspecialidade(Especialidade especialidade);
}