namespace ClinicaOdonto.Dominio.AgregadoPaciente;

public interface IPacienteRepositorio
{
    void Inserir(Paciente paciente);
    void Remover(Paciente paciente);
    void Atualizar(Paciente paciente);
    Paciente? ObterPorCpf(string cpf);
}