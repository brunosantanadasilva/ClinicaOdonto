using System;

namespace ClinicaOdonto.Dominio.AgregadoDentista;

public interface IDentistaRepositorio
{
    Dentista? ObterPorEspecialidade(Especialidade especialidade);
}