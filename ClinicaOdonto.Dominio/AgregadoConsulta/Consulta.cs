using System;
using System.Collections.Generic;
using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;

namespace ClinicaOdonto.Dominio.AgregadoConsulta
{
    public class Consulta
    {
        // ===================================================================================
        // PADRÃO GRASP APLICADO: High Cohesion (Alta Coesão)
        // EXPLICAÇÃO: Todas as propriedades e métodos abaixo são intimamente ligados e focados
        // no conceito de "Consulta". Não há regras de faturamento, cadastro ou prontuário aqui.
        // ===================================================================================
        public Guid Id { get; private set; }
        public Paciente Paciente { get; private set; }
        public Dentista Dentista { get; private set; }
        public DateTime DataHora { get; private set; }

        // Propriedade calculada baseada na regra invariante de que toda consulta dura 50 minutos
        public DateTime DataHoraFim => DataHora.AddMinutes(50);

        // HashSet estático com as horas cheias permitidas de início (08:00 às 17:00)
        private static readonly HashSet<int> HorasValidas = new() { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };

        // Construtor interno: Força a criação apenas através da ConsultaFactory (Padrão Criacional)
        internal Consulta(Paciente paciente, Dentista dentista, DateTime dataHora)
        {
            ValidarInvariantes(dataHora);

            Id = Guid.NewGuid();
            Paciente = paciente ?? throw new ArgumentNullException(nameof(paciente));
            Dentista = dentista ?? throw new ArgumentNullException(nameof(dentista));
            DataHora = dataHora;
        }

        // ENCAPSULAMENTO DE REGRAS: Centraliza as validações de consistência do Domínio
        private static void ValidarInvariantes(DateTime dataHora)
        {
            // Regras focadas estritamente na consistência do agendamento da consulta
            
            // 1. Validação de Dias Úteis (Segunda a Sexta)
            if (dataHora.DayOfWeek == DayOfWeek.Saturday || dataHora.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new InvalidOperationException("As consultas só podem ser agendadas de segunda a sexta-feira.");
            }

            // 2. Validação de Hora Cheia (Minutos e segundos devem ser zerados)
            if (dataHora.Minute != 0 || dataHora.Second != 0 || dataHora.Millisecond != 0)
            {
                throw new InvalidOperationException("As consultas devem ser agendadas em horários cheios (ex: 08:00, 09:00).");
            }

            // 3. Validação da Janela de Atendimento (08h às 17h)
            if (!HorasValidas.Contains(dataHora.Hour))
            {
                throw new InvalidOperationException("O horário de início deve ser entre 08:00 e 17:00.");
            }
        }
    }
}