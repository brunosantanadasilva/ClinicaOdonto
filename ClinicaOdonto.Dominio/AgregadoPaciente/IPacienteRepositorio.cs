namespace ClinicaOdonto.Dominio.AgregadoPaciente;

public interface IPacienteRepositorio
{
    Paciente? ObterPorCpf(string cpf);
    void Inserir(Paciente paciente);
}