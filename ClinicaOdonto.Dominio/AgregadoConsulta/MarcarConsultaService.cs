using ClinicaOdonto.Dominio.AgregadoDentista;
using ClinicaOdonto.Dominio.AgregadoPaciente;
using System;

namespace ClinicaOdonto.Dominio.AgregadoConsulta;

public class MarcarConsultaService : IMarcarConsultaService
{
    // ===================================================================================
    // PRINCÍPIO SOLID APLICADO: Dependency Inversion Principle (DIP)
    // EXPLICAÇÃO: O serviço depende da ABSTRAÇÃO (Interface) e não do detalhe (Banco de Dados).
    // ===================================================================================
    private readonly IConsultaRepositorio _consultaRepositorio;

    // INJEÇÃO DE DEPENDÊNCIA VIA CONSTRUTOR: O acoplamento rígido é quebrado aqui
    public MarcarConsultaService(IConsultaRepositorio consultaRepositorio)
    {
        _consultaRepositorio = consultaRepositorio;
    }

    // ===================================================================================
    // PADRÃO GRASP APLICADO: Low Coupling (Baixo Acoplamento)
    // EXPLICAÇÃO: Reduzimos as dependências desta classe ao mínimo necessário. Ela recebe
    // os objetos Paciente e Dentista já prontos por parâmetro, sem precisar se acoplar
    // aos repositórios IPacienteRepositorio ou IDentistaRepositorio para buscá-los.
    // ===================================================================================
    public void MarcarConsulta(Paciente paciente, Dentista dentista, DateTime dataHoraInicio)
    {
        // 1. Regra de Negócio Cruzada: O dentista está livre nesse horário?
        bool possuiConflito = _consultaRepositorio.ExisteConsultaNoPeriodo(dentista.Id, dataHoraInicio);

        if (possuiConflito)
        {
            throw new InvalidOperationException("Conflito de Agenda: O dentista já possui uma consulta marcada neste horário.");
        }

        // 2. Delega a criação segura para a Factory (Invariantes internas serão testadas lá)
        var novaConsulta = ConsultaFactory.CriarConsulta(paciente, dentista, dataHoraInicio);

        // 3. Persiste no banco através do repositório
        _consultaRepositorio.Inserir(novaConsulta);
    }
}