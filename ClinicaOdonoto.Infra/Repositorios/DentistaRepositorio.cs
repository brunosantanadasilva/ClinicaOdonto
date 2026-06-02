using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.Comum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicaOdonoto.Infra.Repositorios
{
    // Simula a tabela de dentistas já populada
    public class DentistaRepositorio : IDentistaRepositorio
    {
        private static readonly List<Dentista> _bancoDeDados =
         [
            new Dentista("11111111111", "Dra. Taylor", new DateTime(1989, 12, 13), new Whatsapp("11999999999"), Especialidade.Pediatra),
            new Dentista("22222222222", "Dr. Mikael", new DateTime(1980, 5, 20), new Whatsapp("11988888888"), Especialidade.ClinicoGeral)
         ];


        public Dentista? ObterPorEspecialidade(Especialidade esp) => _bancoDeDados.FirstOrDefault(d => d.Especialidade == esp);
    }
}
